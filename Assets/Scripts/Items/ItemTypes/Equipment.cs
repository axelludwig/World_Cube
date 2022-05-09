using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : ItemData
{
    public int Armor;
    public int Thorns;
    public Equipment(ItemData itemData) : base(itemData.Id, itemData.DisplayName, itemData.Icon, itemData.MaxQuantity, itemData.ItemType, itemData.ItemClass, itemData.Stats)
    {
        Armor = Stats.Armor;
        Thorns = Stats.Thorns;
    }
}