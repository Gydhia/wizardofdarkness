using ED.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBeer : MonoBehaviour, IInteractable
{
    public GameObject fullMug;
    public GameObject emptyMug;
    public event ToggleUI ToggleBeer;
    public bool completionCondition;

    public InteractableDatas OverviewDatas => this.OverviewDatas;

    public void IsCompletionCondition()
    {
        completionCondition = true;
    }

    public void Hovered(bool isHovered)
    {
        return;
    }
    public void Unhovered()
    {
        return;
    }
    public void Interact()
    {
        if (fullMug.activeSelf)
        {
            fullMug.SetActive(false);
            emptyMug.SetActive(true);
            PlayerStats.Instance.HP = 100;
        }
    }

    public void Hovered()
    {
        throw new System.NotImplementedException();
    }
}
