using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStat : MonoBehaviour
{
    public bool IsDead = false;
    public bool IsStuned = false;

    public float MaxHP;
    public float HP { get; protected set; }
    public float Def { get; protected set; }
    public float Str { get; protected set; }
    public float AtkSpeed { get; protected set; }
    public float MoveSpeed { get; protected set; }

    public Animator EntityAnimator;
    public GameObject AimPoint;


    public virtual void TakeDamage(int value)
    {
        if (HP - value > 0) {
            HP -= (value - (value / Def));
        }
        else {
            HP -= HP;
            Die();
        }
    }

    public virtual void AddLife(int value)
    {
        if (HP + value > MaxHP)
            HP = MaxHP;
        else
            HP += value;
    }

    public virtual void Die()
    {
        IsDead = true;
        EntityAnimator.SetTrigger("Death");
        Destroy(gameObject, 1.5f);
    }

    public void LaunchStatModifier(float timeOfDebuff, EStatsDebuffs debuffID, int percentReduce)
    {
        StartCoroutine(_statModifier(timeOfDebuff, debuffID, percentReduce));
    }
    private IEnumerator _statModifier(float timeOfDebuff, EStatsDebuffs debuffID, int percentReduce)
    {
        switch (debuffID)
        {
            case EStatsDebuffs.MaxHP: //HP
                float debuff;
                debuff = percentReduce * HP / 100;
                MaxHP -= (int)debuff;
                yield return new WaitForSeconds(timeOfDebuff);
                MaxHP += (int)debuff;
                break;
            case EStatsDebuffs.Defense: //def
                debuff = percentReduce * Def / 100;
                Def -= debuff;
                yield return new WaitForSeconds(timeOfDebuff);
                Def += debuff;
                break;
            case EStatsDebuffs.Strength: //str
                debuff = percentReduce * Str / 100;
                Str -= debuff;
                yield return new WaitForSeconds(timeOfDebuff);
                Str += debuff;
                break;
            case EStatsDebuffs.AttackSpeed: //atqSpeed
                debuff = percentReduce * AtkSpeed / 100;
                AtkSpeed -= debuff;
                yield return new WaitForSeconds(timeOfDebuff);
                AtkSpeed += debuff;
                break;
            case EStatsDebuffs.MoveSpeed: //Movespeed
                debuff = percentReduce * MoveSpeed / 100;
                MoveSpeed -= debuff;
                yield return new WaitForSeconds(timeOfDebuff);
                MoveSpeed += percentReduce;
                break;
        }
    }

    public void LaunchStatusModifier(float timeOfDebuff, EDebuffs debuffID)
    {
        StartCoroutine(_statusModifier(timeOfDebuff, debuffID));
    }

    public IEnumerator _statusModifier(float timeOfDebuff, EDebuffs debuffID)
    {
        switch (debuffID)
        {
            case EDebuffs.Stun: //Stun
                IsStuned = true;
                yield return new WaitForSeconds(timeOfDebuff);
                IsStuned = false;
                break;
        }
    }
}
