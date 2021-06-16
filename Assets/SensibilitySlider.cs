using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensibilitySlider : MonoBehaviour
{
    public Slider MainSlider;

    private void Start()
    {
        MainSlider = this.GetComponent<Slider>();
        MainSlider.value = 25f;
    }

    public void Update()
    {
        MainSlider.onValueChanged.AddListener(delegate { GameUIController.Instance.FireOnChangeSensibility((int)MainSlider.value); });
    }
}
