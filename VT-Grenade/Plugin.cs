﻿using Synapse.Api.Plugin;
using System.Collections.Generic;

namespace VTGrenad
{
    [PluginInformation(
        Author = "VT",
        Description = "Allows you to activate grenades remotely",
        LoadPriority = 5,
        Name = "VT-Grenade",
        SynapseMajor = SynapseController.SynapseMajor,
        SynapseMinor = SynapseController.SynapseMinor,
        SynapsePatch = SynapseController.SynapsePatch,
        Version = "v.1.3.2"
        )]
    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }
        public static Dictionary<int, List<AmorcableGrenade>> DictTabletteGrenades = new Dictionary<int, List<AmorcableGrenade>>();

        [Synapse.Api.Plugin.Config(section = "VT-Grenade")]
        public static Config Config;

        public override void Load()
        {
            Instance = this;
            base.Load();
            new EventHandlers();
        }
    }
}
