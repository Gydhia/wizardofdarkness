using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static PlayerUIManager Instance;

    public GameObject beerUI;
    public bool beerDisplayed = false;
    public GameObject[] HUD;
    public bool hudActive = true;
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        beerUI.SetActive(beerDisplayed);
    }
    public void ToggleHud()
    {
        Debug.Log("pls");
        foreach (GameObject item in HUD)
        {
            item.SetActive(!item.activeSelf);
            hudActive = !hudActive;
        }
    }
}
