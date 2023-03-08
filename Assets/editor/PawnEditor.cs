using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Pawn))]
public class PawnEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Pawn pawn = (Pawn)target;
        List<LaborType>[] LaborTypePriority = pawn.getLaborTypePriority();

        if (pawn != null && LaborTypePriority != null)
        {
            EditorGUILayout.LabelField("Labor Type Priority:");

            for (int i = 0; i < LaborTypePriority.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("List " + i + ":");
                for (int j = 0; j < LaborTypePriority[i].Count; j++)
                {
                    LaborTypePriority[i][j] = (LaborType)EditorGUILayout.EnumPopup(LaborTypePriority[i][j]);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
