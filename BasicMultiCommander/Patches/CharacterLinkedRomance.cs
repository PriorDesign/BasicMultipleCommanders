using BasicMultiCommander.Methods;
using HarmonyLib;
using Kingmaker;
using Kingmaker.Blueprints.Area;
using Kingmaker.EntitySystem.Persistence;
using Kingmaker.UI.BookEvent;
using Kingmaker.UI.Models.Log.CombatLog_ThreadSystem;
using Kingmaker.UI.Models.Log.CombatLog_ThreadSystem.LogThreads.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityModManagerNet;
using static UnityModManagerNet.UnityModManager;

namespace BasicMultiCommander.Patches
{
    public class RoamceData
    {
        public string WenduagRomance = "";
        public string ArueshalaeRomance = "";
        public string SosielRomance = "";
        public string CamelliaRomance = "";
        public string DaeranRomance = "";
        public string LannRomance = "";
        public string UlbrigRomance = "";
    }
    public static class CharacterLinkedRomance
    {

        public static string SettingsPath => $"{Main.modEntry.Path}ModStorage/{Game.Instance.Player.GameId}.json";
        public static string SettingsPathDebug => $"{Main.modEntry.Path}ModStorage/Debug.json";
        public static string WenduagRomance = "";
        public static string ArueshalaeRomance = "";
        public static string SosielRomance = "";
        public static string CamelliaRomance = "";
        public static string DaeranRomance = "";
        public static string LannRomance = "";
        public static string UlbrigRomance = "";


        //LoadGame Patch
        [HarmonyPatch(typeof(LogThreadService), nameof(LogThreadService.OnGameLoaded))]

        internal static class SwitchSaveRoamce
        {
            internal static void Postfix(LogThreadService __instance)
            {
                string loadedGameID = Game.Instance.Player.GameId;
                if (File.Exists(SettingsPath))
                {
                    try
                    {
                        UpdateRomanceState(LoadRomanceJson(loadedGameID));
                    }
                    catch (JsonException)
                    {
                        RoamceData romanceData = new();
                        UpdateRomanceState(romanceData);
                    }
                }
                else {
                    RoamceData romanceData = new();
                    string folderPath = Main.modEntry.Path + "ModStorage";

                    try
                    {
                        if (Directory.Exists(folderPath))
                        {
                        }
                        else  {
                            DirectoryInfo directory = Directory.CreateDirectory(folderPath);
                        }

                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                    UpdateRomanceState(romanceData);
                    DumpRomanceJson();
                }
            }
        }
        public static bool UpdateRomanceState(RoamceData romanceData) {
            WenduagRomance = romanceData.WenduagRomance;
            ArueshalaeRomance = romanceData.ArueshalaeRomance;
            SosielRomance = romanceData.SosielRomance;
            CamelliaRomance = romanceData.CamelliaRomance;
            DaeranRomance = romanceData.DaeranRomance;
            LannRomance = romanceData.LannRomance;
            UlbrigRomance = romanceData.UlbrigRomance;
            return true;
        }

        public static RoamceData LoadRomanceJson(string gameID)
        {
            string filePath = Main.modEntry.Path + "ModStorage/" + gameID + ".json";

            using var settingsReader = File.OpenText(filePath);
            using var jsonReader = new JsonTextReader(settingsReader);
            RoamceData romanceData = new();
            romanceData = JsonSerializer.CreateDefault().Deserialize<RoamceData>(jsonReader);
            return romanceData;
        }

        public static bool DumpRomanceJson()
        {
            //string filePath = modEntry.Path + "ModStorage/" + saveInfo.GameId + ".json";
            RoamceData romanceData = new();
            romanceData.WenduagRomance = WenduagRomance;
            romanceData.ArueshalaeRomance = ArueshalaeRomance;
            romanceData.SosielRomance = SosielRomance;
            romanceData.CamelliaRomance = CamelliaRomance;
            romanceData.DaeranRomance = DaeranRomance;
            romanceData.LannRomance = LannRomance;
            romanceData.UlbrigRomance = UlbrigRomance;
            string jsonString = JsonUtility.ToJson(romanceData);
            string fullPath = Path.Combine(Application.persistentDataPath, SettingsPath);
            try
            {
                File.WriteAllText(fullPath, jsonString);
            }
            catch {
                PFLog.Default.Error("Error finding 'ModStorage' folder when writing! Relation link may not be saved!");
            }
            return true;
        }

        public static bool DumpRomanceJsonDebug()
        {
            //string filePath = modEntry.Path + "ModStorage/" + saveInfo.GameId + ".json";
            RoamceData romanceData = new();
            romanceData.WenduagRomance = WenduagRomance;
            romanceData.ArueshalaeRomance = ArueshalaeRomance;
            romanceData.SosielRomance = SosielRomance;
            romanceData.CamelliaRomance = CamelliaRomance;
            romanceData.DaeranRomance = DaeranRomance;
            romanceData.LannRomance = LannRomance;
            romanceData.UlbrigRomance = UlbrigRomance;
            string jsonString = JsonUtility.ToJson(romanceData);
            string fullPath = Path.Combine(Application.persistentDataPath, SettingsPathDebug);
            try
            {
                File.WriteAllText(fullPath, jsonString);
            }
            catch
            {
                PFLog.Default.Error("Error finding 'ModStorage' folder when writing! Relation link may not be saved!");
            }
            return true;
        }
    }
}
