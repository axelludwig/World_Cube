using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class Entity : NetworkBehaviour
{
    public int base_maxhp;
    public int base_moveSpeed;

    public int hp;
    public int moveSpeed;
    public int experience;
    public int level;
    public Inventory inventory;
    public TeamType team;

    public float AggroDistance = 30;

    public enum TeamType
    {
        Players,
        Biome1,
        Biome2
    }
}