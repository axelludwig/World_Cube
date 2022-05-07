using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemData data;
    public int quantity;

    public Item(ItemData data, int quantity)
    {
        this.data = data;
        this.quantity = quantity;
    }
    public Item(int itemID, int quantity)
    {
        this.data = ItemManager.getItem(itemID);
        this.quantity = quantity;
    }
}
