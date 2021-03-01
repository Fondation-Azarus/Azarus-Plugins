﻿using Synapse.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CustomClass.Config
{
    public class ICExpertPyrotechnieConfig : AbstractConfigSection, IBaseConfig
    {
        [Description("The MapPoint where IC Expert Pyrotechnie should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("HCZ_Room3ar", -1.792f, 1.330017f, -0.004005589f);

        [Description("The Amount of IC Health Expert Pyrotechnie has")]
        public int Health = 100;

        [Description("The Items Expert Pyrotechnie spawns with")]
        public List<SerializedItem> Items = new List<SerializedItem>() { new SerializedItem((int)ItemType.Medkit, 35, 0, 0, 0, Vector3.one) };

        [Description("The Chance of which IC Expert Pyrotechnie spawns")]
        public int SpawnChance => throw new NotImplementedException();

        [Description("The Amount of Players Required in order to have the Chanche that IC Expert Pyrotechnie can spawn")]
        public int RequiredPlayers = 0;

        [Description("The name of the IC Expert Pyrotechnie")]
        public string RoleName = "NONE";

        [Description("The Amount of Health IC Expert Pyrotechnie has")]
        public int ArtificialHealth = 0;

        [Description("The Amount of Health IC Expert Pyrotechnie has")]
        public int MaxArtificialHealth = 100;

        [Description("The Amount of Health IC Expert Pyrotechnie has")]
        public bool loseArtificialHealth = false;

        [Description("The Name Tag is visible or invisible")]
        public bool ShowTag = false;

        public SerializedMapPoint ConfigSpawnPoint => SpawnPoint;
        public int ConfigHealth => Health;
        public List<SerializedItem> ConfigItems => Items;
        public int ConfigSpawnChance => SpawnChance;
        public int ConfigRequiredPlayers => RequiredPlayers;
        public string ConfigRoleName => RoleName;
        public int ConfigArtificialHealth => ArtificialHealth;
        public int ConfigMaxArtificialHealth => MaxArtificialHealth;
        public bool ConfigloseArtificialHealth => loseArtificialHealth;
        public bool CongifShowTag => ShowTag;
    }
}