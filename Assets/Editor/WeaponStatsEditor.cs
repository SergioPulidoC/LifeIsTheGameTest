using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WeaponStats))]
public class WeaponStatsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("weaponPrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletPrefab"));
        SerializedProperty typeProperty = serializedObject.FindProperty("weaponType");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletForce"));
        EditorGUILayout.PropertyField(typeProperty);
        if (typeProperty.enumValueIndex == (int)WeaponStats.Type.Gravitational)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("gravitationalForce"));
        }
        if (typeProperty.enumValueIndex == (int)WeaponStats.Type.Splinter)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("coneAngle"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("pelletCount"));
        }
        serializedObject.ApplyModifiedProperties();
    }
}