using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerStats : MonoBehaviour
{
    /*
     Bienvenue! ce truc est assez insane. Mais ce qui va nous interesser, c'est avant le 2ème header. Descendons...
         */
    [Header("Variables For All Elements")]
    public CharacterStatus statsEmpty;


    public int HP { get; protected set; }
    public int Def { get; protected set; }
    public int Str { get; protected set; }
    public float AtkSpeed { get; protected set; }
    public float MoveSpeed { get; protected set; }

    public Element ActualElement;
    public List<Element> Elements = new List<Element>(3);

    public bool IsDead = false;

    /*
     A partir d'ici, typiquement si vous avez des prefab à stocker, des listes, des variables utiles à garder, c'est ici que vous allez les mettre
     (Sous Wind pour Killian, sous Earth pour Enzo, évidemment)
         voilà, vous savez tout normalement! Allez, codez maintenant!
         (voilà, maintenant vous avez toutes les infos les brows
         */
    public ParticleSystem[] particles;
    public ParticleSystem buffArrow;
    public ParticleSystem debuffArrow;
    
    private void Start()
    {
        foreach (Element element in Elements)
            element.Init();

        ActualElement = Elements.Single(element => element.Type == EElements.Void);
        UpdateStats();
    }
    private void Update()
    {
        
    }

    public void ChangeElement(EElements newElement)
    {
        if (ActualElement.Type != newElement)
        {
            ActualElement.ElementWeapon.SetActive(false);
            ActualElement = Elements.Single(element => element.Type == newElement);
            
            UpdateStats();

            ActualElement.ElementWeapon.SetActive(true);
            ActualElement.ChangementFX(ActualElement.SwitchingParticle);
        }
    }

    public void UpdateStats()
    {
        this.Def = ActualElement.Def;
        this.Str = ActualElement.Str;
        this.AtkSpeed = ActualElement.AtkSpeed;
        this.MoveSpeed = ActualElement.MoveSpeed;

        GameUIController.Instance.FireOnElementChange();
        //player.actualSkills.Clear();
        //foreach (Skill i in skills)
        //{
        //    player.actualSkills.Add(i);
        //}
        //player.CDs = CDs;
        //for (int i = 0; i < player.cooldownBars.Length; i++)
        //{
        //    player.cooldownBars[i].maxValue = CDs[i];
        //}

        //foreach (CoolDown bar in player.cooldownBars)
        //{
        //    bar.mat.SetColor("_Backgroundfillcolor", BarFillColor);
        //    bar.center.color = CenterColor;
        //    bar.mat.SetColor("_Barmincolor", BarMinMaxColor);
        //    bar.mat.SetColor("_Barmaxcolor", BarMinMaxColor);
        //}
    }

    public void TakeDamage(int damageTaken)
    {
        if (HP - damageTaken > 0) {
            HP -= damageTaken;
        } else {
            HP -= HP;
            Die();
        }
        GameUIController.Instance.FireOnDamageTaken(HP);
    }
    public void Die()
    {
        //GameOver Screen
        this.IsDead = true;
        PlayerUIManager.Instance.gameOver.gameObject.SetActive(true);
        PlayerUIManager.Instance.gameOver.SetTrigger("GameOver");
    }
    public IEnumerator StatBuff(float timeOfBuff, EStatsDebuffs buffID, int percentAugment, EElements affectedElemnt = EElements.None)
        //problème, si on change de classe, les bonus ne perdurent pas. Il faudra surement coder autrement.
        //(pire que ça: comme la coroutine perdure, on fini par soustraire buff (pour annuler le buff) alors qu'on a pas eu de buff.
    {
        switch (buffID)
        {
            case EStatsDebuffs.Defense: 
                float buff = percentAugment * Def / 100;
                Def += (int)buff;
                buffArrow.Play();
                yield return new WaitForSeconds(timeOfBuff);
                debuffArrow.Play();
                Def -= (int)buff;
                break;
            case EStatsDebuffs.Strength: 
                buff = percentAugment * Str / 100;
                Str += (int)buff;
                buffArrow.Play();
                yield return new WaitForSeconds(timeOfBuff);
                debuffArrow.Play();
                Str -= (int)buff;
                break;
            case EStatsDebuffs.AttackSpeed: 
                buff = percentAugment * AtkSpeed / 100;
                float buff2 = percentAugment * bendingSpeed / 100;
                AtkSpeed += buff;
                buffArrow.Play();
                bendingSpeed += buff2;
                yield return new WaitForSeconds(timeOfBuff);
                AtkSpeed -= buff;
                debuffArrow.Play();
                bendingSpeed -= buff2;
                break;
            case EStatsDebuffs.MoveSpeed: //Movespeed
                buff = percentAugment * (int)MoveSpeed / 100;
                MoveSpeed += buff;
                buffArrow.Play();
                yield return new WaitForSeconds(timeOfBuff);
                debuffArrow.Play();
                MoveSpeed -= buff;
                break;
        }
    }
}
