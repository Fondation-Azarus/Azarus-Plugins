﻿using VTCustomClass.Pouvoir;
using Interactables.Interobjects.DoorUtils;
using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.Behaviour;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace VTCustomClass.PlayerScript
{
    public abstract class BaseUTRScript : BasePlayerScript, IUtrRole
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;
        protected float oldStaminaUse;
        private bool _protected096 = true;
        protected override void AditionalInit()
        {
            oldStaminaUse = Player.StaminaUsage;
            Player.StaminaUsage = 0;
            Player.Hub.playerStats.artificialHpDecay = 0;
            Timing.CallDelayed(1f, () =>
            {
                Player.GiveEffect(Effect.Visuals939);
                Player.GiveEffect(Effect.Disabled);
            });
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerItemUseEvent += OnUseIteam;
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
            Server.Get.Events.Map.DoorInteractEvent += OnDoorInteract;
            Server.Get.Events.Scp.Scp096.Scp096AddTargetEvent += OnAddTarget;
            Server.Get.Events.Player.PlayerEnterFemurEvent += OnFemur;
            Server.Get.Events.Player.PlayerSetClassEvent += OnScp173Spawn;
        }

        private void OnScp173Spawn(PlayerSetClassEventArgs ev)
        {
            if (ev.Role == RoleType.Scp173 && !ev.Player.Scp173Controller.IgnoredPlayers.Contains(Player))
                ev.Player.Scp173Controller.IgnoredPlayers.Add(Player);
        }

        private void OnFemur(PlayerEnterFemurEventArgs ev)
        {
            if (ev.Player == Player)
                ev.Allow = false;
        }

        private void OnAddTarget(Scp096AddTargetEventArgument ev)
        {
            if (ev.Player == Player)
                ev.Allow = _protected096;
        }

        private void OnDoorInteract(DoorInteractEventArgs ev)
        {
            List<KeycardPermissions> UTRkey = new List<KeycardPermissions>() { KeycardPermissions.ArmoryLevelOne, KeycardPermissions.ArmoryLevelTwo,
                KeycardPermissions.ArmoryLevelThree, KeycardPermissions.Checkpoints, KeycardPermissions.ContainmentLevelOne,
                KeycardPermissions.ContainmentLevelTwo, KeycardPermissions.ExitGates, KeycardPermissions.ScpOverride };

            if (ev.Player == Player && UTRkey.Contains(ev.Door.DoorPermissions.RequiredPermissions))
                ev.Allow = true;
        }

        private void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim == Player && PluginClass.ConfigUTR.ListScpDamge.Contains(ev.Killer.RoleID))
                ev.DamageAmount = PluginClass.ConfigUTR.damage;
            if (ev.Victim == Player && (PluginClass.ConfigUTR.ListScpNoDamge.Contains(ev.Killer.RoleID) || ev.HitInfo.GetDamageType() == DamageTypes.Falldown))
                ev.Allow = false;
            if (ev.Killer == Player && ev.Victim.RoleID == (int)RoleType.Scp096)
                _protected096 = false;
        }

        private void OnUseIteam(PlayerItemInteractEventArgs ev)
        {
            if (ev.Player == Player)
            {
                if (ev.CurrentItem.ItemCategory == ItemCategory.Medical || ev.CurrentItem.ItemCategory == ItemCategory.SCPItem)
                    ev.Allow = false;
            }
        }

        public override void DeSpawn()
        {
            foreach(var player in Server.Get.Players)
            {
                if (player.Scp173Controller.IgnoredPlayers.Contains(Player))
                    player.Scp173Controller.IgnoredPlayers.Remove(player);
            }
            Player.StaminaUsage = oldStaminaUse;
            base.DeSpawn();
            Server.Get.Events.Player.PlayerItemUseEvent -= OnUseIteam;
            Server.Get.Events.Player.PlayerDamageEvent -= OnDamage;
            Server.Get.Events.Map.DoorInteractEvent -= OnDoorInteract;
            Server.Get.Events.Scp.Scp096.Scp096AddTargetEvent -= OnAddTarget;
            Server.Get.Events.Player.PlayerEnterFemurEvent -= OnFemur;
        }
    }
}