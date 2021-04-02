using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    void Update()
    {
        if (Input.GetButtonDown("LeftClickSpell"))
        {
            PlayerStats.Instance.actualSkills[0].ActivatedSkill();
        }
        if (Input.GetButtonDown("RightClickSpell"))
        {
            PlayerStats.Instance.actualSkills[1].ActivatedSkill();

        }
        if (Input.GetButtonDown("FirstKeyboardSpell"))
        {
            PlayerStats.Instance.actualSkills[2].ActivatedSkill();

        }
        if (Input.GetButtonDown("SecondKeyboardSpell"))
        {
            PlayerStats.Instance.actualSkills[3].ActivatedSkill();

        }
        if (Input.GetButtonDown("ThirdKeyboardSpell"))
        {
            PlayerStats.Instance.actualSkills[4].ActivatedSkill();

        }
    }
}
