using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallScript : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public float ballMoveSpeed;
    [HideInInspector] public float ballGrowSpeed;
    [HideInInspector] public Vector3 maxScale;
    private int dmg;
    private bool launched;
    private Vector3 scale;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("LeftClickSpell") && !launched)
        {
            //On grow la bouboule...
            if (transform.localScale.x <= maxScale.x)
            {
                scale = transform.localScale;
                scale.x += ballGrowSpeed*Time.deltaTime;
                scale.y += ballGrowSpeed*Time.deltaTime;
                scale.z += ballGrowSpeed*Time.deltaTime;
                transform.localScale = scale;
            }
        }
        else
        {
            //On lache la bouboule
            if (!launched)
            {
            PlayerAnimationState.Instance.playerAnimator.SetBool("LongCast", false);

                launched = true;
                transform.parent.gameObject.transform.DetachChildren();
                Destroy(gameObject, 10f);
                dmg = (int)(scale.x * 20);
                PlayerMovement.Instance.stamina -= scale.x * PlayerStats.Instance.magicBallStaminaConsumption;
            }
            else
            {
                transform.Translate(Vector3.forward * ballMoveSpeed * Time.deltaTime);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        LayerMask enemy = LayerMask.GetMask("Enemy");
        if ((other.CompareTag("Enemy") && launched))
        {
            other.GetComponent<EnemyStats>().TakeDamage(dmg);
            Debug.Log(dmg);
        }
    }
}
