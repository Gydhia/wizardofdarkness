using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : Skill
{


    public float ballMoveSpeed;
    public float ballGrowSpeed;
    public Vector3 maxScale;
    public override void ActivatedSkill()
    {
        if (PlayerController.Instance.PlayerMovement.stamina >= 20)
        {
            MagicBallScript ball = Instantiate(
                ((VoidElement)PlayerController.Instance.PlayerStats.ActualElement).BallPrefab,
                ((VoidElement)PlayerController.Instance.PlayerStats.ActualElement).BallSpawnSpot).GetComponent<MagicBallScript>(
            );
            //ball.transform.SetParent(ballSpawnSpot);
            ball.ballMoveSpeed = ballMoveSpeed;
            ball.ballGrowSpeed = ballGrowSpeed;
            ball.maxScale = maxScale;
            //PlayerAnimationState.Instance.playerAnimator.SetBool("LongCast", true);
        }
        else
        {
            //Feedback qui dit que la jauge de stamina est pas assez remplie
        }

    }

}
