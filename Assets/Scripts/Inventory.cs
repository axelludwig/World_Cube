using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    private List<Item> itemsList;

    public Inventory()
    {
        itemsList = new List<Item>();
    }

    public void AddItem(int id, int number)
    {
        var itemData = ItemManager.getItem(id);
        this.AddItem(itemData, number);
    }

    public void AddItem(ItemData itemData, int number)
    {
        if (itemData != null)
        {
            var inventoryItem = itemsList.FirstOrDefault(item => item.data == itemData);
            if (inventoryItem != null) inventoryItem.quantity += number;
            else
            {
                itemsList.Add(new Item(itemData, number));
            }
        }
    }

    public void RemoveItem(int id, int number)
    {
        var itemData = ItemManager.getItem(id);
        if (itemData != null)
        {
            var inventoryItem = itemsList.FirstOrDefault(item => item.data.Id == id);
            if (inventoryItem != null)
            {
                inventoryItem.quantity =
                    inventoryItem.quantity - number < 0
                    ? 0 : inventoryItem.quantity - number;
            }
        }
    }

    public List<Item> getInventory()
    {
        return this.itemsList;
    }

    public int GetQuantity(int id)
    {
        var inventoryItem = itemsList.FirstOrDefault(item => item.data.Id == id);
        if (inventoryItem != null)
            return inventoryItem.quantity;
        return 0;
    }
}
