using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonTeleportPoint : Skill
{
    public override void ActivatedSkill()
    {
        if (PlayerStats.Instance.actualTPPoint == null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            LayerMask mask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(ray, out hit, mask))
            {
                GameObject TP = Instantiate(PlayerStats.Instance.teleportPointPrefab, hit.point, Quaternion.identity);
                PlayerStats.Instance.actualTPPoint = TP.GetComponent<TPPointScript>();
                PlayerStats.Instance.actualTPPoint.skillAccessor = this;
            }
        }
        else
        {
            PlayerStats.Instance.actualTPPoint.Teleport();
        }
    }

}
