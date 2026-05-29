using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float typewriterSpeed = 30f; 

    private Coroutine typewriterCoroutine;
    public bool IsTyping { get; private set; }

    public void Show(string text)
    {
        gameObject.SetActive(true);
        if (typewriterCoroutine != null)
            StopCoroutine(typewriterCoroutine);
        typewriterCoroutine = StartCoroutine(TypewriterEffect(text));
    }

    public void SkipToEnd()
    {
        if (typewriterCoroutine != null)
            StopCoroutine(typewriterCoroutine);
        dialogueText.maxVisibleCharacters = dialogueText.text.Length;
        IsTyping = false;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        dialogueText.text = "";
    }

    private IEnumerator TypewriterEffect(string text)
    {
        IsTyping = true;
        dialogueText.text = text;
        dialogueText.maxVisibleCharacters = 0;

        foreach (char _ in text)
        {
            dialogueText.maxVisibleCharacters++;
            yield return new WaitForSeconds(1f / typewriterSpeed);
        }

        IsTyping = false;
    }
}