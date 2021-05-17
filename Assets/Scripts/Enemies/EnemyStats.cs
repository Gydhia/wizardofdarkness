using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDebuffs { Stun }
public enum EStatsDebuffs { MaxHP, Defense, Strength, AttackSpeed, MoveSpeed }

public abstract class EnemyStats : MonoBehaviour
{
    public enum EEnemyElements { Void = 0, Wind = 1, Earth = 2, None = 3 }
    public EEnemyElements ActualElement;
    public int MaxHP;
    public float HP;  //StatDebuff 0
    public float Def; //StatDebuff 1
    public float Str; //StatDebuff 2
    public float AtkSpeek; //StatDebuff 3
    public float MoveSpeed; //StatDebuff 4
    public Skill[] Skills;
    public MeshRenderer MatRenderer;
    bool IsFlashing;
    [Header("Debuff")]
    public bool IsStuned; //ID : 0
    public Animator EnemyAnimator;

    protected void Start()
    {
        EnemyAnimator = this.GetComponent<Animator>();
    }

    public void ActivateSkill(int index)
    {
        Skills[index].ActivatedSkill();
    }
    public virtual void TakeDamage(int value)
    {
        if(HP-value > 0) {
            HP -= (value-(value/Def));
        }
        else {
            HP -= HP;
            Die();
        }
    }

    public void Die()
    {
        //Animation de mort,
        EnemyAnimator.SetTrigger("Death");
        Destroy(gameObject, 1.5f);
    }
    public IEnumerator Debuff(float timeOfDebuff, EDebuffs debuffID)
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
    public IEnumerator StatDebuff(float timeOfDebuff, EStatsDebuffs debuffID, int percentReduce)
    {
        switch (debuffID)
        {
            case EStatsDebuffs.MaxHP: //HP
                float debuff;
                debuff = percentReduce*HP/100;
                MaxHP -= (int)debuff;
                yield return new WaitForSeconds(timeOfDebuff);
                MaxHP += (int)debuff;
                break;
            case EStatsDebuffs.Defense: //def
                debuff = percentReduce*Def/100;
                Def -= debuff;
                yield return new WaitForSeconds(timeOfDebuff);
                Def += debuff;
                break;
            case EStatsDebuffs.Strength: //str
                debuff = percentReduce*Str/100;
                Str -= debuff;
                yield return new WaitForSeconds(timeOfDebuff);
                Str += debuff;
                break;
            case EStatsDebuffs.AttackSpeed: //atqSpeed
                debuff = percentReduce*AtkSpeek/100;
                AtkSpeek -= debuff;
                yield return new WaitForSeconds(timeOfDebuff);
                AtkSpeek += debuff;
                break;
            case EStatsDebuffs.MoveSpeed: //Movespeed
                debuff = percentReduce*(int)MoveSpeed/100;
                MoveSpeed -= debuff;
                yield return new WaitForSeconds(timeOfDebuff);
                MoveSpeed += percentReduce;
                break;
        }
    }
    public void StartCor(IEnumerator cor)
    {
        StartCoroutine(cor);
        Debug.Log(cor.ToString());
    }
}
