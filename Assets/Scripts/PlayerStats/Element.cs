using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Element : MonoBehaviour
{
    public int HP;
    public float strength;
    public float attackSpeed;
    public float moveSpeed;
    public CharacterStatus element;
    public Skill[] skills;


}
