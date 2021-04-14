using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeismicShock : Skill
{
    public override void ActivatedSkill()
    {
        //Ne pouvoir rien faire, pendant une seconde, cast, pas de cc. Pour l'instant, pas d'enemis -> inutile.
        Instantiate(PlayerStats.Instance.earthquakePrefab, transform.position, Quaternion.identity);
    }
}
