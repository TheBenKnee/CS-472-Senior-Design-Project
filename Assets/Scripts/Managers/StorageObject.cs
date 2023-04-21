using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageObject : MonoBehaviour
{
    GameObject[] itemList = new GameObject[50];

    // Method to add an item to the storage object (and the global list in the StorageManager)
    public void AddItem(GameObject item)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == null)
            {
                itemList[i] = item;
                StorageManager.globalItemList.Add(new KeyValuePair<GameObject, StorageObject>(item, this));
                break;
            }
        }
    }
}
