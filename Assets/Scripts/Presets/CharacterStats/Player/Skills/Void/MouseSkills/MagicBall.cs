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
        if (PlayerMovement.Instance.stamina >= 20)
        {
            MagicBallScript ball = Instantiate(PlayerStats.Instance.ballPrefab, PlayerStats.Instance.ballSpawnSpot).GetComponent<MagicBallScript>();
            //ball.transform.SetParent(ballSpawnSpot);
            ball.ballMoveSpeed = ballMoveSpeed;
            ball.ballGrowSpeed = ballGrowSpeed;
            ball.maxScale = maxScale;
        }
        else
        {
            //Feedback qui dit que la jauge de stamina est pas assez remplie
        }

    }

}
