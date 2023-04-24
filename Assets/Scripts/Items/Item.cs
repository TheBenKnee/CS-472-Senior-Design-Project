using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // list reference to all items
    public static List<GameObject> items = new List<GameObject>();

    public string itemName;
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
        isPlaceable = true;
    }

    public void Unitemize()
    {
        isItemized = false;
        isGatherable = false;
        isPlaceable = false;
    }

    public void Deconstruct()
    {
        Itemize();
    }

    void Awake()
    {
        foreach (GameObject item in Resources.LoadAll("prefabs/items", typeof(GameObject)))
        {
            items.Add(item.GetComponent<GameObject>());
        }
    }
}
