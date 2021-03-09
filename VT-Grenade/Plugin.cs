﻿using HarmonyLib;
using Mirror;
using Synapse;
using Synapse.Api.Plugin;
using System.Collections.Generic;
using UnityEngine;

namespace VTGrenad
{
    [PluginInformation(
        Author = "VT-Grenade",
        Description = "Allows you to activate grenades remotely",
        LoadPriority = 1,
        Name = "VT-IpLocker",
        SynapseMajor = SynapseController.SynapseMajor,
        SynapseMinor = SynapseController.SynapseMinor,
        SynapsePatch = SynapseController.SynapsePatch,
        Version = "v.1.2.1"
        )]
    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }
        public static Dictionary<int, List<AmorcableGrenade>> DictTabletteGrenades = new Dictionary<int, List<AmorcableGrenade>>();

        [Synapse.Api.Plugin.Config(section = "VT-Grenade")]
        public static Config Config;

        private void PatchAll()
        {
            var instance = new Harmony("VT-IpLocker");
            instance.PatchAll();
            Server.Get.Logger.Info("Custom class Harmony Patch done!");
        }

        public override void Load()
        {
            Instance = this;
            base.Load();
            PatchAll();
            new EventHandlers();
        }
    }
}
