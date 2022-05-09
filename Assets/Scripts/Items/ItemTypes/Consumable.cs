using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : ItemData
{
    public int RegenPerSecond;
    public int EffectDuration;
    public Consumable(ItemData itemData) : base(itemData.Id, itemData.DisplayName, itemData.Icon, itemData.MaxQuantity, itemData.ItemType, itemData.ItemClass, itemData.Stats)
    {
        RegenPerSecond = Stats.RegenPerSecond;
        EffectDuration = Stats.EffectDuration;
    }
}
