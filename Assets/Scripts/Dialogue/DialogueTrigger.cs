using UnityEngine;


//DE PUS PE NPC (PLAYER PENTRU TEST)
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueLine[] lines;
    [SerializeField] private Transform dialogueBoxAnchor; 

    public DialogueLine[] Lines => lines;
    public Transform Anchor => dialogueBoxAnchor;
}