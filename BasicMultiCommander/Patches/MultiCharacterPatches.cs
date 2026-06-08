using BasicMultiCommander.Methods;
using HarmonyLib;
using Kingmaker;
using Kingmaker.AreaLogic.Etudes;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Parts;
//using Owlcat.Runtime.Core;
using UnityEngine;

namespace BasicMultiCommander.Patches
{
    class MultiCharacterPatches
    {
        public static Vector3 sphere = 2.5f * UnityEngine.Random.insideUnitSphere;


        //安全区角色控制
        [HarmonyPatch(typeof(UnitEntityData), nameof(UnitEntityData.IsDirectlyControllable), MethodType.Getter)]
        internal static class BMC_Deputy_MainCharacter_Patch_Unit
        {
            public static bool Prefix(ref bool __result, UnitEntityData __instance)
            {
                if (__instance.IsMainCharacter) return true;
                if (!Game.Instance.Player.CapitalPartyMode) return true;
                if (__instance == null) return true;
                UnitEntityData master = __instance.Master;

                if ((object)master != null && !master.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(master))
                {
                    //if (DebuptyAlignmentShift.deputyPetIsControlledBySpawner(__instance))
                    //{
                    //    __result = false;
                    //    return false;
                    //}
                    __result = true;
                    return false;
                }
                if (LogicStructures.isPlayerActorBlueprintType(__instance))
                {
                    __result = true;
                    return false;
                }
                return true;
            }
        }

        //安全区角色控制
        [HarmonyPatch(typeof(UnitPartCompanion), nameof(UnitPartCompanion.IsControllableInParty))]
        internal static class BMC_Deputy_MainCharacter_Patch_Dangerous
        {
            public static bool Prefix(ref bool __result, UnitPartCompanion __instance)
            {
                if (__instance.Owner.IsMainCharacter) return true;
                if (!Game.Instance.Player.CapitalPartyMode) return true;
                if (__instance == null) return true;

                UnitEntityData master = __instance.Owner.Master;

                if ((object)master != null && !master.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(master))
                {
                    //if (DebuptyAlignmentShift.deputyPetIsControlledBySpawner(__instance.Owner))
                    //{
                    //    __result = false;
                    //    return false;
                    //}
                    __result = true;
                    return false;
                }
                if (LogicStructures.isPlayerActorBlueprintType(__instance.Owner))
                {
                    __result = true;
                    return false;
                }
                return true;
            }
        }

        //安全区队伍角色列表
        [HarmonyPatch(typeof(Player), nameof(Player.AddCharacterToLists))]
        internal static class BMC_Deputy_MainCharacter_Patch_Remover
        {
            //public static float interval = 0.1f;
            //public static float range = 0.1f;
            //public static bool Prefix(ref UnitEntityData unit)
            //{
            //    if (unit.IsMainCharacter) return true;

            //    if (Game.Instance.Player == null) return true;
            //    if (unit == null) return true;
            //    if (LogicStructures.isPlayerActorBlueprintType(unit))
            //    {
            //        if (!Game.Instance.Player.m_ActiveCompanions.Contains(unit)) Game.Instance.Player.m_ActiveCompanions.Add(unit);
            //        if (!Game.Instance.Player.m_PartyAndPets.Contains(unit)) Game.Instance.Player.m_PartyAndPets.Add(unit);
            //        if (!Game.Instance.Player.m_AllCharacters.Contains(unit)) Game.Instance.Player.m_AllCharacters.Add(unit);
            //        if (!Game.Instance.Player.m_Party.Contains(unit)) Game.Instance.Player.m_Party.Add(unit);
            //        if (Game.Instance.Player.m_RemoteCompanions.Contains(unit)) Game.Instance.Player.m_RemoteCompanions.Remove(unit);

