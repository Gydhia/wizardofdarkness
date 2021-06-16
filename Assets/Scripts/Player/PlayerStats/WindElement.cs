using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindElement : Element
{
    [Header("Wind Variables")]
    //public List<ArrowScript> ActiveArrows = new List<ArrowScript>();
    public Transform ArrowSpawn;
    public float WindArrowStaminaConsumption;
    public bool NextArrowWeakens;
    public float BendingSpeed;
    public float weakenDuration;
    public int weakenPercent;
    public override void ChangementFX(ParticleSystem part)
    {
        part.Play();
    }

}
