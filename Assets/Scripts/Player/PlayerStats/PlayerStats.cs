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

    public ParticleSystem[] Particles;
    public ParticleSystem BuffArrow;
    public ParticleSystem DebuffArrow;
    
    private void Start()
    {
        foreach (Element element in Elements)
            element.Init();

        ActualElement = Elements.SingleOrDefault(element => element.Type == EElements.Void);
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

        GameController.Instance.FireOnElementChange();
    }

    public virtual void TakeDamage(int value)
    {
        base.TakeDamage(value);
        GameUIController.Instance.FireOnDamageTaken((int)HP);
    }

    public override void Die()
    {
        base.Die();
        GameController.Instance.FireOnDeath();
    }
}
