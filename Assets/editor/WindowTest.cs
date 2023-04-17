// LaborOrderManagerWindow.cs
using UnityEditor;
using UnityEngine;

public class LaborOrderManagerWindow : EditorWindow
{
    [MenuItem("Window/Grid Manager")]
    public static void ShowWindow()
    {
        GetWindow<LaborOrderManagerWindow>("Grid Manager");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Populate Object Labor Orders"))
        {
            LaborOrderManager_VM.PopulateObjectLaborOrders();
        }

        if (GUILayout.Button("Populate with Trees"))
        {
            GridManager.PopulateWithTrees();
        }

        if (GUILayout.Button("Populate with Tree"))
        {
            GridManager.PopulateWithTree();
        }

        if (GUILayout.Button("Populate with Bushes"))
        {
            GridManager.PopulateWithBushes();
        }
    }
}
