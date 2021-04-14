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

    public void Init()
    {
        def = elementStats.baseDef;
        strength = elementStats.baseStr;
        attackSpeed = elementStats.attackSpeed;
        moveSpeed = elementStats.moveSpeed;
        skills = elementStats.skills.ToArray();    
        //Debug.Log(def);

    }
    public void UpdateStats(PlayerStats player)
    {
        player.def = def;
        player.str = strength;
        player.atqSpeed = attackSpeed;
        player.moveSpeed = moveSpeed;
        player.actualSkills = skills;
    }
}
