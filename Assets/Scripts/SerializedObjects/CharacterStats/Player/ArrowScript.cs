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
    bool secondCollisonWithPlayer;
    private float lifeTimer = 10f;
    private float timer;
    private bool hitSthg;
    // Update is called once per frame
    private void OnEnable()
    {
        player = PlayerStats.Instance.gameObject;
        arrowRb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(arrowRb.velocity);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= lifeTimer)
        {
            Destroy(gameObject);
        }
        if (!hitSthg)
        {
            transform.rotation = Quaternion.LookRotation(arrowRb.velocity);

        }
        if (isBeingCalledBack)
        {
            if (arrowRb.constraints == RigidbodyConstraints.FreezeAll)
            {
                arrowRb.constraints = RigidbodyConstraints.None;
            }

            dir = (player.transform.position - transform.position).normalized;
            arrowRb.AddForce(dir * arrowSpeed);
            transform.rotation = Quaternion.LookRotation(arrowRb.velocity);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && secondCollisonWithPlayer)
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player") && !secondCollisonWithPlayer) secondCollisonWithPlayer = true;
        else if (!collision.gameObject.CompareTag("Player"))
        {
            hitSthg = true;
            Stick();
        }
    }
    public void Stick()
    {

        arrowRb.constraints = RigidbodyConstraints.FreezeAll;

    }
    private void OnDestroy()
    {
        PlayerStats.Instance.activeArrows.Remove(this);
    }
}
