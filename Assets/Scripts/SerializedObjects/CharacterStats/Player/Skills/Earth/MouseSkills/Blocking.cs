using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : Skill
{
    public override void ActivatedSkill()
    {
        //Faire autrement? Il suffit de mettre dans un update:
        //blocking = Input.GetKey("RightClickSpell");
    }
}
