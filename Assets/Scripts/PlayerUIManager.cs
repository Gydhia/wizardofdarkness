using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static PlayerUIManager Instance;

    public GameObject beerUI;
    public bool beerDisplayed = false;

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        beerUI.SetActive(beerDisplayed);
    }
}
