﻿using Synapse.Config;
using System.ComponentModel;

namespace VTCustomClass.Config
{
    public class ConfigSCP999 : AbstractConfigSection
    {
        [Description("The MapPoint where the class should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("HCZ_Room3ar", -1.792f, 1.330017f, -0.004005589f);

        [Description("The Amount of Health the class have")]
        public int Health = 100;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 25;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = " SCP999";

        [Description("the number of HP the other class gains when it is next to a the class par second")]
        public int HealHp = 10;

        [Description("the distance that the class must have with the other class")]
        public int Distance = 2;

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 0;
        public int MaxArtificialHealth = 0;

        [Description("Shield of the class")]
        public int Shield = 0;
        public int MaxShield = 0;
    }
}