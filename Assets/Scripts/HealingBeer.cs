using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBeer : MonoBehaviour, IInteractable
{
    public GameObject fullMug;
    public GameObject emptyMug;
    public event ToggleUI ToggleBeer;

    public void Hovered(bool isHovered)
    {
        PlayerUIManager.Instance.BeerUIToggle(isHovered);
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
}
