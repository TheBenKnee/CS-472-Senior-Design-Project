using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_VM
{
    // dictionary of items and their quantities
    public Dictionary<GameObject, int> contents = new Dictionary<GameObject, int>();
    public BaseTile_VM location;

    // constructor
    Chest_VM()
    {
        // initialize the contents dictionary
        contents = new Dictionary<GameObject, int>();
        // initialize the location
        location = null;
    }

    // constructor
    Chest_VM(Dictionary<GameObject, int> contents, BaseTile_VM location)
    {
        // initialize the contents dictionary
        this.contents = contents;
        // initialize the location
        this.location = location;
    }

    // Method to add an item to the chest
    public void AddItem(GameObject item)
    {
        // if the item is already in the chest, increment its quantity
        if (contents.ContainsKey(item))
        {
            contents[item]++;
        }
        // otherwise, add it to the chest
        else
        {
            contents.Add(item, 1);
        }
    }

    // Method to remove an item from the chest
    public void RemoveItem(GameObject item)
    {
        // if the item is in the chest, decrement its quantity
        if (contents.ContainsKey(item))
        {
            contents[item]--;
        }
        // if the item's quantity is 0, remove it from the chest
        if (contents[item] == 0)
        {
            contents.Remove(item);
        }
    }

    // Method to get the quantity of an item in the chest
    public int GetItemQuantity(GameObject item)
    {
        // if the item is in the chest, return its quantity
        if (contents.ContainsKey(item))
        {
            return contents[item];
        }
        // otherwise, return 0
        else
        {
            return 0;
        }
    }

    // Method to check if the chest contains an item
    public bool ContainsItem(GameObject item)
    {
        // if the item is in the chest, return true
        if (contents.ContainsKey(item))
        {
            return true;
        }
        // otherwise, return false
        else
        {
            return false;
        }
    }

}
