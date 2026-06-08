using BasicMultiCommander.Methods;
using HarmonyLib;
using JetBrains.Annotations;
using Kingmaker;
using Kingmaker.AreaLogic.Capital;
using Kingmaker.AreaLogic.Etudes;
using Kingmaker.AreaLogic.QuestSystem;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Area;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Quests;
using Kingmaker.Blueprints.Root;
using Kingmaker.Controllers.Dialog;
using Kingmaker.Designers;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.Designers.EventConditionActionSystem.Evaluators;
using Kingmaker.Designers.EventConditionActionSystem.Events;
using Kingmaker.DialogSystem;
using Kingmaker.DialogSystem.Blueprints;
using Kingmaker.DLC;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Persistence;
using Kingmaker.EntitySystem.Persistence.JsonUtility;
using Kingmaker.EntitySystem.Persistence.Versioning.PlayerUpgraderOnlyActions;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.Settings;
using Kingmaker.UI.Common;
using Kingmaker.UI.Dialog;
using Kingmaker.UI.Group;
using Kingmaker.UI.MVVM._VM.Dialog.Dialog;
using Kingmaker.UI.MVVM._VM.Tooltip.Templates;
using Kingmaker.UI.ServiceWindow.CharacterScreen;
using Kingmaker.UI.Tooltip;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using Kingmaker.View;
using Kingmaker.View.Spawners;
using Kingmaker.Visual.CharacterSystem;
using Owlcat.QA.Validation;
//using Owlcat.Runtime.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI.Extensions;
using UnityEngine.UIElements;
using static Kingmaker.AreaLogic.Etudes.EtudesSystem;
using static Kingmaker.Blueprints.Area.FactHolder;
using static Kingmaker.Blueprints.Root.CheatRoot;
using static Kingmaker.Designers.EventConditionActionSystem.Actions.RecruitInactive;
using static Kingmaker.UnitLogic.Mechanics.Properties.StatValueGetter;
using static LayoutRedirectElement;


namespace BasicMultiCommander
{
    static class GameDataPatch
    {
        

        


        //public class BMC_Patched_Capital_Logic : CapitalCompanionLogic
        //{

        //}

        //[HarmonyPatch(typeof(GroupController), nameof(GroupController.WithRemote), MethodType.Getter)]
        //internal static class BMC_Deputy_MainCharacter_Patch
        //{
        //    public static bool Prefix(ref bool __result, GroupController __instance)
        //    {
        //        if (__instance. .Owner.IsMainCharacter) return true;

        //        if (__instance == null) return true;

        //        if (LogicStructures.isPlayerActorBlueprintType(__instance.Owner))
        //        {
        //            __result = true;
        //            return false;
        //        }
        //        return true;
        //    }
        //}


        //[HarmonyPatch(typeof(CompanionSpawner), nameof(CompanionSpawner.CanControlCompanion),MethodType.Getter)]
        //internal static class BMC_Deputy_Pets_Spawner_Control_Patch_Unit
        //{
        //    public static bool Prefix( ref bool __result, CompanionSpawner __instance)
        //    {
        //        if (unit == null)
        //        {
        //            __result = false;
        //        }
        //        UnitEntityData master = unit.Master;
        //        if ((object)master != null && !master.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(master))
        //        {
        //            if (DebuptyAlignmentShift.deputyPetIsControlledBySpawner(unit))
        //            {
        //                __result = true;
        //                return false;
        //            }
        //            return true;
        //        }
        //        else  {
        //            return true;
        //        }
               
        //    }
        //}


        //[HarmonyPatch(typeof(CapitalCompanionLogic), nameof(CapitalCompanionLogic.PlaceAllCompanions))]
        //internal static class BMC_Deputy_CharacterPets_With_Spwaner
        //{
        //    public static bool Prefix( CapitalCompanionLogic __instance)
        //    {
        //        _ = CapitalCompanionSpawnPlace.Instances;
        //        Game.Instance.LoadedAreaState.Settings.SetCapitalMode(value: true);
        //        foreach (UnitEntityData item in Game.Instance.Player.RemoteCompanions.ToTempList())
        //        {
        //            if (item == null || item.IsMainCharacter)
        //            {
        //                continue;
        //            }

        //            UnitEntityData master = item.Master;
        //            if (!(master != null) || (!master.IsMainCharacter && !(master.Get<UnitPartCompanion>()?.GetCurrentSpawner())) && !LogicStructures.isPlayerActorBlueprintType(master))
        //            {
        //                UnitPartCompanion unitPartCompanion = item.Get<UnitPartCompanion>();
        //                if (unitPartCompanion == null || (!unitPartCompanion.GetCurrentSpawner() && !unitPartCompanion.IgnoresSpawners))
        //                {
        //                    item.IsInGame = false;
        //                }
        //            }
        //        }

