using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleScript : MonoBehaviour
{
    public float AOERadius;
    public Transform center;
    public float attractForce;

    void Update()
    {
        LayerMask enemy = LayerMask.GetMask("Enemy");
        Collider[] hitColliders = Physics.OverlapSphere(PlayerController.Instance.transform.position, AOERadius, enemy);
        foreach (Collider hitCollider in hitColliders)
        {
            hitCollider.transform.Translate((center.position - hitCollider.transform.position) * Time.deltaTime * attractForce);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
        }
    }
}
