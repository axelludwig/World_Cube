using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class Entity : NetworkBehaviour
{
    public float base_maxhp;
    public int base_moveSpeed;

    public float hp;
    public int moveSpeed;
    public int experience;
    public int level;
    public TeamType team;
    public float Range;

    public float Damages;

    public float AggroDistance = 30;

    private EntityHealthDisplayManager healthDisplayer;

    private void Awake()
    {
        healthDisplayer = GetComponent<EntityHealthDisplayManager>();
    }

    public enum TeamType
    {
        Players,
        Biome1,
        Biome2
    }

    public float DealDamage(Entity target)
    {
        ulong netObjId = target.GetComponent<NetworkObject>().NetworkObjectId;

        target.GetDamagesServerRpc(Damages, netObjId);

        return Damages;
    }

    [ServerRpc]
    public void GetDamagesServerRpc(float damages, ulong networkObjectId)
    {
        Debug.Log("SERVER : " + damages);

        GetDamagesClientRpc(damages, networkObjectId);

        if (hp - damages < 0)
        {
            Die();
        }
    }

    [ClientRpc]
    public void GetDamagesClientRpc(float damages, ulong networkObjectId)
    {
        Debug.Log("CLIENT : " + damages);

        hp -= damages;

        healthDisplayer.RefreshHealthBar(hp);

        if (IsServer)
        {
            //Check die
        }
    }

    public abstract void Die();
}