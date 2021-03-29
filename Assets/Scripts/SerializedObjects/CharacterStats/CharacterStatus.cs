using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatusData", menuName = "StatusObjects/CharacterData", order = 1)]
public class CharacterStatus : ScriptableObject
{
    public string charName = "name";
    public int baseHp = 0;
    public int baseStr = 0;
    public int baseDef = 0;
    public float moveSpeed = 1;
    public float attackSpeed = 1;
}