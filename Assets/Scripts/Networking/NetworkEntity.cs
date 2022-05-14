using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkEntity : NetworkBehaviour
{
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
    public NetworkVariable<Quaternion> Rotation = new NetworkVariable<Quaternion>();

    [ServerRpc]
    public void SubmitPositionRequestServerRpc(Vector3 newPos, Quaternion newRot)
    {
        SetNetworkParams(newPos, newRot);
    }
    public void SetNetworkParams(Vector3 newPos, Quaternion newRot)
    {
        Position.Value = Vector3.Lerp(transform.position, newPos, 50f * Time.deltaTime);
        Rotation.Value = newRot;
    }

    public void SetLocalParams()
    {
        transform.position = Vector3.Lerp(transform.position, Position.Value, 50f * Time.deltaTime);
        transform.rotation = Rotation.Value;
    }
}
