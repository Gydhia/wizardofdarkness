using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int WeaponDamages;
    public bool CanHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CanHit) {
            if(other.TryGetComponent(out PlayerStats player)) {
                player.TakeDamage(WeaponDamages);
            }
        }
    }

}
