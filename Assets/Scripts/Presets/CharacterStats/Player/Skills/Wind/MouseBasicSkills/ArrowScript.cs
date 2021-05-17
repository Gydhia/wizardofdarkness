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
    private GameObject player;
    private Vector3 dir;    //direction
    private CapsuleCollider arrowCollider;

    private float lifeTimer = 10f;
    private float timer;
    private bool hitSthg;//hit Something: a touché qlqchose

    bool launched;
    bool weakeningArrow;
    public float bending;
    public float maxBending;
    public int dmg;
    // Update is called once per frame
    private void OnEnable()
    {
        player = PlayerStats.Instance.gameObject;
        arrowCollider = GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        
        if (Input.GetButton("LeftClickSpell") && !launched)
        {
            //On bande
            if (bending <= maxBending)
            {
                bending += Time.deltaTime * PlayerStats.Instance.bendingSpeed;
            }
            if (PlayerStats.Instance.nextArrowWeakens)
            {
                weakeningArrow = true;
            }
        }
        else
        {
            //On lance la flèche
            if (!launched)
            {
                launched = true;
                transform.parent.gameObject.transform.DetachChildren();
                Destroy(gameObject, lifeTimer);
                dmg = (int)(bending * 20);
                PlayerMovement.Instance.stamina -= bending * PlayerStats.Instance.windArrowStaminaConsumption;
                arrowSpeed += bending;
            }
            else
            {
                if (!isBeingCalledBack)
                {
                    transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
                }
                else
                {
                    
                    dir = (player.transform.position - transform.position).normalized;
                    transform.Translate(dir * arrowSpeed * Time.deltaTime);
                    //fait revenir la flèche
                    transform.rotation = Quaternion.LookRotation(dir); //Fais venir les flèche vers moi VITE
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        LayerMask enemy = LayerMask.GetMask("Enemy");
        if ((other.CompareTag("Enemy") && launched))
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(dmg);
            Debug.Log(dmg);
            if (weakeningArrow)
            {
                enemyStats.StatDebuff(PlayerStats.Instance.weakenDuration,EStatsDebuffs.Defense,PlayerStats.Instance.weakenPercent);
            }
        }
        else if (other.CompareTag("Player") && isBeingCalledBack)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        PlayerStats.Instance.activeArrows.Remove(this);//On pense a retirer la flèche de l'array des flèches actives
    }
}
/*
 Ouch, c'était physique quand même!     (jsuis en moula)
 Mais assez simple en esprit            (jsuis en esprit)
 Bon maintenant bro c'est WeakeningArrow.cs !
 */
