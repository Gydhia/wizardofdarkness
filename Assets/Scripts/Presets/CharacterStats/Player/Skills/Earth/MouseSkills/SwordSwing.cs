using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : Skill
{
    public float range;
    public float timing;
    public override void ActivatedSkill()
    {
        /*
         Ce truc juste en dessous, c'est qu'unity nous dise "HE HE OH CALME AU SECOURS BROW T'as oubli� de cr�er une fonction � cet endroit pr�cis! va la faire!!!"; c'est tout
         Ici, c'est le clic gauche, du coup
         simplement le coup d'�p�e
         donc en vrai ici y'aura peut-�tre juste un trigger d'animation.
         Bref, go sur Blocking.cs mtn!
         */
        PlayerCoroutines.Instance.LaunchRoutine(PlayerCoroutines.Instance.SwordSwing(timing, this));
        base.ActivatedSkill();
    }
}
