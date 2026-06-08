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
using static Kingmaker.Blueprints.Root.CheatRoot;
using static Kingmaker.Localization.Shared.LocaleExtensions;
using static UnityModManagerNet.UnityModManager;
using Utilities = Kingmaker.Cheats.Utilities;
namespace BasicMultiCommander
{
    internal static class Main
    {
        public static Vector3 sphere = 2.5f * UnityEngine.Random.insideUnitSphere;
        public static ModEntry modEntry { get; set; } = null;
        public static float interval = 0.1f;
        public static float range = 0.1f;
        public static bool IsInGame => Game.Instance.Player?.Party.Any() ?? false;
        public static bool Enabled;
        public static Settings Settings;
        //public static UnitEntityData newCommanderUnit;
        internal static Harmony HarmonyInstance;
        //public static bool checkMythicMark;

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            
            HarmonyInstance = new Harmony(modEntry.Info.Id);
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
            Settings = Settings.Load<Settings>(modEntry);
            Settings.SavedMainCharacterGUID = "";
            modEntry.OnToggle = OnToggle;
            modEntry.OnGUI = OnGUI;
            modEntry.OnSaveGUI = OnSaveGUI;
            Main.modEntry = modEntry;
            return true;
        }

        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            Enabled = value;

            return true;
        }
        static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if (SettingsRoot.Game.Main.Localization == UILocale.zhCN) MenuGui.ChineseLanguageGui();
            else MenuGui.EgnlishLanguageGui();
        }

        static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            Settings.Save(modEntry);
        }
       

    }
}