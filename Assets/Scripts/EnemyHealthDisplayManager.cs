using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealthDisplayManager : MonoBehaviour
{
    public GameObject Panel;
    public TextMeshProUGUI Text;
    // Start is called before the first frame update
    void Start()
    {
        Text = Panel.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        var health = gameObject.GetComponent<Monster>().hp;
        RefreshHealthBar(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshHealthBar(int health)
    {
        Text.text = health.ToString();
    }
}
