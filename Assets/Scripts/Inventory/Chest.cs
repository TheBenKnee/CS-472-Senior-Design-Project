using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : PlacedObject
{
    public List<Item> contents = new List<Item>();

    public GlobalStorage globalStorage;

    public Chest(GlobalStorage gs)
    {
        globalStorage = gs;
        globalStorage.inventories.Add(this);
    }

    public bool DoesContainItem(Item item)
    {
        return contents.Contains(item);
    }

    public void ItemAddedToChest(Item item)
    {
        globalStorage.AddItem(item, this);
    }
}