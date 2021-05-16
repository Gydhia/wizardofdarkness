using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackwardsDash : Skill
{
    public override void ActivatedSkill()
    {
        /*
             Dash en arrière (Comme le chasseur dans WoW et Archeage, ralentit les
             ennemis juste devant)
             aaaah, les dashs... Mon né-mé-sis. Je veux faire ça bien, mais comment faire ça bien? hmmmm...$
             Go AttackSpeedBoost, maintenant!
         */
        canLaunch = false;
        throw new System.NotImplementedException();
    }
}
