using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSlownessTotem : Skill
{
    public override void ActivatedSkill()
    {
        if (PlayerStats.Instance.actualSlowTot == null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            LayerMask mask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(ray, out hit, mask))
            {
                GameObject tot = Instantiate(PlayerStats.Instance.slowTotemPrefab, hit.point, Quaternion.identity);
                PlayerStats.Instance.actualSlowTot = tot.GetComponent<SlowTotScript>();
            }
        }
    }

}
