using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerStats : EntityStat
{
    /*
     Bienvenue! ce truc est assez insane. Mais ce qui va nous interesser, c'est avant le 2ème header. Descendons...
         */
    [Header("Variables For All Elements")]
    public CharacterStatus statsEmpty;

    public Element ActualElement;
    public List<Element> Elements = new List<Element>(3);


    /*
     A partir d'ici, typiquement si vous avez des prefab à stocker, des listes, des variables utiles à garder, c'est ici que vous allez les mettre
     (Sous Wind pour Killian, sous Earth pour Enzo, évidemment)
         voilà, vous savez tout normalement! Allez, codez maintenant!
         (voilà, maintenant vous avez toutes les infos les brows
         */
    public ParticleSystem[] Particles;
    public ParticleSystem BuffArrow;
    public ParticleSystem DebuffArrow;
    
    private void Start()
    {
        foreach (Element element in Elements)
            element.Init();

        ActualElement = Elements.Single(element => element.Type == EElements.Void);
        UpdateStats();
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

    public virtual void TakeDamage(int value)
    {
        base.TakeDamage(value);
        GameUIController.Instance.FireOnDamageTaken((int)HP);
    }

    public override void Die()
    {
        base.Die();
        PlayerUIManager.Instance.gameOver.gameObject.SetActive(true);
        PlayerUIManager.Instance.gameOver.SetTrigger("GameOver");
    }
}
