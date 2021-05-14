using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSpawner : MonoBehaviour
{
    public GameObject[] runesPrefab;
    [Range(0, 100)] public int spawnRange = 44;
    private int runeOrNot;
    private int runeChosen;
    // Start is called before the first frame update
    void Start()
    {
        //56% de spawn rien, 44 de spawn une rune
        runeOrNot = Random.Range(0, 100);
        if (runeOrNot >= (100 - spawnRange))
        {
            runeChosen = Random.Range(0, runesPrefab.Length);
            Instantiate(runesPrefab[runeChosen], transform);
        }
    }

}
