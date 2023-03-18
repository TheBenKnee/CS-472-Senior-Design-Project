using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaircaseItem : Item
{
    PrefabList prefabList;
    EntityDictionary entityDictionary;
    public new string BlockName = "staircase";

    public override void InvokePlacing(BaseNPC placer)
    {
        prefabList = GlobalInstance.Instance.prefabList;
        entityDictionary = GlobalInstance.Instance.entityDictionary;

        GameObject staircaseOriginBlock = entityDictionary.InstantiateEntity(BlockName);
        Staircase staircaseOriginComp = staircaseOriginBlock.GetComponent<Staircase>();
        bool success = staircaseOriginComp.PlaceObject(placer.GetCurrentLocation());

        GameObject staircaseDestinationBlock = entityDictionary.InstantiateEntity(BlockName);
        Staircase staircaseDestinationComp = staircaseDestinationBlock.GetComponent<Staircase>();
        success = success && staircaseDestinationComp.PlaceObject(staircaseOriginComp.GetDestinationTile(), false);
        //Do something if fail...or succeed, I suppose

        Debug.Log("Whole process was success: " + success);
    }
}
