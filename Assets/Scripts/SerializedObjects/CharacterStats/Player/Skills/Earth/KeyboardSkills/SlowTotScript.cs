using UnityEngine;
using System.Collections;

public class SlowTotScript : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Slow l'enemy
        }
    }
}
