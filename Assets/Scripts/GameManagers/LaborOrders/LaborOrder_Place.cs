using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaborOrder_Place : LaborOrder_Base_VM
{
    GameObject itemToPlace;

    // constructor
    public LaborOrder_Place(GameObject item) : base()
    {
        laborType = LaborType.Place;
        timeToComplete = 3f;
        orderNumber = LaborOrderManager.getNumOfLaborOrders();
        itemToPlace = item;

        // Get a random level
        int randomLevelIndex = UnityEngine.Random.Range(0, GridManager.mapLevels.Count);
        Level level = GridManager.mapLevels[randomLevelIndex];
        // Get a random x and y
        int randomX = UnityEngine.Random.Range(level.getXMin(), level.getXMax());
        int randomY = UnityEngine.Random.Range(level.getYMin(), level.getYMax());

        // Set labor order location
        location = new Vector3Int(randomX, randomY, 0);
    }

    public override IEnumerator Execute(Pawn_VM pawn)
    {
        // wait the time to complete
        yield return new WaitForSeconds(timeToComplete);
        // instantiate ErrorObject item from resources/prefabs as a child of the GameManager -> Objects using resources load at the location of the labor order (and )
        Transform parentTransform = GameObject.Find("Objects").transform;
        GameObject item = UnityEngine.Object.Instantiate(itemToPlace, GridManager.grid.GetCellCenterWorld(location), Quaternion.identity, parentTransform);
        // set the resource of the tile to the item and set the count to 1
        BaseTile_VM tile = (BaseTile_VM)GridManager.tileMap.GetTile(location);
        tile.SetTileInformation(tile.type, false, item, 1, location);
        yield return null;
    }
}