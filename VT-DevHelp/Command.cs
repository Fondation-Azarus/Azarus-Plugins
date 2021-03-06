﻿using Assets._Scripts.Dissonance;
using Interactables.Interobjects.DoorUtils;
using Synapse;
using Synapse.Api.Items;
using Synapse.Command;
using Synapse.Config;
using System;
using System.Linq;
using UnityEngine;
using VT_Referance.Method;

namespace VTDevHelp
{
    [CommandInformation(
      Name = "DevDoorInfo",
      Aliases = new[] { "VTFdoor" },
      Description = "For find door",
      Permission = "",
      Platforms = new[] { Platform.RemoteAdmin },
      Usage = ""
      )]
    public class DoorInfoCommand : ISynapseCommand
    {

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            IOrderedEnumerable<Synapse.Api.Door> door = Synapse.Api.Map.Get.Doors.OrderBy(p => Math.Abs(Vector3.Distance(p.Position, context.Player.Position)));
            result.Message = $"No Door find";
            result.State = CommandResultState.Ok;
            if (door.Any())
            {
                result.Message = 
                    $"Door : " +
                    $"\n Name -> {door.First().Name} " +
                    $"\n DoorType -> {door.First().DoorType} " +
                    $"\n Position -> {door.First().Position} " +
                    $"\n Rotation -> {door.First().Rotation} " +
                    $"\n Is Open -> {door.First().Open} " +
                    $"\n Is Breakable -> {door.First().IsBreakable} " +
                    $"\n Door Permissions -> {door.First().DoorPermissions}";
                result.State = CommandResultState.Ok;
            }
            return result;
        }
    }

    [CommandInformation(
    Name = "DevDoorPos",
    Aliases = new[] { "VTPdoor" },
    Description = "",
    Permission = "",
    Platforms = new[] { Platform.RemoteAdmin },
    Usage = ""
    )]
    public class DoorPosCommand : ISynapseCommand
    {

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            var PlayerPos = context.Player.Position;
            
            Plugin.DoorPosition = new SerializedMapPoint(context.Player.Room.RoomName, PlayerPos.x, PlayerPos.y, PlayerPos.z);
            Server.Get.Logger.Info($"{context.Player.Room.RoomName}, {PlayerPos.x}, {PlayerPos.y}, {PlayerPos.z}");
            
            Plugin.DoorRotation = context.Player.gameObject.transform.rotation;
            Server.Get.Logger.Info(context.Player.Rotation);
            
            return result;
        }
    }

    [CommandInformation(
  Name = "DevDoorSpawn",
  Aliases = new[] { "VTSdoor" },
  Description = "",
  Permission = "",
  Platforms = new[] { Platform.RemoteAdmin },
  Usage = ""
  )]
    public class DoorSpawnCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            var door = Synapse.Api.Door.SpawnDoorVariant(Plugin.DoorPosition.Parse().Position, Plugin.DoorRotation);
            door.Open = true;
            door.Open = false;
            
            return result;
        }
    }

    [CommandInformation(
     Name = "DevTest",
     Aliases = new[] { "VTTest" },
     Description = "For TEST",
     Permission = "",
     Platforms = new[] { Platform.RemoteAdmin },
     Usage = ""
     )]
    public class AdvenceEscapeCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            return result;
        }
    }

    [CommandInformation(
     Name = "DevitemInfo",
     Aliases = new[] { "VTIteam" },
     Description = "Dev iteam info",
     Permission = "",
     Platforms = new[] { Platform.RemoteAdmin },
     Usage = ""
     )]
    public class itemInfo : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            var iteamHand = context.Player?.ItemInHand;
            if (iteamHand == null)
            {
                result.State = CommandResultState.Error;
                return result;
            }

            Server.Get.Logger.Info($"{iteamHand} INFO : \n " +
                             $"Generale : ID->{iteamHand.ID}, Custom->{iteamHand.IsCustomItem}, Category->{iteamHand.ItemCategory}, Name->{iteamHand.Name}, Type->{iteamHand.ItemType}\n" +
                             $"Weapon : Barrel->{iteamHand.Barrel}, Sight->{iteamHand.Sight}, Other->{iteamHand.Other}, Durabillity->{iteamHand.Durabillity}\n" +
                             $"Other : Scale->{iteamHand.Scale}, Scale->{iteamHand.State}");
            result.Message = $"{iteamHand} INFO : \n " +
                             $"Generale : ID->{iteamHand.ID}, Custom->{iteamHand.IsCustomItem}, Category->{iteamHand.ItemCategory}, Name->{iteamHand.Name}, Type->{iteamHand.ItemType}\n" +
                             $"Weapon : Barrel->{iteamHand.Barrel}, Sight->{iteamHand.Sight}, Other->{iteamHand.Other}, Durabillity->{iteamHand.Durabillity}\n" +
                             $"Other : Scale->{iteamHand.Scale}, Scale->{iteamHand.State}";
            result.State = CommandResultState.Ok;
            return result;
        }
    }

    [CommandInformation(
    Name = "DevDecont",
    Aliases = new[] { "VTDecont" },
    Description = "Dev Decont Test",
    Permission = "",
    Platforms = new[] { Platform.RemoteAdmin, Platform.ServerConsole },
    Usage = ""
    )]
    public class TestDecont : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            Server.Get.Map.Decontamination?.CallMethod("InstantStart");
            Server.Get.Map.Decontamination?.Controller?.CallMethod("FinishDecontamination");
            result.State = CommandResultState.Error;
            return result;
        }
    }

    [CommandInformation(
    Name = "DevPermitino",
    Aliases = new[] { "VTPerm" },
    Description = "Dev Perm for Test",
    Permission = "",
    Platforms = new[] { Platform.RemoteAdmin },
    Usage = ""
    )]
    public class DevGive : ISynapseCommand
    {

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Arguments.FirstElement().Any())
            { 
                int Id;
                int.TryParse(context.Arguments.FirstElement(), out Id);
                var player = Server.Get.GetPlayers(p => p.PlayerId == Id).FirstOrDefault();
                player.RemoteAdminAccess = !player.RemoteAdminAccess; 
            }
            return result;
        }
    }

    [CommandInformation(
       Name = "DevSong",
       Aliases = new[] { "VTSong" },
       Description = "Dev Song Test",
       Permission = "",
       Platforms = new[] { Platform.RemoteAdmin },
       Usage = ""
       )]
    public class Song : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            result.State = CommandResultState.Error;
            int ID;
            if (int.TryParse(context.Arguments.FirstElement(), out ID))
            {
                DissonanceUserSetup dissonanceUserSetup = context.Player.gameObject.GetComponent<DissonanceUserSetup>();
                Methods.PlayAmbientSound(ID);
                result.State = CommandResultState.Ok;
            }
            return result;
        }
    }

    [CommandInformation(
    Name = "DevGrenad",
    Aliases = new[] { "VTGrenad" },
    Description = "Dev Test Plugin",
    Permission = "",
    Platforms = new[] { Platform.RemoteAdmin },
    Usage = ""
    )]
    public class DevGrenad : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            int i = 10;
            context.Player.Cuffer = context.Player;
            if (context.Arguments.First() != null)
                int.TryParse(context.Arguments.First(), out i);
            while (i != 0)
            {
                i--;
                context.Player.Inventory.AddItem(ItemType.GrenadeFrag, 0, 0, 0, 0);
            }
            return result;
        }
    }

    [CommandInformation(
       Name = "DevClear",
       Aliases = new[] { "VTDClear" },
       Description = "",
       Permission = "",
       Platforms = new[] { Platform.RemoteAdmin },
       Usage = ".VTClear (Iteam, Corp ou All)"
       )]
    public class Clear : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            var iteams = Server.Get.Map.Items.Where(p => p.State == Synapse.Api.Enum.ItemState.Map);
            var ragdolls = Server.Get.Map.Ragdolls;
            switch (context.Arguments.FirstElement())
            {
                case "Iteam":
                    foreach (var iteam in iteams)
                        iteam.Destroy();
                    result.State = CommandResultState.Ok;
                    break;

                case "Corp":
                    foreach (var ragdoll in ragdolls)
                        UnityEngine.Object.DestroyImmediate(ragdoll.GameObject, true);
                    result.State = CommandResultState.Ok;
                    break;

                case "All":
                    foreach (var iteam in iteams)
                        iteam.Despawn();
                    foreach (var ragdoll in ragdolls)
                        UnityEngine.Object.DestroyImmediate(ragdoll.GameObject, true);
                    result.State = CommandResultState.Ok;
                    break;
            }
            return result;
        }
    }

    [CommandInformation(
     Name = "Méliodas",
     Aliases = new[] { "Méliodas" },
     Description = "Méliodas!!!!!!!!!",
     Permission = "",
     Platforms = new[] { Platform.ClientConsole, Platform.RemoteAdmin }, 
     Usage = ""
     )]
    public class MéliodasCommand : ISynapseCommand
    {

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            result.Message = "Méliodas é bô !";
            result.State = CommandResultState.Ok;
            return result;
        }
    }
}

