using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeIfGetDamage : Upgrade
{
    public override void UpgradePlayer()
    {
        player.Explode();
    }
}
