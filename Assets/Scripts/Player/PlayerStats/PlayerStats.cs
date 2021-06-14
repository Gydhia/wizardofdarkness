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
    public bool isInTutorial = false;

    public Element ActualElement;
    public List<Element> Elements;

    public ParticleSystem[] Particles;
    public ParticleSystem BuffArrow;
    public ParticleSystem DebuffArrow;

    private void Awake()
    {
        foreach (Element element in Elements) {
            element.Init();
            foreach (Skill skill in element.Skills)
                skill.EntityHolder = this;
        }

        ActualElement = Elements.SingleOrDefault(element => element.Type == EElements.Void);
        ActualElement.ElementWeapon.SetActive(true);

        if (SceneManagementSystem.Instance.GetActualSceneName().Equals("Tutorial"))
        {
            foreach (Element elem in Elements)
                elem.IsActive = false;
        }
            
    }

    private void Start()
    {
        this.HP = this.MaxHP;
        UpdateStats();
    }

    public void ChangeElement(EElements newElement)
    {
        if (ActualElement.Type != newElement && Elements.Single(elem => elem.Type == newElement).IsActive)
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
        //GameController.Instance.FireOnDeath();
        DungeonManager.Instance.TeleportPlayer();
    }
}
