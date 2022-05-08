using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class ItemManager : MonoBehaviour
{
    private static ItemManager _instance;
    public static ItemData[] loadedItems;

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
        return loadedItems.SingleOrDefault(item => item.id == id);
    }

    public static ItemData getItem(string displayName)
    {
        return loadedItems.SingleOrDefault(item => item.displayName == displayName);
    }

    private static void Start()
    {
        var jsonString = readTextFile("Assets/Scripts/Items/items.json");
        Debug.Log(jsonString);
        loadedItems = JsonHelper.FromJson<ItemData>(jsonString);
    }
    private static string readTextFile(string file_path)
    {
        return File.ReadAllText(file_path);
    }
}