        //        if (Game.Instance.UI.SelectionManager != null)
        //        {
        //            Game.Instance.UI.SelectionManager.SelectUnit(Game.Instance.Player.MainCharacter.Value.View, single: true, sendEvent: true, ask: false);
        //        }
        //        return false;
        //    }
        //}
        //[HarmonyPatch(typeof(CompanionSpawner), nameof(CompanionSpawner.GetMyCompanion))]
        //internal static class BMC_Deputy_MainCharacter_Patch_Spawner
        //{
        //    public static bool Prefix(ref UnitEntityData __result, CompanionSpawner __instance)
        //    {


        //        var allCharList = Game.Instance.Player.AllCharacters;
        //        allCharList.Remove(Game.Instance.Player.MainCharacter);
        //        UnitEntityData tempEntity = allCharList.SingleItem((UnitEntityData u) => u.Blueprint.CheckEqualsWithPrototype(__instance.Blueprint));
        //        allCharList.Remove(tempEntity);
        //        UnitEntityData unitEntityData = allCharList.SingleItem((UnitEntityData u) => u.Blueprint.AssetGuidThreadSafe == LogicStructures.playerActorBlueprintTypeGUID);
        //        if (__instance.m_UsePet)
        //        {
        //            unitEntityData = unitEntityData?.Pets.FirstItem((EntityPartRef<UnitEntityData, UnitPartPet> i) => i.EntityPart?.Type == __instance.m_PetType).Entity;
        //        }
        //        __result = unitEntityData;

        //        return false;
        //        //if (__instance.IsMainCharacter) return true;

        //        //if (__instance == null) return true;
        //        //UnitEntityData master = __instance.Master;
        //        //if ((object)master != null && !master.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(master))
        //        //{
        //        //    __result = true;
        //        //    return false;
        //        //}
        //        //if (LogicStructures.isPlayerActorBlueprintType(__instance))
        //        //{
        //        //    __result = true;
        //        //    return false;
        //        //}
        //        //return true;
        //    }
        //}

        //[HarmonyPatch(typeof(Player), nameof(Player.OnAreaLoaded))]
        //internal static class BMC_Deputy_MainCharacter_Patch_Placer
        //{

        //    public static void Postfix(Player __instance)
        //    {
        //        if (__instance == null)
        //        {
        //            return;
        //        }
        //        if (true)
        //        {
        //            return;
        //        }
        //        if (__instance.CapitalPartyMode)
        //        {

        //            foreach (UnitEntityData item in Game.Instance.Player.AllCharacters.ToTempList())
        //            {
        //                if (item == null || item.IsMainCharacter)
        //                {
        //                    continue;
        //                }
        //                CompanionState? obj = item.Get<UnitPartCompanion>()?.State;
        //                bool flag = obj == CompanionState.InParty;
        //                UnitEntityData unit = item;
        //                if (!item.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(item) && flag)
        //                {


        //                    unit.Position = Game.Instance.Player.MainCharacter.Value.Position;

        //                    Vector3 position;
        //                    position = new(unit.Position.x + sphere.x, unit.Position.y, unit.Position.z + sphere.z);
        //                    unit.Position = position;

        //                }

        //                if (unit.IsPet && !unit.Master.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(unit.Master))
        //                {
        //                    obj = unit.Master.Get<UnitPartCompanion>()?.State;
        //                    flag = obj == CompanionState.InParty;
        //                    if (flag)
        //                    {
        //                        if (!Game.Instance.Player.m_PartyAndPets.Contains(unit)) Game.Instance.Player.m_PartyAndPets.Add(unit);
        //                        if (!unit.IsInGame) unit.IsInGame = true;
        //                        unit.Position = unit.Master.Position;
        //                        Vector3 petsPosition;
        //                        petsPosition = new(unit.Position.x + sphere.x, unit.Position.y, unit.Position.z + sphere.z);
        //                        unit.Position = petsPosition;
        //                    }
        //                }
        //            }



        //        }

        //    }
        //}


        //[HarmonyPatch(typeof(EtudeStatus), nameof(EtudeStatus.CheckCondition))]
        //internal static class BMC_Deputy_Character_Mythic_Quest_Patch_Dangerous_old
        //{
        //    public static void Postfix(ref bool __result, EtudeStatus __instance)
        //    {
        //        if (LogicStructures.GetPartyCommanderNumber() <= 1)
        //        {
        //            return;
        //        }
        //        if (__instance == null) return;
        //        if (__instance.Etude == null) return;
        //        if (!mythicGuidEtudes.Contains(__instance.Etude.AssetGuidThreadSafe)) return;
        //        if (!Main.Settings.EnabledMemberMythicEtude) return;

