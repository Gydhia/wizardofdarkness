using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidElement : Element
{
    [Header("Void Variables")]
    public float magicBallStaminaConsumption;
    public Transform ballSpawnSpot;
    public GameObject ballPrefab;
    public GameObject blackHolePrefab;
    public GameObject teleportPointPrefab;
    [HideInInspector] public TPPointScript actualTPPoint;
    public GameObject projBarrier;
    public GameObject AimPoint;
    public float projBarrierStaminaConsumption;
    public override void ChangementFX(ParticleSystem part)
    {
        part.Play();
    }
}
