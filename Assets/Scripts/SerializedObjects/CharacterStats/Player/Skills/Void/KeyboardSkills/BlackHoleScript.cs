using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleScript : MonoBehaviour
{
    public GameObject AOE;
    public Transform center;
    public float attractForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.transform.Translate((center.position - other.transform.position)*Time.deltaTime*attractForce);
        }
    }
}
