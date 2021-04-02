using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public enum EElements { Void = 0, Wind = 1, Earth = 2 }
    public EElements actualEElement;
    public int HP;
    public int def;
    public int str;
    public float atqSpeed;
    public float moveSpeed;
    public int actualElement;
    public Element[] elements;
    public Skill[] actualSkills;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        elements[actualElement].UpdateStats(this);
    }
    public void ChangeElement(EElements newElement)
    {
        if (actualEElement != newElement)
        {
            actualEElement = newElement;
            actualElement = (int)actualEElement;
            elements[actualElement].UpdateStats(this);
        }
    }


}
