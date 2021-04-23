using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBomb : Skill
{
    public float overheatingIncrementor;
    public override void ActivatedSkill()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        LayerMask mask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(ray, out hit, mask))
        {
            GameObject mine = Instantiate(PlayerStats.Instance.fireMinePrefab, hit.point,Quaternion.identity);
            this.canLaunch = false;

        }

    }
}
