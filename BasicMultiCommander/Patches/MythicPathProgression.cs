using BasicMultiCommander.Methods;
using HarmonyLib;
using JetBrains.Annotations;
using Kingmaker;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Root;
using Kingmaker.DialogSystem.Blueprints;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
//using Owlcat.Runtime.Core;
using System.Linq;

namespace BasicMultiCommander.Patches
{
    class MythicPathProgression
    {

        [HarmonyPatch(typeof(LevelUpController), nameof(LevelUpController.IsPossibleMythicSelection), MethodType.Getter)]
        internal static class BMC_Deputy_CommanderMythicSelectionPatch
        {
            internal static void Postfix(ref bool __result, LevelUpController __instance)
            {

                if (!__instance.Unit.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(__instance.Unit))
                {
                    __result = true;
                }
            }
        }
        //[HarmonyPatch(typeof(PrerequisiteEtude), nameof(PrerequisiteEtude.CheckInternal))]
        //internal static class BMC_Main_CommanderMythicSwitchSelection_Patch
        //{
        //    internal static void Postfix(ref bool __result, PrerequisiteEtude __instance)
        //    {

        //        if (__instance.OwnerBlueprint.AssetGuidThreadSafe != "daf1235b6217787499c14e4e32142523" || __instance.Etude.deserializedGuid != "067212d277e846a4f9ff96aee6138f0b") return;
        //        var charList = LogicStructures.GetAllCharacterList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();

        //        foreach (var unit in charList)
        //        {
        //            var thisCommanderMythic = unit.Progression.LastMythicClass;
        //            //var thisCommanderMythic = unit.Descriptor.Progression.GetCurrentMythicClass()?.CharacterClass.NameForAcronym;
        //            if (thisCommanderMythic == null) continue;
        //            if (thisCommanderMythic.NameForAcronym.Contains("Dragon"))
        //            {
        //                __result = true;
        //                return;
        //            }
        //        }
        //    }
        //}
        [HarmonyPatch(typeof(Kingmaker.DialogSystem.Blueprints.BlueprintMythicsSettings), nameof(Kingmaker.DialogSystem.Blueprints.BlueprintMythicsSettings.IsMythicClassUnlocked))]
        public static class BMC_Unlockable_Higher_Mythic_Path
        {
            private static void Postfix(ref BlueprintCharacterClass mythicClass, ref bool __result, BlueprintMythicsSettings __instance)
            {
                var playerCharacter = Game.Instance.Player.MainCharacter.Value;
                var commanderMythic = playerCharacter.Descriptor.Progression.MythicLevel;
                var charList = LogicStructures.GetAllCharacterList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
                if (playerCharacter == null || commanderMythic == 0 || charList == null) return;
                foreach (var unit in charList)
                {
                    //if (unit.IsMainCharacter) continue;
                    var thisCommanderMythic = unit.Progression.LastMythicClass;
                    if (thisCommanderMythic == null) continue;
                    if (thisCommanderMythic.NameForAcronym == mythicClass.NameForAcronym)
                    {
                        __result = true;
                        return;
                    }
                    //if (mythicclass.ishighermythic && deputycommandermythic.mythiclevel >= 7 && commandermythic >= 8)
                    //{
                    //    __result = true;
                    //    return;
                    //}
                }
                if (mythicClass.NameForAcronym.Contains("Dragon"))
                {
                    foreach (var unit in charList)
                    {
                        if (unit.IsMainCharacter) continue;
                        var deputyCommanderMythic = unit.Descriptor.Progression;
                        if (deputyCommanderMythic == null) continue;
                        if (mythicClass.IsHigherMythic && deputyCommanderMythic.MythicLevel == 7 && commanderMythic >= 8)
                        {
                            __result = true;
                            return;
                        }
                    }
                }
            }
        }
        [HarmonyPatch(typeof(BlueprintCharacterClass), nameof(BlueprintCharacterClass.MeetsPrerequisites))]
        private static class BMC_Deputy_Commander_Mythic_MeetsPrerequisites_Patch
        {
            private static void Postfix(ref bool __result, BlueprintCharacterClass __instance,UnitDescriptor unit,LevelUpState state)
            {
                if (__instance == null || unit == null || state == null) {
                    return;
                }
                if (unit.IsMainCharacter || !__instance.IsMythic) return;

                if (LogicStructures.isPlayerActorBlueprintType(unit) && !unit.IsMainCharacter)
                {
                    if (__instance == BlueprintRoot.Instance.Progression.MythicCompanionClass && state.NextMythicLevel != 1 && unit.Progression.LastMythicClass != BlueprintRoot.Instance.Progression.MythicCompanionClass)
                    {
                        __result = false;
                        return;
                    }
                    if (!Main.Settings.EnabledMultipleSameMythic && __instance != BlueprintRoot.Instance.Progression.MythicStartingClass && unit.Progression.LastMythicClass != __instance && LogicStructures.GetSameMythicUnitNum(__instance) > 0)
                    {
                        __result = false;
                        return;
                    }
                    if (state.NextMythicLevel >= 8 && __instance == BlueprintRoot.Instance.Progression.MythicStartingClass && unit.Progression.LastMythicClass == __instance)
                    {
                        __result = false;
                        return;
                    }
                    if (__instance.AssetGuidThreadSafe == "53cb5892d19e4c4586c9dd4d6337d943")
                    {
                        __result = false;
                        return;
                    }
                    if (__instance.m_IsHigherMythic)
                    {
                        if (state.NextMythicLevel == 8)
                        {
                            __result = true;
                            return;
                        }
                    }
                    else {
                        if (state.NextMythicLevel == 3)
                        {
                            __result = true;
                            return;
                        }
                    }

                    if (state.NextMythicLevel > 8 && !LogicStructures.MythicLvEightQuestCompleted(unit.Progression.LastMythicClass))
                    {
                        __result = false;
                        return;
                    }
                    if (state.NextMythicLevel == 1 && __instance == BlueprintRoot.Instance.Progression.MythicStartingClass)
                    {
                        __result = true;
                        return;
                    }
                    //if (state.NextMythicLevel == 8 && __instance.m_IsHigherMythic)
                    //{
                    //    __result = true;
                    //    return;
                    //}
                    //if (state.NextMythicLevel == 3 && !__instance.m_IsHigherMythic)
                    //{
                    //    __result = true;
                    //    return;
                    //}

                    //if (unit.Progression.LastMythicClass == BlueprintRoot.Instance.Progression.MythicLegen && __instance == BlueprintRoot.Instance.Progression.MythicStartingClass)
                    //{
                    //    __result = true;
                    //    return;
                    //}

                    if (unit.Progression.LastMythicClass == __instance)
                    {
                        __result = true;

                    }


                }
            }
        }
    }
}
