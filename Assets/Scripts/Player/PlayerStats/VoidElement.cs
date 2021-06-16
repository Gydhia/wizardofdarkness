using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidElement : Element
{
    [Header("Void Variables")]
    public float MagicBallStaminaConsumption;
    public Transform BallSpawnSpot;
    public GameObject BallPrefab;
    public GameObject BlackHolePrefab;
    public GameObject TeleportPointPrefab;
    [HideInInspector] public GameObject ActualTPPoint;
    public GameObject projBarrier;
    public GameObject AimPoint;
    public float projBarrierStaminaConsumption;
    public override void ChangementFX(ParticleSystem part)
    {
        part.Play();
    }
}
