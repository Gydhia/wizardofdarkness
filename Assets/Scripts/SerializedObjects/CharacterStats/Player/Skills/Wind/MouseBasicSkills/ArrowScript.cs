using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public bool isBeingCalledBack;
    [SerializeField]
    private float arrowSpeed;
    [SerializeField]
    private Rigidbody arrowRb;
    private GameObject player;
    private Vector3 dir;
    // Update is called once per frame
    private void OnEnable()
    {
        player = PlayerStats.Instance.gameObject;
    }
    void Update()
    {
        if (isBeingCalledBack)
        {
            dir = (player.transform.position - transform.position).normalized;
            arrowRb.AddForce(dir * arrowSpeed);
        }
    }
}
