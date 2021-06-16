using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCollision : MonoBehaviour
{
    public int ProjectileDamages;

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out PlayerStats player))
            {
                player.TakeDamage(ProjectileDamages);
            }
        }

        Invoke("DelayDestroy", 1.5f);
    }
    private void DelayDestroy()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
