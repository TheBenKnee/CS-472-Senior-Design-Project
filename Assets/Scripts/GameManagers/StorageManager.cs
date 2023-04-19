using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    // list of gameobjects
    public static List<KeyValuePair<GameObject, StorageObject>> globalItemList = new List<KeyValuePair<GameObject, StorageObject>>();

    // Method to check if a given item is present in the global list
    public static bool CheckItem(GameObject item)
    {
        for (int i = 0; i < globalItemList.Count; i++)
        {
            if (globalItemList[i].Key == item)
            {
                return true;
            }
        }
        return false;
    }
}