        //        var charList = LogicStructures.GetPartyList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
        //        foreach (var unit in charList)
        //        {

        //            if (unit.IsMainCharacter || !unit.Descriptor.m_IsEssentialForGame) continue;
        //            var deputyCommanderMythic = unit.Descriptor.Progression.GetCurrentMythicClass();
        //            if (deputyCommanderMythic == null) continue;
        //            var deputyCommanderMythicPathName = deputyCommanderMythic.CharacterClass.NameForAcronym;
        //            var requiredMythicPathName = __instance.Etude.name.Replace("PlayerIs", "");
        //            if (deputyCommanderMythicPathName.Contains(requiredMythicPathName))
        //            {
        //                __result = true;
        //                return;
        //            }
        //        }
        //    }
        //}
        //[HarmonyPatch(typeof(SaveImportSettings), nameof(SaveImportSettings.DoImport))]
        //internal static class BMC_Deputy_Character_DLCONE_Importer
        //{
        //    public static void Postfix(SaveImportSettings __instance)
        //    {
        //        if (__instance.Companions)
        //        {
        //            foreach (UnitEntityData item in from u in crossSceneState.AllEntityData.OfType<UnitEntityData>()
        //                                            where u.Blueprint.GetComponent<UnitIsStoryCompanion>() != null
        //                                            select u)
        //            {
        //                UnitEntityData unitEntityData2 = CreateUnitFromSave(item);
        //                unitEntityData2.Ensure<UnitPartCompanion>().SetState(CompanionState.ExCompanion);
        //                unitEntityData2.IsInGame = false;
        //            }
        //        }

        //        var charList = LogicStructures.GetPartyList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
        //        foreach (var unit in charList)
        //        {

        //            if (unit.IsMainCharacter || !unit.Descriptor.m_IsEssentialForGame) continue;
        //            var deputyCommanderMythic = unit.Descriptor.Progression.GetCurrentMythicClass();
        //            if (deputyCommanderMythic == null) continue;
        //            var deputyCommanderMythicPathName = deputyCommanderMythic.CharacterClass.NameForAcronym;
        //            var requiredMythicPathName = __instance.Etude.name.Replace("PlayerIs", "");
        //            if (deputyCommanderMythicPathName.Contains(requiredMythicPathName))
        //            {
        //                __result = true;
        //                return;
        //            }
        //        }
        //    }
        //}

        //[HarmonyPatch(typeof(AddFactIfEtudePlaying), nameof(AddFactIfEtudePlaying.RunActionOverride))]
        //internal static class BMC_Deputy_Character_Mythic_Quest_Reward_Companion_Patch
        //{
        //    public static bool Prefix( AddFactIfEtudePlaying __instance)
        //    {
        //        if (LogicStructures.GetPartyCommanderNumber() <= 1)
        //        {
        //            return true;
        //        }
        //        if (__instance == null) return true;
        //        if (__instance.Etude == null) return true;
        //        if (!mythicGuidEtudes.Contains(__instance.Etude.AssetGuidThreadSafe)) return true;
        //        if (!Main.Settings.EnabledMemberMythicEtude) return true;

        //        UnitEntityData deputySpeaker = null; 
        //        var charList = LogicStructures.GetPartyList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
        //        foreach (var unit in charList)
        //        {

        //            if (unit.IsMainCharacter || !unit.Descriptor.m_IsEssentialForGame) continue;
        //            var deputyCommanderMythic = unit.Descriptor.Progression.GetCurrentMythicClass();
        //            if (deputyCommanderMythic == null) continue;
        //            var deputyCommanderMythicPathName = deputyCommanderMythic.CharacterClass.NameForAcronym;
        //            var requiredMythicPathName = __instance.Etude.name.Replace("PlayerIs", "");
        //            if (deputyCommanderMythicPathName.Contains(requiredMythicPathName))
        //            {

        //                return true;
        //            }
        //        }
        //        if (deputySpeaker == null) { return true; }
        //        else
        //        {
        //            Etude fact = Game.Instance.Player.EtudesSystem.Etudes.GetFact(__instance.Etude);
        //            if (fact == null || !fact.IsPlaying)
        //            {
        //                return true;
        //            }

        //            switch (__instance.m_Target)
        //            {
        //                case AddFactIfEtudePlaying.TargetType.MainCharacter:
        //                    Game.Instance.Player.MainCharacter.Value.AddFact(__instance.Fact);
        //                    break;
        //                case AddFactIfEtudePlaying.TargetType.AllCompanions:
        //                    {
        //                        foreach (UnitEntityData allCharacter in Game.Instance.Player.AllCharacters)
        //                        {
        //                            allCharacter.AddFact(__instance.Fact);
        //                        }

