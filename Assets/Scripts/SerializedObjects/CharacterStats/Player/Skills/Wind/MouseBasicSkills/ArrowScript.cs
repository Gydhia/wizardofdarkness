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
    private CapsuleCollider arrowCollider;

    private float lifeTimer = 10f;
    private float timer;
    private bool hitSthg;
    // Update is called once per frame
    private void OnEnable()
    {
        player = PlayerStats.Instance.gameObject;
        arrowRb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(arrowRb.velocity);
        arrowCollider = GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTimer)
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
            var pos = player.transform.position;
            pos.y += 1.5f;
            dir = (pos - transform.position).normalized;
            arrowRb.AddForce(dir * arrowSpeed*10);
            transform.rotation = Quaternion.LookRotation(arrowRb.velocity);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (isBeingCalledBack && collision.gameObject.CompareTag("Player")) Destroy(gameObject);
        /*else if (!isBeingCalledBack)
        {
            Physics.IgnoreCollision(collision.collider, arrowCollider);
            faudrait qu'on passe a travers, non? :/ edit: non là c'est insane en vrai
        }*/
        if (!collision.gameObject.CompareTag("Player"))
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
