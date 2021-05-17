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

    // EVENTS
    public event InteractOverviewChange OnInteractOverviewChange;
    public event InteractOverviewCancel OnInteractOverviewCancel;

    public Image Cursor;
    public TextMeshProUGUI InteractText;

    public static GameUIController Instance;
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
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
}
