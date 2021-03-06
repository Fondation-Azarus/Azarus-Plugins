﻿using HarmonyLib;
using Scp914;
using System;

namespace VT_Referance.Patch.Event
{
    [HarmonyPatch(typeof(PlayerInteract), nameof(PlayerInteract.CallCmdChange914Knob))]
    class Scp914ChangeSetingPatch
    {
        private static bool Prefix(PlayerInteract __instance)
        {
            try
            {
                Scp914Machine Sing914 = Scp914Machine.singleton;
                if (!__instance._playerInteractRateLimit.CanExecute())
                    return false;
                if (__instance._hc.CufferId > 0 || __instance._hc.ForceCuff && !PlayerInteract.CanDisarmedInteract || (Scp914Machine.singleton.working || !__instance.ChckDis(Scp914Machine.singleton.knob.position)))
                    return false;
                bool flag = true;
                Sing914.curKnobCooldown = Sing914.knobCooldown;
                
                Scp914Knob scp914Knob = Sing914.knobState + 1;
                if (scp914Knob > Scp914Machine.knobStateMax)
                    scp914Knob = Scp914Machine.knobStateMin;
                
                VTController.Server.Events.Map.InvokeChange914KnobSettingEvent(__instance.GetPlayer(), Sing914.knobState, ref scp914Knob, ref flag);
                if (flag) 
                {
                    __instance.OnInteract();
                    Sing914.NetworkknobState = scp914Knob;
                }
                return false;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: Change914Knob failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}
