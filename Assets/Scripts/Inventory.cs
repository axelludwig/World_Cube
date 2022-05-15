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
        try
        {
            var itemData = ItemManager.Instance.getItem(id);
            if (itemData != null)
                this.AddItem(itemData, number);
        }
        catch (System.Exception)
        {
            //Debug.Log("louuuuul l'instance du singleton et nulent là ??");
            //throw;
        }
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
        var itemData = ItemManager.Instance.getItem(id);
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

    public List<Item> GetInventory()
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
