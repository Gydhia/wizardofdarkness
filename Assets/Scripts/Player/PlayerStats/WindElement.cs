using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindElement : Element
{
    [Header("Wind Variables")]
    public List<ArrowScript> activeArrows = new List<ArrowScript>();
    public Transform arrowSpawn;
    public float windArrowStaminaConsumption;
    public bool nextArrowWeakens;
    public float bendingSpeed;
    public float weakenDuration;
    public int weakenPercent;
    public override void ChangementFX(ParticleSystem part)
    {
        part.Play();
    }

}
