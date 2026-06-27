using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Dialogue/Dialogue Data")]
public class DialogueDataSO : ScriptableObject
{
    public DialogueLine[] lines;
}