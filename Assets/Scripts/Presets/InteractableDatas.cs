using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Interactable Overview", menuName = "ED/Interactable Overview")]
public class InteractableDatas : ScriptableObject
{
    public bool ShowImage;
    public bool ShowText;

    [ConditionalField("ShowImage")]
    public Sprite ImageToShow;

    [ConditionalField("ShowText")]
    public string TextToShow;
}
