using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashGround : MeleeAttackSkill
{
    public float SmashJumpHeight;

    public override void ActivatedSkill()
    {
        if (PlayerController.Instance.PlayerMovement.isGrounded)
        {
            PlayerController.Instance.PlayerMovement.velocity.y = Mathf.Sqrt(SmashJumpHeight * -7f * PlayerController.Instance.PlayerMovement.Gravity);
            /*
         Donc, ici, c'est le nouveau spell qui a remplac� le totem: on frappe le sol pour faire un aoe de d�gats.
         Pour les d�gats, je ferais pareil  que la STUNAOE, je sais pas
         et pour le saut, je l'ai essay� ici, mais jsp si �a marche mdr
         C'est ton spell tfa�on fait ce que tu veux brow
         Bref, go sur SeismicShock.cs mtn!

         */
            LayerMask enemy = LayerMask.GetMask("Enemy");
            Collider[] hitColliders = Physics.OverlapSphere(PlayerController.Instance.transform.position, AoeRadius, enemy);
            foreach (Collider hitCollider in hitColliders)
            {
                EnemyStats enemyStats = hitCollider.GetComponent<EnemyStats>();
                enemyStats.TakeDamage((int)Damages);
            }
            Instantiate(SpellParticles, PlayerController.Instance.transform.position, Quaternion.identity);
        }

        base.ActivatedSkill();
    }

}