        //                        break;
        //                    }
        //                default:
        //                    throw new ArgumentOutOfRangeException();
        //            }
        //            return false;

        //        }
        //        return true;
        //    }
        //}

        //[HarmonyPatch(typeof(EtudePlayTrigger), nameof(EtudePlayTrigger.MaybeTrigger))]
        //internal static class BMC_Deputy_Character_Mythic_Quest_Reward_Companion_Patch
        //{
        //    public static bool Prefix(EtudePlayTrigger __instance)
        //    {
        //        if (LogicStructures.GetPartyCommanderNumber() <= 1)
        //        {
        //            return true;
        //        }
        //        if (__instance == null) return true;
        //        if (__instance.Conditions == null) return true;
        //        if (__instance.Actions == null) return true;
        //        var etudeFact = (Etude)__instance.Fact;
        //        if (!mythicGuidEtudes.Contains((Etude)__instance.Fact.AssetGuidThreadSafe)) return true;
        //        if (!Main.Settings.EnabledMemberMythicEtude) return true;

        //        UnitEntityData deputySpeaker = null;
        //        var charList = LogicStructures.GetPartyList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
        //        foreach (var unit in charList)
        //        {

        //            if (unit.IsMainCharacter || !unit.Descriptor.m_IsEssentialForGame) continue;
        //            var deputyCommanderMythic = unit.Descriptor.Progression.GetCurrentMythicClass();
        //            if (deputyCommanderMythic == null) continue;
        //            var deputyCommanderMythicPathName = deputyCommanderMythic.CharacterClass.NameForAcronym;
        //            var requiredMythicPathName = __instance.Etude.name.Replace("PlayerIs", "");
        //            if (deputyCommanderMythicPathName.Contains(requiredMythicPathName))
        //            {

        //                return true;
        //            }
        //        }
        //        if (deputySpeaker == null) { return true; }
        //        else
        //        {
        //            Etude fact = Game.Instance.Player.EtudesSystem.Etudes.GetFact(__instance.Etude);
        //            if (fact == null || !fact.IsPlaying)
        //            {
        //                return true;
        //            }

        //            switch (__instance.m_Target)
        //            {
        //                case AddFactIfEtudePlaying.TargetType.MainCharacter:
        //                    Game.Instance.Player.MainCharacter.Value.AddFact(__instance.Fact);
        //                    break;
        //                case AddFactIfEtudePlaying.TargetType.AllCompanions:
        //                    {
        //                        foreach (UnitEntityData allCharacter in Game.Instance.Player.AllCharacters)
        //                        {
        //                            allCharacter.AddFact(__instance.Fact);
        //                        }

        //                        break;
        //                    }
        //                default:
        //                    throw new ArgumentOutOfRangeException();
        //            }
        //            return false;

        //        }
        //        return true;
        //    }
        //}


        //[HarmonyPatch(typeof(DialogCurrentPart), nameof(DialogCurrentPart.Fill))]
        //internal static class BMC_Deputy_Character_Dialogue_CUE_Patch_Dangerously_DEEP
        //{
        //    public static bool Prefix(ref CueShowData cueData, Kingmaker.UI.Dialog.DialogCurrentPart __instance)
        //    {
        //        if (!Main.Settings.EnabledMemberMythicEtude) return true;
        //        if (!Main.Settings.EnabledMemberMythicEtudeDeep) return true;
        //        if (LogicStructures.GetPartyCommanderNumber() <= 1)
        //        {
        //            return true;
        //        }
        //        var cue = cueData.Cue;
        //        if (__instance == null) return true;
        //        if (cue == null) return true;
        //        if (cue.ElementsArray.Count == 0) return true;
        //        UnitEntityData deputySpeaker = null;
        //        foreach (var condition in cue.ElementsArray)
        //        {
        //            UnitClass classCondition = condition as UnitClass;
        //            EtudeStatus etudeStatus = condition as EtudeStatus;
        //            if (classCondition == null && etudeStatus == null) continue;
        //            if (classCondition != null) if(!classCondition.Unit.name.Contains("PlayerCharacter")) continue;
        //            if (etudeStatus != null) if (!classCondition.Unit.name.Contains("PlayerCharacter")) continue;


