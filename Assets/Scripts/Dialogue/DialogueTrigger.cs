using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueDataSO dialogueData;
    [SerializeField] private float interactRange = 2f;
    [SerializeField] private Transform bubbleAnchor; 

    private Transform player;

    public Transform BubbleAnchor => bubbleAnchor != null ? bubbleAnchor : transform;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    public bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.position) <= interactRange;
    }

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogueData, BubbleAnchor);
    }
}