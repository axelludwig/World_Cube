using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkPlayer : NetworkBehaviour
{
    private NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
    private NetworkVariable<Quaternion> Rotation = new NetworkVariable<Quaternion>();

    public GameObject TestEnnemy;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            GetComponent<PlayerPrefabScript>().InitCameras();
            InitPosition();
            gameObject.transform.position = ChunkLoader.spawnPosition;
            ChunkLoader.player = gameObject.transform;
        }
        else
        {
            GetComponent<PlayerPrefabScript>().DestroyControllers();
        }

        gameObject.name = "Player prefab";

        if (IsServer)
        {
            GameObject go = Instantiate(TestEnnemy, new Vector3(800, 80, 800), Quaternion.identity);
            go.GetComponent<NetworkObject>().Spawn();
        }
    }

    public void InitPosition()
    {
        var initialPosition = GetRandomPositionOnPlane();
        transform.position = initialPosition;

        if (NetworkManager.Singleton.IsServer)
        {
            Position.Value = initialPosition;
        }
        else
        {
            SubmitPositionRequestServerRpc(transform.position, transform.rotation);
        }
    }

    [ServerRpc]
    void SubmitPositionRequestServerRpc(Vector3 newPos, Quaternion newRot)
    {
        SetNetworkParams(newPos, newRot);
    }

    void Update()
    {
        if (IsOwner)
        {
            if (IsServer)
                SetNetworkParams(transform.position, transform.rotation);
            else
                SubmitPositionRequestServerRpc(transform.position, transform.rotation);
        }
        else
        {
            SetLocalParams();
        }

    }

    static Vector3 GetRandomPositionOnPlane()
    {
        return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
    }

    private void SetNetworkParams(Vector3 newPos, Quaternion newRot)
    {
        Position.Value = Vector3.Lerp(transform.position, newPos, 50f * Time.deltaTime);
        Rotation.Value = newRot;
    }

    private void SetLocalParams()
    {
        transform.position = Vector3.Lerp(transform.position, Position.Value, 50f * Time.deltaTime);
        transform.rotation = Rotation.Value;
    }
}
