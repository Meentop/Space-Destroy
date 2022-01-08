using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Item
{
    [SerializeField] Sprite sprite;
    [SerializeField] Color color;

    [SerializeField] float secondsShield;
    protected override void GetUpPlayer()
    {
        if(!player.shield)
            player.StartShield(secondsShield, sprite, color);
    }
}
