using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueBubble : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float typewriterSpeed = 40f;
    [SerializeField] private float heightOffset = 1.5f;

    [SerializeField] private AudioSource audioSource;

    private Coroutine typewriterCoroutine;
    private Transform followTarget;

    public bool IsTyping { get; private set; }

    public void Show(string text, Transform target, AudioClip voiceClip)
    {
        gameObject.SetActive(true);
        followTarget = target;

        if (typewriterCoroutine != null)
            StopCoroutine(typewriterCoroutine);

        typewriterCoroutine = StartCoroutine(TypewriterEffect(text, voiceClip));
    }

    public void SkipToEnd()
    {
        if (typewriterCoroutine != null)
            StopCoroutine(typewriterCoroutine);

        dialogueText.maxVisibleCharacters = dialogueText.text.Length;
        IsTyping = false;

        if (audioSource != null)
            audioSource.Stop(); // opreste audio-ul cand dai skip
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        dialogueText.text = "";
        followTarget = null;

        if (audioSource != null)
            audioSource.Stop();
    }

    private void LateUpdate()
    {
        if (followTarget != null)
            transform.position = followTarget.position + Vector3.up * heightOffset;
    }

    private IEnumerator TypewriterEffect(string text, AudioClip voiceClip)
    {
        IsTyping = true;
        dialogueText.text = text;
        dialogueText.maxVisibleCharacters = 0;

        if (audioSource != null)
        {
            audioSource.Stop(); 
            if (voiceClip != null)
            {
                audioSource.clip = voiceClip;
                audioSource.Play();
            }
        }

        foreach (char _ in text)
        {
            dialogueText.maxVisibleCharacters++;
            yield return new WaitForSeconds(1f / typewriterSpeed);
        }

        IsTyping = false;
    }
}