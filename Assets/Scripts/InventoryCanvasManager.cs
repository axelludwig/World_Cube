using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class InventoryCanvasManager : MonoBehaviour
{
    public GameObject Panel;
    public TextMeshProUGUI Text;

    private static InventoryCanvasManager _instance;

    public static InventoryCanvasManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    public void ShowInventory(bool state)
    {
        this.gameObject.SetActive(state);
    }

    // Start is called before the first frame update
    void Start()
    {
        Text = gameObject.transform.Find("Panel/Text (TMP)").GetComponent<TextMeshProUGUI>();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
