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

    public delegate void DamageTaken(int value);
    public delegate void StaminaConsumed();

    // EVENTS
    public event InteractOverviewChange OnInteractOverviewChange;
    public event InteractOverviewCancel OnInteractOverviewCancel;

    public event DamageTaken OnDamageTaken;
    public event StaminaConsumed OnStaminaConsumed;


    // ATTRIBUTES
    public List<ElementColor> ElementsColor;
    public Dictionary<Element, ElementColor> ElementsColors = new Dictionary<Element, ElementColor>();

    public Image Cursor;
    public TextMeshProUGUI InteractText;

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
    }

    private void Start()
    {
        foreach(ElementColor elementColor in ElementsColor) {
            if (!ElementsColors.ContainsKey(elementColor.Element))
                ElementsColors.Add(elementColor.Element, elementColor);
        }
        // For now the game over screen isn't that much elaborated, but we'll need to 
        // create its own script later. 
        GameController.Instance.OnDeath += LaunchGameOverScreen;
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
    public void FireOnDamageTaken(int value)
    {
        if (OnDamageTaken != null)
            OnDamageTaken.Invoke(value);
    }
    public void FireOnStaminaConsumed()
    {
        if (OnStaminaConsumed != null)
            OnStaminaConsumed.Invoke();
    }



    // Class methods
    public void FadeOut()
    {
        StartCoroutine(_fadeOut());
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
