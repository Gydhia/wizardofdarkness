using ED.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBeer : MonoBehaviour, IInteractable
{
    public GameObject fullMug;
    public GameObject emptyMug;
    public bool completionCondition;

    public InteractableDatas OverviewDatas => this.OverviewDatas;

    public void IsCompletionCondition()
    {
        completionCondition = true;
    }

    public void Hovered()
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
            PlayerController.Instance.PlayerStats.AddLife(100);
        }
    }

}
