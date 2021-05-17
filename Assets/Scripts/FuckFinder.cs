using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckFinder : MonoBehaviour
{
    public PlayerUIManager toFind;
    public void Start()
    {
        toFind = FindObjectOfType<PlayerUIManager>();
    }
}
