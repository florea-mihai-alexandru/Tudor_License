using UnityEngine;

public class TeacherRoom : Room
{
    [System.Serializable]
    public class StageNpcGroup
    {
        public int stage; // 1 = An1, 2 = An2, 3 = An3, 4 = Licenta
        public GameObject[] npcsToActivate;
    }

    [Header("Profesori per Stage")]
    public StageNpcGroup[] stageGroups;

    public override void EnterRoom()
    {
        Debug.Log($"Stage Groups count: {stageGroups.Length}");

        LevelManager.Instance.LevelCompleted();

        SpawnProfesoriForStage(LevelManager.Instance.currentStage);
    }

    private void SpawnProfesoriForStage(int stage)
    {

        // Dezactivezi toti profesorii din toate grupurile
        foreach (var group in stageGroups)
        {
            foreach (var npc in group.npcsToActivate)
            {
                if (npc != null)
                    npc.SetActive(false);
            }
        }

        // Activezi doar grupul corespunzator stage-ului curent
        var match = System.Array.Find(stageGroups, g => g.stage == stage);

        if (match != null)
        {
            foreach (var npc in match.npcsToActivate)
            {
                if (npc != null)
                {
                    npc.SetActive(true);
                }
            }
        }
    }
}