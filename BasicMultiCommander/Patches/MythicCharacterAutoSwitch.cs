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
using Kingmaker.DialogSystem.Blueprints;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.RandomEncounters.Settings;
using Kingmaker.UI.MVVM._VM.Dialog.Dialog;
using Owlcat.QA.Validation;
//using Owlcat.Runtime.Core;
using System;
using System.Linq;
using UnityEngine;
using static Kingmaker.AreaLogic.Etudes.EtudesSystem;

namespace BasicMultiCommander.Patches
{
    class MythicCharacterAutoSwitch
    {
        public static Vector3 sphere = 2.5f * UnityEngine.Random.insideUnitSphere;

        [HarmonyPatch(typeof(PlayCutscene), nameof(PlayCutscene.RunAction))]
        internal static class BMC_Deputy_Character_Scene_Switch_Patch
        {
            public static bool Prefix(PlayCutscene __instance)
            {
                if (!Main.Settings.EnabledMemberMythicEtude) return true;
                if (!Main.Settings.EnabledMemberMythicEtudeDeep) return true;
                if (!Main.Settings.EnabledAutoSwitchMythicCharacter) return true;
                if (Main.Settings.DisableMythicMemberIntoDialogue) return true;

                if (LogicStructures.GetPartyCommanderNumber() <= 1)
                {
                    return true;
                }
                BlueprintEtude blueprintEtude = null;
                try
                {
                    blueprintEtude = (BlueprintEtude)__instance.Owner;

                }
                catch { return true; }
                var playerCharacter = Game.Instance.Player.MainCharacter.Value;
                string requiredMythicPathName = null;
                var charList = LogicStructures.GetPartyList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
                UnitEntityData romanceInitator = null;
                if (Main.Settings.EnabledAutoSwitchMythicCharacter && Main.Settings.EnabledRomanceAutoSwitchMythicCharacter && blueprintEtude.AddedAreaMechanics.Count() > 0 && StaticData.romanceAreaMechnicaGuid.Contains(blueprintEtude.AddedAreaMechanics.FirstOrDefault().deserializedGuid.ToString())) {
                    if (true)
                    {
                        if (Main.Settings.CharacterLinkedRomance)
                        {
                            switch (blueprintEtude.AddedAreaMechanics.FirstOrDefault().deserializedGuid.ToString())
                            {
                                case "ce2576c45ec0d0248910c8527eced895":
                                    if (CharacterLinkedRomance.CamelliaRomance == "") break;
                                    if (CharacterLinkedRomance.CamelliaRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id)
                                    {
                                        foreach (var unit in charList)
                                        {
                                            if (unit.Proxy.Id == CharacterLinkedRomance.CamelliaRomance)
                                            {
                                                romanceInitator = unit;
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                case "e5940f2cfee4beb46944a7e3e7e95ab6":
                                    if (CharacterLinkedRomance.DaeranRomance == "") break;
                                    if (CharacterLinkedRomance.DaeranRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id)
                                    {
                                        foreach (var unit in charList)
                                        {
                                            if (unit.Proxy.Id == CharacterLinkedRomance.DaeranRomance)
                                            {
                                                romanceInitator = unit;
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                case "644baac64a1af7342958baff0bcde601":
                                    if (CharacterLinkedRomance.LannRomance == "") break;
                                    if (CharacterLinkedRomance.LannRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id)
                                    {
                                        foreach (var unit in charList)
                                        {
                                            if (unit.Proxy.Id == CharacterLinkedRomance.LannRomance)
                                            {
                                                romanceInitator = unit;
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                case "36651f28bb1e4bbe8abb7ee4e931d56c":
                                    if (CharacterLinkedRomance.UlbrigRomance == "") break;
                                    if (CharacterLinkedRomance.UlbrigRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id)
                                    {
                                        foreach (var unit in charList)
                                        {
                                            if (unit.Proxy.Id == CharacterLinkedRomance.UlbrigRomance)
                                            {
                                                romanceInitator = unit;
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                case "c7b51ac4d31ec0d44a9bf247970392d9":
                                    if (CharacterLinkedRomance.ArueshalaeRomance == "") break;
                                    if (CharacterLinkedRomance.ArueshalaeRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id)
                                    {
                                        foreach (var unit in charList)
                                        {
                                            if (unit.Proxy.Id == CharacterLinkedRomance.ArueshalaeRomance)
                                            {
                                                romanceInitator = unit;
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                case "d53e29755de89ec48842a15581aa869c":
                                    if (CharacterLinkedRomance.WenduagRomance == "") break;
                                    if (CharacterLinkedRomance.WenduagRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id)
                                    {
                                        foreach (var unit in charList)
                                        {
                                            if (unit.Proxy.Id == CharacterLinkedRomance.WenduagRomance)
                                            {
                                                romanceInitator = unit;
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            return true;
                        }

                        if (romanceInitator == null) { return true; }
                        else
                        {
                            if (Main.Settings.EnabledAutoSwitchMythicCharacter)
                            {
                                if (romanceInitator != null && !romanceInitator.IsMainCharacter && Main.Settings.EnabledAutoSwitchMythicCharacter && !Main.Settings.DisableMythicMemberIntoDialogue && romanceInitator.Descriptor.m_IsEssentialForGame)
                                {
                                    if (!Game.Instance.Player.MainCharacter.Value.Descriptor.m_IsEssentialForGame) Game.Instance.Player.MainCharacter.Value.Descriptor.AddEssentialMark();
                                    if (Main.Settings.EtudesSavedMainCharacterGUID == "") Main.Settings.EtudesSavedMainCharacterGUID = Game.Instance.Player.MainCharacter.Value.Proxy.Id;
                                    if (Main.Settings.EtudesSaved == "") Main.Settings.EtudesSaved = blueprintEtude.AssetGuidThreadSafe;
                                    if (Game.Instance != null)
                                    {
                                        Game.Instance.Player.MainCharacter = romanceInitator;
                                        PFLog.Mods.Error("Char switch romanceInitator");
                                    }
                                }
                            }
                            return true;
                        }
                    }
                }

                if (blueprintEtude.Parent != null || blueprintEtude.name != null) //本级名称，父etude名称
                {
                    foreach (var unit in charList)
                    {
                        //if (unit.IsMainCharacter) continue;
                        var deputyCommanderMythic = unit.Progression.LastMythicClass;
                        if (deputyCommanderMythic == null) continue;
                        string parentAboverequiredMythicPathName = null;
                        var teamLastMythic = deputyCommanderMythic.NameForAcronym.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "");
                        var parentrequiredMythicPathName = blueprintEtude.Parent.NameSafe();
                        try { parentAboverequiredMythicPathName = blueprintEtude.Parent?.Get()?.Parent?.NameSafe(); }
                        catch { parentAboverequiredMythicPathName = null; }

                        requiredMythicPathName = blueprintEtude.name;

                        if (blueprintEtude.AssetGuidThreadSafe == "3e571ea3988e06d4486cd7a309172cda" ) {
                            BlueprintEtude chapter05Etude = ResourcesLibrary.TryGetBlueprint<BlueprintEtude>("5b01aa690202e584888dfc600a4aac0a");
                            if (chapter05Etude != null)
                            {
                               
                                if (Game.Instance.Player.EtudesSystem.EtudeIsNotStarted(chapter05Etude)) return true;
                            }
                            
                        }
                        if (requiredMythicPathName.Contains(teamLastMythic))
                        {
                            if (unit != null && unit.IsMainCharacter) {
                                return true;
                            }
                            if (unit != null && !unit.IsMainCharacter && Main.Settings.EnabledAutoSwitchMythicCharacter && !Main.Settings.DisableMythicMemberIntoDialogue && unit.Descriptor.m_IsEssentialForGame)
                            {
                                if (!Game.Instance.Player.MainCharacter.Value.Descriptor.m_IsEssentialForGame) Game.Instance.Player.MainCharacter.Value.Descriptor.AddEssentialMark();
                                if (Main.Settings.SavedMainCharacterGUID == "") Main.Settings.SavedMainCharacterGUID = Game.Instance.Player.MainCharacter.Value.Proxy.Id;
                                if (Game.Instance != null)
                                {
                                    if (Main.Settings.CharacterSwaped)
                                    {
                                        Main.Settings.CharacterSwaped = false;
                                    }
                                    else
                                    {
                                        UnitEntityData mainUnit = Game.Instance.Player.MainCharacter;
                                        Vector3 position;
                                        position = new(unit.Position.x + sphere.x, unit.Position.y, unit.Position.z + sphere.z);
                                        mainUnit.Position = position;
                                        Main.Settings.CharacterSwaped = true;
                                    }
                                    Game.Instance.Player.MainCharacter = unit;
                                    PFLog.Mods.Error("Char switch 1");
                                }
                            }
                            return true;
                        }
                        if (blueprintEtude.Parent != null && parentrequiredMythicPathName.Contains(teamLastMythic))
                        {
                            if (unit != null && unit.IsMainCharacter)
                            {
                                return true;
                            }
                            if (unit != null && !unit.IsMainCharacter && Main.Settings.EnabledAutoSwitchMythicCharacter && !Main.Settings.DisableMythicMemberIntoDialogue && unit.Descriptor.m_IsEssentialForGame)
                            {
                                if (!Game.Instance.Player.MainCharacter.Value.Descriptor.m_IsEssentialForGame) Game.Instance.Player.MainCharacter.Value.Descriptor.AddEssentialMark();
                                if (Main.Settings.SavedMainCharacterGUID == "") Main.Settings.SavedMainCharacterGUID = Game.Instance.Player.MainCharacter.Value.Proxy.Id;
                                if (Game.Instance != null)
                                {
                                    if (Main.Settings.CharacterSwaped)
                                    {
                                        Main.Settings.CharacterSwaped = false;
                                    }
                                    else
                                    {
                                        UnitEntityData mainUnit = Game.Instance.Player.MainCharacter;
                                        Vector3 position;
                                        position = new(unit.Position.x + sphere.x, unit.Position.y, unit.Position.z + sphere.z);
                                        mainUnit.Position = position;
                                        Main.Settings.CharacterSwaped = true;
                                    }
                                    Game.Instance.Player.MainCharacter = unit;
                                    PFLog.Mods.Error(blueprintEtude.Parent.deserializedGuid + parentrequiredMythicPathName + "Char switch 2");
                                }
                            }
                            return true;
                        }


                        if (parentAboverequiredMythicPathName != null && parentAboverequiredMythicPathName.Contains(teamLastMythic))
                        {
                            if (unit != null && unit.IsMainCharacter)
                            {
                                return true;
                            }
                            if (unit != null && !unit.IsMainCharacter && Main.Settings.EnabledAutoSwitchMythicCharacter && !Main.Settings.DisableMythicMemberIntoDialogue && unit.Descriptor.m_IsEssentialForGame)
                            {
                                if (!Game.Instance.Player.MainCharacter.Value.Descriptor.m_IsEssentialForGame) Game.Instance.Player.MainCharacter.Value.Descriptor.AddEssentialMark();
                                if (Main.Settings.SavedMainCharacterGUID == "") Main.Settings.SavedMainCharacterGUID = Game.Instance.Player.MainCharacter.Value.Proxy.Id;
                                if (Game.Instance != null)
                                {
                                    if (Main.Settings.CharacterSwaped)
                                    {
                                        Main.Settings.CharacterSwaped = false;
                                    }
                                    else
                                    {
                                        UnitEntityData mainUnit = Game.Instance.Player.MainCharacter;
                                        Vector3 position;
                                        position = new(unit.Position.x + sphere.x, unit.Position.y, unit.Position.z + sphere.z);
                                        mainUnit.Position = position;
                                        Main.Settings.CharacterSwaped = true;
                                    }
                                    Game.Instance.Player.MainCharacter = unit;
                                    PFLog.Mods.Error("Char switch 3");
                                }
                            }
                            return true;
                        }
                    }
                }
                string commanderMythic = null;
                try
                {
                    commanderMythic = playerCharacter.Progression.LastMythicClass?.NameForAcronym?.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "");
                }
                catch {
                    return true;
                }
                if (blueprintEtude?.ActivationCondition.Conditions.Count() > 0) //条件
                {
                    var conditions = blueprintEtude?.ActivationCondition.Conditions;
                    foreach (var condition in conditions)
                    {
                        if (condition.name.Contains("EtudeStatus") && condition.Not == false)
                        {
                            EtudeStatus realcondition = (EtudeStatus)condition;

                            if (realcondition != null)
                            {
                                requiredMythicPathName = realcondition.Etude.name;
                                if (commanderMythic != null && requiredMythicPathName != null && requiredMythicPathName.Contains(commanderMythic)) return true;
                                foreach (var unit in charList)
                                {
                                    //if (unit.IsMainCharacter) continue;
                                    var deputyCommanderMythic = unit.Progression.LastMythicClass;
                                    if (deputyCommanderMythic == null) continue;
                                    var teamLastMythic = deputyCommanderMythic.NameForAcronym.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "");
                                    if (requiredMythicPathName.Contains(teamLastMythic))
                                    {
                                        if (unit != null && !unit.IsMainCharacter && Main.Settings.EnabledAutoSwitchMythicCharacter && !Main.Settings.DisableMythicMemberIntoDialogue && unit.Descriptor.m_IsEssentialForGame)
                                        {
                                            if (!Game.Instance.Player.MainCharacter.Value.Descriptor.m_IsEssentialForGame) Game.Instance.Player.MainCharacter.Value.Descriptor.AddEssentialMark();
                                            if (Main.Settings.SavedMainCharacterGUID == "") Main.Settings.SavedMainCharacterGUID = Game.Instance.Player.MainCharacter.Value.Proxy.Id;
                                            if (Game.Instance != null)
                                            {
                                                if (Main.Settings.CharacterSwaped)
                                                {
                                                    Main.Settings.CharacterSwaped = false;
                                                }
                                                else
                                                {
                                                    UnitEntityData mainUnit = Game.Instance.Player.MainCharacter;
                                                    Vector3 position;
                                                    position = new(unit.Position.x + sphere.x, unit.Position.y, unit.Position.z + sphere.z);
                                                    mainUnit.Position = position;
                                                    Main.Settings.CharacterSwaped = true;
                                                }
                                                Game.Instance.Player.MainCharacter = unit;
                                                PFLog.Mods.Error("Char switch 4");
                                            }
                                        }
                                        return true;
                                    }
                                }

                                //requiredMythicPathName = blueprintEtude.Parent.NameSafe();
                                //foreach (var unit in charList)
                                //{
                                //    if (unit.IsMainCharacter) continue;
                                //    var deputyCommanderMythic = unit.Progression.LastMythicClass;
                                //    if (deputyCommanderMythic == null) continue;
                                //    var teamLastMythic = deputyCommanderMythic.NameForAcronym.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "");
                                //    if (requiredMythicPathName.Contains(teamLastMythic))
                                //    {
                                //        if (unit != null && !unit.IsMainCharacter && Main.Settings.EnabledAutoSwitchMythicCharacter && !Main.Settings.DisableMythicMemberIntoDialogue && unit.Descriptor.m_IsEssentialForGame)
                                //        {
                                //            if (!Game.Instance.Player.MainCharacter.Value.Descriptor.m_IsEssentialForGame) Game.Instance.Player.MainCharacter.Value.Descriptor.AddEssentialMark();
                                //            if (Main.Settings.SavedMainCharacterGUID == "") Main.Settings.SavedMainCharacterGUID = Game.Instance.Player.MainCharacter.Value.Proxy.Id;
                                //            if (Game.Instance != null)
                                //            {

                                //                Game.Instance.Player.MainCharacter = unit;
                                //            }
                                //        }
                                //        return true;
                                //    }
                                //}
                            }
                        }
                    }

                }

                return true;
            }
        }

        [HarmonyPatch(typeof(StartDialog), nameof(StartDialog.RunAction))]
        internal static class BMC_Deputy_Character_Romance_Dialogue_Triggered
        {
            public static bool Prefix( StartDialog __instance)
            {



                if (!Main.Settings.EnabledMemberMythicEtude) return true;
                if (!Main.Settings.EnabledMemberMythicEtudeDeep) return true;
                if (!Main.Settings.CharacterLinkedRomance) return true;
                if (!Main.Settings.EnabledAutoSwitchMythicCharacter) return true;
                if (Main.Settings.DisableMythicMemberIntoDialogue) return true;
                BlueprintCampingEncounter romanceCampingEncounter = null;
                ConditionsChecker romanceCampingConditions = null;
                if (LogicStructures.GetPartyCommanderNumber() <= 1)
                {
                    return true;
                }
                try {
                    romanceCampingEncounter = (BlueprintCampingEncounter)__instance.Owner;
                }
                catch {
                    romanceCampingEncounter = null;
                    return true;
                }
                try
                {
                    romanceCampingConditions = romanceCampingEncounter.Conditions;
                }
                catch
                {
                    romanceCampingConditions = null;
                    return true;
                }
                //var cue = __instance.CurrentCue;
                if ( romanceCampingEncounter == null || romanceCampingConditions == null || romanceCampingConditions.Conditions.Count() == 0) return true;
                //if (cue == null) return true;
                //if (cue.ElementsArray.Count == 0) return true;
                bool romanceMark = false;
                EtudeStatus romanceEtudeStatus = null;
                foreach (Condition condition in romanceCampingConditions.Conditions)
                {
                    EtudeStatus conditionStatus = null;
                    try {
                         conditionStatus = (EtudeStatus)condition;
                    }
                    catch { continue; }
                    if (StaticData.romanceGuid.Contains( conditionStatus.Etude.AssetGuidThreadSafe))
                    { 
                        romanceMark = true;
                        romanceEtudeStatus = conditionStatus;
                        break;
                    }
                }
                UnitEntityData romanceInitator = null;
                var charList = LogicStructures.GetPartyList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
                if (romanceEtudeStatus  != null) {
                    if (Main.Settings.CharacterLinkedRomance)
                    {
                        switch (romanceEtudeStatus.Etude.AssetGuidThreadSafe)
                        {
                            case "fe6ee37b2aa394e4eaa51208cf7d7f86":
                                if (CharacterLinkedRomance.CamelliaRomance == "") break;
                                if (CharacterLinkedRomance.CamelliaRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id)
                                {
                                    foreach (var unit in charList)
                                    {
                                        if (unit.Proxy.Id == CharacterLinkedRomance.CamelliaRomance)
                                        {
                                            romanceInitator = unit;
                                            break;
                                        }
                                    }
                                }
                                break;
                            case "e49702f590611644580f09c8f9ef0e5b":
                                if (CharacterLinkedRomance.CamelliaRomance == "") break;
                                if (CharacterLinkedRomance.CamelliaRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id)
                                {
                                    foreach (var unit in charList)
                                    {
                                        if (unit.Proxy.Id == CharacterLinkedRomance.CamelliaRomance)
                                        {
                                            romanceInitator = unit;
                                            break;
                                        }
                                    }
                                }
                                break;
                            case "8541453b31379964e834cf2309444388":
                                if (CharacterLinkedRomance.DaeranRomance == "") break;
                                if (CharacterLinkedRomance.DaeranRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id)
                                {
                                    foreach (var unit in charList)
                                    {
                                        if (unit.Proxy.Id == CharacterLinkedRomance.DaeranRomance)
                                        {
                                            romanceInitator = unit;
                                            break;
                                        }
                                    }
                                }
                                break;
                            case "af394a43ba7c1314bb31aea74a5e4c0e":
                                if (CharacterLinkedRomance.LannRomance == "") break;
                                if (CharacterLinkedRomance.LannRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id)
                                {
                                    foreach (var unit in charList)
                                    {
                                        if (unit.Proxy.Id == CharacterLinkedRomance.LannRomance)
                                        {
                                            romanceInitator = unit;
                                            break;
                                        }
                                    }
                                }
                                break;
                            case "14650554734f4adc81af61e442fd628b":
                                if (CharacterLinkedRomance.UlbrigRomance == "") break;
                                if (CharacterLinkedRomance.UlbrigRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id)
                                {
                                    foreach (var unit in charList)
                                    {
                                        if (unit.Proxy.Id == CharacterLinkedRomance.UlbrigRomance)
                                        {
                                            romanceInitator = unit;
                                            break;
                                        }
                                    }
                                }
                                break;
                            case "efb130b8a22c9534ca40a1e41ef8e931":
                                if (CharacterLinkedRomance.SosielRomance == "") break;
                                if (CharacterLinkedRomance.SosielRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id)
                                {
                                    foreach (var unit in charList)
                                    {
                                        if (unit.Proxy.Id == CharacterLinkedRomance.SosielRomance)
                                        {
                                            romanceInitator = unit;
                                            break;
                                        }
                                    }
                                }
                                break;
                            case "6a3fdd0758fe78d4aa2c3b26d7614fbc":
                                if (CharacterLinkedRomance.ArueshalaeRomance == "") break;
                                if (CharacterLinkedRomance.ArueshalaeRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id)
                                {
                                    foreach (var unit in charList)
                                    {
                                        if (unit.Proxy.Id == CharacterLinkedRomance.ArueshalaeRomance)
                                        {
                                            romanceInitator = unit;
                                            break;
                                        }
                                    }
                                }
                                break;
                            case "33c4c2f66f2461e4993df21566252079":
                                if (CharacterLinkedRomance.WenduagRomance == "") break;
                                if (CharacterLinkedRomance.WenduagRomance != Game.Instance.Player.MainCharacter.Value.Proxy.Id)
                                {
                                    foreach (var unit in charList)
                                    {
                                        if (unit.Proxy.Id == CharacterLinkedRomance.WenduagRomance)
                                        {
                                            romanceInitator = unit;
                                            break;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else {
                        return true;
                    }
                }

                

             
                if (romanceInitator == null) { return true; }
                else
                {
                    if (Main.Settings.EnabledAutoSwitchMythicCharacter)
                    {
                        if (romanceInitator != null && !romanceInitator.IsMainCharacter && Main.Settings.EnabledAutoSwitchMythicCharacter && !Main.Settings.DisableMythicMemberIntoDialogue && romanceInitator.Descriptor.m_IsEssentialForGame)
                        {
                            if (!Game.Instance.Player.MainCharacter.Value.Descriptor.m_IsEssentialForGame) Game.Instance.Player.MainCharacter.Value.Descriptor.AddEssentialMark();
                            if (Main.Settings.SavedMainCharacterGUID == "") Main.Settings.SavedMainCharacterGUID = Game.Instance.Player.MainCharacter.Value.Proxy.Id;
                            if (Game.Instance != null)
                            {
                                Game.Instance.Player.MainCharacter = romanceInitator;
                                PFLog.Mods.Error("Char switch 5");
                            }
                        }
                    }
                    return true;
                }



                return true;
            }
        }

        [HarmonyPatch(typeof(Kingmaker.Controllers.Dialog.DialogController), nameof(Kingmaker.Controllers.Dialog.DialogController.AddHistoryEntry), new Type[] { typeof(BlueprintAnswer) })]
        internal static class BMC_Deputy_Character_Dialogue_SpeakerName_Patch_Player
        {
            public static bool Prefix(ref BlueprintAnswer answer, Kingmaker.Controllers.Dialog.DialogController __instance)
            {



                if (!Main.Settings.EnabledMemberMythicEtude) return true;
                if (!Main.Settings.EnabledMemberMythicEtudeDeep) return true;
                if (Main.Settings.DisableMythicMemberIntoDialogue) return true;

                if (LogicStructures.GetPartyCommanderNumber() <= 1)
                {
                    return true;
                }
                if (answer.MythicRequirement == 0) return true;
                //var cue = __instance.CurrentCue;

                //if (cue == null) return true;
                //if (cue.ElementsArray.Count == 0) return true;
                UnitEntityData deputySpeaker = null;

                var charList = LogicStructures.GetPartyList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
                foreach (var unit in charList)
                {
                    var deputyCommanderMythic = unit.Progression.LastMythicClass;
                    if (deputyCommanderMythic == null) continue;
                    var deputyCommanderMythicPathName = deputyCommanderMythic.NameForAcronym;
                    var requiredMythicPathName = answer.MythicRequirement.GetEnumDescription().Replace("PlayerIs", "");
                    if (deputyCommanderMythicPathName.Contains(requiredMythicPathName))
                    {
                        if (unit.IsMainCharacter) return true;
                        deputySpeaker = unit;
                        break;
                    }
                }

                if (deputySpeaker == null) { return true; }
                else
                {
                    if (answer.AddToHistory)
                    {
                        string line;
                        if (__instance.Dialog.Type == DialogType.Common)
                        {
                            string answerFormatWithColorName = DialogFormats.AnswerFormatWithColorName;
                            string characterName = deputySpeaker.CharacterName;
                            string arg = ColorUtility.ToHtmlStringRGB(GameHelper.GetPlayerCharacter().Blueprint.Color);
                            line = string.Format(answerFormatWithColorName, arg, characterName, answer.Text);
                        }
                        else
                        {
                            string answerFormatWithoutName = DialogFormats.AnswerFormatWithoutName;
                            line = string.Format(answerFormatWithoutName, answer.Text);
                        }
                        if (!Main.Settings.EnabledAutoSwitchMythicCharacter)
                        {
                            DebuptyAlignmentShift.DebuptyApplyAlignmentShift(answer, deputySpeaker);
                        }
                        EventBus.RaiseEvent(delegate (IDialogHandler h)
                        {
                            h.HandleOnDialogHistory(line);
                        });
                        if (deputySpeaker != null && !deputySpeaker.IsMainCharacter && Main.Settings.EnabledAutoSwitchMythicCharacter && !Main.Settings.DisableMythicMemberIntoDialogue && deputySpeaker.Descriptor.m_IsEssentialForGame)
                        {
                            if (!Game.Instance.Player.MainCharacter.Value.Descriptor.m_IsEssentialForGame) Game.Instance.Player.MainCharacter.Value.Descriptor.AddEssentialMark();
                            if (Main.Settings.SavedMainCharacterGUID == "") Main.Settings.SavedMainCharacterGUID = Game.Instance.Player.MainCharacter.Value.Proxy.Id;
                            if (Game.Instance != null)
                            {
                                DebuptyAlignmentShift.DebuptyApplyAlignmentShift(answer, Game.Instance.Player.MainCharacter.Value);
                                Game.Instance.Player.MainCharacter = deputySpeaker;
                                PFLog.Mods.Error("Char switch 6");
                            }
                        }
                    }
                    return false;
                }



                return true;
            }
        }

        [HarmonyPatch(typeof(Kingmaker.Controllers.Dialog.DialogController), nameof(Kingmaker.Controllers.Dialog.DialogController.StopDialog))]

        internal static class BMC_Auto_MainCharacter_SwitchBack_Patch
        {
            //internal static bool Prefix(Kingmaker.Controllers.Dialog.DialogController __instance) {
            //    //Main.Settings.CheckerWillplayCutScene = true;
            //    if (Main.Settings.SavedMainCharacterGUID != "" && Main.Settings.EnabledAutoSwitchMythicCharacter && !Main.Settings.DisableMythicMemberIntoDialogue)
            //    {
            //        if (__instance.CurrentCue != null && __instance.CurrentCue.OnStop.HasActions)
            //        {
            //            //bool willPlayCutScene = false;
            //            PlayCutscene playCutscene = null;
            //            foreach (var action in __instance.CurrentCue.OnStop.Actions)
            //            {
            //                try
            //                {
            //                    playCutscene = (PlayCutscene)action;
            //                }

            //                catch { }
            //                if (playCutscene!=null && playCutscene.Cutscene != null)
            //                {
            //                    Main.Settings.CheckerWillplayCutScene = true;
            //                    return true;
            //                }
            //            }
            //        }
            //    }
            //    return true;
            //}
            internal static void Postfix(Kingmaker.Controllers.Dialog.DialogController __instance)
            {

                if (Main.Settings.SavedMainCharacterGUID != "" && Main.Settings.EnabledAutoSwitchMythicCharacter && !Main.Settings.DisableMythicMemberIntoDialogue)
                {

                    UnitEntityData mainCharToSet = LogicStructures.GetAllCharacterList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor) && actor.Proxy.Id == Main.Settings.SavedMainCharacterGUID).ToList().SingleOrDefault();
                    if (Game.Instance != null && mainCharToSet != null)
                    {
                        //if (Main.Settings.CheckerWillplayCutScene)
                        //{
                        //    Main.Settings.CheckerWillplayCutScene = false;
                        //    return;
                        //}
                        if (Main.Settings.CharacterSwaped)
                        {
                            UnitEntityData mainUnit = Game.Instance.Player.MainCharacter;
                            Vector3 position;
                            position = new(mainCharToSet.Position.x + sphere.x, mainCharToSet.Position.y, mainCharToSet.Position.z + sphere.z);
                            mainUnit.Position = position;
                            Main.Settings.CharacterSwaped = false;
                        }
                        Game.Instance.Player.MainCharacter = mainCharToSet;
                        PFLog.Mods.Error("Char switch 7");

                        Main.Settings.SavedMainCharacterGUID = "";
                    }
                    Main.Settings.SavedMainCharacterGUID = "";

                }
                if (Main.Settings.SavedMainCharacterGUID == "" && Main.Settings.CharacterSwaped) Main.Settings.CharacterSwaped = false;
            }
        }
    }
}
