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

    public float CastTime;

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

    /// <summary>
    /// Called by clients or IAs
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public float DealDamage(Entity target)
    {
        ulong netObjId = target.GetComponent<NetworkObject>().NetworkObjectId;

        if (NetworkManager.Singleton.IsServer) //Si l'IA fait des dégats
        {
            GetDamagesServerSide(Damages);
        }
        else //Si c'est un joueur qui fait des dégats
        {
            target.GetDamagesServerRpc(Damages, netObjId);
        }

        return Damages;
    }

    [ServerRpc]
    public void GetDamagesServerRpc(float damages, ulong networkObjectId)
    {
        GetDamagesServerSide(damages);
    }

    /// <summary>
    /// Only on server side
    /// </summary>
    /// <param name="damages"></param>
    public void GetDamagesServerSide(float damages)
    {
        ConsoleLogger.instance.Log("entity took " + damages + " damages");

        GetDamagesClientRpc(damages);

        if (hp - damages < 0)
        {
            Die();
        }
    }

    [ClientRpc]
    public void GetDamagesClientRpc(float damages)
    {
        //Debug.Log("CLIENT : " + damages);

        hp -= damages;

        healthDisplayer.RefreshHealthBar(hp);

        if (IsServer)
        {
            //Check die
        }
    }

    public abstract void Die();
}