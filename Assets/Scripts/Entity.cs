using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity
{
    public int base_maxhp;
    public int base_moveSpeed;

    public int hp;
    public int moveSpeed;
    public int experience;
    public int level;
    public Inventory inventory;

    enum Teams
    {
        Players,
        Biome1,
        Biome2
    }
}