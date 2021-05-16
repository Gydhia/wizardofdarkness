using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackwardsDash : Skill
{
    public float dashRange;
    public override void ActivatedSkill()
    {
        /*
             Dash en arrière (Comme le chasseur dans WoW et Archeage, ralentit les
             ennemis juste devant)
             aaaah, les dashs... Mon né-mé-sis. Je veux faire ça bien, mais comment faire ça bien? hmmmm...$
             Go AttackSpeedBoost, maintenant!
         */
        canLaunch = false;
        LayerMask mask = LayerMask.GetMask("Wall");
        RaycastHit hit;
        if (Physics.Raycast(PlayerMovement.Instance.transform.position, Vector3.back, out hit, dashRange, mask))
        {
            //Dash jusqu'a
            //hit.distance;
        }
        else
        {
            //dash de dashRange
        }
        throw new System.NotImplementedException();
    }
}
