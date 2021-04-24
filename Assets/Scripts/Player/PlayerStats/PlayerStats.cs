using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    /*
     Bienvenue! ce truc est assez insane. Mais ce qui va nous interesser, c'est avant le 2ème header. Descendons...
         */
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
    [HideInInspector] public Skill[] actualSkills;
    public bool canOpenDoors;
    public GameObject[] elementsWeapons;
    public float[] CDs;
    [SerializeField] float[] timers = new float[5];
    public CoolDown[] cooldownBars = new CoolDown[5];
    /*
     A partir d'ici, typiquement si vous avez des prefab à stocker, des listes, des variables utiles à garder, c'est ici que vous allez les mettre
     (Sous Wind pour Killian, sous Earth pour Enzo, évidemment)
         voilà, vous savez tout normalement! Allez, codez maintenant!
         (voilà, maintenant vous avez toutes les infos les brows
         */
    [Header("Wind Variables")]
    public List<ArrowScript> activeArrows = new List<ArrowScript>();
    public Transform arrowSpawn;

    [Header("Void Variables")]
    public Transform ballSpawnSpot;
    public GameObject ballPrefab;
    public GameObject blackHolePrefab;
    public GameObject teleportPointPrefab;
    public TPPointScript actualTPPoint;
    public GameObject projBarrier;

    [Header("Earth Variables")]
    public GameObject earthquakePrefab;

    private void Awake()
    {
        Instance = this;

    }
    private void Start()
    {
        //Debug.Log("wsh?");
        foreach (Element e in elements)
        {
            e.Init();
        }
        elements[actualElement].UpdateStats(this);
    }
    private void Update()
    {
        for (int i = 0; i < actualSkills.Length; i++)
        {
            if (!actualSkills[i].canLaunch)
            {
                if (timers[i] >= CDs[i])
                {
                    timers[i] = 0;
                    actualSkills[i].canLaunch = true;
                }
                else
                {
                    timers[i] += Time.deltaTime;
                }
            }
            cooldownBars[i].fillValue = timers[i];
        }
        if(actualEElement == EElements.Void)
        {
            projBarrier.SetActive(Input.GetButton("RightClickSpell"));
        }
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
            elements[actualElement].ChangementFX();
        }
    }
}
