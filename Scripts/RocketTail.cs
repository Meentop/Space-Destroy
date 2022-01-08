using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTail : Upgrade
{
    public override void UpgradePlayer()
    {
        player.RocketTail();
    }
}
