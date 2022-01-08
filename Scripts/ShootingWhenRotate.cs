using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingWhenRotate : Upgrade
{
    public override void UpgradePlayer()
    {
        player.ShootingWhenRotate();
    }
}
