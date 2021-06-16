using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatusData", menuName = "StatusObjects/CharacterData", order = 1)]
public class CharacterStatus : ScriptableObject
{
    //public string charName = "name";
    public int baseDef = 0;
    public int baseStr = 0;
    public float attackSpeed = 1;
    public float moveSpeed = 1;
    public List<Skill> skills = new List<Skill>();
    //0: clic gauche
    //1: clic droit
    //2:a
    //3:e
    //4:r
    //Au dessus: passifs;           Update: peut-être pas? jsp
}