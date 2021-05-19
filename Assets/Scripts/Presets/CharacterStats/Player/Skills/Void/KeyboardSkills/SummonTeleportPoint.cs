using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonTeleportPoint : Skill
{
    public override void ActivatedSkill()
    {
        if (((VoidElement)PlayerController.Instance.PlayerStats.ActualElement).ActualTPPoint == null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            LayerMask mask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(ray, out hit, mask))
            {
                GameObject TP = Instantiate(((VoidElement)PlayerController.Instance.PlayerStats.ActualElement).TeleportPointPrefab, hit.point, Quaternion.identity);
                ((VoidElement)PlayerController.Instance.PlayerStats.ActualElement).ActualTPPoint = TP.GetComponent<TPPointScript>();
                ((VoidElement)PlayerController.Instance.PlayerStats.ActualElement).ActualTPPoint.skillAccessor = this;
            }
        }
        else
        {
            ((VoidElement)PlayerController.Instance.PlayerStats.ActualElement).ActualTPPoint.Teleport();
        }
        base.ActivatedSkill();
    }

}
