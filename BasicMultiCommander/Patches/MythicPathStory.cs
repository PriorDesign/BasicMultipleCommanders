using BasicMultiCommander.Methods;
using HarmonyLib;
using Kingmaker;
using Kingmaker.AreaLogic.Etudes;
using Kingmaker.AreaLogic.QuestSystem;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Quests;
using Kingmaker.Blueprints.Root;
using Kingmaker.Designers;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.Designers.EventConditionActionSystem.Evaluators;
using Kingmaker.DialogSystem.Blueprints;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.UI.MVVM._VM.Dialog.Dialog;
using Owlcat.QA.Validation;
//using Owlcat.Runtime.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace BasicMultiCommander.Patches
{
    class MythicPathStory
    {
        //public static Vector3 sphere = 2.5f * UnityEngine.Random.insideUnitSphere;
        //static EtudesSystem etudeSystem;

        //一般神话对话解锁
        [HarmonyPatch(typeof(BlueprintAnswerBase), nameof(BlueprintAnswerBase.IsMythicRequirementSatisfied), MethodType.Getter)]
        internal static class BMC_Deputy_CommanderMythicDialoguePatch_Simple
        {
            internal static void Postfix(BlueprintAnswerBase __instance, ref bool __result)
            {
                if (!Main.Settings.EnabledMemberMythicCommonDialogue) return;
                if (LogicStructures.GetPartyCommanderNumber() <= 1)
                {
                    return;
                }

                if (__instance.MythicRequirement == 0) return;


                var charList = LogicStructures.GetPartyList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
                foreach (var unit in charList)
                {
                    var deputyCommanderMythic = unit.Progression.LastMythicClass;
                    if (deputyCommanderMythic == null) continue;
                    var deputyCommanderMythicPathName = deputyCommanderMythic.NameForAcronym;
                    var requiredMythicPathName = __instance.MythicRequirement.GetEnumDescription().Replace("PlayerIs", "");
                    if (deputyCommanderMythicPathName.Contains(requiredMythicPathName))
                    {
                        __result = true;
                        return;
                    }
                }
            }
        }
        //传奇道途冲突解决
        [HarmonyPatch(typeof(QuestStatus), nameof(QuestStatus.CheckCondition))]
        internal static class BMC_Deputy_Character_Mythic_Legend_QuestStatus_Dangerous
        {
            //internal static bool Prefix(ref bool __result, EtudeStatus __instance)
            //{
            //    if (__instance == null) return true;
            //    if (__instance.Etude == null) return true;
            //    if ((BlueprintFeature)__instance.Owner == null) return true;
            //    BlueprintFeature wayPoint = (BlueprintFeature)__instance.Owner;



            //    return true;

            //}
            public static void Postfix(ref bool __result, QuestStatus __instance)
            {
                if (LogicStructures.GetCommanderNumber() <= 1)
                {
                    return;
                }
                if (__instance == null) return;
                if (__instance.Quest == null) return;
                var requiredMythicPath = __instance.Quest.name;
                if (!Main.Settings.EnabledMemberMythicEtude) return;
                if (requiredMythicPath != "IAmLegend_c5_legend_quest" && requiredMythicPath != "WheresMyDragon_azata_c4_quest") return;
                var charList = LogicStructures.GetAllCharacterList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
                BlueprintCue cueTemp = null;
                try
                {
                    if (__instance.Owner != null) cueTemp = (BlueprintCue)__instance.Owner;
                }
                catch
                {
                    cueTemp = null;
                }
                if (cueTemp != null && cueTemp.AssetGuidThreadSafe == "80e4fe9c800bd6f44b79016bd54a9ca0")
                {


                    if (requiredMythicPath == "IAmLegend_c5_legend_quest" && charList != null)
                    {
                        //BlueprintCue cueTemp = (BlueprintCue)__instance.Owner;
                        //if (cueTemp != null && cueTemp.AssetGuidThreadSafe != "3d75ae2e83db0084396b1a2c71775929") return;
                        foreach (var unit in charList)
                        {
                            var teamMythic = unit.Descriptor.Progression.GetCurrentMythicClass()?.CharacterClass.NameForAcronym.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "");
                            if (teamMythic == null) continue;
                            if (teamMythic.Contains("Angel") && Game.Instance.Player.QuestBook.GetQuest(ResourcesLibrary.TryGetBlueprint<BlueprintQuest>("083019d0509bb1b4d85682338d9e2228")) == null)
                            {
                                Game.Instance.Player.QuestBook.GiveObjective(ResourcesLibrary.TryGetBlueprint<BlueprintQuest>("083019d0509bb1b4d85682338d9e2228").Objectives.ToList()[0]);
                                Game.Instance.Player.QuestBook.GiveObjective(ResourcesLibrary.TryGetBlueprint<BlueprintQuest>("083019d0509bb1b4d85682338d9e2228").Objectives.ToList()[2]);
                                Game.Instance.Player.QuestBook.GiveObjective(ResourcesLibrary.TryGetBlueprint<BlueprintQuest>("083019d0509bb1b4d85682338d9e2228").Objectives.ToList()[3]);

                            }
                            if (!teamMythic.Contains("Legend"))
                            {
                                __result = false;

                            }
                        }

                    }
                    return;
                }
                if (cueTemp != null && cueTemp.AssetGuidThreadSafe == "bac6014274bd49c41bba66aa5bff37d2" && __result == true)
                {


                    if (requiredMythicPath == "WheresMyDragon_azata_c4_quest" && charList != null)
                    {
                        //BlueprintCue cueTemp = (BlueprintCue)__instance.Owner;
                        //if (cueTemp != null && cueTemp.AssetGuidThreadSafe != "3d75ae2e83db0084396b1a2c71775929") return;
                        foreach (var unit in charList)
                        {
                            var teamMythic = unit.Descriptor.Progression.GetCurrentMythicClass()?.CharacterClass.NameForAcronym.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "");
                            if (teamMythic == null) continue;
                            if (teamMythic.Contains("Azata"))
                            {
                                __result = false;

                            }
                        }

                    }
                    return;
                }
                if (cueTemp != null && StaticData.cornoationcueGuid.Contains(cueTemp.AssetGuidThreadSafe))
                {
                    //foreach (var cue2 in coronationCues)
                    //{
                    //    if (cue2.deserializedGuid == cueTemp.AssetGuidThreadSafe)
                    //    {

                    //        if (requiredMythicPath == "PlayerIsLegend" && charList != null)
                    //        {
                    //        }
                    //    }
                    //}

                    if (requiredMythicPath == "IAmLegend_c5_legend_quest" && charList != null)
                    {
                        foreach (var unit in charList)
                        {
                            var teamMythic = unit.Descriptor.Progression.GetCurrentMythicClass()?.CharacterClass.NameForAcronym.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "");
                            if (teamMythic == null) continue;
                            if (!teamMythic.Contains("Legend"))
                            {
                                __result = true;
                                return;
                            }
                        }

                    }
                }
                if (requiredMythicPath == "IAmLegend_c5_legend_quest" && __instance.State == QuestState.None)
                {
                    //foreach (var cue2 in coronationCues)
                    //{
                    //    if (cue2.deserializedGuid == cueTemp.AssetGuidThreadSafe)
                    //    {

                    //        if (requiredMythicPath == "PlayerIsLegend" && charList != null)
                    //        {
                    //        }
                    //    }
                    //}

                    if (requiredMythicPath == "IAmLegend_c5_legend_quest" && charList != null)
                    {
                        foreach (var unit in charList)
                        {
                            var teamMythic = unit.Descriptor.Progression.GetCurrentMythicClass()?.CharacterClass.NameForAcronym.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "");
                            if (teamMythic == null) continue;
                            if (!teamMythic.Contains("Legend"))
                            {
                                __result = true;
                                return;
                            }
                        }

                    }
                }
            }
        }
        //深度神话机制解锁
        [HarmonyPatch(typeof(EtudeStatus), nameof(EtudeStatus.CheckCondition))]
        internal static class BMC_Deputy_Character_Mythic_Quest_Patch_Dangerous
        {
            //internal static bool Prefix(ref bool __result, EtudeStatus __instance)
            //{
            //    if (__instance == null) return true;
            //    if (__instance.Etude == null) return true;
            //    if ((BlueprintFeature)__instance.Owner == null) return true;
            //    BlueprintFeature wayPoint = (BlueprintFeature)__instance.Owner;



            //    return true;

            //}
            public static void Postfix(ref bool __result, EtudeStatus __instance)
            {
                if (LogicStructures.GetCommanderNumber() <= 1)
                {
                    return;
                }
                if (__instance == null) return;
                if (__instance.Etude == null) return;

                if (!Main.Settings.EnabledMemberMythicEtude) return;
                BlueprintCue cueTemp = null;
                BlueprintEtude etudeTemp = null;
                BlueprintAnswer answerTemp = null;
                try
                {
                    if (__instance.Owner!) cueTemp = (BlueprintCue)__instance.Owner;
                }
                catch
                {
                    cueTemp = null;
                }
                try
                {
                    if (__instance.Owner!) etudeTemp = (BlueprintEtude)__instance.Owner;
                }
                catch
                {
                    etudeTemp = null;
                }
                try
                {
                    if (__instance.Owner!) answerTemp = (BlueprintAnswer)__instance.Owner;
                }
                catch
                {
                    answerTemp = null;
                }
                var charList = LogicStructures.GetAllCharacterList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
                List<string> charguidList = [];
                foreach (var charguid in charList) {
                    charguidList.Append(charguid.UniqueId);
                }
                //if (__instance.Etude.NameSafe().Contains("Romance_Active") || __instance.Etude.NameSafe().Contains("Romance_PreStart") || __instance.Etude.NameSafe().Contains("Romance_PreStart"))
                if (answerTemp! && StaticData.romanceGuid.Contains(__instance.Etude.AssetGuidThreadSafe)  && Main.Settings.CharacterLinkedRomance )
                {

                    //if (
                    //    charguidList.Contains(CharacterLinkedRomance.CamelliaRomance) ||
                    //    charguidList.Contains(CharacterLinkedRomance.DaeranRomance) ||
                    //    charguidList.Contains(CharacterLinkedRomance.LannRomance) ||
                    //    charguidList.Contains(CharacterLinkedRomance.UlbrigRomance) ||
                    //    charguidList.Contains(CharacterLinkedRomance.SosielRomance) ||
                    //    charguidList.Contains(CharacterLinkedRomance.ArueshalaeRomance) ||
                    //    charguidList.Contains(CharacterLinkedRomance.WenduagRomance)
                    //    )
                    //{ }

                    switch (__instance.Etude.AssetGuidThreadSafe)
                    {

                        case "fe6ee37b2aa394e4eaa51208cf7d7f86":
                            if (CharacterLinkedRomance.CamelliaRomance == "") break;
                            if (LogicStructures.TargetIsInGame(charguidList, CharacterLinkedRomance.CamelliaRomance)) break;
                            if (CharacterLinkedRomance.CamelliaRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id) __result = false;
                            return;
                        case "e49702f590611644580f09c8f9ef0e5b":
                            if (CharacterLinkedRomance.CamelliaRomance == "") break;
                            if (LogicStructures.TargetIsInGame(charguidList, CharacterLinkedRomance.CamelliaRomance)) break;
                            if (CharacterLinkedRomance.CamelliaRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id) __result = false;
                            return;
                        case "8541453b31379964e834cf2309444388":
                            if (CharacterLinkedRomance.DaeranRomance == "") break;
                            if (LogicStructures.TargetIsInGame(charguidList, CharacterLinkedRomance.DaeranRomance)) break;
                            if (CharacterLinkedRomance.DaeranRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id) __result = false;
                            return;
                        case "af394a43ba7c1314bb31aea74a5e4c0e":
                            if (CharacterLinkedRomance.LannRomance == "") break;
                            if (LogicStructures.TargetIsInGame(charguidList, CharacterLinkedRomance.LannRomance)) break;
                            if (CharacterLinkedRomance.LannRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id) __result = false;
                            return;
                        case "14650554734f4adc81af61e442fd628b":
                            if (CharacterLinkedRomance.UlbrigRomance == "") break;
                            if (LogicStructures.TargetIsInGame(charguidList, CharacterLinkedRomance.UlbrigRomance)) break;
                            if (CharacterLinkedRomance.UlbrigRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id) __result = false;
                            return;
                        case "efb130b8a22c9534ca40a1e41ef8e931":
                            if (CharacterLinkedRomance.SosielRomance == "") break;
                            if (LogicStructures.TargetIsInGame(charguidList, CharacterLinkedRomance.SosielRomance)) break;
                            if (CharacterLinkedRomance.SosielRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id) __result = false;
                            return;
                        case "6a3fdd0758fe78d4aa2c3b26d7614fbc":
                            if (CharacterLinkedRomance.ArueshalaeRomance == "") break;
                            if (LogicStructures.TargetIsInGame(charguidList, CharacterLinkedRomance.ArueshalaeRomance)) break;
                            if (CharacterLinkedRomance.ArueshalaeRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id) __result = false;
                            return;
                        case "33c4c2f66f2461e4993df21566252079":
                            if (CharacterLinkedRomance.WenduagRomance == "") break;
                            if (LogicStructures.TargetIsInGame(charguidList, CharacterLinkedRomance.WenduagRomance)) break;
                            if (CharacterLinkedRomance.WenduagRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id) __result = false;
                            return;
                        default:
                            break;
                        
                    }
                }
                if (etudeTemp != null && etudeTemp.Parent != null && etudeTemp.name.Contains("Weather") && etudeTemp.Parent.deserializedGuid == "4f78e1fee73c468ea09109c6bb609d40")
                {
                    var settedMythicPathName = Main.Settings.CurrentMythicWeather;
                    var playerCharacter = Game.Instance.Player.MainCharacter.Value;
                    var commanderMythic = playerCharacter.Progression.LastMythicClass?.NameForAcronym;
                    var requiredMythicPathName = __instance.Etude.name.Replace("PlayerIs", "");
                    if (settedMythicPathName == "") {
                        //if (!commanderMythic.Contains(requiredMythicPathName))
                        //{
                        //    __result = false;
                        //    return;

                        //}
                    }
                    else
                    {
                        if (!StaticData.mythicGuidEtudes.Contains(__instance.Etude.AssetGuidThreadSafe)) return;
                        
                        bool emptyMythicMark = true;
                        
                        
                        //int trueNum = 0;
                        //int falseNum = 0;
                        foreach (var unit in charList)
                        {

                            if (!unit.IsMainCharacter && unit.IsImportedCustomCompanion) continue;
                            var deputyCommanderMythic = unit.Progression.LastMythicClass;
                            if (deputyCommanderMythic == null) continue;
                            //if (!__instance.Etude.name.Contains("PlayerIs")|| !mythicPathGuidEtudes.Contains(__instance.Etude. Parent.guid)) continue;
                            var deputyCommanderMythicPathName = deputyCommanderMythic.NameForAcronym;

                            if (settedMythicPathName == deputyCommanderMythicPathName)
                            {
                                emptyMythicMark = false;
                                break;
                            }
                        }
                        if (!emptyMythicMark)
                        {
                            foreach (var unit in charList)
                            {

                                if (!unit.IsMainCharacter && unit.IsImportedCustomCompanion) continue;
                                var deputyCommanderMythic = unit.Progression.LastMythicClass;
                                if (deputyCommanderMythic == null) continue;
                                //if (!__instance.Etude.name.Contains("PlayerIs")|| !mythicPathGuidEtudes.Contains(__instance.Etude. Parent.guid)) continue;
                                var deputyCommanderMythicPathName = deputyCommanderMythic.NameForAcronym;

                                if (deputyCommanderMythicPathName.Contains(requiredMythicPathName))
                                {
                                    if (settedMythicPathName == deputyCommanderMythicPathName)
                                    {
                                        __result = true;
                                        return;
                                    }
                                    else
                                    {
                                        __result = false;
                                        return;
                                    }


                                }
                            }
                        }
                        else {
                            Main.Settings.CurrentMythicWeather = "";
                            //if (!commanderMythic.Contains(requiredMythicPathName))
                            //{
                            //    __result = false;
                            //    return;
                            //}
                            //else
                            //{
                            //    __result = true;
                            //    return;
                            //}
                        }
                        //if (trueNum == 0)
                        //{

                        //}
                        //else {
                        //    return;
                        //}
                        
                        //foreach (var unit in charList)
                        //{

                        //    if (unit.IsMainCharacter || !unit.IsImportedCustomCompanion) continue;
                        //    var deputyCommanderMythic = unit.Progression.LastMythicClass;
                        //    if (deputyCommanderMythic == null) continue;
                        //    //if (!__instance.Etude.name.Contains("PlayerIs")|| !mythicPathGuidEtudes.Contains(__instance.Etude. Parent.guid)) continue;
                        //    var deputyCommanderMythicPathName = deputyCommanderMythic.NameForAcronym;
                        //    if (unit.IsImportedCustomCompanion && deputyCommanderMythicPathName.Contains(requiredMythicPathName))
                        //    {
                        //        __result = false;
                        //        return;
                        //    }
                        //}

                    }
                    
                }

                if (__instance.Owner != null && (__instance.Owner.name == "MythicPathFailed" || __instance.Owner.name == "MythicPathChanged" || __instance.Owner.name == "Legend_Begins" || (cueTemp != null && (cueTemp.AssetGuidThreadSafe == "3d75ae2e83db0084396b1a2c71775929" || cueTemp.AssetGuidThreadSafe == "1dd04b5a707c9474faafd189354d944e"))))
                {
                    if (cueTemp != null && (cueTemp.AssetGuidThreadSafe == "3d75ae2e83db0084396b1a2c71775929" || cueTemp.AssetGuidThreadSafe == "1dd04b5a707c9474faafd189354d944e"))
                    {

                        var requiredMythicPath = __instance.Etude.name;
                        if (requiredMythicPath == "PlayerIsLegend" && charList != null)
                        {

                            foreach (var unit in charList)
                            {
                                var teamMythic = unit.Descriptor.Progression.GetCurrentMythicClass()?.CharacterClass.NameForAcronym.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "").Replace("Fake", "");
                                if (teamMythic == null) continue;
                                if (!teamMythic.Contains("Legend"))
                                {
                                    __result = false;
                                    return;
                                }
                            }

                        }
                    }
                    else
                    {

                        var requiredMythicPath = __instance.Etude.name;

                        if (charList != null)
                        {
                            foreach (var unit in charList)
                            {
                                var teamMythic = unit.Progression.LastMythicClass?.NameForAcronym.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "").Replace("Fake", "");
                                if (teamMythic == null) continue;

                                if (requiredMythicPath.Contains(teamMythic))
                                {
                                    __result = false;
                                    return;
                                }
                            }
                        }
                    }
                }

                else
                {

                    if (!StaticData.mythicGuidEtudes.Contains(__instance.Etude.AssetGuidThreadSafe)) return;
                    var playerCharacter = Game.Instance.Player.MainCharacter.Value;
                    var commanderMythic = playerCharacter.Progression.LastMythicClass?.NameForAcronym;
                    var requiredMythicPathName = __instance.Etude.name.Replace("PlayerIs", "");
                    if (commanderMythic != null && commanderMythic.Contains(requiredMythicPathName))
                    {
                        return;
                    }

                    foreach (var unit in charList)
                    {

                        if (unit.IsMainCharacter || unit.IsImportedCustomCompanion) continue;
                        var deputyCommanderMythic = unit.Progression.LastMythicClass;
                        if (deputyCommanderMythic == null) continue;
                        //if (!__instance.Etude.name.Contains("PlayerIs")|| !mythicPathGuidEtudes.Contains(__instance.Etude. Parent.guid)) continue;
                        var deputyCommanderMythicPathName = deputyCommanderMythic.NameForAcronym;

                        if (deputyCommanderMythicPathName.Contains(requiredMythicPathName))
                        {
                            __result = true;
                            return;
                        }
                    }
                    foreach (var unit in charList)
                    {

                        if (unit.IsMainCharacter || !unit.IsImportedCustomCompanion) continue;
                        var deputyCommanderMythic = unit.Progression.LastMythicClass;
                        if (deputyCommanderMythic == null) continue;
                        //if (!__instance.Etude.name.Contains("PlayerIs")|| !mythicPathGuidEtudes.Contains(__instance.Etude. Parent.guid)) continue;
                        var deputyCommanderMythicPathName = deputyCommanderMythic.NameForAcronym;
                        if (unit.IsImportedCustomCompanion && deputyCommanderMythicPathName.Contains(requiredMythicPathName))
                        {
                            __result = false;
                            return;
                        }
                    }
                    return;
                }
            }
        }

        //副角色神话道途视作主角色持有，并完全开启机制
        [HarmonyPatch(typeof(UnitClass), nameof(UnitClass.CheckCondition))]
        internal static class BMC_Deputy_Character_Mythic_Quest_Patch_Dangerously_DEEP
        {
            public static void Postfix(ref bool __result, UnitClass __instance)
            {
                if (LogicStructures.GetCrusadeCommanderNumber() <= 1)
                {
                    return;
                }
                if (!Main.Settings.EnabledMemberMythicEtude) return;
                if (!Main.Settings.EnabledMemberMythicEtudeDeep) return;
                if (__instance == null) return;
                if (__instance.Unit == null) return;
                if (__instance.Class == null) return;
                if (!__instance.Unit.name.Contains("PlayerCharacter")) return;
                if (!__instance.Class.IsMythic) return;
                if (__result == true && !__instance.Class.NameForAcronym.Contains("Devil") && !__instance.Not) return;
                //

                var charList = LogicStructures.GetAllCharacterList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
                if (__result == true && __instance.Class.NameForAcronym.Contains("Devil") && __instance.Not)
                {
                    foreach (var unit in charList)
                    {
                        if (!unit.IsMainCharacter && !unit.Descriptor.m_IsEssentialForGame) continue;
                        var deputyCommanderMythic = unit.Progression.LastMythicClass;
                        if (deputyCommanderMythic == null) continue;
                        if (__instance.Owner != null && __instance.Owner.name.Contains("PlayerIs"))
                        {

                            var teamLastMythic = deputyCommanderMythic.NameForAcronym.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "");

                            if (!deputyCommanderMythic.NameForAcronym.Contains("Devil") && __instance.Owner.name.Contains(teamLastMythic))
                            {
                                __result = false;
                                break;
                            }

                        }
                        else
                        {
                            if (!deputyCommanderMythic.NameForAcronym.Contains("Devil"))
                            {
                                __result = false;
                                break;
                            }
                        }
                    }
                    return;
                }

                BlueprintCharacterClass checkedClass = __instance.Class;
                try
                {
                    BlueprintCharacterClass requiresFrom = (BlueprintCharacterClass)__instance.Owner;
                    if (requiresFrom != null && checkedClass.AssetGuidThreadSafe == requiresFrom.AssetGuidThreadSafe) return;
                }
                catch { }
                //try
                //{
                //    BlueprintEtude requiresFrom = (BlueprintEtude)__instance.Owner;

                //    if (__instance.Class.NameForAcronym == "DevilMythicClass" && __instance.Not == true && ( requiresFrom.name.Contains("PlayerIsAeon")|| requiresFrom.name.Contains("PlayerIsAzata")))
                //    {
                //        foreach (var unit in charList)
                //        {
                //            if (!unit.IsMainCharacter && !unit.Descriptor.m_IsEssentialForGame) continue;
                //            var teamMythic = unit.Descriptor.Progression.GetCurrentMythicClass()?.CharacterClass.NameForAcronym.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "");

                //            var deputyCommanderMythic = unit.Descriptor.Progression.GetCurrentMythicClass();
                //            if (deputyCommanderMythic == null) continue;
                //            var requiredMythicPath = unit.Descriptor.Progression.GetClassData(__instance.Class);
                //            if (requiresFrom.name.Contains(teamMythic))
                //            {
                //                __result = false;
                //                return;
                //            }
                //        }

                //    }

                //}
                //catch { }
                foreach (var unit in charList)
                {
                    if (unit.IsMainCharacter || !unit.Descriptor.m_IsEssentialForGame) continue;
                    var deputyCommanderMythic = unit.Progression.LastMythicClass;


                    var requiredMythicPath = unit.Descriptor.Progression.GetClassData(__instance.Class);
                    if (requiredMythicPath == null) continue;
                    if (requiredMythicPath.CharacterClass.NameForAcronym == "LegendClass")
                    {
                        deputyCommanderMythic = unit.Descriptor.Progression.GetCurrentMythicClass().CharacterClass;
                    }

                    if (deputyCommanderMythic == null) continue;
                    if (requiredMythicPath.CharacterClass == deputyCommanderMythic)
                    {
                        __result = true;
                        return;
                    }
                }
            }
        }


    }
}
