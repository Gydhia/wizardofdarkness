using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidElement : Element
{
    public override void ChangementFX(ParticleSystem part)
    {
        part.Play();
    }
}
