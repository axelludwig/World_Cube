using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData
{
    public int Id;
    public string DisplayName;
    public Sprite Icon;
    //public GameObject prefab;
    public int MaxQuantity;
    public ItemType ItemType;
    public string ItemClass;
    public Attribute Stats;

    public ItemData(int id, string displayName, Sprite icon, int maxQuantity, ItemType itemType, string itemClass, Attribute stats)
    {
        Id = id;
        DisplayName = displayName;
        Icon = icon;
        MaxQuantity = maxQuantity;
        ItemType = itemType;
        ItemClass = itemClass;
        Stats = stats;
    }
}

// utilisé pour la conversion du json --> tableau Items dans le json
[Serializable]
public class Root
{
    public ItemData[] Items;
}

// structure des attributs : tous les types possibles
[Serializable]
public class Attribute
{
    public int Damage;
    public int AttackSpeed;

    public int Armor;
    public int Thorns;

    public int RegenPerSecond;
    public int EffectDuration;
}


public enum ItemType
{
    ChestPlate,
    Slibard,
    Helmet,
    Potion,
    Artifact,
    Sword,
    GreatSword,
    MagicStaff
}
