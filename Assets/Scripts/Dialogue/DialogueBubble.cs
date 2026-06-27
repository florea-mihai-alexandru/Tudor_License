using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueBubble : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float typewriterSpeed = 40f;
    [SerializeField] private float heightOffset = 1.5f;

    private Coroutine typewriterCoroutine;
    private Transform followTarget;

    public bool IsTyping { get; private set; }

    public void Show(string text, Transform target)
    {
        gameObject.SetActive(true);
        followTarget = target;

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
        followTarget = null;
    }

    private void LateUpdate()
    {
        if (followTarget != null)
            transform.position = followTarget.position + Vector3.up * heightOffset;
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