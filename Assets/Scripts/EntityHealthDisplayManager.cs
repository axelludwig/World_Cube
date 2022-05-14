using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityHealthDisplayManager : MonoBehaviour
{
    public GameObject Panel;
    private TextMeshProUGUI Text;

    // Start is called before the first frame update
    void Start()
    {
        Text = Panel.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        var health = gameObject.GetComponent<Entity>().hp;
        RefreshHealthBar(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshHealthBar(float health)
    {
        int healthInt = Convert.ToInt32(health);
        Text.text = healthInt.ToString();
    }
}
