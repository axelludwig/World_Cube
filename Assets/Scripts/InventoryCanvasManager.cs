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
    public int MaxItemsPerLines = 3;
    public GameObject SpawnPoint;
    private List<GameObject> Slots;
    private Transform Content;

    public GameObject ItemPrefab;

    private static InventoryCanvasManager _instance;

    public static InventoryCanvasManager Instance
    {
        get { return _instance; }
        private set { _instance = value; }
    }
    private void Awake()
    {
        Slots = new List<GameObject>();
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        _instance = this;

        Text = Panel.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        Content = Panel.transform.Find("ScrollView/Viewport/ContentInventory");

        foreach (Transform t in Content.transform)
            Slots.Add(t.gameObject);
        Debug.Log(Slots.Count);
        Panel.gameObject.SetActive(false);
    }

    public void BuildInventory()
    {
        var playerInventory = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject().gameObject.GetComponent<Player>().Inventory.GetInventory();
        if (playerInventory != null)
        {                
            for (int i = 0; i < playerInventory.Count; i++)
            {
                GameObject temp = Slots[i];
                Item item = playerInventory[i];
                var prefabText = temp.transform.Find("TextDisplayName").GetComponent<TextMeshProUGUI>();
                var prefabQuantity = temp.transform.Find("TextQuantity").GetComponent<TextMeshProUGUI>();
                var prefabImage = temp.transform.Find("RawImage").GetComponent<RawImage>();
                prefabText.text = item.data.DisplayName;
                prefabQuantity.text = item.quantity.ToString();
                prefabImage.texture = Resources.Load<Texture2D>("ItemIcons/" + item.data.Icon);
                temp.SetActive(true);
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
