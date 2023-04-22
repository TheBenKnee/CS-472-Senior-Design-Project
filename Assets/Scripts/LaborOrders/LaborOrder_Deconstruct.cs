using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class LaborOrder_Deconstruct : LaborOrder_Base_VM
{

    // constructor
    public LaborOrder_Deconstruct() : base()
    {
        laborType = LaborType.Deconstruct;
        timeToComplete = 3f;
        orderNumber = LaborOrderManager_VM.GetNumOfLaborOrders();

        // get tile with resource
        BaseTile_VM tile;
        do
        {
            // Get a random level
            int randomLevelIndex = UnityEngine.Random.Range(0, GridManager.mapLevels.Count);
            Level level = GridManager.mapLevels[randomLevelIndex];
            // Get a random x and y
            int randomX = UnityEngine.Random.Range(level.getXMin(), level.getXMax());
            int randomY = UnityEngine.Random.Range(level.getYMin(), level.getYMax());

            // Set labor order location
            location = new Vector3Int(randomX, randomY, 0);
            tile = GridManager.GetTile(location);
        }
        while(tile.resource == null);
    }

    public override IEnumerator Execute(Pawn_VM pawn)
    {
        yield return new WaitForSeconds(timeToComplete);
        Transform parentTransform = GameObject.Find("Objects").transform;
        // Remove the resources from the tile at the location of the labor order
        BaseTile_VM tile = GridManager.GetTile(location);

        if (tile != null)
        {
            if (tile.resource != null)
            {
                GameObject resource = tile.resource;
                tile.SetTileInformation(tile.type, false, null, 0, tile.position);
                // Remove the game object at that location 
                UnityEngine.Object.Destroy(resource);
            }
            else
            {
                Debug.LogWarning("Tile does not contain an item. Cannot destroy an item on this tile.");
            }
        }
        else
        {
            Debug.LogWarning("No tile found at the specified location.");
        }

        yield return null;
    }

}