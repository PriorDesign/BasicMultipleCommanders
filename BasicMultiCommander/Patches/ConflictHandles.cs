using BasicMultiCommander.Methods;
using HarmonyLib;
using Kingmaker;
using Kingmaker.AreaLogic.Etudes;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Area;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Quests;
using Kingmaker.Blueprints.Root;
using Kingmaker.Designers;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.EventConditionActionSystem.Events;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Persistence;
using Kingmaker.UI.MVVM._VM.ActionBar;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;

//using Owlcat.Runtime.Core;
using System;
using System.Linq;

namespace BasicMultiCommander.Patches
{
    class ConflictHandles
    {

        [HarmonyPatch(typeof(DeactivateTrigger), nameof(DeactivateTrigger.OnDeactivate))]
        internal static class BMC_Devil_ReEnable_BugFix
        {
            internal static bool Prefix(DeactivateTrigger __instance)
            {
                if (__instance.OwnerBlueprint.AssetGuidThreadSafe == "9a3739370f84b0b4196d0e4d326ea3a8")
                {
                    var charList = LogicStructures.GetAllCharacterList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
                    if (charList != null)
                    {
                        foreach (var unit in charList)
                        {
                            //string teamMythic = unit.Descriptor.Progression.GetCurrentMythicClass()?.CharacterClass.NameForAcronym;
                            var teamMythic = unit.Progression.LastMythicClass?.NameForAcronym.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "");

                            if (teamMythic == null) continue;
                            if (!teamMythic.Contains("Demon")) { continue; }
                            else { return false; }
                           

                            // if (teamMythic.Contains(__instance.Comment.Replace("Player is ", ""))) return false;
                        }
                    }
                    return true;
                }
                else {
                    return true;
                }



            }
        }

        [HarmonyPatch(typeof(LockAlignment), nameof(LockAlignment.RunAction))]
        internal static class BMC_LockAlignment_Target_Fixes
        {
            internal static bool Prefix(LockAlignment __instance)
            {
                if (__instance.Owner != null && StaticData.mythicGuidEtudes.Contains(__instance.Owner.AssetGuid.ToString()))
                {
                    
                    string mythicRequired = __instance.Owner.NameSafe();
                    var charList = LogicStructures.GetAllCharacterList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
                    if (charList != null)
                    {
                        foreach (var unit in charList)
                        {
                            //string teamMythic = unit.Descriptor.Progression.GetCurrentMythicClass()?.CharacterClass.NameForAcronym;
                            var teamMythic = unit.Progression.LastMythicClass?.NameForAcronym.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "");

                            if (teamMythic == null) continue;
                            if (!mythicRequired.Contains(teamMythic)) { continue; }
                            else {
                                unit.Descriptor.Alignment.LockAlignment(__instance.AlignmentMask, __instance.TargetAlignment);
                                return false; 
                            
                            }


                            // if (teamMythic.Contains(__instance.Comment.Replace("Player is ", ""))) return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return true;
                }



            }
        }

        [HarmonyPatch(typeof(Conditional), nameof(Conditional.RunAction))]

        internal static class BMC_GoldenDragon_Devil_Legend_Conditional_Patch
        {
            internal static bool Prefix(Conditional __instance)
            {
                if (__instance == null) return true;
                if (__instance.Owner == null) return true;
                if (__instance.Owner.name == null) return true;

                var wayPoint = __instance.Owner.name;

                if (wayPoint == "PlayerIsDragon" || wayPoint == "DevilExAeon" || wayPoint == "DevilExAzata")
                {

                    var charList = LogicStructures.GetAllCharacterList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
                    var requiredMythicPath = __instance.Comment;


                    if (requiredMythicPath.Contains("Unlocked"))
                    {
                        return false;
                    }
                    else
                    {
                        if (charList != null)
                        {
                            foreach (var unit in charList)
                            {
                                //string teamMythic = unit.Descriptor.Progression.GetCurrentMythicClass()?.CharacterClass.NameForAcronym;
                                var teamMythic = unit.Progression.LastMythicClass?.NameForAcronym.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "");

                                if (teamMythic == null) continue;
                                if (requiredMythicPath.Contains(teamMythic)) return false;

                                // if (teamMythic.Contains(__instance.Comment.Replace("Player is ", ""))) return false;
                            }
                        }
                    }
                }
                var guidwayPoint = __instance.Owner.AssetGuid;
                if (guidwayPoint == "36492116b1edecf489a2ccb58d4fc19d")
                {

                    var charList = LogicStructures.GetAllCharacterList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
                    var requiredMythicPath = __instance.name;
                    if (!requiredMythicPath.Contains("f8162b0d-09db-4cee-9675-487b0e6110db")) return true;
                    if (charList != null)
                    {
                        foreach (var unit in charList)
                        {
                            //string teamMythic = unit.Descriptor.Progression.GetCurrentMythicClass()?.CharacterClass.NameForAcronym;
                            var teamMythic = unit.Progression.LastMythicClass?.NameForAcronym.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "");

                            if (teamMythic == null) continue;
                            if (!teamMythic.Contains("Devil")) return false;

                            // if (teamMythic.Contains(__instance.Comment.Replace("Player is ", ""))) return false;
                        }
                    }

                }
                //switch (wayPoint.NameForAcronym)
                //{
                //    case "PlayerIsDragon":


