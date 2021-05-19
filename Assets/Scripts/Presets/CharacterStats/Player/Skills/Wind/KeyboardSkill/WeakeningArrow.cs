using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakeningArrow : Skill
{
    public override void ActivatedSkill()
    {
        /*Tu connais ça non?
         Ce truc juste en dessous, c'est qu'unity nous dise "HE HE OH CALME AU SECOURS BROW T'as oublié de créer une fonction à cet endroit précis! va la faire!!!"; c'est tout
         "Flèche affaiblissante : pendant 10s, +20% dégâts sur l'ennemi"
         Assez explicite, maintenant on peux pas le faire tout de suite, parce qu'on a même pas d'ennemis mdr
         Surtout, comment on fait? faut bander une flèche comme une autre.
          Bon maintenant bro c'est BackwardsDash!*/
        ((WindElement)PlayerController.Instance.PlayerStats.ActualElement).NextArrowWeakens = true;
        CanLaunch = false;
    }

}
