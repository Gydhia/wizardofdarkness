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
    private GameObject Player;
    private Vector3 dir;    //direction
    private CapsuleCollider arrowCollider;

    private float lifeTimer = 10f;
    private float timer;
    private bool hitSthg;//hit Something: a touché qlqchose

    bool launched;
    bool WeakeningArrow;
    public float bending;
    public float maxBending;
    public int dmg;
    // Update is called once per frame
    private void OnEnable()
    {
        Player = PlayerController.Instance.gameObject;
        arrowCollider = GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        
        if (Input.GetButton("LeftClickSpell") && !launched)
        {
            //On bande
            if (bending <= maxBending)
            {
                bending += Time.deltaTime * ((WindElement)PlayerController.Instance.PlayerStats.ActualElement).BendingSpeed;
            }
            if (((WindElement)PlayerController.Instance.PlayerStats.ActualElement).NextArrowWeakens)
            {
                WeakeningArrow = true;
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
                PlayerController.Instance.PlayerMovement.stamina -= bending * ((WindElement)PlayerController.Instance.PlayerStats.ActualElement).WindArrowStaminaConsumption;
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
                    
                    dir = (Player.transform.position - transform.position).normalized;
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
            if (other.TryGetComponent(out EntityStat entityStat))
            {
                entityStat.TakeDamage(dmg);
                if (WeakeningArrow)
                {
                    entityStat.LaunchStatModifier(
                        ((WindElement)PlayerController.Instance.PlayerStats.ActualElement).weakenDuration, 
                        EStatsDebuffs.Defense,
                        ((WindElement)PlayerController.Instance.PlayerStats.ActualElement).weakenPercent
                    );
                }
            }            
        }
        else if (other.CompareTag("Player") && isBeingCalledBack)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        ((WindElement)PlayerController.Instance.PlayerStats.ActualElement).ActiveArrows.Remove(this);
    }
}
/*
 Ouch, c'était physique quand même!     (jsuis en moula)
 Mais assez simple en esprit            (jsuis en esprit)
 Bon maintenant bro c'est WeakeningArrow.cs !
 */
