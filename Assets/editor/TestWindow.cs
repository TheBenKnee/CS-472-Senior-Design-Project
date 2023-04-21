// LaborOrderManagerWindow.cs
using UnityEditor;
using UnityEngine;

public class TestWindow : EditorWindow
{
    [MenuItem("Window/Test Functions")]
    public static void ShowWindow()
    {
        GetWindow<TestWindow>("Test Functions");
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

        if (GUILayout.Button("Generate Place Order (ErrorObject)"))
        {
            GameObject testObj = Resources.Load("prefabs/ErrorObject") as GameObject;
            LaborOrderManager_VM.AddPlaceLaborOrder(testObj);
        }

        if (GUILayout.Button("Clear Labor Orders"))
        {
            LaborOrderManager_VM.ClearLaborOrders();
        }

        if (GUILayout.Button("Spawn Chest"))
        {
            GridManager.PopulateWithChest();
        }

        if (GUILayout.Button("Generate Destroy Order (ErrorObject)"))
        {
            LaborOrderManager_VM.AddDestroyLaborOrder();
        }
    }
}
