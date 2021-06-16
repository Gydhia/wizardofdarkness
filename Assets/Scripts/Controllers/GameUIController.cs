using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    // DELEGATES
    public delegate void InteractOverviewChange(InteractableDatas Overview);
    public delegate void InteractOverviewCancel();
    public delegate void ChangeSensibility(int value);

    public delegate void DamageTaken(int value);
    public delegate void StaminaChange();

    // EVENTS
    public event InteractOverviewChange OnInteractOverviewChange;
    public event InteractOverviewCancel OnInteractOverviewCancel;
    public event ChangeSensibility OnChangeSensibility;

    public event DamageTaken OnDamageTaken;
    public event StaminaChange OnStaminaChange;


    // ATTRIBUTES
    public List<ElementColor> ElementsColor;
    public Dictionary<EElements, ElementColor> ElementsColors = new Dictionary<EElements, ElementColor>();

    public Image Cursor;

    // Transitions
    public bool FadedOut = false;
    public Animator SceneFade;
    public Animator GameOver;

    public static GameUIController Instance;


    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        foreach (ElementColor elementColor in ElementsColor) {
            if (!ElementsColors.ContainsKey(elementColor.Element))
                ElementsColors.Add(elementColor.Element, elementColor);
        }
    }
    
    private void Start()
    {
        // For now the game over screen isn't that much elaborated, but we'll need to 
        // create its own script later. 
        GameController.Instance.OnDeath += LaunchGameOverScreen;
    }

    public void OnLevelWasLoaded(int level)
    {
       
    }

    public void FireOnInteractOverviewChange(InteractableDatas Overview)
    {
        if (OnInteractOverviewChange != null)
            OnInteractOverviewChange.Invoke(Overview);
    }
    public void FireOnInteractOverviewCancel()
    {
        if (OnInteractOverviewCancel != null)
            OnInteractOverviewCancel.Invoke();
    }

    public void FireOnChangeSensibility(int value)
    {
        if (OnChangeSensibility != null)
            OnChangeSensibility.Invoke(value);
    }
    public void FireOnDamageTaken(int value)
    {
        if (OnDamageTaken != null)
            OnDamageTaken.Invoke(value);
    }
    public void FireOnStaminaChange()
    {
        if (OnStaminaChange != null)
            OnStaminaChange.Invoke();
    }



    // Class methods
    public void FadeOut()
    {
        //StartCoroutine(_fadeOut());
    }
    private IEnumerator _fadeOut()
    {
        FadedOut = false;
        SceneFade.SetTrigger("FadeOut");
        yield return new WaitForSeconds(SceneFade.GetCurrentAnimatorStateInfo(0).length);
        FadedOut = true;
    }
    public void LaunchGameOverScreen()
    {
        GameOver.gameObject.SetActive(true);
        GameOver.SetTrigger("GameOver");
    }
}
