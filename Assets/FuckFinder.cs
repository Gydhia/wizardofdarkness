using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckFinder : MonoBehaviour
{
    public SkillCooldown[] Found;

    public void Start()
    {
        Found = FindObjectsOfType<SkillCooldown>();
    }
}
