using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public BaseTile_VM location;
    public bool isGatherable = false;
    public bool isPlaceable = false;
    public bool isDesconstructable = false;
    public bool isWoodcuttable = false;
    public bool isMineable = false;
    public bool isForageable = false;
    public bool isCraftable = false;
    public bool isPlantcuttable = false;

    public bool isItemized = false;

    public void Itemize()
    {
        isItemized = true;
        isGatherable = true;
    }

    public void Unitemize()
    {
        isItemized = false;
        isGatherable = false;
    }
}
