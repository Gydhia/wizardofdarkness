using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAOE : Skill
{
    public float stunTime;
    [Tooltip("Enter with a '0%-100%' format.")] public int breakDefPercent;
    public float breakDefTime;

    public override void ActivatedSkill()
    {
        /*
         Sinon, ici c'est le stun, donc il me semble qu'on doit rester appuyé sur un bouton pour charger, puis stun et breakDef. 
         Ou, selon le gdd:
         "AoE de stun et reduction de défense - 10% (chargement de 1s, innarêtable durant le cast)"
         "Comment faire"? Bah en vrai je sais pas, perso je pense que soit je calculerais la distance entre moi et TOUT LES ENNEMIS,
         mais 1- c relou a faire, 2- ça fait bcp de calculs pour pas gd chose
         
        Deuxième solution, tu met, dans player stats, sous le header "Earth Variables", un prefab de boxcollider ou de collider quelconque, et tu l'instanciate sur le joueur quand
        il doit stun.
        Tu regarde avec quoi ce truc a collide, tu trie les ennemis, et pif paf pouf t'as ton stun
        a demander a killian, jsuis pas sur pour la forme
         Bref, go sur SmashGround.cs mtn!
         */
        LayerMask enemy = LayerMask.GetMask("Enemy");
        Collider[] hitColliders = Physics.OverlapSphere(PlayerStats.Instance.transform.position, AOERadius, enemy);
        foreach (Collider hitCollider in hitColliders)
        {
            EnemyStats enemyStats = hitCollider.GetComponent<EnemyStats>();
            //Les stuns
            enemyStats.StartCor(enemyStats.Debuff(stunTime, EDebuffs.Stun));
            //-10% de def
            enemyStats.StartCor(enemyStats.StatDebuff(breakDefTime, EStatsDebuffs.Defense, breakDefPercent));
            //dmg? quand même? un poquito
            enemyStats.AddDamage(dmg);
        }
        canLaunch = false;


    }

}
