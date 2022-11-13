using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpawnManager : BaseSingleton<SpawnManager>
{
    public GameObject TestEnnemy;

    // Start is called before the first frame update
    void Start()
    {
        AddEnnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddEnnemy()
    {
        GameObject ennemy1 = Instantiate(Resources.Load<GameObject>("Ennemy1"), new Vector3(800, 80, 780), Quaternion.identity);
        ennemy1.GetComponent<NetworkObject>().Spawn();
        ennemy1.AddComponent<MonsterController>();

        GameObject ennemy2 = Instantiate(Resources.Load<GameObject>("Ennemy2"), new Vector3(780, 80, 780), Quaternion.identity);
        ennemy2.GetComponent<NetworkObject>().Spawn();
        ennemy2.AddComponent<MonsterController>();

        ConsoleLogger.instance.Log("2 Ennemies added");
    }
}