        //            if (!mythicClassGuid.Contains(classCondition.Class.AssetGuidThreadSafe)) continue;
        //            var commanderMythic = Game.Instance.Player.MainCharacter.Value.Descriptor.Progression.GetCurrentMythicClass();
        //            if (classCondition.Class.AssetGuidThreadSafe == commanderMythic.CharacterClass.AssetGuidThreadSafe) continue;
        //            var charList = LogicStructures.GetPartyList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
        //            foreach (var unit in charList)
        //            {
        //                if (unit.IsMainCharacter || !unit.Descriptor.m_IsEssentialForGame) continue;
        //                var deputyCommanderMythic = unit.Descriptor.Progression.GetCurrentMythicClass();
        //                if (deputyCommanderMythic == null) continue;
        //                var requiredMythicPath = classCondition.Class.AssetGuidThreadSafe;
        //                if (requiredMythicPath == deputyCommanderMythic.CharacterClass.AssetGuidThreadSafe)
        //                {
        //                    deputySpeaker = unit;
        //                    //__result = true;

        //                }
        //            }

        //        }
        //        if (deputySpeaker == null) return true;

        //        __instance.ClearNextCuesButtons();
        //        List<SkillCheckResult> list = ((cueData.SkillChecks.Count > 0 && (bool)SettingsRoot.Game.Dialogs.ShowSkillcheckResult) ? cueData.SkillChecks : null);
        //        if (__instance.m_Tooltip == null)
        //        {
        //            __instance.m_Tooltip = __instance.DialogPhrase.GetComponent<TooltipTriggerGlossary>();
        //        }

        //        if (__instance.m_Tooltip != null)
        //        {
        //            __instance.m_Tooltip.SkillCheckData = list;
        //        }

        //        Kingmaker.Controllers.Dialog.DialogController dialogController = Game.Instance.DialogController;
        //        string text = ((dialogController.CurrentSpeakerBlueprint != null) ? string.Format(DialogFormats.SpeakerFormatWithColorName, UIUtility.SkillCheckText(list), ColorUtility.ToHtmlStringRGB(dialogController.CurrentSpeakerBlueprint.Color), deputySpeaker.CharacterName, dialogController.CurrentCue.DisplayText, ColorUtility.ToHtmlStringRGB(BlueprintRoot.Instance.UIRoot.DialogColors.Narrator)) : string.Format(DialogFormats.SpeakerFormatWithoutName, dialogController.CurrentCue.DisplayText, UIUtility.SkillCheckText(list)));
        //        if (BuildModeUtility.IsShowDevComment && !string.IsNullOrEmpty(dialogController.CurrentCue.Comment))
        //        {
        //            text = text + "\n<color=#" + ColorUtility.ToHtmlStringRGB(new UnityEngine.Color(0.192156866f, 0.239215687f, 0.8509804f)) + ">[DevComment]:" + dialogController.CurrentCue.Comment + "</color>";
        //        }

        //        __instance.DialogPhrase.text = text;
        //        __instance.ScheduleNotificationUpdate(cueData);
        //        int num = 1;
        //        foreach (BlueprintAnswer answer in dialogController.Answers)
        //        {
        //            if (!answer.IsSystem())
        //            {
        //                TempOptionUI widget = WidgetFactory.GetWidget(__instance.CueAnswer);
        //                widget.Initialize(num++, answer);
        //                widget.transform.SetParent(__instance.AnswersPanel.transform, worldPositionStays: false);
        //                __instance.m_AnswersList.Add(widget);
        //            }
        //        }

        //        if (Application.isPlaying)
        //        {
        //            __instance.StartCoroutine(__instance.Show());
        //        }


        //        return true;
        //    }
        //}



        //[HarmonyPatch(typeof(Kingmaker.Controllers.Dialog.DialogController), nameof(Kingmaker.Controllers.Dialog.DialogController.CurrentSpeakerName), MethodType.Getter)]
        //internal static class BMC_Deputy_Character_Dialogue_SpeakerName_Patch_NPC
        //{
        //    public static void Postfix(ref string __result, Kingmaker.Controllers.Dialog.DialogController __instance)
        //    {


        //        //return;
        //        if (!Main.Settings.EnabledMemberMythicEtude) return;
        //        if (!Main.Settings.EnabledMemberMythicEtudeDeep) return;
        //        if (LogicStructures.GetPartyCommanderNumber() <= 1)
        //        {
        //            return;
        //        }
        //        var cue = __instance.CurrentCue;
        //        if (__instance == null) return;
        //        if (cue == null) return;
        //        if (cue.ElementsArray.Count == 0) return;
        //        UnitEntityData deputySpeaker = null;
        //        foreach (var condition in cue.ElementsArray)
        //        {
        //            UnitClass classCondition = condition as UnitClass;
        //            EtudeStatus etudeStatus = condition as EtudeStatus;
        //            if (classCondition == null && etudeStatus == null) continue;
        //            if (classCondition != null) if (!classCondition.Unit.name.Contains("PlayerCharacter")) continue;
        //            if (etudeStatus != null) if (!mythicGuidEtudes.Contains(etudeStatus.Etude.AssetGuidThreadSafe)) continue;


