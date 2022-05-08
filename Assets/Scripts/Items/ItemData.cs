using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData
{
    public int id;
    public string displayName;
    public Sprite icon;
    //public GameObject prefab;
    public int maxQuantity;
}

// utilisé pour la conversion du json
[Serializable]
public class Root
{
    public ItemData[] items;
}
