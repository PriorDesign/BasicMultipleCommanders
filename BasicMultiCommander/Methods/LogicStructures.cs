using BasicMultiCommander.Patches;
using HarmonyLib;
using Kingmaker;
using Kingmaker.AreaLogic.Etudes;
using Kingmaker.AreaLogic.QuestSystem;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Quests;
using Kingmaker.Blueprints.Root;
using Kingmaker.Controllers;
using Kingmaker.Designers;
using Kingmaker.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.Designers.EventConditionActionSystem.Evaluators;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.GameModes;
using Kingmaker.Localization.Shared;
using Kingmaker.Settings;
using Kingmaker.UI.Common;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using Kingmaker.Visual.CharacterSystem;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityModManagerNet;
using static Kingmaker.AreaLogic.Etudes.EtudesSystem;
using static Kingmaker.Blueprints.Root.CheatRoot;
using static Kingmaker.Localization.Shared.LocaleExtensions;
using Utilities = Kingmaker.Cheats.Utilities;
namespace BasicMultiCommander.Methods
{
    class LogicStructures
    {
        public static Vector3 sphere = 2.5f * UnityEngine.Random.insideUnitSphere;
        public static float interval = 0.1f;
        public static float range = 0.1f;
        public static bool IsInGame => Game.Instance.Player?.Party.Any() ?? false;
        //public static bool Enabled;
        //public static Settings Settings;
        //public static UnitEntityData newCommanderUnit;
        public static int GetCommanderNumber()
        {
            var tempResult = GetAllCharacterList().Where(actor => isPlayerActorBlueprintType(actor)).ToList().Count;
            //  +GetPartyList().Where(actor => isPlayerActorBlueprintType(actor)).ToList().Count
            //    + GetDetachedPartyList().Where(actor => isPlayerActorBlueprintType(actor)).ToList().Count 
            //   + GetAllCharacterList().Where(actor => isPlayerActorBlueprintType(actor)).ToList().Count;
            return tempResult;
        }

        public static int GetSameMythicUnitNum(BlueprintCharacterClass mythicClass)
        {
            var tempResult = GetAllCharacterList().Where(actor => isPlayerActorBlueprintType(actor) && IsntBasicMythics(mythicClass) && actor.Progression.LastMythicClass == mythicClass).ToList().Count;
            //  +GetPartyList().Where(actor => isPlayerActorBlueprintType(actor)).ToList().Count
            //    + GetDetachedPartyList().Where(actor => isPlayerActorBlueprintType(actor)).ToList().Count 
            //   + GetAllCharacterList().Where(actor => isPlayerActorBlueprintType(actor)).ToList().Count;
            return tempResult;
        }
        public static int GetPartyCommanderNumber()
        {
            var tempResult = GetPartyList().Where(actor => isPlayerActorBlueprintType(actor)).ToList().Count;
            //  +GetPartyList().Where(actor => isPlayerActorBlueprintType(actor)).ToList().Count
            //    + GetDetachedPartyList().Where(actor => isPlayerActorBlueprintType(actor)).ToList().Count 
            //   + GetAllCharacterList().Where(actor => isPlayerActorBlueprintType(actor)).ToList().Count;
            return tempResult;
        }
        public static int GetCrusadeCommanderNumber()
        {
            var tempResult = GetAllCharacterList().Where(actor => isPlayerActorBlueprintType(actor)).ToList().Count;
            //  +GetPartyList().Where(actor => isPlayerActorBlueprintType(actor)).ToList().Count
            //    + GetDetachedPartyList().Where(actor => isPlayerActorBlueprintType(actor)).ToList().Count 
            //   + GetAllCharacterList().Where(actor => isPlayerActorBlueprintType(actor)).ToList().Count;
            return tempResult;
        }
        public static bool IsntBasicMythics(BlueprintCharacterClass mythicClass)
        {
            var checkResult = (mythicClass != BlueprintRoot.Instance.Progression.MythicCompanionClass) && (mythicClass != BlueprintRoot.Instance.Progression.MythicStartingClass) && (mythicClass.AssetGuidThreadSafe != "53cb5892d19e4c4586c9dd4d6337d943");
            return checkResult;
        }

        public static bool IsInfirstScene()
        {
            if (!IsInGame) return false;
            var questList = Game.Instance?.Player?.QuestBook.Quests.ToArray();
            foreach (Quest quest in questList)
            {
                if (quest.Blueprint.AssetGuidThreadSafe == "55a534760bb7a204bbe0906ab68d7b72" && quest.State != QuestState.Completed) return true;
            }
            return false;
        }
        public static bool MythicLvEightQuestCompleted(BlueprintCharacterClass mythicClass)
        {

            return true;
        }



        public static List<UnitEntityData> GetPartyList()
        {
            return Game.Instance.Player.Party;
        }

        public static List<UnitEntityData> GetDetachedPartyList()
        {
            return Game.Instance.Player.PartyAndPetsDetached;
        }
        public static bool GetCharacterClass(string classGuid)
        {
            return ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(classGuid);
        }
        public static bool TargetIsInGame(List<string> characterList, string uuid)
        {
            return characterList.Contains(uuid);
        }
        public static BlueprintEtude GetDevilEx()
        {
            return ResourcesLibrary.TryGetBlueprint<BlueprintEtude>("1f8f094f7dcf4c6e8ddead6f1900c57b");
        }
        public static bool IsMainCampaign()
        {
            BlueprintEtude dlc1etude = ResourcesLibrary.TryGetBlueprint<BlueprintEtude>("99622b80d692457890f58f73ed864f30");
            if (dlc1etude != null )
            {

                Game.Instance.Player.EtudesSystem.m_EtudesData.TryGetValue(dlc1etude, out var value);
                if (!Game.Instance.Player.EtudesSystem.EtudeIsNotStarted(dlc1etude) && value != EtudeState.Started) return false;
            }
            BlueprintEtude dlc3etude = ResourcesLibrary.TryGetBlueprint<BlueprintEtude>("995ba39f45f44e1dba01e18efc55b0ec");

            if ( dlc3etude != null)
            {
                Game.Instance.Player.EtudesSystem.m_EtudesData.TryGetValue(dlc3etude, out var value);
                if (!Game.Instance.Player.EtudesSystem.EtudeIsNotStarted(dlc3etude) && value != EtudeState.Started) return false;
            }

            return true;
        }

        public static List<UnitEntityData> GetAllCharacterList()
        {
            return Game.Instance.Player.AllCharacters;
        }
        public static UnitEntityData SpawnCommanderUnit(BlueprintUnit newCommanderUnit, int count)
        {
            UnitEntityData startGameUnit = null;
            var spawnPosition = Game.Instance.Player.MainCharacter.Value.Position;
            if (!(newCommanderUnit == null))
            {
                startGameUnit = Game.Instance.EntityCreator.SpawnUnit(newCommanderUnit, spawnPosition, Quaternion.identity, Game.Instance.State.LoadedAreaState.MainState);

            }
            return startGameUnit;
        }


        public static bool isPlayerActorBlueprintType(UnitEntityData characterUnit) => StaticData.playerActorBlueprintTypeGuidList.Contains(characterUnit.Blueprint.AssetGuidThreadSafe);
        //=> characterUnit.Blueprint.AssetGuidThreadSafe == playerActorBlueprintTypeGUID;

        private static bool isSameAsMainActorBlueprintType(UnitEntityData characterUnit) => characterUnit.Blueprint == Game.Instance.Player.MainCharacter.Value.Blueprint;
    }
}
