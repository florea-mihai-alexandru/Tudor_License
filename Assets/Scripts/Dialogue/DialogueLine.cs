using System;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    [TextArea(2, 4)]
    public string text;

    public AudioClip voiceClip;
}