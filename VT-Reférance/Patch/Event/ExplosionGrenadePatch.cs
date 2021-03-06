﻿using Grenades;
using HarmonyLib;
using Mirror;
using Synapse.Api.Enum;
using System;


namespace VT_Referance.Patch.Event
{
    [HarmonyPatch(typeof(FragGrenade), nameof(FragGrenade.ServersideExplosion))]
    class FragExplosionGrenadePatch
    {
        private static bool Prefix(FragGrenade __instance, ref bool __result)
        {
            try
            {
                if (!NetworkServer.active) return false;
                EffectGrenade grenade = __instance;
                GrenadeType Type;
                if (__instance.GetType() == typeof(FragGrenade))
                    Type = GrenadeType.Grenade;
                else if (__instance.GetType() == typeof(Scp018Grenade))
                    Type = GrenadeType.Scp018;
                else
                    Type = (GrenadeType)4;
                bool falg = true;
                VTController.Server.Events.Grenade.InvokeExplosionGrenadeEvent(grenade, Type, ref falg);
                if (!falg) 
                    __result = __instance.ServersideExplosion();
                return falg;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: GrenadeFragExplosion failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }

    [HarmonyPatch(typeof(FlashGrenade), nameof(FlashGrenade.ServersideExplosion))]
    class FlashExplosionGrenadePatch
    {
        private static bool Prefix(FlashGrenade __instance, ref bool __result)
        {
            try
            {
                EffectGrenade grenade = __instance;
                GrenadeType Type;
                if (__instance.GetType() == typeof(FlashGrenade))
                    Type = GrenadeType.Flashbang;
                else
                    Type = (GrenadeType)4;
                bool falg = true;
                VTController.Server.Events.Grenade.InvokeExplosionGrenadeEvent(grenade, Type, ref falg);
                if (!falg)
                    __result = __instance.ServersideExplosion();
                return falg;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: GrenadeFlashExplosion failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}
