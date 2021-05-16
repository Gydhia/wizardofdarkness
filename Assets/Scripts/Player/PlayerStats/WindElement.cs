using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindElement : Element
{
    public override void ChangementFX(ParticleSystem part)
    {
        part.Play();
    }

}
