using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElement : Element
{
    [Header("Earth Variables")]
    public GameObject FXPrefab;
    public bool blocking;
    public Animator hammer;

    public override void ChangementFX(ParticleSystem part)
    {
        part.Play();
    }
}
