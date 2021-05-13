using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArrow : Skill
{
    public GameObject arrowPrefab;
    public Transform arrowSpawn;
    public float shootForce = 20f;
    public float maxBending;

    public override void ActivatedSkill()
    {
        arrowSpawn = PlayerStats.Instance.arrowSpawn;
        ArrowScript arrow = Instantiate(arrowPrefab, arrowSpawn).GetComponent<ArrowScript>();
        arrow.maxBending = maxBending;
        PlayerStats.Instance.activeArrows.Add(arrow.GetComponent<ArrowScript>());
        /*
         Bon, là c'est tout fait, on va pas se mentir. mais c'est un proto, quoi, parce qu'à priori ça va changer, mais ça fait ce que ça fait, quand même...
         Faudrait par exemple, genre... Faire le bandage de l'arc :)
        bref, maintenant brow go CallingBackArrows.cs! Oeoe

         */
    }

}
