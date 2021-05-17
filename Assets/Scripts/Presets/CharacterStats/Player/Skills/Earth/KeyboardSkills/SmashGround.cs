using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashGround : Skill
{
    public float smashJumpHeight;
    public override void ActivatedSkill()
    {
        if (PlayerMovement.Instance.isGrounded)
        {
            PlayerMovement.Instance.velocity.y = Mathf.Sqrt(smashJumpHeight * -7f * PlayerMovement.Instance.gravity);
            /*
         Donc, ici, c'est le nouveau spell qui a remplac� le totem: on frappe le sol pour faire un aoe de d�gats.
         Pour les d�gats, je ferais pareil  que la STUNAOE, je sais pas
         et pour le saut, je l'ai essay� ici, mais jsp si �a marche mdr
         C'est ton spell tfa�on fait ce que tu veux brow
         Bref, go sur SeismicShock.cs mtn!

         */
            LayerMask enemy = LayerMask.GetMask("Enemy");
            Collider[] hitColliders = Physics.OverlapSphere(PlayerStats.Instance.transform.position, AOERadius, enemy);
            foreach (Collider hitCollider in hitColliders)
            {
                EnemyStats enemyStats = hitCollider.GetComponent<EnemyStats>();
                enemyStats.TakeDamage(dmg);
            }
            Instantiate(PlayerStats.Instance.FXPrefab, PlayerStats.Instance.transform.position, Quaternion.identity);
            canLaunch = false;
        }
    }

}
