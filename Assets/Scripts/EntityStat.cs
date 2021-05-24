using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDebuffs { Stun }
public enum EStatsBuffs { MaxHP, Defense, Strength, AttackSpeed, MoveSpeed }
public enum EEnemyElements { Void = 0, Wind = 1, Earth = 2, None = 3 }

public class EntityStat : MonoBehaviour
{
    public bool IsDead = false;
    public bool IsStuned = false;

    public float MaxHP;
    public float HP { get; protected set; }
    public float Def { get; protected set; }
    public float Str { get; protected set; }
    public float AtkSpeed { get; protected set; }
    public float MoveSpeed = 10;

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
        Destroy(gameObject, 1.5f);
    }

    public void LaunchStatModifier(float timeOfDebuff, EStatsBuffs debuffID, int percentReduce)
    {
        StartCoroutine(_statModifier(timeOfDebuff, debuffID, percentReduce));
    }
    private IEnumerator _statModifier(float timeOfDebuff, EStatsBuffs debuffID, int percent, bool isBuffing = true)
    {
        switch (debuffID)
        {
            case EStatsBuffs.MaxHP: //HP
                float modifier;
                modifier = percent * HP / 100;
                MaxHP = isBuffing ? MaxHP + (int)modifier : MaxHP - (int)modifier;
                yield return new WaitForSeconds(timeOfDebuff);
                MaxHP = isBuffing ? MaxHP - (int)modifier : MaxHP + (int)modifier;
                break;
            case EStatsBuffs.Defense: //def
                modifier = percent * Def / 100;
                Def = isBuffing ? Def + modifier : Def - modifier;
                yield return new WaitForSeconds(timeOfDebuff);
                Def = isBuffing ? Def - modifier : Def + modifier;
                break;
            case EStatsBuffs.Strength: //str
                modifier = percent * Str / 100;
                Str = isBuffing ? Str + modifier : Str - modifier;
                yield return new WaitForSeconds(timeOfDebuff);
                Str = isBuffing ? Str - modifier : Str + modifier;
                break;
            case EStatsBuffs.AttackSpeed: //atqSpeed
                modifier = percent * AtkSpeed / 100;
                AtkSpeed = isBuffing ? AtkSpeed + modifier : AtkSpeed - modifier;
                yield return new WaitForSeconds(timeOfDebuff);
                AtkSpeed = isBuffing ? AtkSpeed - modifier : AtkSpeed + modifier;
                break;
            case EStatsBuffs.MoveSpeed: //Movespeed
                modifier = percent * MoveSpeed / 100;
                MoveSpeed = isBuffing ? MoveSpeed + modifier: MoveSpeed - modifier;
                yield return new WaitForSeconds(timeOfDebuff);
                MoveSpeed = isBuffing ? MoveSpeed - modifier : MoveSpeed + modifier;
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
