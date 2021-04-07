using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Variables For All Elements")]
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
    public GameObject[] elementsWeapons;

    [Header("Wind Variables")]
    public List<ArrowScript> activeArrows = new List<ArrowScript>();
    public Transform arrowSpawn;

    private void Awake()
    {
        Instance = this;
        foreach(Element e in elements)
        {
            e.Init();
        }
    }
    private void Start()
    {
        //Debug.Log("wsh?");
        elements[actualElement].UpdateStats(this);
    }
    public void ChangeElement(EElements newElement)
    {
        if (actualEElement != newElement)
        {
            elementsWeapons[actualElement].SetActive(false);
            actualEElement = newElement;
            actualElement = (int)actualEElement;
            elements[actualElement].UpdateStats(this);
            elementsWeapons[actualElement].SetActive(true);
        }
    }
}
