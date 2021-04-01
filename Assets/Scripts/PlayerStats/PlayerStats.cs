using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public enum EElements { Void=0, Wind=1, Earth=2}
    public EElements actualEElement;
    public int HP;
    public int def;
    public int str;
    public float atqSpeed;
    public float moveSpeed;
    public Element[] elements;
    public int actualElement;

    private void Update()
    {
        if (Input.GetButtonDown("LeftMouseSpell"))
        {
            elements[actualElement].elementStats.skills[0].ActivatedSkill();
        }
        if (Input.GetButtonDown("RightMouseSpell"))
        {
            elements[actualElement].elementStats.skills[1].ActivatedSkill();

        }
        if (Input.GetButtonDown("FirstKeyboardSpell"))
        {
            elements[actualElement].elementStats.skills[2].ActivatedSkill();

        }
        if (Input.GetButtonDown("SecondKeyboardSpell"))
        {
            elements[actualElement].elementStats.skills[3].ActivatedSkill();

        }
        if (Input.GetButtonDown("ThirdKeyboardSpell"))
        {
            elements[actualElement].elementStats.skills[4].ActivatedSkill();

        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeElement(EElements.Void);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeElement(EElements.Wind);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeElement(EElements.Earth);
        }
    }
    public void ChangeElement(EElements newElement)
    {
        if (actualEElement != newElement)
        {
            actualEElement = newElement;
            actualElement = (int)actualEElement;
        }
    }


}
