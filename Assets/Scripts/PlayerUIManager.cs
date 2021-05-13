using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ToggleUI(bool state);
public delegate void SetSlider(float value);

public class PlayerUIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static PlayerUIManager Instance;

    [Header("General")]
    public GameObject[] HUD;
    public bool hudActive = true;

    [Header("Beer")]
    public GameObject beerUI;
    public bool beerDisplayed = false;

    [Header("Stamina")]
    public Slider staminaBar;
    
    void Awake()
    {
        Instance = this;

    }
    void Start()
    {
        PlayerMovement.Instance.UpdateStamina += UpdateStamina;
    }
    private void UpdateStamina(float value)
    {
        staminaBar.value = value;
    }

    public void BeerUIToggle(bool state)
    {
        beerUI.SetActive(state);
    }

    public void ToggleHud(bool newState)
    {
        foreach (GameObject item in HUD)
        {
            item.SetActive(newState);
            hudActive = newState;
        }
    }
}
