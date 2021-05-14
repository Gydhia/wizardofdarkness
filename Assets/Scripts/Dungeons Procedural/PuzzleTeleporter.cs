using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTeleporter : MonoBehaviour
{
    public int TPNumber;
    public RuneChoice[] runes;
    private void Start()
    {
        foreach (RuneChoice rune in runes)
        {
            rune.ChooseRune(TPNumber);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PuzzleRoomManager.Instance.NextStep(TPNumber);
        }
    }
}
