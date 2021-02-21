﻿using MEC;
using Synapse;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace VT079
{
    static class Methode
    {
        internal static bool _isAirBombGoing = false;
        public static IEnumerator<float> AirSupportBomb(int waitforready = 7, int limit = -1)
        {
            if (_isAirBombGoing)
            {
                yield break;
            }
            else
                _isAirBombGoing = true;

            Map.Get.Cassie("danger . outside zone emergency termination sequence activated .", false);
            yield return Timing.WaitForSeconds(5f);

            while (waitforready > 0)
            {
                PlayAmbientSound(7);
                waitforready--;
                yield return Timing.WaitForSeconds(1f);
            }

            int throwcount = 0;
            while (_isAirBombGoing)
            {
                List<Vector3> randampos = OutsideRandomAirbombPos.Load().OrderBy(x => Guid.NewGuid()).ToList();
                foreach (var pos in randampos)
                {
                    Map.Get.SpawnGrenade(pos, Vector3.zero, 0.1f);
                    yield return Timing.WaitForSeconds(0.1f);
                }
                throwcount++;
                if (limit != -1 && limit <= throwcount)
                {
                    _isAirBombGoing = false;
                    break;
                }
                yield return Timing.WaitForSeconds(0.25f);
            }

            Map.Get.Cassie("outside zone termination completed .", false);
            yield break;
        }

        public static void PlayAmbientSound(int id)
        {
            PlayerManager.localPlayer.GetComponent<AmbientSoundPlayer>().CallMethod("RpcPlaySound", id);
        }

        public static object CallMethod(this object o, string methodName, params object[] args)
        {
            var mi = o.GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (mi != null)
            {
                return mi.Invoke(o, args);
            }
            return null;
        }

        public static T GetFieldValue<T>(this object element, string fieldName)
        {

            FieldInfo field = element.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)field.GetValue(element);
        }

        public static IEnumerator<float> GasRoom(Room room, ReferenceHub scp)
        {
            float a2cooldown = Time.timeSinceLevelLoad;
            List<Synapse.Api.Door> doors = room.Doors;
            foreach (var door in doors)
            {
                door.Open = true;
                door.Locked = true;
            }
            for (int Temps = 8; Temps > 0; Temps--)
            {
                foreach (var player in Server.Get.Players)
                {
                    if (player.Room != null && player.Room == room)
                    {
                        player.GetComponent<AmbientSoundPlayer>().CallMethod("RpcPlaySound", 7);
                        player.BroadcastMessage($"La pièce serra Gazée dans {Temps} seconde(s)");
                    }
                }
                yield return Timing.WaitForSeconds(1f);
            }
            foreach (var door in doors)
            {
                door.Open = false;
                door.Locked = true;
            }
            for (int i = 0; i < 12 * 2; i++)
            {
                foreach (var player in Server.Get.Players)
                {
                    if (player.Team != Team.CHI && player.Team != Team.SCP && player.Team != Team.SCP && player.Team != Team.RIP && player.Room != null && player.Room == room)
                    {
                        player.GiveEffect(Synapse.Api.Enum.Effect.Decontaminating);
                        if (player.RoleID == (int)RoleType.Spectator)
                        {
                            scp.scp079PlayerScript.AddExperience(20);
                        }
                    }
                }
                yield return Timing.WaitForSeconds(0.5f);
            }
            foreach (var item in doors)
            {
                item.Locked = false;
            }
        }

    }
}