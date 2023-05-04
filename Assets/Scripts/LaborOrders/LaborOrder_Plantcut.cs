using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaborOrder_Plantcut : LaborOrder_Base
{
    public static Item resource;
    private static float BASE_TTC = 3f;
    private Item targetPlant;

    // constructor
    public LaborOrder_Plantcut(Item targetPlant)
    {
        laborType = LaborType.Plantcut;
        timeToComplete = BASE_TTC;
        if (resource == null) resource = Resources.Load<Item>("prefabs/items/WheatItem");
        this.targetPlant = targetPlant;
        location = Vector3Int.FloorToInt(targetPlant.transform.position);
    }

    // override of the execute method to preform the labor order
    public override IEnumerator Execute(Pawn pawn)
    {
        pawn.path.Clear();

        if (targetPlant != null)
        {
            // cutting down tree
            yield return new WaitForSeconds(timeToComplete);

            if (targetPlant != null)
            {
                // delete tree
                Vector3 treePosition = targetPlant.transform.position;
                Transform treeParent = targetPlant.transform.parent;
                UnityEngine.Object.Destroy(targetPlant.gameObject);

                // spawn seeds in an adjacent tile that is not collision and does not have a resource
                GlobalStorage.AddItemToChest(resource);
                Debug.Log("Added item to storage because there was no adjacent tile to spawn the item.");
            }
        }
        yield break;
    }

}




