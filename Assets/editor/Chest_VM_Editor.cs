using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Chest_VM))]
public class Chest_VM_Editor : Editor
{
    SerializedProperty contents;
    SerializedProperty location;

    private void OnEnable()
    {
        contents = serializedObject.FindProperty("contents");
        location = serializedObject.FindProperty("location");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Chest_VM chest = (Chest_VM)target;

        EditorGUILayout.PropertyField(location, new GUIContent("Location"));

        EditorGUILayout.LabelField("Chest Contents", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical(GUI.skin.box);

        if (chest.contents.Count == 0)
        {
            EditorGUILayout.LabelField("Chest is empty");
        }
        else
        {
            foreach (var item in chest.contents)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(item.Key, typeof(GameObject), true);
                EditorGUILayout.IntField(item.Value);
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
