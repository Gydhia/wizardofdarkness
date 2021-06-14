using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
   public enum ETrapMode { Enter, Stay }
    public ETrapMode TrapMode;
    public int Damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (TrapMode == ETrapMode.Enter)
            {
                PlayerController.Instance.PlayerStats.TakeDamage(Damage);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (TrapMode == ETrapMode.Stay)
            {
                PlayerController.Instance.PlayerStats.TakeDamage(Damage);
            }
        }
    }
}
