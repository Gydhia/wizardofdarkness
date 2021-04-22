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
         Donc, ici, c'est le nouveau spell qui a remplacé le totem: on frappe le sol pour faire un aoe de dégats.
         Pour les dégats, je ferais pareil  que la STUNAOE, je sais pas
         et pour le saut, je l'ai essayé ici, mais jsp si ça marche mdr
         C'est ton spell tfaçon fait ce que tu veux brow
         Bref, go sur SeismicShock.cs mtn!

         */
        }
    }

}
