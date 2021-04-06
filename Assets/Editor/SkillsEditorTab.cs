using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Skill)), CanEditMultipleObjects]
public class SkillInEditorScript : Editor
{
    public SerializedProperty
        dmgPercent,
        skillCD,
        AOERadius,
        castTime,

        state,

        speedBoost,

        defBoost;
    private void OnEnable()
    {
        dmgPercent = serializedObject.FindProperty("dmgPercent");
        skillCD = serializedObject.FindProperty("skillCD");
        AOERadius = serializedObject.FindProperty("AOERadius");
        castTime = serializedObject.FindProperty("castTime");
        state = serializedObject.FindProperty("state");
        speedBoost = serializedObject.FindProperty("speedBoost");
        defBoost = serializedObject.FindProperty("defBoost");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(state);//Ici erreur


        Skill.ESkillType ty = (Skill.ESkillType)state.intValue;

        switch (ty)
        {
            case Skill.ESkillType.Damage:
                EditorGUILayout.PropertyField(dmgPercent);
                EditorGUILayout.PropertyField(skillCD);
                EditorGUILayout.PropertyField(AOERadius);
                EditorGUILayout.PropertyField(castTime);
                break;
            case Skill.ESkillType.Defense:
                EditorGUILayout.PropertyField(defBoost);
                break;
            case Skill.ESkillType.Movement:
                EditorGUILayout.PropertyField(speedBoost);
                break;
        }
        serializedObject.ApplyModifiedProperties();
        //base.OnInspectorGUI();
    }
}