        //            if (classCondition != null && classCondition.Unit.name.Contains("PlayerCharacter"))
        //            {
        //                if (!mythicClassGuid.Contains(classCondition.Class.AssetGuidThreadSafe)) continue;
        //                var commanderMythic = Game.Instance.Player.MainCharacter.Value.Descriptor.Progression.GetCurrentMythicClass();
        //                if (classCondition.Class.AssetGuidThreadSafe == commanderMythic.CharacterClass.AssetGuidThreadSafe) continue;
        //                var charList = LogicStructures.GetPartyList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();

        //                foreach (var unit in charList)
        //                {
        //                    if (unit.IsMainCharacter || !unit.Descriptor.m_IsEssentialForGame) continue;
        //                    var deputyCommanderMythic = unit.Descriptor.Progression.GetCurrentMythicClass();
        //                    if (deputyCommanderMythic == null) continue;
        //                    var requiredMythicPath = classCondition.Class.AssetGuidThreadSafe;
        //                    if (requiredMythicPath == deputyCommanderMythic.CharacterClass.AssetGuidThreadSafe)
        //                    {
        //                        deputySpeaker = unit;
        //                        //__result = true;
        //                        break;

        //                    }
        //                }
        //            }

        //            if (etudeStatus != null && !mythicGuidEtudes.Contains(etudeStatus.Etude.AssetGuidThreadSafe))
        //            {
        //                var charList = LogicStructures.GetPartyList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
        //                foreach (var unit in charList)
        //                {

        //                    if (unit.IsMainCharacter || !unit.Descriptor.m_IsEssentialForGame) continue;
        //                    var deputyCommanderMythic = unit.Descriptor.Progression.GetCurrentMythicClass();
        //                    if (deputyCommanderMythic == null) continue;
        //                    var deputyCommanderMythicPathName = deputyCommanderMythic.CharacterClass.NameForAcronym;
        //                    var requiredMythicPathName = etudeStatus.Etude.name.Replace("PlayerIs", "");
        //                    if (deputyCommanderMythicPathName.Contains(requiredMythicPathName))
        //                    {
        //                        deputySpeaker = unit;
        //                        break;
        //                    }
        //                }
        //            }

        //        }
        //        if (deputySpeaker == null) { return; }
        //        else { __result = deputySpeaker.CharacterName; }



        //        return;
        //    }
        //}
        //[HarmonyPatch(typeof(Kingmaker.Controllers.Dialog.DialogController), nameof(Kingmaker.Controllers.Dialog.DialogController.AddHistoryEntry), new Type[] { typeof(BlueprintCue), typeof(String), typeof(String) })]
        //internal static class BMC_Deputy_Character_Dialogue_NPC_Cue_Switch
        //{
        //    public static bool Prefix(ref BlueprintCue cue,ref string speakerName,ref string speakerColor, Kingmaker.Controllers.Dialog.DialogController __instance)
        //    {
        //        if (!Main.Settings.EnabledMemberMythicEtude) return true;
        //        if (!Main.Settings.EnabledMemberMythicEtudeDeep) return true;
        //        if (Main.Settings.DisableMythicMemberIntoDialogue) return true;

        //        if (LogicStructures.GetPartyCommanderNumber() <= 1)
        //        {
        //            return true;
        //        }
        //        var requiredMythicPathNameList = cue.Conditions?.Conditions?.Where(condition => condition.ToString().Contains("PlayerIs")).ToList();
        //        var requiredContinuedMythicPathNameList = cue.Conditions?.Conditions?.Where(condition => condition.ToString().Contains("Cue Seen")).ToList();
        //        if (cue != null && requiredMythicPathNameList == null && requiredContinuedMythicPathNameList == null &&  cue.Answers?.FirstOrDefault().ToString() == "DefaultContinue" && Main.Settings.SavedMainCharacterGUID != "" && Game.Instance.Player.MainCharacter.Value.Proxy.Id != Main.Settings.SavedMainCharacterGUID) {
        //            UnitEntityData mainCharToSet = LogicStructures.GetAllCharacterList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor) && actor.Proxy.Id == Main.Settings.SavedMainCharacterGUID).ToList().SingleOrDefault();
        //            if (Game.Instance != null && mainCharToSet != null)
        //            {
        //                Game.Instance.Player.MainCharacter = mainCharToSet;
        //                Main.Settings.SavedMainCharacterGUID = "";
        //            }
        //            Main.Settings.SavedMainCharacterGUID = "";
        //        }
        //        if (cue.Conditions.HasConditions == false) return true;
        //        //var cue = __instance.CurrentCue;

        //        if (requiredMythicPathNameList == null) return true;

        //        //if (cue == null) return true;
        //        //if (cue.ElementsArray.Count == 0) return true;
        //        UnitEntityData deputySpeaker = null;

