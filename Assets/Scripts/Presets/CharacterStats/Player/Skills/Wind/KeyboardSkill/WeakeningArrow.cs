using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakeningArrow : Skill
{
    public override void ActivatedSkill()
    {
        /*Tu connais �a non?
         Ce truc juste en dessous, c'est qu'unity nous dise "HE HE OH CALME AU SECOURS BROW T'as oubli� de cr�er une fonction � cet endroit pr�cis! va la faire!!!"; c'est tout
         "Fl�che affaiblissante : pendant 10s, +20% d�g�ts sur l'ennemi"
         Assez explicite, maintenant on peux pas le faire tout de suite, parce qu'on a m�me pas d'ennemis mdr
         Surtout, comment on fait? faut bander une fl�che comme une autre.
          Bon maintenant bro c'est BackwardsDash!*/
        ((WindElement)PlayerController.Instance.PlayerStats.ActualElement).NextArrowWeakens = true;
        CanLaunch = false;
    }

}
