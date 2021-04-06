using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;

    [SerializeField]
    private int maxStamina = 1000;   
    public int currentStamina;
    public bool IsRunning;
    public int StaminaRegenMinimum = 300;
    
    private WaitForSeconds regenTick = new WaitForSeconds(0.5f);
    private Coroutine regen;





    public static StaminaBar instance;
    public void Awake()
    {
        instance = this;
    }

    public void Start() 
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
        StaminaRegenMinimum = maxStamina / 3;
    }

    public void UseStamina(int amount) 
    {
        if(currentStamina - amount >= 0) 
        {
            if (regen != null)
                StopCoroutine(regen);
            currentStamina -= amount;
            staminaBar.value = currentStamina;
           
            IsRunning = true;
            regen = null;
        }

    }
    public void StopRunning()

    {
        IsRunning = false;
        if (regen == null && currentStamina < StaminaRegenMinimum)
            regen = StartCoroutine(RegenStamina());          
    }

    public IEnumerator RegenStamina() 
    {
        yield return new WaitForSeconds(2);

        while(currentStamina < maxStamina) 
        {
            currentStamina += maxStamina / 10;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
        regen = null;
    }
}
