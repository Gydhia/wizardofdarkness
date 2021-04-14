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
        MagicBallScript ball = Instantiate(PlayerStats.Instance.ballPrefab,PlayerStats.Instance.ballSpawnSpot).GetComponent<MagicBallScript>();
        //ball.transform.SetParent(ballSpawnSpot);
        ball.ballMoveSpeed = ballMoveSpeed;
        ball.ballGrowSpeed = ballGrowSpeed;
        ball.maxScale = maxScale;
    }

    void Update()
    {
        
    }
}
