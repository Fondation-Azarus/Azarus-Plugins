﻿using VTCustomClass.PlayerScript;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System.Collections.Generic;
using System.Linq;
using VT_Referance.Variable;
using VT_Referance;
using System;
using Synapse.Permission;

namespace VTCustomClass
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.SpawnPlayersEvent += OnSpawn;
            Server.Get.Events.Round.TeamRespawnEvent += OnReSpawn;
            Server.Get.Events.Player.PlayerSetClassEvent += OnClass;
            Server.Get.Events.Player.PlayerJoinEvent += OnJoin;
            Server.Get.Events.Round.RoundRestartEvent += () => Data.PlayerRole.Clear();
            VTController.Server.Events.Player.PlayerSetClassEvent += OnSetID;
        }

        private void OnSetID(VT_Referance.Event.EventArguments.PlayerSetClassEventArgs ev)
        {
            if (ev.NewID != (int)RoleID.Spectator && ev.NewID != (int)RoleID.Staff)
                RemouveSeeInvisble(ev.Player);
            else if (ev.Player.RemoteAdminAccess || ev.NewID == (int)RoleID.Staff)
                AddSeeInvisble(ev.Player);
        }

        private void OnJoin(PlayerJoinEventArgs ev)
        {
            if (ev.Player.RemoteAdminAccess == true)
                AddSeeInvisble(ev.Player);
        }

        private void AddSeeInvisble(Player player)
        {
            string OrginalGroupeName = PermissionHandler.Get.Groups.FirstOrDefault(g => g.Value == player.SynapseGroup).Key;
            if (string.IsNullOrEmpty(OrginalGroupeName))
                return;
            OrginalGroupeName = $"VT_SeeInvislble{OrginalGroupeName}";
            player.SynapseGroup = PermissionHandler.Get.GetServerGroup(OrginalGroupeName);
        }

        private void RemouveSeeInvisble(Player player)
        {
            string OrginalGroupeName = PermissionHandler.Get.Groups.FirstOrDefault(g => g.Value == player.SynapseGroup).Key;
            if (string.IsNullOrEmpty(OrginalGroupeName))
                return;
            OrginalGroupeName.Replace("VT_SeeInvislble", "");
            player.SynapseGroup = PermissionHandler.Get.GetServerGroup(OrginalGroupeName);
        }

        private void OnClass(PlayerSetClassEventArgs ev)
        {
            Data.PlayerRole[ev.Player] = (int)ev.Role;
            if (RespawnPlayer.Contains(ev.Player))
            {
                ev.SpawnRole(RoleType.ChaosInsurgency, new CHILeaderScript());
                ev.SpawnRole(RoleType.ChaosInsurgency, new CHIHackerScript());
                ev.SpawnRole(RoleType.ChaosInsurgency, new CHIExpertPyrotechnieScript());
                ev.SpawnRole(RoleType.ChaosInsurgency, new CHIKamikazeScript());
                ev.SpawnRole(RoleType.ChaosInsurgency, new CHIMastodonteScript());
                ev.SpawnRole(RoleType.ChaosInsurgency, new CHIInfirmierScript());
                ev.SpawnRole(RoleType.NtfCadet, new CHISPYScript());
                ev.SpawnRole(RoleType.NtfCadet, new NTFCapitaineScript());
                ev.SpawnRole(RoleType.NtfCadet, new NTFSergentScript());
                ev.SpawnRole(RoleType.NtfLieutenant, new NTFExpertPyrotechnieScript());
                ev.SpawnRole(RoleType.NtfLieutenant, new NTFExpertReconfinementScript());
                ev.SpawnRole(RoleType.NtfLieutenant, new NTFInfirmierScript());
                ev.SpawnRole(RoleType.NtfLieutenant, new NTFVirologueScript());
                RespawnPlayer.Remove(ev.Player);
            }
        }

        public static List<Player> RespawnPlayer = new List<Player>();

        private void OnReSpawn(TeamRespawnEventArgs ev)
        {
            RespawnPlayer.Clear();
            Plugin.Instance.RespawnedPlayer.Clear();
            RespawnPlayer.AddRange(ev.Players);
        }

        private void OnSpawn(SpawnPlayersEventArgs ev)
        {
            if (ev.SpawnPlayers != null)
            {
                ev.SpawnPlayers.SpawnRole(RoleType.ClassD, new ConciergeScript());
                ev.SpawnPlayers.SpawnRole(RoleType.ClassD, new SCP507Script());
                ev.SpawnPlayers.SpawnRole(RoleType.ClassD, new CHIIntrusScript());
                ev.SpawnPlayers.SpawnRole(RoleType.Scientist, new ScientifiqueSuperviseurScript());
                ev.SpawnPlayers.SpawnRole(RoleType.FacilityGuard, new FoundationUTRScript());
                ev.SpawnPlayers.SpawnRole(RoleType.FacilityGuard, new GardeSuperviseurScript());
                ev.SpawnPlayers.SpawnRole(RoleType.FacilityGuard, new TechnicienScript());
                ev.SpawnPlayers.SpawnRole(RoleType.ClassD, new DirecteurSiteScript());
                ev.SpawnPlayers.SpawnRole(RoleType.ClassD, new ZoneManagerScript());
                ev.SpawnPlayers.SpawnRole(RoleType.ClassD, new GardePrisonScript());
                    
                if (ev.SpawnPlayers.Count() > 25)
                {
                    ev.SpawnPlayers.SpawnRole(RoleType.ClassD, new SCP008Script());
                    ev.SpawnPlayers.SpawnRole(RoleType.ClassD, new SCP966cript());
                }
                else
                {
                    ev.SpawnPlayers.SpawnSCPRole(new SCP008Script());
                    ev.SpawnPlayers.SpawnSCPRole(new SCP966cript());
                }
                foreach (var player in ev.SpawnPlayers.Keys)
                {
                    Data.PlayerRole[player] = ev.SpawnPlayers[player];
                }
            }
        }
    }
}
