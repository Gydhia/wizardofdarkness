using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTeleporter : MonoBehaviour
{
    public int TPNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PuzzleRoomManager.Instance.NextStep(TPNumber);
        }
    }
}
