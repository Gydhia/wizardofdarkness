using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Melee : MonoBehaviour
{
    public int Dmg;
    private Animator MeleeAnimator;
    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // Center of the screen
    public float attackRange;
    LayerMask enemies;
    public GameObject BloodFXPrefab;

    private void Start()
    {
        MeleeAnimator = GetComponent<Animator>();
    }

    public void Damage()
    {
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        enemies = LayerMask.GetMask("Enemy", "TriggeredEnemy");
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, attackRange, enemies))
        {
            if (hit.collider.gameObject.TryGetComponent(out EntityStat entity))
            {
                entity.TakeDamage(Dmg);
                Instantiate(BloodFXPrefab, new Vector3(hit.point.x,hit.point.y,hit.point.z), Quaternion.identity);
            }
        }
    }
}
