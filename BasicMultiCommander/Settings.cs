using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityModManagerNet;

namespace BasicMultiCommander
{
    public class Settings : UnityModManager.ModSettings
    {
        public int CommanderSpawnLimit = 0;
        public bool MyBoolOption = false;
        public bool DebugOptions = false;
        public bool AfterIntroCommanderSpawning = false;
        public bool EnabledMemberMythicEtude = false;
        public bool EnabledMemberMythicCommonDialogue = false;
        public bool EnabledMemberMythicEtudeDeep = false;
        public bool DisableMythicMemberIntoDialogue = false;
        public bool EnabledInstructions = true;
        public bool EnabledMultipleSameMythic = false;
        public bool EnabledAutoSwitchMythicCharacter = false;
        public bool CheckerWillplayCutScene = false;
        public bool CharacterSwaped = false;
        public bool CharacterLinkedRomance = false;
        public bool DisableDialogueChecksMythicCharacter = false;
        public string MyTextOption = "Hello";
        public string SavedMainCharacterGUID = "";
        public string EtudesSavedMainCharacterGUID = "";
        public string EtudesSaved = "";
        public string CurrentMythicWeather = "";
        public bool ChangeMythicWeather = false;
        public bool EnabledRomanceAutoSwitchMythicCharacter = false;

        //public string WenduagRomance = "";
        //public string ArueshalaeRomance = "";
        //public string SosielRomance = "";
        //public string CamelliaRomance = "";
        //public string DaeranRomance = "";
        //public string LannRomance = "";
        //public string UlbrigRomance = "";
        public override void Save(UnityModManager.ModEntry modEntry)
        {
            Save(this, modEntry);
        }
    }
}
