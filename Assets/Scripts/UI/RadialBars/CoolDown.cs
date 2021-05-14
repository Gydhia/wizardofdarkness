using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CoolDown : MonoBehaviour
{
    public Material mat;
    public float fillValue;
    public float fillPercentage;
    private float initialFillPercentage;
    public float maxValue;
    public RawImage center;
    public float fillSpeed = 1f;
    public TextMeshProUGUI textField;

    // Start is called before the first frame update
    void Awake()
    {
        mat = GetComponent<RawImage>().material;
        initialFillPercentage = mat.GetFloat("_Fillpercentage");
        fillPercentage = initialFillPercentage;
        mat.SetFloat("_Fillpercentage", fillPercentage);
    }
    void FixedUpdate()
    {
        //fillPercentage = Mathf.PingPong(Time.time * fillSpeed, 1f);
        if(maxValue > 0)
        {
            fillPercentage = fillValue / maxValue;
            mat.SetFloat("_Fillpercentage", fillPercentage);
            textField.text = (Math.Round(maxValue-fillValue, 1)).ToString();
        }
        else
        {
            fillPercentage = 1;
            mat.SetFloat("_Fillpercentage", fillPercentage);
            textField.text = "NoCD";
        }
    }
}
