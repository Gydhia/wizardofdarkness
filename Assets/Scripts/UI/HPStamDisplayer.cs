using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class HPStamDisplayer : MonoBehaviour
{
    public TextMeshProUGUI HP;
    public Image HPbar;
    public TextMeshProUGUI Stamina;


    // Update is called once per frame
    void Update()
    {
        HP.text = PlayerStats.Instance.HP.ToString("#") + "%";
        HPbar.fillAmount = (float)PlayerStats.Instance.HP/100;
        Stamina.text = ((int)PlayerMovement.Instance.stamina).ToString("#") + "%";
    }
}
