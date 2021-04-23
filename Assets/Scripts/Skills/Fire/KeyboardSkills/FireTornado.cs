using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTornado : Skill
{
    public float overheatingIncrementor;
    public override void ActivatedSkill()
    {
        GameObject tornado = Instantiate(PlayerStats.Instance.fireTornadoPrefab, PlayerMovement.Instance.transform.position, Quaternion.identity);
        Destroy(tornado, 2f);
        PlayerMovement.Instance.Jump(PlayerMovement.Instance.jumpHeight * 3.5f);
        canLaunch = false;
    }
}
