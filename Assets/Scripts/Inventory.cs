using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.GoldFlower, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.RedPotion, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.BlueFruit, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.BlueFruit, amount = 1 });
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList() {
        return itemList;
    }
}
