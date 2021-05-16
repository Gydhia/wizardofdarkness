using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        if (PlayerStats.Instance.elements.Count != 0 && PlayerStats.Instance.actualEElement != EElements.None)
        {
            if (Input.GetButtonDown("LeftClickSpell"))
            {
                if (PlayerStats.Instance.actualSkills[0].canLaunch)
                {
                    PlayerStats.Instance.actualSkills[0].ActivatedSkill();
                }
            }
            if (Input.GetButtonDown("RightClickSpell"))
            {
                if (PlayerStats.Instance.actualSkills[1].canLaunch)
                {
                    PlayerStats.Instance.actualSkills[1].ActivatedSkill();
                }
            }
            if (Input.GetButtonDown("FirstKeyboardSpell"))
            {
                if (PlayerStats.Instance.actualSkills[2].canLaunch)
                {
                    PlayerStats.Instance.actualSkills[2].ActivatedSkill();
                }
            }
            if (Input.GetButtonDown("SecondKeyboardSpell"))
            {
                if (PlayerStats.Instance.actualSkills[3].canLaunch)
                {
                    PlayerStats.Instance.actualSkills[3].ActivatedSkill();
                }
            }
            if (Input.GetButtonDown("ThirdKeyboardSpell"))
            {
                if (PlayerStats.Instance.actualSkills[4].canLaunch)
                {
                    PlayerStats.Instance.actualSkills[4].ActivatedSkill();
                }
            }
        }
    }
}
