using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TextContainer : MonoBehaviour
{
    public string[] dialogLines;
    protected void HydrateAndStart()
    {
        TextingSystemManager.Instance.dialogLines = dialogLines;
        TextingSystemManager.Instance.BeginDialogue();
    }
}
