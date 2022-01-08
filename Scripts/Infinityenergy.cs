using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infinityenergy : Item
{
    [SerializeField] Sprite sprite;
    [SerializeField] Color color;

    [SerializeField] float secondsInfinitEnergy;
    protected override void GetUpPlayer()
    {
        player.StartInfinityEnergy(secondsInfinitEnergy, sprite, color);
    }
}