        //        var charList = LogicStructures.GetPartyList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
        //        foreach (var unit in charList)
        //        {
        //            var deputyCommanderMythic = unit.Descriptor.Progression.GetCurrentMythicClass();
        //            if (deputyCommanderMythic == null) continue;
        //            var deputyCommanderMythicPathName = deputyCommanderMythic.CharacterClass.NameForAcronym;
        //            var teamMythic = deputyCommanderMythicPathName.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "");

                    
        //            foreach (var requiredMythicPathName in requiredMythicPathNameList)
        //            if (requiredMythicPathName.ToString().Contains(teamMythic))
        //            {
        //                if (unit.IsMainCharacter) return true;
        //                deputySpeaker = unit;
        //                break;
        //            }
        //        }
        //        if (deputySpeaker == null) { return true; }
        //        else
        //        {
        //            string line;
        //            if (!string.IsNullOrEmpty(speakerName))
        //            {
        //                if (!string.IsNullOrEmpty(speakerColor))
        //                {
        //                    line = string.Format(DialogFormats.SpeakerFormatWithColorName, "", speakerColor, speakerName, cue.DisplayText, ColorUtility.ToHtmlStringRGB(BlueprintRoot.Instance.UIRoot.DialogColors.Narrator));
        //                }
        //                else
        //                {
        //                    line = string.Format(DialogFormats.SpeakerFormatWithName, "", speakerName, cue.DisplayText, ColorUtility.ToHtmlStringRGB(BlueprintRoot.Instance.UIRoot.DialogColors.Narrator));
        //                }
        //            }
        //            else
        //            {
        //                line = string.Format(DialogFormats.NarratorsTextFormat, cue.DisplayText);
        //            }

        //            EventBus.RaiseEvent(delegate (IDialogHandler h)
        //            {
        //                h.HandleOnDialogHistory(line);
        //            });

        //            if (deputySpeaker != null && Main.Settings.EnabledAutoSwitchMythicCharacter && !Main.Settings.DisableMythicMemberIntoDialogue && deputySpeaker.Descriptor.m_IsEssentialForGame)
        //            {
        //                if (!Game.Instance.Player.MainCharacter.Value.Descriptor.m_IsEssentialForGame) Game.Instance.Player.MainCharacter.Value.Descriptor.AddEssentialMark();
        //                if (Main.Settings.SavedMainCharacterGUID == "") Main.Settings.SavedMainCharacterGUID = Game.Instance.Player.MainCharacter.Value.Proxy.Id;
        //                if (Game.Instance != null)
        //                {
        //                    Game.Instance.Player.MainCharacter = deputySpeaker;
        //                }
        //            }
        //            return false;
        //        }



        //        return true;
        //    }
        //}


        //[HarmonyPatch(typeof(EtudeStatus), nameof(EtudeStatus.CheckCondition))]

        //internal static class BMC_GoldenDragon_Devil_Legend_EtudeStatus_Patch
        //{

        //}




        //[HarmonyPatch(typeof(CharacterScreenController), nameof(CharacterScreenController.SetupInfo))]
        //internal static class BMC_Deputy_CommanderAlignmentUIPatch
        //{
        //    internal static bool Prefix(CharacterScreenController __instance)
        //    {

        //        if (__instance.m_CurrentCharacter.Unit.IsMainCharacter || !LogicStructures.isPlayerActorBlueprintType(__instance.m_CurrentCharacter.Unit))
        //        {
        //            return true;
        //        }
        //        if (!__instance.m_CurrentCharacter.Unit.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(__instance.m_CurrentCharacter.Unit))
        //        {
        //            try
        //            {
        //            __instance.AbilityScores.FillData(__instance.m_CurrentCharacter);
        //            __instance.CharName.text = UIUtility.GetSaberBookFormat(__instance.m_CurrentCharacter.CharacterName);
        //            __instance.Alignment.UpdateData(__instance.m_CurrentCharacter);

        //            __instance.AlignmentHistory.AlwaysHidden = false;
        //            __instance.AlignmentHistoryBiography.AlwaysHidden = false;
        //            __instance.AlignmentHistory.UpdateData(__instance.m_CurrentCharacter);
        //            __instance.AlignmentHistoryBiography.UpdateData(__instance.m_CurrentCharacter);
        //            __instance.Stories.AlwaysHidden = true;


        //            __instance.Level.FillData(__instance.m_CurrentCharacter);
        //            __instance.ShowSection(__instance.m_CurrentSection);
        //            }

        //            finally
        //            {
        //            }
        //        }
        //        return false;
        //    }
        //}

