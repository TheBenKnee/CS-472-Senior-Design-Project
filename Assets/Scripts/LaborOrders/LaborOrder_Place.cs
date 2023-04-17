using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaborOrder_Place : LaborOrder
{
    PlacedObject itemToPlace;

    // constructor
    public LaborOrder_Place(GameObject targetTree) : base()
    {
        laborType = LaborType.Place;
        timeToComplete = 3f;
        orderNumber = LaborOrderManager.getNumOfLaborOrders();

        //woodPrefab = GlobalInstance.Instance.prefabList.prefabDictionary["WoodenWall"].prefab;
    }

    public override IEnumerator execute()
    {
        yield return null;
    }
}