            //    }
            //    return true;
            //}
            public static void Postfix(ref UnitEntityData unit)
            {
                if (unit == null) return;
                if (unit.IsMainCharacter) return;

                if (Game.Instance.Player == null) return;

                if (!Game.Instance.Player.CapitalPartyMode) return;
                if (LogicStructures.isPlayerActorBlueprintType(unit))
                {

                    CompanionState? obj = unit.Get<UnitPartCompanion>()?.State;
                    bool flag = obj == CompanionState.InParty;
                    if (flag)
                    {
                        if (!Game.Instance.Player.m_ActiveCompanions.Contains(unit)) Game.Instance.Player.m_ActiveCompanions.Add(unit);
                        if (!Game.Instance.Player.m_PartyAndPets.Contains(unit)) Game.Instance.Player.m_PartyAndPets.Add(unit);
                        if (!Game.Instance.Player.m_AllCharacters.Contains(unit)) Game.Instance.Player.m_AllCharacters.Add(unit);
                        if (!Game.Instance.Player.m_Party.Contains(unit)) Game.Instance.Player.m_Party.Add(unit);
                        if (Game.Instance.Player.m_RemoteCompanions.Contains(unit)) Game.Instance.Player.m_RemoteCompanions.Remove(unit);
                        if (!unit.IsInGame) unit.IsInGame = true;



                        //if (unit.IsVisibleForPlayer == false) {
                        //    var testVector = Game.Instance.Player.MainCharacter.Value.Position;
                        //    unit.Position = testVector;
                        //    Vector3 position;
                        //    position = new(unit.Position.x + sphere.x, unit.Position.y, unit.Position.z + sphere.z);
                        //    unit.Position = position;
                        //}

                    }
                }
                if (unit.Master == null) return;


                if (unit.IsPet && !unit.Master.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(unit.Master))
                {

                    CompanionState? objpet = unit.Master.Get<UnitPartCompanion>()?.State;
                    bool petflag = objpet == CompanionState.InParty;
                    if (petflag)
                    {
                        //if (!DebuptyAlignmentShift.deputyPetIsControlledBySpawner(unit))
                        if (!Game.Instance.Player.m_PartyAndPets.Contains(unit)) Game.Instance.Player.m_PartyAndPets.Add(unit);

                        if (!unit.IsInGame) unit.IsInGame = true;

                        //if (unit.IsVisibleForPlayer == false)
                        //{
                        //    Vector3 petsPosition;
                        //    unit.Position = unit.Master.Position;
                        //    petsPosition = new(unit.Position.x + sphere.x, unit.Position.y, unit.Position.z + sphere.z);
                        //    unit.Position = petsPosition;
                        //}
                    }
                }
            }
        }

        //区域变更传送
        [HarmonyPatch(typeof(EtudesSystem), nameof(EtudesSystem.OnAreaPartChanged))]
        internal static class BMC_Deputy_MainCharacter_Patch_Loading
        {
            public static void Postfix(EtudesSystem __instance)
            {
                if (Game.Instance.Player == null) return;
                if (!Game.Instance.Player.CapitalPartyMode) return;
                foreach (UnitEntityData item in Game.Instance.Player.m_PartyAndPets)
                {
                    if (item == null || item.IsMainCharacter)
                    {
                        continue;
                    }
                    if (item.IsPet && (item.Master.IsMainCharacter || !LogicStructures.isPlayerActorBlueprintType(item.Master)))
                    {
                        continue;
                    }
                    if (!item.IsPet && !LogicStructures.isPlayerActorBlueprintType(item))
                    {
                        continue;
                    }

                    CompanionState? obj = item.Get<UnitPartCompanion>()?.State;
                    bool flag = obj == CompanionState.InParty;
                    UnitEntityData unit = item;
                    if (!item.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(item) && flag)
                    {
                        unit.Position = Game.Instance.Player.MainCharacter.Value.Position;

                        Vector3 position;
                        position = new(unit.Position.x + sphere.x, unit.Position.y, unit.Position.z + sphere.z);
                        unit.Position = position;

                    }
                    if (unit.Master == null) continue;
                    //if (unit.Master != null && DebuptyAlignmentShift.deputyPetIsControlledBySpawner(unit)) continue;
                    if (unit.IsPet && !unit.Master.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(unit.Master))
                    {
                        obj = unit.Master.Get<UnitPartCompanion>()?.State;
                        flag = obj == CompanionState.InParty;
                        if (flag)
                        {
                            unit.Position = unit.Master.Position;
                            Vector3 petsPosition;
                            petsPosition = new(unit.Position.x + sphere.x, unit.Position.y, unit.Position.z + sphere.z);
                            unit.Position = petsPosition;
                        }
                    }
                }
            }
        }
    }
}
