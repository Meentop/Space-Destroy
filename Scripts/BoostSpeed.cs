using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSpeed : Upgrade
{
    public override void UpgradePlayer()
    {
        player.BoostSpeed();
    }
}
