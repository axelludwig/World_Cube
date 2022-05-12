using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class InventoryCanvasManager : MonoBehaviour
{
    private InventoryCanvasManager() { }
    public GameObject Panel;
    public TextMeshProUGUI Text;
    public int MaxItemsPerLines;
    public GameObject SpawnPoint;

    public GameObject ItemPrefab;

    private static InventoryCanvasManager _instance;

    public static InventoryCanvasManager Instance
    {
        get { return _instance; }
        private set { _instance = value; }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        _instance = this;

        Text = Panel.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        BuildInventory();
        Panel.gameObject.SetActive(false);
    }

    public void BuildInventory()
    {
        var playerInventory = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject().gameObject.GetComponent<Player>().Inventory.GetInventory();
        if (playerInventory != null)
        {
            foreach (Item item in playerInventory)
            {
                Instantiate(ItemPrefab, SpawnPoint.transform);

            }
        }
    }

    public void ShowInventory(bool state)
    {
        Panel.gameObject.SetActive(state);
        //var t = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject().gameObject.GetComponent<Player>().GetInventory();
        //Debug.Log(t);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
