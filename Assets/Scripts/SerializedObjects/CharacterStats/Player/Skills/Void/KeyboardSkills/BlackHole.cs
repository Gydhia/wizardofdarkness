using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : Skill
{
    public override void ActivatedSkill()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        LayerMask mask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(ray, out hit, mask))
        {
            GameObject BH = Instantiate(PlayerStats.Instance.blackHolePrefab, hit.point, Quaternion.identity);
            Destroy(BH, 7f);
        }
    }
}
