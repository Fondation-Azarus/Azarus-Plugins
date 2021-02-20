﻿using Synapse.Config;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class ICLeaderScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.MTF };

        protected override List<int> FriendsList => new List<int> { (int)Team.CHI, (int)Team.CDP };

        protected override RoleType RoleType => RoleType.ChaosInsurgency;

        protected override int RoleTeam => (int)Team.CHI;

        protected override int RoleId => (int)MoreClasseID.CHILeader;

        protected override string RoleName => Plugin.ConfigCHILeader.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigCHILeader;
    }
}
