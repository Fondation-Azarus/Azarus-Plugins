﻿using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using UnityEngine;
using VT_Referance.ItemScript;

namespace VTDevHelp
{
    internal class DestructeurDeGameObject : BaseWeaponScript
    {

        protected override uint Ammo => 100;

        protected override AmmoType AmmoType => AmmoType.Ammo5;

        protected override int ID => 301;

        protected override ItemType ItemType => ItemType.GunUSP;

        protected override string Name => "DestructeurDeGameObject";

        protected override void Shoot(PlayerShootEventArgs ev)
        {
            GameObject.Destroy(ev.Player.LookingAt);
        }
    }
}