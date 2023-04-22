using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStorage_VM
{
    // dictionary of chests and their location
    public Dictionary<Chest_VM, Vector3> chests = new Dictionary<Chest_VM, Vector3>();
    // dictionary of items and the chests they are in
    public Dictionary<GameObject, Chest_VM> itemLocations = new Dictionary<GameObject, Chest_VM>();

    // constructor
    GlobalStorage_VM()
    {
        // initialize the chests dictionary
        chests = new Dictionary<Chest_VM, Vector3>();
        // initialize the itemLocations dictionary
        itemLocations = new Dictionary<GameObject, Chest_VM>();
    }

    // constructor
    GlobalStorage_VM(Dictionary<Chest_VM, Vector3> chests, Dictionary<GameObject, Chest_VM> itemLocations)
    {
        // initialize the chests dictionary
        this.chests = chests;
        // initialize the itemLocations dictionary
        this.itemLocations = itemLocations;
    }

    // Method to add a chest to the global storage
    public void AddChest(Chest_VM chest, Vector3 location)
    {
        // if the chest is already in the global storage, update its location
        if (chests.ContainsKey(chest))
        {
            chests[chest] = location;
        }
        // otherwise, add it to the global storage
        else
        {
            chests.Add(chest, location);
        }
    }

    // Method to remove a chest from the global storage
    public void RemoveChest(Chest_VM chest)
    {
        // if the chest is in the global storage, remove it
        if (chests.ContainsKey(chest))
        {
            chests.Remove(chest);
        }
    }

    // Method to add an item to the global storage
    public void AddItem(GameObject item, Chest_VM chest)
    {
        // if the item is already in the global storage, update its chest
        if (itemLocations.ContainsKey(item))
        {
            itemLocations[item] = chest;
        }
        // otherwise, add it to the global storage
        else
        {
            itemLocations.Add(item, chest);
        }
    }

}
