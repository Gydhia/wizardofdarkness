using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EElements { Void = 0, Wind = 1, Earth = 2, None = -1 }
public class PlayerStats : MonoBehaviour
{
    /*
     Bienvenue! ce truc est assez insane. Mais ce qui va nous interesser, c'est avant le 2�me header. Descendons...
         */
    [Header("Variables For All Elements")]
    public static PlayerStats Instance;
    public EElements actualEElement;
    [Min(0)] public int HP;
    public int def;
    public int str;
    public float atqSpeed;
    public float moveSpeed;
    public int actualElement;
    public List<Element> elements = new List<Element>(3);
    public List<Skill> actualSkills = new List<Skill>();
    public bool canOpenDoors;
    public GameObject[] elementsWeapons;
    public float[] CDs;
    [SerializeField] float[] timers = new float[5];
    public CoolDown[] cooldownBars = new CoolDown[5];
    public Material HPBar;
    float HPPercentage;

    /*
     A partir d'ici, typiquement si vous avez des prefab � stocker, des listes, des variables utiles � garder, c'est ici que vous allez les mettre
     (Sous Wind pour Killian, sous Earth pour Enzo, �videmment)
         voil�, vous savez tout normalement! Allez, codez maintenant!
         (voil�, maintenant vous avez toutes les infos les brows
         */
    [Header("Wind Variables")]
    public List<ArrowScript> activeArrows = new List<ArrowScript>();
    public Transform arrowSpawn;
    public float windArrowStaminaConsumption;
    public bool nextArrowWeakens;
    public float bendingSpeed;
    public float weakenDuration;
    public int weakenPercent;

    [Header("Void Variables")]
    public float magicBallStaminaConsumption;
    public Transform ballSpawnSpot;
    public GameObject ballPrefab;
    public GameObject blackHolePrefab;
    public GameObject teleportPointPrefab;
    [HideInInspector]public TPPointScript actualTPPoint;
    public GameObject projBarrier;
    public float projBarrierStaminaConsumption;

    [Header("Earth Variables")]
    //public GameObject earthquakePrefab; j'avais pr�vu �a pour le shader hihi
    public bool blocking;

    private void Awake()
    {
        Instance = this;
        if (elements.Count == 0)
        {
            actualEElement = EElements.None;
            PlayerUIManager.Instance.ToggleHud(false);
        }
    }
    private void Start()
    {
        if (actualEElement != EElements.None)
        {
            //Debug.Log("wsh?");
            foreach (Element e in elements)
            {
                e.Init();
            }
            elements[actualElement].UpdateStats(this);
            for (int i = 0; i < timers.Length; i++)
            {
                timers[i] = 0;
            }
        }
    }
    private void Update()
    {
        blocking = false;
        HPPercentage = (float)HP / 100;
        HPBar.SetFloat("_Fillpercentage", HPPercentage);
        if (actualEElement != EElements.None)
        {
            for (int i = 0; i < actualSkills.Count; i++)
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
        }
        else if (actualEElement == EElements.Void)
        {

            if (projBarrier.activeSelf) PlayerMovement.Instance.stamina -= Time.deltaTime * projBarrierStaminaConsumption;
            if (Input.GetButtonDown("RightClickSpell"))
            {
                projBarrier.SetActive(true);
            }
            else if (Input.GetButtonUp("RightClickSpell") || PlayerMovement.Instance.stamina <= 2)
            {
                projBarrier.SetActive(false);
            }
        }
        else if (actualEElement == EElements.Earth)
        {
            blocking = Input.GetButton("RightClickSpell");
        }

    }
    public void ChangeElement(EElements newElement)
    {

        if (actualEElement != newElement)
        {
            elementsWeapons[actualElement].SetActive(false);
            actualEElement = newElement;
            actualElement = (int)actualEElement;
            foreach (Skill s in actualSkills)
            {
                s.canLaunch = true;
            }
            elements[actualElement].UpdateStats(this);
            for (int i = 0; i < timers.Length; i++)
            {
                timers[i] = 0;
            }
            elementsWeapons[actualElement].SetActive(true);
            elements[actualElement].ChangementFX();
        }
    }
    void AddDamage(int damageTaken)
    {
        if (HP - damageTaken > 0)
        {
            HP -= damageTaken;
        }
        else
        {
            HP -= HP;
            Die();
        }
    }
    void Die()
    {
        //GameOver Screen
    }
    public IEnumerator StatBuff(float timeOfBuff, EStatsDebuffs buffID, int percentAugment)
        //probl�me, si on change de classe, les bonus ne perdurent pas. Il faudra surement coder autrement.
        //(pire que �a: comme la coroutine perdure, on fini par soustraire buff (pour annuler le buff) alors qu'on a pas eu de buff.
    {
        switch (buffID)
        {
            case EStatsDebuffs.Defense: //def
                float buff = percentAugment * def / 100;
                def += (int)buff;
                yield return new WaitForSeconds(timeOfBuff);
                def -= (int)buff;
                break;
            case EStatsDebuffs.Strength: //str
                buff = percentAugment * str / 100;
                str += (int)buff;
                yield return new WaitForSeconds(timeOfBuff);
                str -= (int)buff;
                break;
            case EStatsDebuffs.AttackSpeed: //atqSpeed
                buff = percentAugment * atqSpeed / 100;
                float buff2 = percentAugment * bendingSpeed / 100;
                atqSpeed += buff;
                bendingSpeed += buff2;
                yield return new WaitForSeconds(timeOfBuff);
                atqSpeed -= buff;
                bendingSpeed -= buff2;
                break;
            case EStatsDebuffs.MoveSpeed: //Movespeed
                buff = percentAugment * (int)moveSpeed / 100;
                moveSpeed += buff;
                yield return new WaitForSeconds(timeOfBuff);
                moveSpeed -= percentAugment;
                break;
        }
    }
}
