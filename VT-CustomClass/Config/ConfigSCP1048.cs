﻿using Synapse.Config;
using System.ComponentModel;

namespace VTCustomClass.Config
{
    public class ConfigSCP1048 : AbstractConfigSection
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
        public string RoleName = " SCP1048";

        [Description("The cooldown of the class Power")]
        public int CoolDown = 90;

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 0;
    }
}
