using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : Item
{
    [SerializeField] Sprite sprite;
    [SerializeField] Color color;

    [SerializeField] float secondsShooting;
    protected override void GetUpPlayer()
    {
        if(!player.shooting)
            player.StartShooting(secondsShooting, sprite, color);
    }
}
