using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStorage : MonoBehaviour
{
    public List<Chest> inventories = new List<Chest>();
    public Dictionary<Item, List<Chest>> itemReferences = new Dictionary<Item, List<Chest>>();

    public void AddItem(Item item, Chest chest)
    {
        if(itemReferences.ContainsKey(item))
        {
            if(!itemReferences[item].Contains(chest))
            {
                itemReferences[item].Add(chest);
            }
        }
        else
        {
            itemReferences.Add(item, new List<Chest>{ chest });
        }
    }

    public List<Chest> GetChestWithItem(Item item)
    {
        if(itemReferences.ContainsKey(item))
        {
            return itemReferences[item];
        }
        return new List<Chest>();
    }

    public Chest GetClosedChestWithItem(Item item)
    {
        List<Chest> chestList = GetChestWithItem(item);

        //Find closest
        Chest closest = new Chest(this);

        return closest;
    }
}