        //[HarmonyPatch(typeof(Kingmaker.UI.Dialog.DialogController), nameof(Kingmaker.UI.Dialog.DialogController.HandleOnCueShow))]
        //internal static class BMC_Deputy_Character_Dialogue_Patch_Dangerously_DEEP
        //{

        //    public static bool Prefix(ref CueShowData data, Kingmaker.UI.Dialog.DialogController __instance)
        //    {



        //        if (!Main.Settings.EnabledMemberMythicEtude) return true;
        //        if (!Main.Settings.EnabledMemberMythicEtudeDeep) return true;
        //        if (LogicStructures.GetPartyCommanderNumber() <= 1)
        //        {
        //            return true;
        //        }
        //        BlueprintCue cue = null;
        //        if (__instance == null) return true;
        //        if (cue == null) return true;
        //        if (cue.ElementsArray.Count == 0) return true;
        //        UnitEntityData deputySpeaker = null;
        //        foreach (var condition in cue.ElementsArray)
        //        {
        //            UnitClass classCondition = condition as UnitClass;
        //            if (classCondition == null) continue;
        //            if (!classCondition.Unit.name.Contains("PlayerCharacter")) continue;
        //            if (!mythicClassGuid.Contains(classCondition.Class.AssetGuidThreadSafe)) continue;
        //            var commanderMythic = Game.Instance.Player.MainCharacter.Value.Descriptor.Progression.GetCurrentMythicClass();
        //            if (classCondition.Class.AssetGuidThreadSafe == commanderMythic.CharacterClass.AssetGuidThreadSafe) continue;
        //            var charList = LogicStructures.GetPartyList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
        //            foreach (var unit in charList)
        //            {
        //                if (unit.IsMainCharacter || !unit.Descriptor.m_IsEssentialForGame) continue;
        //                var deputyCommanderMythic = unit.Descriptor.Progression.GetCurrentMythicClass();
        //                if (deputyCommanderMythic == null) continue;
        //                var requiredMythicPath = classCondition.Class.AssetGuidThreadSafe;
        //                if (requiredMythicPath == deputyCommanderMythic.CharacterClass.AssetGuidThreadSafe)
        //                {
        //                    deputySpeaker = unit;
        //                    //__result = true;
        //                    return false;
        //                }
        //            }

        //        }
        //        if (deputySpeaker == null) return true;
        //        //data.Cue.Listener = deputySpeaker;

        //        //if ((bool)cue)
        //        //{
        //        //    __instance.CurrentPart.Hide();
        //        //    UnitEntityData currentSpeaker = Game.Instance.DialogController.CurrentSpeaker;
        //        //    BlueprintUnit speakerPortrait = cue.Speaker.SpeakerPortrait;
        //        //    Sprite sprite = ((speakerPortrait != null) ? speakerPortrait.PortraitSafe.HalfLengthPortrait : currentSpeaker?.Portrait.HalfLengthPortrait);
        //        //    __instance.SpeakerPortait.gameObject.SetActive(sprite != null);
        //        //    if ((bool)sprite)
        //        //    {
        //        //        __instance.SpeakerPortait.sprite = sprite;
        //        //    }

        //        //    string text = speakerPortrait?.CharacterName ?? currentSpeaker?.CharacterName;
        //        //    __instance.SpeakerName.gameObject.SetActive(text != null);
        //        //    if (text != null)
        //        //    {
        //        //        __instance.SpeakerName.text = UIUtility.GetSaberBookFormat(text);
        //        //    }

        //        //    __instance.SpeakerHolder.gameObject.SetActive(sprite == null && text == null);
        //        //    UnitEntityData value = deputySpeaker;
        //        //    sprite = ((cue.Listener == null) ? value.Portrait.HalfLengthPortrait : cue.Listener.PortraitSafe?.HalfLengthPortrait);
        //        //    __instance.AnswererPortait.gameObject.SetActive(sprite != null);
        //        //    if ((bool)sprite)
        //        //    {
        //        //        __instance.AnswererPortait.sprite = sprite;
        //        //    }

        //        //    __instance.AnswererName.text = UIUtility.GetSaberBookFormat(cue.Listener?.CharacterName ?? value.CharacterName);
        //        //    BlueprintAnswer answer = Game.Instance.DialogController.Answers.First();
        //        //    if (answer.IsSystem())
        //        //    {
        //        //        __instance.SystemButton.Show(answer);
        //        //    }
        //        //    else
        //        //    {
        //        //        __instance.SystemButton.Hide();
        //        //    }

        //        //    __instance.CurrentPart.Fill(data);
        //        //    data.Cue.PlayVoiceOver();
        //        //    __instance.m_ShowingCueData = data;
        //        //    Canvas.ForceUpdateCanvases();
        //        //    return false;
        //        //}




        //        return true;
        //    }
        //}


    }
}
