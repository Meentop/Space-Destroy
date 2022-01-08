using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : Item
{
    [SerializeField] int heal;

    protected override void GetUpPlayer()
    {
        player.Healing(heal);
    }
}
