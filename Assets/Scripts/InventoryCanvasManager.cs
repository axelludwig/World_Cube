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
        Panel.gameObject.SetActive(false);
    }

    public void ShowInventory(bool state)
    {
        Panel.gameObject.SetActive(state);
        //NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject().gameObject
        //Debug()
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
