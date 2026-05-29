using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private DialogueBox dialogueBox;

    private DialogueLine[] currentLines;
    private int currentIndex;
    public bool IsActive { get; private set; }

    private void Awake()
    {
        Instance = this;
        dialogueBox.Hide();
    }

    public void StartDialogue(DialogueTrigger trigger)
    {
        IsActive = true;
        currentLines = trigger.Lines;
        currentIndex = 0;

        dialogueBox.transform.position = trigger.Anchor.position;

        ShowCurrentLine();
    }

    public void HandleEnterPress()
    {
        if (!IsActive) return;

        if (dialogueBox.IsTyping)
        {
            dialogueBox.SkipToEnd();
            return;
        }
    
        currentIndex++;
        if (currentIndex < currentLines.Length)
        {
            ShowCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    private void ShowCurrentLine()
    {
        dialogueBox.Show(currentLines[currentIndex].text);
    }

    private void EndDialogue()
    {
        IsActive = false;
        dialogueBox.Hide();
    }
}