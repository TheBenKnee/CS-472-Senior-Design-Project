using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colony : MonoBehaviour
{
    public static List<Zone> zones = new List<Zone>();
    [SerializeField] private SpriteRenderer zoneSprite;
    [SerializeField] private string colonyName = "Test Colony";

    public string GetColonyName()
    {
        return colonyName;
    }

    /////////////////////////////////////
    // Global Storage Methods
    /////////////////////////////////////

    public bool ColonyHasItem(string itemName, int quantity)
    {
        return (GlobalStorage.GetItemCount(itemName) > quantity);
    }

    public int GetNumberOfItemInGalaxy(string itemName)
    {
        return GlobalStorage.GetItemCount(itemName);
    }

    public static void GenerateLaborOrdersFromZones()
    {
        foreach(Zone zone in zones){
            if((int)zone.GetZoneType() == 1)
            {
                for (int i = (int)zone.bottomLeft.x; i < (int)zone.topRight.x; i++)
                {
                    for (int j = (int)zone.bottomLeft.y; j < (int)zone.topRight.y; j++)
                    {
                        // if there is no resource at this tile
                        if (((BaseTile)(GridManager.tileMap.GetTile(new Vector3Int(i, j, 0)))).resource == null)
                        {
                            Item itemToPlace = Resources.Load<GameObject>("prefabs/items/Wheat").GetComponent<Wheat>();
                            LaborOrderManager.AddPlaceLaborOrder(itemToPlace, new Vector2(i, j));
                        }
                    }
                }
            }
        }
    }

    public bool RemoveItemFromColony(string itemName, int quantity)
    {
        if(GlobalStorage.GetItemCount(itemName) < quantity)
        {
            return false;
        }

        List<Chest> chestWithItem = GlobalStorage.GetChestsWithItem(itemName);
        foreach(Chest chest in chestWithItem)
        {
            if(quantity <= 0)
            {
                break;
            }

            int itemsDeleted = chest.GetItemQuantity(itemName);
            if(itemsDeleted < quantity)
            {
                chest.RemoveNumberOfItems(itemName, itemsDeleted);
                quantity -= itemsDeleted;
            }
            else
            {
                chest.RemoveAllItems(itemName);
                quantity = 0;
            }
        }

        return true;
    }

    public void AddItemToColony(string itemName, int itemQuantity)
    {
        if(itemName == "Pawn")
        {
            for(int i = 0; i < itemQuantity; i++)
            {
                LaborOrderManager.CreateNewPawn();
            }
        }

        // Chest lootLocation = GlobalStorage.GetClosestChest(new Vector3());
        // if(lootLocation == null)
        // {
        //     return;
        // }

        // lootLocation.AddItem(item);
    }

    /////////////////////////////////////
    // Zone Methods 
    /////////////////////////////////////

    public void AddZone(Zone newZone)
    {
        zones.Add(newZone);
    }

    public int GetNextZoneNumber()
    {
        return zones.Count + 1;
    }

    public Sprite GetZoneSprite()
    {
        return zoneSprite.sprite;
    }

    public void RemoveZone(Zone zoneToRemove)
    {
        Destroy(zoneToRemove.GetVisualBox());
        if (zones.Contains(zoneToRemove))
        {
            zones.Remove(zoneToRemove);
        }
    }

    public List<Zone> GetZones()
    {
        return zones;
    }
}



