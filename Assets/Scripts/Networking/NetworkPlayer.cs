using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkPlayer : NetworkEntity
{
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            GetComponent<PlayerPrefabScript>().InitCameras();
            gameObject.transform.position = ChunkLoader.spawnPosition;
            ChunkLoader.player = gameObject.transform;
        }
        else
        {
            GetComponent<PlayerPrefabScript>().DestroyControllers();
        }

        gameObject.name = "Player " + GetComponent<NetworkObject>().NetworkObjectId;

        if (IsServer && IsOwnedByServer)
        {
            SpawnManager.Instantiate();
        }
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
}
