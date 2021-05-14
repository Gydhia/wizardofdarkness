using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    /* dans l'ensemble, ce script est assez simple. Lit le de haut en bas, c'est simplissime*/
    public bool isBeingCalledBack;
    [SerializeField]
    private float arrowSpeed;
    [SerializeField]
    private Rigidbody arrowRb;
    private GameObject player;
    private Vector3 dir;    //direction
    private CapsuleCollider arrowCollider;

    private float lifeTimer = 10f;
    private float timer;
    private bool hitSthg;//hit Something: a touché qlqchose

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
        }//TF, j'aurais pu faire tellement plus simple, genre mettre un "Destroy(gameObject, lifeTimer);" dans le OnEnable???? jsp mdr
        if (!hitSthg)
        {
            transform.rotation = Quaternion.LookRotation(arrowRb.velocity); //Quand tu touche quelque chose, tu te plante de manière realiste
        }
        if (isBeingCalledBack)
        {
            if (arrowRb.constraints == RigidbodyConstraints.FreezeAll)//Si, de base, j'ai totalement freeze la flèche dans l'espace:
            {
                arrowRb.constraints = RigidbodyConstraints.None;//Unfreeze la
            }
            var pos = player.transform.position;
            pos.y += 1.5f;
            dir = (pos - transform.position).normalized;
            arrowRb.AddForce(dir * arrowSpeed*10);
            transform.rotation = Quaternion.LookRotation(arrowRb.velocity); //Fais venir les flèche vers moi VITE
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (isBeingCalledBack && collision.gameObject.CompareTag("Player")) Destroy(gameObject); //Pretty obvious
        /*else if (!isBeingCalledBack)
        {
            Physics.IgnoreCollision(collision.collider, arrowCollider); //J'avais mis ça, mais en fait non, sinon elle disparraissen tjamais (sauf si le timer expire)
        }*/
        if (!collision.gameObject.CompareTag("Player"))//Si on a touché autre chose que le joueur, alors on se plante dans cette chose
        {
            hitSthg = true;
            Stick();
        }
    }
    public void Stick()
    {
        arrowRb.constraints = RigidbodyConstraints.FreezeAll;//On bloque totalement la flèche dans l'espace
    }
    private void OnDestroy()
    {
        PlayerStats.Instance.activeArrows.Remove(this);//On pense a retirer la flèche de l'array des flèches actives
    }
    /*
     Ouch, c'était physique quand même!     (jsuis en moula)
     Mais assez simple en esprit            (jsuis en esprit)
     Bon maintenant bro c'est WeakeningArrow.cs !
     */
}
