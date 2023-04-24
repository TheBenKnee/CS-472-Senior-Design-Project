using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaborOrder_Gather : LaborOrder_Base_VM
{
    // constructor
    public LaborOrder_Gather()
    {
        laborType = LaborType.Gather;
        timeToComplete = 1f;
    }

    public override IEnumerator Execute(Pawn_VM pawn)
    {
        // remove the resource from the tile
        BaseTile_VM tile = GridManager.GetTile(location);
        GameObject resource = tile.resource = null;
        Chest_VM chest = GlobalStorage_VM.GetClosestChest(pawn.transform.position);
        pawn.path = PathfindingManager.GetPath(Vector3Int.FloorToInt(pawn.transform.position), Vector3Int.FloorToInt(chest.transform.position), pawn.GetTileLevel(), true);
        // add the resource to the inventory of the chest
        chest.AddItem(resource);
        yield return null;
    }
}