using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Element : MonoBehaviour
{
    protected int def;
    protected int strength;
    protected float attackSpeed;
    protected float moveSpeed;
    public CharacterStatus elementStats;
    protected Skill[] skills;
    protected float[] CDs = new float[5];
    [SerializeField]protected Color BarFillColor;
    [SerializeField]protected Color BarBorderColor;
    [SerializeField]protected Color BarMinMaxColor;

    public void Init()
    {
        def = elementStats.baseDef;
        strength = elementStats.baseStr;
        attackSpeed = elementStats.attackSpeed;
        moveSpeed = elementStats.moveSpeed;
        skills = elementStats.skills.ToArray();
        //Debug.Log(def);
        for (int i = 0; i < skills.Length; i++)
        {
            CDs[i] = skills[i].coolDown;
        }
    }
    public void UpdateStats(PlayerStats player)
    {
        player.def = def;
        player.str = strength;
        player.atqSpeed = attackSpeed;
        player.moveSpeed = moveSpeed;
        player.actualSkills = skills;
        player.CDs = CDs;
        for (int i = 0; i < player.cooldownBars.Length; i++)
        {
            player.cooldownBars[i].maxValue = CDs[i];
        }
        
        foreach (CoolDown bar in player.cooldownBars)
        {
            bar.mat.SetColor("_Backgroundfillcolor", BarFillColor);
            bar.mat.SetColor("_Backgroundbordercolor", BarBorderColor);
            bar.mat.SetColor("_Barmincolor", BarMinMaxColor);
            bar.mat.SetColor("_Barmaxcolor", BarMinMaxColor);
        }
    }
    public abstract void ChangementFX();/*  Cette fonction, override dans les autres, va gérer les FX qu'on déclencheras lors d'un changement. La différence entre cette méthode et UpdateStats;
                                            C'est que du coup UpdateStats est commune pour toutes les classes. Dans cette fonction, on met alors les choses que les classes n'ont pas toutes en commun.
                                            Cette fonction sera appellée quand on changeras de classe, si c'était pas évident lmao

     */
}
