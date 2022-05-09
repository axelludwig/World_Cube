using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ItemData
{
    public Weapon(ItemData itemData): base(itemData.Id, itemData.DisplayName, itemData.Icon, itemData.MaxQuantity, itemData.ItemType, itemData.ItemClass, itemData.Stats)
    {
        Damage = Stats.Damage;
        AttackSpeed = Stats.AttackSpeed;
    }


    public int Damage;
    public int AttackSpeed;

    
}
