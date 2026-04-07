using UnityEngine;

public class ContextSteering
{
    int directionCount = 32;

    private Vector3[] directions;
    private float[] interest;
    private float[] danger;
    private Vector3 lastChosenDir;
    private float inertiaWeight = 0.4f;
    private float distToPlayer;
    private float forwardBlock;

    public Vector3 centrePos { get; set; }

    public Vector3[] Directions => directions;
    public float[] Interest => interest;
    public float[] Danger => danger;
    public Vector3 LastChosenDirection => lastChosenDir;

    public float NpcAvoidRadius { get; private set; } = 16f;
    public float WallCheckDistance { get; private set; } = 16f;
    private LayerMask wallMask;
    private LayerMask npcMask;

    private Transform agent;
    private Enemy enemy;

    Collider[] npcBuffer = new Collider[16];

    public ContextSteering(Enemy enemy, Vector3 centrePos, LayerMask wallMask, LayerMask npcMask, int directionCount = 16)
    {
        this.enemy = enemy;
        this.agent = enemy.transform;
        this.wallMask = wallMask;
        this.npcMask = npcMask;
        this.directionCount = directionCount;
        this.centrePos = centrePos;

        directions = new Vector3[directionCount];
        interest = new float[directionCount];
        danger = new float[directionCount];

        GenerateDirections();
    }

    void GenerateDirections()
    {
        for (int i = 0; i < directionCount; i++)
        {
            float angle = i * Mathf.PI * 2 / directionCount;
            directions[i] = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        }
    }

    public Vector3 GetDirection(Vector3 navDirection)
    {
        ClearMaps();

        ComputeForwardDanger(navDirection);

        AddInterest(navDirection);
        AddNpcDanger();
        AddWallDanger();

        PropagateDanger();
         
        return ChooseDirection();
    }

    private void ClearMaps()
    {
        for (int i = 0; i < directionCount; i++)
        {
            interest[i] = 0;
            danger[i] = 0;
        }
    }

    private void ComputeForwardDanger(Vector3 navDir)
    {
        float forwardDanger = 0f;

        for (int i = 0; i < directionCount; i++)
        {
            float alignment = Vector3.Dot(directions[i], navDir);

            if (alignment > 0.7f) // forward cone
            {
                forwardDanger += danger[i];
            }
        }

        forwardBlock =  Mathf.Clamp01(forwardDanger);
    }

    private void AddInterest(Vector3 navDir)
    {  ///TODO FINISH CIRCLING
       ///UPDATE: Forgot what I had to finish, been a month..
       ///
        Vector3 toPlayer = enemy.toPlayerVector();
        float dist = toPlayer.magnitude;
        Vector3 dirToPlayer = toPlayer.normalized;
        
        Vector3 left = new Vector3(-dirToPlayer.z, 0, dirToPlayer.x);
        Vector3 right = new Vector3(dirToPlayer.z, 0, -dirToPlayer.x);

        float circleStrength = Mathf.InverseLerp(6f, 2f, dist);

        // boost circling when blocked
        circleStrength *= (0.5f + forwardBlock);

        for (int i = 0; i < directionCount; i++)
        {
            float alignment = Vector3.Dot(directions[i], navDir);

            float forwardWeight = Mathf.Lerp(1f, 0.4f, circleStrength);

            interest[i] = Mathf.Max(0, alignment) * forwardWeight;

            float inertia = Vector3.Dot(directions[i], lastChosenDir);
            interest[i] += Mathf.Max(0, inertia) * inertiaWeight;
           
            float leftAlign = Vector3.Dot(directions[i], left);
            float rightAlign = Vector3.Dot(directions[i], right);

            float sideInterest = Mathf.Max(leftAlign, rightAlign);

            interest[i] += sideInterest * circleStrength * 0.6f;
        }

    }

    private void AddNpcDanger()
    {
        int count = Physics.OverlapSphereNonAlloc(centrePos, NpcAvoidRadius, npcBuffer, npcMask);
        for (int n = 0; n < count; n++)
        {
            Collider npc = npcBuffer[n];
            if (npc.transform == agent) continue;
            Enemy other = npc.GetComponent<Enemy>();
            
            bool attacking = false;
            
            if (other != null && other.StateMachine.CurrentState is EnemyAttackState)
                attacking = true;

            Vector3 toNpc = npc.transform.position - centrePos;
            float dist = toNpc.magnitude;

            if (dist < 0.01f) continue;

            Vector3 npcDir = toNpc.normalized;

            for (int i = 0; i < directionCount; i++)
            {
                float alignment = Vector3.Dot(directions[i], npcDir);

                float dangerValue = Mathf.Max(0, alignment) * (NpcAvoidRadius / dist);
                
                float attackInfluence = 1f;

                if (attacking)
                {
                    attackInfluence = Mathf.Lerp(0.2f, 0.6f, dist / NpcAvoidRadius);
                }

                dangerValue *= attackInfluence;

                danger[i] += Mathf.Min(dangerValue * 0.6f, 2f);
                danger[i] = Mathf.Min(danger[i], 3f);
            }
        }
    }

    private void AddWallDanger()
    {
        for (int i = 0; i < directionCount; i++)
        {
            Vector3 dir = directions[i];
            Vector3 origin = centrePos + dir * 6.3f;
            //Debug.Log("i + " +  dir);
            if (Physics.Raycast(centrePos, dir, WallCheckDistance, wallMask))
            {
                danger[i] += 3f;
            }
        }
    }

    private void PropagateDanger()
    {
        float[] propagated = new float[directionCount];

        for (int i = 0; i < directionCount; i++)
        {
            float d = danger[i];

            if (d <= 0) continue;

            propagated[i] += d;

            propagated[(i + 1) % directionCount] += d * 0.5f;
            propagated[(i - 1 + directionCount) % directionCount] += d * 0.5f;

            propagated[(i + 2) % directionCount] += d * 0.25f;
            propagated[(i - 2 + directionCount) % directionCount] += d * 0.25f;
        }

        for (int i = 0; i < directionCount; i++)
        {
            danger[i] = propagated[i];
        }
    }

    private Vector3 ChooseDirection()
    {
        float bestScore = -Mathf.Infinity;
        Vector3 bestDir = Vector3.zero;

        for (int i = 0; i < directionCount; i++)
        {
            float score = interest[i] - danger[i];

            //Debug.Log("Name: " + enemy.name + "Interest: " + interest[i] + "Danger: " + danger[i] + "Score: " + score);

            if (score > bestScore)
            {
                bestScore = score;
                bestDir = directions[i];
            }
        }

        lastChosenDir = bestDir;

        return bestDir.normalized;
    }
}