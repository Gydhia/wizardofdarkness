using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : Skill
{
    public override void ActivatedSkill()
    {
        //Faire autrement? Il suffit de mettre dans un update:
        /*
         Ce que le thomas du passé voulais dire, c'est que si jamais tu met ça dans n'importe quelle update, ça marche 
         tout ça parce que PlayerStats est un singleton hehe
         Mais, on veux pas supprimer ce script, parce que tu voudras surement mettre un trigger d'animation aussi ou un truc du genre, je suppose
         Bref, go sur StunAOE.cs mtn!

         */


        /*if(PlayerStats.Instance.actualElement == 2)       (si la classe actuelle est la classe de terre)
              blocking = Input.GetKey("RightClickSpell"); (Assez explicite?)
         */
    }
}
