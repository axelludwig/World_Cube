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
        GameObject go = Instantiate(Resources.Load<GameObject>("Ennemy1"), new Vector3(800, 80, 800), Quaternion.identity);
        go.GetComponent<NetworkObject>().Spawn();
        go.AddComponent<MonsterController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
