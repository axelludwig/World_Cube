using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Inventory Inventory;

    public Player()
    {
        Inventory = new Inventory();
        Inventory.AddItem(1, 1);
        Inventory.AddItem(2, 1);
    }

    public Inventory GetInventory()
    {
        return Inventory;
    }

    public override void Die()
    {

    }
}