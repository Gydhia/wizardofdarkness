using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDebuffs { Stun }
public enum EStatsDebuffs { MaxHP, Defense, Strength, AttackSpeed, MoveSpeed }

public abstract class EnemyStats : MonoBehaviour
{
    public enum EEnemyElements { Void = 0, Wind = 1, Earth = 2, None = 3 }
    public EEnemyElements ActualElement;
    public int maxHP;
    public float HP;  //StatDebuff 0
    public float def; //StatDebuff 1
    public float str; //StatDebuff 2
    public float atqSpeed; //StatDebuff 3
    public float moveSpeed; //StatDebuff 4
    public Skill[] Skills;
    public MeshRenderer matRenderer;
    bool flashing;
    [Header("Debuff")]
    public bool isStuned; //ID : 0

    public void ActivateSkill(int index)
    {
        Skills[index].ActivatedSkill();
    }
    public void AddDamage(int damageTaken)
    {
        StartCoroutine(Flashing());
        if(HP-damageTaken > 0)
        {
            HP -= damageTaken;
        }
        else
        {
            HP -= HP;
            Die();
        }

    }
    private IEnumerator Flashing()
    {
        //Red
        if (flashing == false)
        {
            flashing = true;
            Color defaultCol = matRenderer.material.color;
            matRenderer.material.color = Color.red;
            yield return new WaitForSeconds(.5f);
            flashing = false;
            matRenderer.material.color = defaultCol;
        }
    }
    void Die()
    {
        //Animation de mort,
        Destroy(gameObject, 1.5f);
    }
    public IEnumerator Debuff(float timeOfDebuff, EDebuffs debuffID)
    {

        switch (debuffID)
        {
            case EDebuffs.Stun: //Stun
                isStuned = true;
                yield return new WaitForSeconds(timeOfDebuff);
                isStuned = false;
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
                maxHP -= (int)debuff;
                yield return new WaitForSeconds(timeOfDebuff);
                maxHP += (int)debuff;
                break;
            case EStatsDebuffs.Defense: //def
                debuff = percentReduce*def/100;
                def -= debuff;
                yield return new WaitForSeconds(timeOfDebuff);
                def += debuff;
                break;
            case EStatsDebuffs.Strength: //str
                debuff = percentReduce*str/100;
                str -= debuff;
                yield return new WaitForSeconds(timeOfDebuff);
                str += debuff;
                break;
            case EStatsDebuffs.AttackSpeed: //atqSpeed
                debuff = percentReduce*atqSpeed/100;
                atqSpeed -= debuff;
                yield return new WaitForSeconds(timeOfDebuff);
                atqSpeed += debuff;
                break;
            case EStatsDebuffs.MoveSpeed: //Movespeed
                debuff = percentReduce*(int)moveSpeed/100;
                moveSpeed -= debuff;
                yield return new WaitForSeconds(timeOfDebuff);
                moveSpeed += percentReduce;
                break;
        }
    }
    public void StartCor(IEnumerator cor)
    {
        StartCoroutine(cor);
        Debug.Log(cor.ToString());
    }
}
