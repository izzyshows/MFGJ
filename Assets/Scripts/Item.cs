using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] 
public class Item
{
    public enum ItemType {
        GoldFlower,
        BlueFruit,
        RedPotion,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite() {
        switch (itemType) {
            default:
            case ItemType.BlueFruit:    return ItemAssets.Instance.BlueFruit;
            case ItemType.GoldFlower:   return ItemAssets.Instance.GoldFlower;
            case ItemType.RedPotion:   return ItemAssets.Instance.RedPotion;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.BlueFruit:
            case ItemType.GoldFlower:
            case ItemType.RedPotion:
                return true;
        }
    }
    
}
