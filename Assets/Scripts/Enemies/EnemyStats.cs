using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStats : MonoBehaviour
{
    public enum EEnemyElements { Void = 0, Wind = 1, Earth = 2, None = 3 }
    public EEnemyElements ActualElement;
    public int HP;
    public int def;
    public int str;
    public float atqSpeed;
    public float moveSpeed;
    public Skill[] Skills;

    public void ActivateSkill(int index)
    {
        Skills[index].ActivatedSkill();
    }
}
