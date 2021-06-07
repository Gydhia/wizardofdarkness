using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHP;

    public GameObject HealthBarUI;
    public Slider Slider;

    public void Init(float MaxHP)
    {
        this.MaxHP = MaxHP;
        RefreshHealth(MaxHP);
    }

    public void RefreshHealth(float HP)
    {
        Slider.value = HP / MaxHP;
    }
}
