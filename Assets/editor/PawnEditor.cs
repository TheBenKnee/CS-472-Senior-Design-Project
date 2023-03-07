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

        if (pawn != null && pawn.queueAnswerPriority != null)
        {
            EditorGUILayout.LabelField("Queue Answer Priority:");

            for (int i = 0; i < pawn.queueAnswerPriority.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("List " + i + ":");
                for (int j = 0; j < pawn.queueAnswerPriority[i].Count; j++)
                {
                    pawn.queueAnswerPriority[i][j] = (LaborType)EditorGUILayout.EnumPopup(pawn.queueAnswerPriority[i][j]);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
