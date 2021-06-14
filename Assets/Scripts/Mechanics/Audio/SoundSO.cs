using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ED.Controllers;

[ExecuteAlways]
[CreateAssetMenu(fileName = "New Music", menuName = "FeuFollet/GameSound")]
public class SoundSO : ScriptableObject
{
    public SoundNames SoundID;
    public AudioClip Sound;
}

