using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftableItemsDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    private List<Item> craftableItems;

    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        LoadCraftableItems();
        PopulateDropdown();
        dropdown.onValueChanged.AddListener(delegate { UpdateCraftItemIndex(); });
    }

    private void LoadCraftableItems()
    {
        craftableItems = new List<Item>();
        foreach (Item item in Resources.LoadAll("prefabs/items", typeof(Item)))
        {
            if (item.isCraftable)
            {
                craftableItems.Add(item.GetComponent<Item>());
            }
        }
    }

    private void PopulateDropdown()
    {
        dropdown.options.Clear();
        foreach (Item craftableItem in craftableItems)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(craftableItem.itemName));
        }
    }

    public void UpdateCraftItemIndex()
    {
        GlobalSelection.craftItemIndex = dropdown.value;
    }
}
