using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [SerializeField] private static ActionUIManager myActionManager;

    // Check for user input
    public static void CheckForInput()
    {
        // Update camera
        CameraManager.UpdateCamera();

        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = GridManager.tileMap.WorldToCell(mousePosition);
            BaseTile tile = (BaseTile)GridManager.tileMap.GetTile(gridPosition);

            LaborOrderManager.PopulateObjectLaborOrderTile(tile);

            if (tile != null)
            {
                Debug.Log("MOUSE CLICK " + mousePosition + " FOUND: " + tile.ToString());
            }
            else
            {
                Debug.Log("No tile found at position: " + mousePosition);
            }

            Debug.Log(tile.DetermineSprite(new WaterTile()));
        }

        if (Input.GetKey("delete"))
        {
            UIManager.SelectUIMode(1);
            if ((int)UIManager.myMode >= 7)
            {
                myActionManager?.DeSelectAll();
            }
        }

        if (Input.GetKey("z"))
        {
            TestUIScript.AddResourcesToWorld();
        }

        if(Input.GetKey("l"))
        {
            GridManager.PrintSits();
            // var foundTextMeshObjects = FindObjectsOfType(typeof(SandTile));
            // Debug.Log(foundTextMeshObjects.Length);
            // foreach(SandTile myTile in foundTextMeshObjects)
            // {
            //     myTile.AdjustSprite();
            //     GridManager.tileMap.RefreshAllTiles();
            // }
        }
    }

    private void Awake()
    {
        TestUIScript.AddResourcesToWorld();
    }
}