                //        break;
                //    case "DevilExAeon":
                //        OnGUI(objectiveStatus, source);
                //        break;
                //    case "DevilExAzata":
                //        OnGUI(objectiveStatus, source);
                //        break;
                //}
                //if (wayPoint.NameForAcronym == "PlayerIsDragon"  && !Game.Instance.Player.MainCharacter.Value.Descriptor.Progression.GetCurrentMythicClass().CharacterClass.NameForAcronym.Contains("Dragon")) return false;
                //if (wayPoint.NameForAcronym == "DevilExAeon" && !Game.Instance.Player.MainCharacter.Value.Descriptor.Progression.GetCurrentMythicClass().CharacterClass.NameForAcronym.Contains("Aeon")) return false;
                //if (wayPoint.NameForAcronym == "DevilExAzata" && !Game.Instance.Player.MainCharacter.Value.Descriptor.Progression.GetCurrentMythicClass().CharacterClass.NameForAcronym.Contains("Azata")) return false;





                return true;
            }
        }
        [HarmonyPatch(typeof(StartEtude), nameof(StartEtude.RunAction))]

        internal static class BMC_GoldenDragon_Devil_Legend_StartEtude_Patch
        {
            internal static bool Prefix(StartEtude __instance)
            {
                if (__instance == null) return true;
                if (__instance.Etude == null) return true;
                if (Main.Settings.CharacterLinkedRomance && StaticData.romanceGuid.Contains(__instance.Etude.ToString()))
                {
                    bool romanceFound = true;
                    //string romanceTarget = "";
                    //foreach (string romance in StaticData.romanceGuid)
                    //{
                    //    if (__instance.Etude.deserializedGuid == romance)
                    //    {
                    //        romanceFound = true;
                    //        romanceTarget = romance;
                    //        break;
                    //    }
                    //}
                    if (romanceFound)
                    {
                        switch (__instance.Etude.ToString())
                        {
                            case "fe6ee37b2aa394e4eaa51208cf7d7f86":
                                CharacterLinkedRomance.CamelliaRomance = Game.Instance.Player.MainCharacter.Value.Proxy.Id;
                                break;
                            case "e49702f590611644580f09c8f9ef0e5b":
                                CharacterLinkedRomance.CamelliaRomance = Game.Instance.Player.MainCharacter.Value.Proxy.Id;
                                break;
                            case "8541453b31379964e834cf2309444388":
                                CharacterLinkedRomance.DaeranRomance = Game.Instance.Player.MainCharacter.Value.Proxy.Id;
                                break;
                            case "af394a43ba7c1314bb31aea74a5e4c0e":
                                CharacterLinkedRomance.LannRomance = Game.Instance.Player.MainCharacter.Value.Proxy.Id;
                                break;
                            case "14650554734f4adc81af61e442fd628b":
                                CharacterLinkedRomance.UlbrigRomance = Game.Instance.Player.MainCharacter.Value.Proxy.Id;
                                break;
                            case "efb130b8a22c9534ca40a1e41ef8e931":
                                CharacterLinkedRomance.SosielRomance = Game.Instance.Player.MainCharacter.Value.Proxy.Id;
                                break;
                            case "6a3fdd0758fe78d4aa2c3b26d7614fbc":
                                CharacterLinkedRomance.ArueshalaeRomance = Game.Instance.Player.MainCharacter.Value.Proxy.Id;
                                break;
                            case "33c4c2f66f2461e4993df21566252079":
                                CharacterLinkedRomance.WenduagRomance = Game.Instance.Player.MainCharacter.Value.Proxy.Id;
                                break;
                            default:
                                break;
                        }
                        CharacterLinkedRomance.DumpRomanceJson();


                    }
                }

                if (__instance.Owner == null) return true;
                if (__instance.Owner.name == null) return true;

                var wayPoint = __instance.Owner.name;

                if (wayPoint == "MythicPathFailed" || wayPoint == "MythicPathChanged")
                {
                    if (__instance.Etude.deserializedGuid != "1ced7ca79fec4da5b68ceeb7783792c6" && __instance.Etude.deserializedGuid != "1aa56301f56644fa8605ac991ab81091") return true; //AeonPathChanged &&  Failed
                    var charList = LogicStructures.GetAllCharacterList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();

                    //var requiredMythicPath = __instance.Etude.Cached.name;
                    if (charList != null)
                    {
                        int aeonCommanders = 0;
                        foreach (var unit in charList)
                        {
                            var teamMythic = unit.Progression.LastMythicClass?.NameForAcronym.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "");
                            if (teamMythic == null) continue;
                            if (!teamMythic.Contains("Devil"))
                            {
                                return false;

                            }
                            if (teamMythic.Contains("Aeon"))
                            {
                                aeonCommanders++;

                            }
                        }
                        Etude fact = Game.Instance.Player.EtudesSystem.Etudes.GetFact(LogicStructures.GetDevilEx());
                        if (aeonCommanders > 1 || !fact.IsPlaying) return false;
                    }
                }
                //switch (wayPoint.NameForAcronym)
                //{
                //    case "PlayerIsDragon":


                //        break;
                //    case "DevilExAeon":
                //        OnGUI(objectiveStatus, source);
                //        break;
                //    case "DevilExAzata":
                //        OnGUI(objectiveStatus, source);
                //        break;
                //}
                //if (wayPoint.NameForAcronym == "PlayerIsDragon"  && !Game.Instance.Player.MainCharacter.Value.Descriptor.Progression.GetCurrentMythicClass().CharacterClass.NameForAcronym.Contains("Dragon")) return false;
                //if (wayPoint.NameForAcronym == "DevilExAeon" && !Game.Instance.Player.MainCharacter.Value.Descriptor.Progression.GetCurrentMythicClass().CharacterClass.NameForAcronym.Contains("Aeon")) return false;
                //if (wayPoint.NameForAcronym == "DevilExAzata" && !Game.Instance.Player.MainCharacter.Value.Descriptor.Progression.GetCurrentMythicClass().CharacterClass.NameForAcronym.Contains("Azata")) return false;





                return true;
            }
            internal static void Postfix(StartEtude __instance)
            {
                if (__instance == null) return;
                if (__instance.Etude == null) return;

            }

        }
        
        [HarmonyPatch(typeof(CompleteEtude), nameof(CompleteEtude.RunAction))]

        internal static class BMC_GoldenDragon_Devil_Legend_Complete_Etude_Patch
        {
            internal static bool Prefix(CompleteEtude __instance)
            {
                if (__instance == null) return true;
                if (__instance.Etude == null) return true;
                if (__instance.Owner == null) return true;
                if (__instance.Owner.name == null) return true;

                var wayPoint = __instance.Owner.AssetGuid;

                if (wayPoint == "36492116b1edecf489a2ccb58d4fc19d")
                {
                    if (__instance.Etude.deserializedGuid != "2d8dc72cd55439646adb2eda73ab1829" && __instance.Etude.deserializedGuid != "7674d9f32e0c6624a8c8ad12eb6d4afc") return true;
                    var charList = LogicStructures.GetAllCharacterList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();

                    //var requiredMythicPath = __instance.Etude.Cached.name;
                    if (charList != null && (__instance.Etude.deserializedGuid == "2d8dc72cd55439646adb2eda73ab1829" || __instance.Etude.deserializedGuid == "7674d9f32e0c6624a8c8ad12eb6d4afc"))
                    {
                        foreach (var unit in charList)
                        {
                            var teamMythic = unit.Progression.LastMythicClass?.NameForAcronym.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "");
                            if (teamMythic == null) continue;

                            if (teamMythic.Contains("Azata"))
                            {
                                return false;
                            }
                        }
                    }
                }
                //switch (wayPoint.NameForAcronym)
                //{
                //    case "PlayerIsDragon":


                //        break;
                //    case "DevilExAeon":
                //        OnGUI(objectiveStatus, source);
                //        break;
                //    case "DevilExAzata":
                //        OnGUI(objectiveStatus, source);
                //        break;
                //}
                //if (wayPoint.NameForAcronym == "PlayerIsDragon"  && !Game.Instance.Player.MainCharacter.Value.Descriptor.Progression.GetCurrentMythicClass().CharacterClass.NameForAcronym.Contains("Dragon")) return false;
                //if (wayPoint.NameForAcronym == "DevilExAeon" && !Game.Instance.Player.MainCharacter.Value.Descriptor.Progression.GetCurrentMythicClass().CharacterClass.NameForAcronym.Contains("Aeon")) return false;
                //if (wayPoint.NameForAcronym == "DevilExAzata" && !Game.Instance.Player.MainCharacter.Value.Descriptor.Progression.GetCurrentMythicClass().CharacterClass.NameForAcronym.Contains("Azata")) return false;





                return true;
            }
            internal static void Postfix(CompleteEtude __instance)
            {
                if (__instance == null || __instance.Etude == null || __instance.Owner == null) return;
                if (!Main.Settings.EnabledAutoSwitchMythicCharacter || !Main.Settings.EnabledRomanceAutoSwitchMythicCharacter) return;
                if (Main.Settings.EtudesSaved != "" && __instance.Etude.Guid == Main.Settings.EtudesSaved) { // && StaticData.romanceAreaMechnicaGuid.Contains(__instance.Etude.Get().AddedAreaMechanics.FirstOrDefault().deserializedGuid.ToString())) {
                    if (Main.Settings.EtudesSavedMainCharacterGUID != "" && Main.Settings.EnabledAutoSwitchMythicCharacter && !Main.Settings.DisableMythicMemberIntoDialogue)
                    {

                        UnitEntityData mainCharToSet = LogicStructures.GetAllCharacterList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor) && actor.Proxy.Id == Main.Settings.EtudesSavedMainCharacterGUID).ToList().SingleOrDefault();
                        if (Game.Instance != null && mainCharToSet != null)
                        {
                            //if (Main.Settings.CheckerWillplayCutScene)
                            //{
                            //    Main.Settings.CheckerWillplayCutScene = false;
                            //    return;
                            //}
                            Game.Instance.Player.MainCharacter = mainCharToSet;

                            //Main.Settings.EtudesSavedMainCharacterGUID = "";
                        }
                        Main.Settings.EtudesSaved = "";
                        Main.Settings.EtudesSavedMainCharacterGUID = "";

                    }
                }


            }

            
         }



        //DLC import conflicts
        [HarmonyPatch(typeof(UnitHelper), nameof(UnitHelper.IsCustomCompanion), new Type[] { typeof(UnitEntityData), typeof(BlueprintCampaign) })]

        internal static class BMC_DlcOne_ImportPatch
        {
            internal static void Postfix(ref UnitEntityData _this, ref BlueprintCampaign reserveCampaign, ref bool __result)
            {
                if (!LogicStructures.isPlayerActorBlueprintType(_this))
                {
                    return;
                }


                if (reserveCampaign != null && LogicStructures.isPlayerActorBlueprintType(_this) && !_this.IsMainCharacter)
                {
                    __result = true;
                }

            }
        }
        [HarmonyPatch(typeof(Game), nameof(Game.LoadNewGame), new Type[] { typeof(BlueprintAreaPreset), typeof(SaveInfo) })]

        internal static class BMC_DLC_SaveImport_Patch
        {
            internal static void Postfix(ref BlueprintAreaPreset preset, ref SaveInfo importFrom, Game __instance)
            {

                var charList = LogicStructures.GetAllCharacterList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
                if (charList != null)
                {
                    foreach (var unit in charList)
                    {
                        if (__instance.Player.MainCharacter.Value == unit) continue;
                        if (unit.IsImportedCustomCompanion) unit.IsImportedCustomCompanion = false;
                    }
                }

            }
        }
        [HarmonyPatch(typeof(RemoveItemFromPlayer), nameof(RemoveItemFromPlayer.RunAction))]

        internal static class BMC_GoldenDragon_Class_Patch
        {
            internal static bool Prefix(RemoveItemFromPlayer __instance)
            {
                if (__instance == null || __instance.Owner == null) return true;
                if (!__instance.ItemToRemove.NameForAcronym.Contains("Artifact_") || !__instance.ItemToRemove.NameForAcronym.Contains("CloakItem")) return true;

                try
                {
                    //BlueprintFeature wayPoint = (BlueprintFeature)__instance.Owner;
                    //if (wayPoint == null) return true;
                    //if (!Game.Instance.Player.MainCharacter.Value.Descriptor.Progression.GetCurrentMythicClass().CharacterClass.NameForAcronym.Contains("Dragon")) return false;
                    var charList = LogicStructures.GetAllCharacterList().Where(actor => LogicStructures.isPlayerActorBlueprintType(actor)).ToList();
                    if (charList != null)
                    {
                        foreach (var unit in charList)
                        {
                            var teamMythic = unit.Progression.LastMythicClass?.NameForAcronym.Replace("MythicClass", "").Replace("Class", "").Replace("Golden", "");
                            if (__instance.ItemToRemove.NameForAcronym.Contains(teamMythic)) return false;


                        }
                    }
                }
                catch
                {
                }


                return true;

            }
        }

        [HarmonyPatch(typeof(AddItemToPlayer), nameof(AddItemToPlayer.RunAction))]

        internal static class BMC_GoldenDragon_Class_Cloak_Patch
        {
            internal static bool Prefix(AddItemToPlayer __instance)
            {
                if (__instance == null || __instance.Owner == null) return true;
                if (__instance.m_ItemToGive.Get().NameForAcronym != "Artifact_DragonCloakItem") return true;
                try
                {
                    BlueprintFeature wayPoint = (BlueprintFeature)__instance.Owner;
                    var halEtude = ResourcesLibrary.TryGetBlueprint<BlueprintEtude>("d946de0a36b31e947a99728576ddf436");
                    //if (etudeSystem.GetSavedState(halEtude) == EtudeState.Unknown)
                    //{

                    //}

                    if (wayPoint.NameForAcronym != "DragonLevel1Immunities") return true;
                    var cloakItem = __instance.m_ItemToGive;

                    if (wayPoint.NameForAcronym == "DragonLevel1Immunities" && GameHelper.GetPlayerCharacter().Inventory.Count(cloakItem) > 0) return false;
                    if (Game.Instance.Player.QuestBook.GetQuest(ResourcesLibrary.TryGetBlueprint<BlueprintQuest>("084960ae22f408e4282f537a8a9debc1")) == null && LogicStructures.IsMainCampaign())
                    {
                        Game.Instance.Player.QuestBook.GiveObjective(ResourcesLibrary.TryGetBlueprint<BlueprintQuest>("084960ae22f408e4282f537a8a9debc1").Objectives.First());
                        Game.Instance.Player.EtudesSystem.StartEtude(ResourcesLibrary.TryGetBlueprint<BlueprintEtude>("d946de0a36b31e947a99728576ddf436"));
                    }
                }
                catch
                {
                    if (__instance.m_ItemToGive.Get().NameForAcronym == "Artifact_DragonCloakItem" && GameHelper.GetPlayerCharacter().Inventory.Count(__instance.m_ItemToGive) == 0)
                    {
                        if (Game.Instance.Player.QuestBook.GetQuest(ResourcesLibrary.TryGetBlueprint<BlueprintQuest>("084960ae22f408e4282f537a8a9debc1")) == null && LogicStructures.IsMainCampaign())
                        {
                            Game.Instance.Player.QuestBook.GiveObjective(ResourcesLibrary.TryGetBlueprint<BlueprintQuest>("084960ae22f408e4282f537a8a9debc1").Objectives.First());
                            Game.Instance.Player.EtudesSystem.StartEtude(ResourcesLibrary.TryGetBlueprint<BlueprintEtude>("d946de0a36b31e947a99728576ddf436"));
                        }
                    }
                }

                return true;

            }
        }

    }
}
