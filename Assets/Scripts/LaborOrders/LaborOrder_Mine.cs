using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaborOrder_Mine_VM : LaborOrder_Base_VM
{
    public static GameObject resource;
    private static float BASE_TTC = 3f;
    private GameObject targetRock;

    // constructor
    public LaborOrder_Mine_VM(GameObject targetRock)
    {
        laborType = LaborType.Mine;
        timeToComplete = BASE_TTC;
        if (resource == null) resource = Resources.Load<GameObject>("prefabs/items/Coin");
        this.targetRock = targetRock;
        location = Vector3Int.FloorToInt(targetRock.transform.position);
    }

    // override of the execute method to preform the labor order
    public override IEnumerator Execute(Pawn_VM pawn)
    {
        if (targetRock != null)
        {
            // cutting down tree
            yield return new WaitForSeconds(timeToComplete);

            if (targetRock != null)
            {
                // delete tree
                Vector3 rockPosition = targetRock.transform.position;
                Transform rockParent = targetRock.transform.parent;
                UnityEngine.Object.Destroy(targetRock);

                // create wood in tree's place
                BaseTile_VM tile = (BaseTile_VM)GridManager.tileMap.GetTile(Vector3Int.FloorToInt(rockPosition));
                GameObject coinObject = UnityEngine.Object.Instantiate(resource, rockPosition, Quaternion.identity);
                coinObject.transform.SetParent(rockParent);
                tile.SetTileInformation(tile.type, false, coinObject, tile.resourceCount, tile.position);
            }
        }
        yield break;
    }
}
