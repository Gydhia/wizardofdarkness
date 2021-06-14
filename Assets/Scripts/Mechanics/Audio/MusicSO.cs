using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ED.Controllers;

[ExecuteAlways]
[CreateAssetMenu(fileName = "New Music", menuName = "FeuFollet/GameMusic")]
public class MusicSO : ScriptableObject
{
    public MusicNames MusicID;
    public AudioClip Music;
}
