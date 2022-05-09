using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class ItemManager : MonoBehaviour
{
    private static ItemManager _instance;
    public static List<ItemData> loadedItems;

    public static ItemManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public static ItemData getItem(int id)
    {
        return loadedItems.SingleOrDefault(item => item.Id == id);
    }

    public static ItemData getItem(string displayName)
    {
        return loadedItems.SingleOrDefault(item => item.DisplayName == displayName);
    }

    private void Start()
    {
        var jsonString = readTextFile("Assets/Scripts/Items/items.json");
        loadedItems = new List<ItemData>();

        var genericItems = JsonHelper.FromJson<ItemData>(jsonString);
        foreach (ItemData genericItem in genericItems)
        {
            switch (genericItem.ItemClass)
            {
                case "Weapon":
                    loadedItems.Add(new Weapon(genericItem));
                    break;
                case "Equipment":
                    loadedItems.Add(new Equipment(genericItem));
                    break;
                case "Consumable":
                    loadedItems.Add(new Consumable(genericItem));
                    break;
            }
        }
    }
    private string readTextFile(string file_path)
    {
        return File.ReadAllText(file_path);
    }
}

