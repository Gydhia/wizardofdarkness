using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using System;

public class SkillCooldown : MonoBehaviour
{
    public SkillBind SkillBind;
    private Skill ActualSkill;
    private ElementColor SkillColor;

    private Material SkillMaterial;
    public RawImage CenterImage;
    public TextMeshProUGUI CooldownText;

    private float MaxValue;
    private float FillPercentage;
    private float InitialFillPercentage;

    void Awake()
    {
        
    }

    void FixedUpdate()
    {
        if (MaxValue > 0) {
            FillPercentage = ActualSkill.CooldownTimer / MaxValue;
            SkillMaterial.SetFloat("_Fillpercentage", FillPercentage);
            CooldownText.text = (Math.Round(MaxValue - ActualSkill.CooldownTimer, 1)).ToString();
        }
    }

    void Start()
    {
        ActualSkill = PlayerController.Instance.PlayerStats.ActualElement.Skills.
            SingleOrDefault(skill => skill.SkillBind == this.SkillBind);

        SkillMaterial = this.GetComponent<RawImage>().material;
        InitialFillPercentage = SkillMaterial.GetFloat("_Fillpercentage");
        MaxValue = ActualSkill.Cooldown;
        FillPercentage = InitialFillPercentage;
        SkillMaterial.SetFloat("_Fillpercentage", FillPercentage);

        if (MaxValue <= 0) {
            FillPercentage = 1;
            SkillMaterial.SetFloat("_Fillpercentage", FillPercentage);
            CooldownText.text = "";
        }

        GameController.Instance.OnElementChange += ChangeFocusedSkill;
        ChangeFocusedSkill();
    }

    public void ChangeFocusedSkill()
    {
        ActualSkill = PlayerController.Instance.PlayerStats.ActualElement.Skills.SingleOrDefault(skill => skill.SkillBind == this.SkillBind);
        SkillColor = GameUIController.Instance.ElementsColors[PlayerController.Instance.PlayerStats.ActualElement.Type] ;

        SkillMaterial.SetColor("_Backgroundfillcolor",  SkillColor.BackgroundFillColor);
        CenterImage.material.color = SkillColor.CenterColor;
        SkillMaterial.SetColor("_Barmincolor", SkillColor.BarMinColor);
        SkillMaterial.SetColor("_Barmaxcolor", SkillColor.BarMaxColor);
    }
}
