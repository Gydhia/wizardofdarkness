using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActualCanvas : MonoBehaviour
{
    public GameObject BeerUI;
    public GameObject Bars;
    public Image Crosshair;
    public TextMeshProUGUI InteractText;
    public Animator fade;
    public TextMeshProUGUI DialogBox;
    public Image StaminaBarFill;
    public Image HPBarFill;
    public GameObject GameOverScreen;

    public static ActualCanvas Instance;
    private void Awake()
    {
        Instance = this;
    }
}
