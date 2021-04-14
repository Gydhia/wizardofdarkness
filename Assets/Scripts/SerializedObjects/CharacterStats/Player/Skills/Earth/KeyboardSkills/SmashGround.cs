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
        }
    }

}
