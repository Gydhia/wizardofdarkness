using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public TextMeshProUGUI FillText;
    public Image Bar;

    void Start()
    {
        GameUIController.Instance.OnStaminaConsumed += UpdateStaminaBar;
    }

    public void UpdateStaminaBar()
    {
        FillText.text = PlayerController.Instance.PlayerMovement.stamina.ToString("#") + "%";
        Bar.fillAmount = (float)PlayerController.Instance.PlayerMovement.stamina / 100;
    }
}
