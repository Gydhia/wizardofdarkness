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
        GameUIController.Instance.OnStaminaChange += UpdateStaminaBar;
        FillText.text = "100" + "%";
        Bar.fillAmount = 1f;
    }

    public void UpdateStaminaBar()
    {
        FillText.text = PlayerController.Instance.PlayerMovement.stamina.ToString("#") + "%";
        Bar.fillAmount = (float)PlayerController.Instance.PlayerMovement.stamina / 100;
    }

    private void OnDestroy()
    {
        GameUIController.Instance.OnStaminaChange -= UpdateStaminaBar;
    }
}
