using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Visual.CharacterSystem;
using UnityEngine;

namespace BasicMultiCommander.Features
{
    class WeatherController
    {
        public static void WeatherSwitchGuiChinese(UnitEntityData character)
        {
            var characterMythic = character.Descriptor.Progression;
            if (Main.Settings.ChangeMythicWeather && characterMythic.LastMythicClass != null )
            {
                GUILayout.Toggle(character.Progression.LastMythicClass?.NameForAcronym == Main.Settings.CurrentMythicWeather, "", GUILayout.ExpandWidth(false));
                GUILayout.Space(10);
                if (GUILayout.Button(character.Progression.LastMythicClass?.LocalizedName+"[ 眷泽天相 ]", GUI.skin.box, GUILayout.ExpandWidth(false)))
                {
                    if (Main.Settings.CurrentMythicWeather == character.Progression.LastMythicClass?.NameForAcronym)
                    {
                        Main.Settings.CurrentMythicWeather = "";
                        return;
                    }
                    if ( characterMythic.LastMythicClass != BlueprintRoot.Instance.Progression.MythicStartingClass && characterMythic.LastMythicClass != BlueprintRoot.Instance.Progression.MythicCompanionClass) Main.Settings.CurrentMythicWeather = character.Progression.LastMythicClass?.NameForAcronym;
                }
            }
        }
        public static void WeatherSwitchGuiEnglish(UnitEntityData character)
        {
            var characterMythic = character.Descriptor.Progression;
            if (Main.Settings.ChangeMythicWeather && characterMythic.LastMythicClass != null )
            {
                GUILayout.Toggle(character.Progression.LastMythicClass?.NameForAcronym == Main.Settings.CurrentMythicWeather, "", GUILayout.ExpandWidth(false));
                GUILayout.Space(10);
                if (GUILayout.Button(character.Progression.LastMythicClass?.LocalizedName + "[ Mythic Weather ]", GUI.skin.box, GUILayout.ExpandWidth(false)))
                {
                    if (Main.Settings.CurrentMythicWeather == character.Progression.LastMythicClass?.NameForAcronym)
                    {
                        Main.Settings.CurrentMythicWeather = "";
                        return;
                    }
                    if (characterMythic.LastMythicClass != BlueprintRoot.Instance.Progression.MythicStartingClass && characterMythic.LastMythicClass != BlueprintRoot.Instance.Progression.MythicCompanionClass) Main.Settings.CurrentMythicWeather = character.Progression.LastMythicClass?.NameForAcronym;
                }
            }
        }
    }
}
