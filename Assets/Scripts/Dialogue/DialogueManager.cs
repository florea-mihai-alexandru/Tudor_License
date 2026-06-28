using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private DialogueBubble npcBubble;

    private DialogueLine[] currentLines;
    private int currentIndex;

    public bool IsActive { get; private set; }

    private Transform currentAnchor;

    private void Awake()
    {
        Instance = this;
        npcBubble.Hide();
    }

    public void StartDialogue(DialogueDataSO data, Transform anchor)
    {
        IsActive = true;
        currentLines = data.lines;
        currentIndex = 0;
        currentAnchor = anchor;
        ShowCurrentLine();
    }

    public void HandleEnterPress()
    {
        if (!IsActive) return;

        if (npcBubble.IsTyping)
        {
            npcBubble.SkipToEnd();
            return;
        }

        currentIndex++;

        if (currentIndex < currentLines.Length)
            ShowCurrentLine();
        else
            EndDialogue();
    }

    private void ShowCurrentLine()
    {
        DialogueLine line = currentLines[currentIndex];
        npcBubble.Show(line.text, currentAnchor, line.voiceClip);
    }

    private void EndDialogue()
    {
        IsActive = false;
        npcBubble.Hide();
    }
}