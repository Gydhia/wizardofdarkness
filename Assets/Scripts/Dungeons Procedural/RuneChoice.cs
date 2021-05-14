using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneChoice : MonoBehaviour
{
    public GameObject[] runes;

    public void ChooseRune(int n)
    {
        GameObject r = Instantiate(runes[n], transform);
    }
    
}
