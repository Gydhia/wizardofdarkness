using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : Skill
{
    public float overheatingIncrementor;
    public override void ActivatedSkill()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        LayerMask mask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(ray, out hit, mask))
        {
            GameObject FW = Instantiate(PlayerStats.Instance.fireWallPrefab, hit.point,PlayerStats.Instance.gameObject.transform.rotation*Quaternion.Euler(new Vector3(0,90,0)));
            canLaunch = false;
            Destroy(FW, 7f);
        }
    }
}
