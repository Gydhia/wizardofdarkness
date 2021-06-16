using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Element Color", menuName = "ED/Element Color")]

public class ElementColor : ScriptableObject
{
    public EElements Element;

    public Color BackgroundFillColor;
    public Color CenterColor;
    public Color BarMinColor;
    public Color BarMaxColor;
}
