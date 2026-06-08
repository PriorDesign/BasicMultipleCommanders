using BasicMultiCommander.Features;
using BasicMultiCommander.Methods;
using BasicMultiCommander.Patches;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Designers;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.GameModes;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;
using Kingmaker.Visual.CharacterSystem;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using Utilities = Kingmaker.Cheats.Utilities;

namespace BasicMultiCommander
{
    class MenuGui
    {
        //public static  guiOptions;
        public static GUIStyle widthLimitation;
        public static Vector3 sphere = 2.5f * UnityEngine.Random.insideUnitSphere;
        public static float interval = 0.1f;
        public static float range = 0.1f;
        public static bool IsInGame => Game.Instance.Player?.Party.Any() ?? false;
        //public static bool Enabled;
        //public static Settings Settings;
        public static UnitEntityData newCommanderUnit;
        //internal static Harmony HarmonyInstance;
        private static string buttonName = null;
        //public static bool checkMythicMark;
        public static void EgnlishLanguageGui()
        {
            if (IsInGame)
            {
                GUILayout.BeginVertical();
                GUILayout.Label("WARING!!! This mod is still under development, use at your own risk.", GUILayout.ExpandWidth(false));
                GUILayout.Space(10);
                GUILayout.Label("Deputy Characters can be spawn or marked Main before FINISHING the Intro Quest.", GUILayout.ExpandWidth(false));
                GUILayout.Label("After intro those will be hide behind Debug Option for Immersion reasons.", GUILayout.ExpandWidth(false));
                Main.Settings.EnabledInstructions = GUILayout.Toggle(Main.Settings.EnabledInstructions, "  Instructions", GUILayout.ExpandHeight(false));

                GUILayout.Label("--------------------------------------------------------------------------------------------------------", GUILayout.ExpandWidth(false));
                GUILayout.Space(25);
                GUILayout.EndVertical();

                var commanderCount = 0;
                newCommanderUnit = null;
                commanderCount = LogicStructures.GetCommanderNumber();
                if (Main.Settings.AfterIntroCommanderSpawning || LogicStructures.IsInfirstScene())
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Spawn Limit", GUILayout.ExpandWidth(false));
                    GUILayout.Space(10);

                    Main.Settings.CommanderSpawnLimit = ((int)GUILayout.HorizontalSlider(Main.Settings.CommanderSpawnLimit, 0, 5, GUILayout.Width(300f)));
                    var numLimit = Main.Settings.CommanderSpawnLimit;
                    GUILayout.Label(numLimit.ToString(), GUILayout.ExpandWidth(false));
                    GUILayout.EndHorizontal();
                    if (commanderCount - 1 < numLimit)
                    {
                        buttonName = "Add";

                    }
                    else
                    {
                        buttonName = "No Slot Aviliable";
                    }
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Spawn More Commanders:", GUILayout.ExpandWidth(false));
                    GUILayout.Space(10);
                    if (GUILayout.Button(buttonName, GUI.skin.box, GUILayout.ExpandWidth(false)))
                    {
                        commanderCount = LogicStructures.GetCommanderNumber();
                        if (commanderCount - 1 < numLimit)
                        {
                            newCommanderUnit = LogicStructures.SpawnCommanderUnit(Utilities.GetBlueprintByGuid<BlueprintUnit>(StaticData.playerActorBlueprintTypeGUID), 1);
                            if (newCommanderUnit != null)
                            {
                                newCommanderUnit = GameHelper.RecruitNPC(newCommanderUnit, newCommanderUnit.Blueprint);
                                Game.Instance.Player.AttachPartyMember(newCommanderUnit);

                            }
                            buttonName = "Add";

                        }
                        else
                        {
                            buttonName = "No Slot Aviliable";
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginVertical();
                    GUILayout.Label("--------------------------------------------------------------------------------------------------------", GUILayout.ExpandWidth(false));
                    GUILayout.Space(25);
                    GUILayout.EndVertical();

                }




                GUILayout.BeginHorizontal();
                Main.Settings.CharacterLinkedRomance = GUILayout.Toggle(Main.Settings.CharacterLinkedRomance, "  Romance Relations linked to Initiators", GUILayout.ExpandHeight(false));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (Main.Settings.EnabledInstructions) GUILayout.Label("Romance Relations will be linked to their Initiator, dialogue options will be locked for other characters, can be manually setted in Settings.xml using character's guid.", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                Main.Settings.EnabledMemberMythicCommonDialogue = GUILayout.Toggle(Main.Settings.EnabledMemberMythicCommonDialogue, "  Deputy unlocks Common Mythic Dialog Options", GUILayout.ExpandHeight(false));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (Main.Settings.EnabledInstructions) GUILayout.Label("Common dialog responses will check mythic path of all inparty deputies.", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();
                Main.Settings.EnabledMemberMythicEtude = GUILayout.Toggle(Main.Settings.EnabledMemberMythicEtude, "  Deputy advances Mythic Quests and unlock important Option/Features", GUILayout.ExpandHeight(false));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (Main.Settings.EnabledInstructions) GUILayout.Label("If deputies' mythic quests are triggered, those can be advanced normally(Roughly Tested) without the need to switch mark.", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();


                if (Main.Settings.EnabledMemberMythicEtude)
                {

                    GUILayout.BeginVertical();
                    Main.Settings.EnabledMemberMythicEtudeDeep = GUILayout.Toggle(Main.Settings.EnabledMemberMythicEtudeDeep, "  Deputy Mythic Path counts as Main's ( Testing !)", GUILayout.ExpandHeight(false));
                    GUILayout.Space(5);
                    GUILayout.BeginHorizontal();
                    if (Main.Settings.EnabledInstructions) GUILayout.Label("Triggering of Mythic Campaign and Scenes need manually switching Character Mark in Debug Option.", GUILayout.ExpandWidth(false));
                    GUILayout.EndHorizontal();
                    if (Main.Settings.EnabledInstructions) GUILayout.Label("Or enable the Testing function above, their mythic path will works like the Main Character has it.", GUILayout.ExpandWidth(false));

                    if (Main.Settings.EnabledMemberMythicEtudeDeep)
                    {
                        Main.Settings.ChangeMythicWeather = GUILayout.Toggle(Main.Settings.ChangeMythicWeather, "  Customize Mythic Weather in Chapter 5", GUILayout.ExpandHeight(false));
                        Main.Settings.DisableMythicMemberIntoDialogue = GUILayout.Toggle(Main.Settings.DisableMythicMemberIntoDialogue, "  Disable Deputies Speaking in Mythic Dialogues", GUILayout.ExpandHeight(false));
                        GUILayout.BeginHorizontal();
                        if (Main.Settings.EnabledInstructions) GUILayout.Label("Dialogue lines unlocked by deputies can now be spoken by them, but main character can always speak those lines if you want.", GUILayout.ExpandWidth(false));
                        GUILayout.EndHorizontal();

                        if (!Main.Settings.DisableMythicMemberIntoDialogue)
                        {
                            Main.Settings.EnabledAutoSwitchMythicCharacter = GUILayout.Toggle(Main.Settings.EnabledAutoSwitchMythicCharacter, "  Auto Switch Main Character (testing)", GUILayout.ExpandWidth(false));
                            //GUILayout.Label("Only for debuging purpose.", GUILayout.ExpandWidth(false));
                            GUILayout.BeginHorizontal();
                            if (Main.Settings.EnabledInstructions) GUILayout.Label("Auto switch main character when take mythic actions in dialogues, this will make the performance to be aligned with correct characters, original main character will be restored when dialogue is done.", GUILayout.ExpandWidth(false));
                            GUILayout.EndHorizontal();
                            if (Main.Settings.EnabledAutoSwitchMythicCharacter)
                            {
                                Main.Settings.EnabledRomanceAutoSwitchMythicCharacter = GUILayout.Toggle(Main.Settings.EnabledRomanceAutoSwitchMythicCharacter, "  Auto Switch Main Actor in Romance Events (testing)", GUILayout.ExpandWidth(false));
                                GUILayout.BeginHorizontal();
                                if (Main.Settings.EnabledInstructions) GUILayout.Label("Auto switch Main Actor when Romance Events are triggered, need linked character in party.", GUILayout.ExpandWidth(false));
                                GUILayout.EndHorizontal();

                            }
                        }

                    }
                    GUILayout.Space(8);

                    GUILayout.Label("Deputy Characters should have [Mythic Quests] enabled below if you want them deeply influence the story.", GUILayout.ExpandWidth(false));
                    if (Main.Settings.EnabledInstructions) GUILayout.Label("Or they will stay as normal companions with Mythic Classes. City scene, mythic npcs and other features will be deeply influenced.", GUILayout.ExpandWidth(false));
                    GUILayout.Label("Deputy Characters can have [ Block Influence ] enabled, if you want to block them from influencing sotry Temporarily, while keeping their Mythic Quest progress.", GUILayout.ExpandWidth(false));
                    if (Main.Settings.EnabledInstructions) GUILayout.Label("Save and reload if checks are not renewed. Demon Path can only be activated once, if [Mythic Quests] was turned off it will be closed permanently.", GUILayout.ExpandWidth(false));
                    GUILayout.Space(15);
                    GUILayout.EndVertical();
                }
                GUILayout.BeginVertical();
                GUILayout.Label("--------------------------------------------------------------------------------------------------------", GUILayout.ExpandWidth(false));
                GUILayout.EndVertical();

                GUILayout.BeginHorizontal();
                GUILayout.Label("List:                Total:" + commanderCount.ToString(), GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();


                var player = Game.Instance.Player;
                var currentCharList = LogicStructures.GetAllCharacterList();

                if (currentCharList != null)
                {
                    foreach (var character in currentCharList)
                    {

                        GUILayout.BeginHorizontal();
                        if (player.AllCharacters.Contains(character) && LogicStructures.isPlayerActorBlueprintType(character) && !character.IsStoryCompanion())
                        {
                            GUILayoutOption[] guiOptions = new GUILayoutOption[] { GUILayout.Width(100) };
                            GUILayout.Label(character.CharacterName, guiOptions);
                        
                        GUILayout.Space(10);

                        if (Main.Settings.EnabledMemberMythicEtude && !character.IsPet && player.AllCharacters.Contains(character) && !character.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(character) && !character.IsStoryCompanion())
                        {
                            character.IsImportedCustomCompanion = GUILayout.Toggle(character.IsImportedCustomCompanion, "  [  Block Influence ]", GUILayout.ExpandWidth(false));
                            if (player.PartyAndPets.Contains(character))
                            {
                                GUILayout.Toggle(character.Descriptor.m_IsEssentialForGame, "  ", GUILayout.ExpandWidth(false));
                                //GUILayout.Space(10);
                                if (GUILayout.Button("[ Mythic Quests ]", GUI.skin.box, GUILayout.ExpandWidth(false)))
                                {

                                    if (!character.Descriptor.m_IsEssentialForGame)
                                    {
                                        character.Descriptor.AddEssentialMark();


                                    }
                                    else
                                    {
                                        character.Descriptor.RemoveEssentialMark();


                                    }
                                }
                            }

                            GUILayout.Space(40);
                        }


                        if ((Main.Settings.MyBoolOption || LogicStructures.IsInfirstScene()) && player.AllCharacters.Contains(character) && player.PartyAndPets.Contains(character) && !character.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(character) && !character.IsStoryCompanion())
                        {
                            GUILayout.Space(20);
                            if (GUILayout.Button("Set Main", GUI.skin.box, GUILayout.ExpandWidth(false)))
                            {

                                if (character != null)
                                {
                                    if (!character.Descriptor.m_IsEssentialForGame) character.Descriptor.AddEssentialMark();
                                    if (!character.IsImportedCustomCompanion) character.IsImportedCustomCompanion = false;
                                    //if (character.IsImportedCustomCompanion) character.IsImportedCustomCompanion= false;
                                    if (Game.Instance != null)
                                    {
                                        Game.Instance.Player.MainCharacter = character;
                                    }
                                }
                            }
                            GUILayout.Space(25);
                        }
                        if (!character.IsPet && player.AllCharacters.Contains(character) && player.PartyAndPets.Contains(character) && !character.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(character) && !character.IsStoryCompanion())
                        {

                            var currentMode = Game.Instance.CurrentMode;
                            if (Main.Settings.DebugOptions && (currentMode == GameModeType.Default || currentMode == GameModeType.Pause))
                            {
                                if (GUILayout.Button("Move to Commander", GUI.skin.box, GUILayout.ExpandWidth(false)))
                                {
                                    if (character != null)
                                    {

                                        if (Game.Instance != null)
                                        {
                                            character.Position = Game.Instance.Player.MainCharacter.Value.Position;
                                            Vector3 position;
                                            position = new(character.Position.x + sphere.x, character.Position.y, character.Position.z + sphere.z);
                                            character.Position = position;
                                            foreach (var pet in character.Pets)
                                            {
                                                Vector3 petsPosition;
                                                petsPosition = new(character.Position.x + sphere.x, character.Position.y, character.Position.z + sphere.z);
                                                pet.Entity.Position = petsPosition;

                                            }


                                        }
                                    }
                                }
                                GUILayout.Space(25);
                            }
                        }
                        if (!character.IsPet && player.AllCharacters.Contains(character) && !player.PartyAndPets.Contains(character) && !character.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(character) && !character.IsStoryCompanion())
                        {

                            if (GUILayout.Button("Set in Party", GUI.skin.box, GUILayout.ExpandWidth(false)))
                            {

                                if (character != null)
                                {

                                    if (Game.Instance != null)
                                    {
                                        if (character.IsInGame == false)
                                        {
                                            var currentMode = Game.Instance.CurrentMode;
                                            if (currentMode == GameModeType.Default || currentMode == GameModeType.Pause)
                                            {

                                                character.IsInGame = true;
                                                character.Position = Game.Instance.Player.MainCharacter.Value.Position;

                                                Vector3 position;
                                                position = new(character.Position.x + sphere.x, character.Position.y, character.Position.z + sphere.z);
                                                character.Position = position;
                                                foreach (var pet in character.Pets)
                                                {
                                                    Vector3 petsPosition;
                                                    petsPosition = new(character.Position.x + sphere.x, character.Position.y, character.Position.z + sphere.z);
                                                    pet.Entity.Position = petsPosition;

                                                }
                                            }
                                        }
                                        if (!Game.Instance.Player.m_ActiveCompanions.Contains(character)) Game.Instance.Player.m_ActiveCompanions.Add(character);
                                        if (!Game.Instance.Player.m_PartyAndPets.Contains(character)) Game.Instance.Player.m_PartyAndPets.Add(character);

                                        if (!Game.Instance.Player.m_Party.Contains(character)) Game.Instance.Player.m_Party.Add(character);
                                        if (Game.Instance.Player.m_RemoteCompanions.Contains(character)) Game.Instance.Player.m_RemoteCompanions.Remove(character);
                                        foreach (var pet in character.Pets)
                                        {
                                            if (!Game.Instance.Player.m_PartyAndPets.Contains(pet)) Game.Instance.Player.m_PartyAndPets.Add(pet);
                                        }

                                        if (Game.Instance.UI.SelectionManager != null)
                                        {
                                            Game.Instance.UI.SelectionManager.SelectUnit(character.View, single: true, sendEvent: true, ask: false);
                                        }
                                        break;


                                    }
                                }
                            }
                            GUILayout.Space(25);
                        }
                            WeatherController.WeatherSwitchGuiEnglish(character);
                            //if (!character.IsMainCharacter) WeatherController.WeatherSwitchGuiEnglish(character);
                        }
                        GUILayout.EndHorizontal();

                    }
                }


                GUILayout.BeginVertical();
                GUILayout.Space(25);
                GUILayout.Label("--------------------------------------------------------------------------------------------------------", GUILayout.ExpandWidth(false));
                Main.Settings.DebugOptions = GUILayout.Toggle(Main.Settings.DebugOptions, "  Debug Options", GUILayout.ExpandWidth(false));

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                if (Main.Settings.DebugOptions)
                {
                    Main.Settings.AfterIntroCommanderSpawning = GUILayout.Toggle(Main.Settings.AfterIntroCommanderSpawning, "  Enable Deputy Spawning after Intro Scene", GUILayout.ExpandWidth(false));
                    //GUILayout.Label("Only for debuging purpose.", GUILayout.ExpandWidth(false));
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                if (Main.Settings.DebugOptions)
                {
                    Main.Settings.MyBoolOption = GUILayout.Toggle(Main.Settings.MyBoolOption, "  Enable Main Commander Changing", GUILayout.ExpandWidth(false));
                    //GUILayout.Label("Only for debuging purpose.", GUILayout.ExpandWidth(false));
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                if (Main.Settings.DebugOptions)
                {
                    Main.Settings.EnabledMultipleSameMythic = GUILayout.Toggle(Main.Settings.EnabledMultipleSameMythic, "  Allow Multiple Same Mythic Path", GUILayout.ExpandWidth(false));
                    //GUILayout.Label("Only for debuging purpose.", GUILayout.ExpandWidth(false));
                }
                GUILayout.EndHorizontal();
                if (GUILayout.Button("Export Current Relation State", GUI.skin.box, GUILayout.ExpandWidth(false)))
                {

                    CharacterLinkedRomance.DumpRomanceJsonDebug();
                }
                if (Main.Settings.EnabledInstructions) GUILayout.Label("Export as ModStorage/debug.json。", GUILayout.ExpandWidth(false));




                GUILayout.EndVertical();
            }
            else
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Please Load a Save, this mod does nothing in mainmenu", GUILayout.ExpandWidth(false));
                GUILayout.Space(10);
                GUILayout.EndHorizontal();
            }
        }
        public static void ChineseLanguageGui()
        {
            if (IsInGame)
            {
                GUILayout.BeginVertical();
                GUILayout.Label("注意！Mod仍在开发中！", GUILayout.ExpandWidth(false));
                GUILayout.Space(10);
                GUILayout.Label("副角色可以在完成“建城日”任务前创建，主角色标签也可在指挥官蓝图的角色之间任意切换。", GUILayout.ExpandWidth(false));
                GUILayout.Label("序章任务之后相关机制将被隐藏，可从Debug选项中开启。", GUILayout.ExpandWidth(false));
                Main.Settings.EnabledInstructions = GUILayout.Toggle(Main.Settings.EnabledInstructions, "  说明", GUILayout.ExpandHeight(false));

                GUILayout.Label("--------------------------------------------------------------------------------------------------------", GUILayout.ExpandWidth(false));
                GUILayout.Space(25);
                GUILayout.EndVertical();

                var commanderCount = 0;
                newCommanderUnit = null;
                commanderCount = LogicStructures.GetCommanderNumber();
                if (Main.Settings.AfterIntroCommanderSpawning || LogicStructures.IsInfirstScene())
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("刷新限制", GUILayout.ExpandWidth(false));
                    GUILayout.Space(10);

                    Main.Settings.CommanderSpawnLimit = ((int)GUILayout.HorizontalSlider(Main.Settings.CommanderSpawnLimit, 0, 5, GUILayout.Width(300f)));
                    var numLimit = Main.Settings.CommanderSpawnLimit;
                    GUILayout.Label(numLimit.ToString(), GUILayout.ExpandWidth(false));
                    GUILayout.EndHorizontal();
                    if (commanderCount - 1 < numLimit)
                    {
                        buttonName = "添加";

                    }
                    else
                    {
                        buttonName = "到达人数上限";
                    }
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("加入更多角色:", GUILayout.ExpandWidth(false));
                    GUILayout.Space(10);
                    if (GUILayout.Button(buttonName, GUI.skin.box, GUILayout.ExpandWidth(false)))
                    {
                        commanderCount = LogicStructures.GetCommanderNumber();
                        if (commanderCount - 1 < numLimit)
                        {
                            newCommanderUnit = LogicStructures.SpawnCommanderUnit(Utilities.GetBlueprintByGuid<BlueprintUnit>(StaticData.playerActorBlueprintTypeGUID), 1);
                            if (newCommanderUnit != null)
                            {
                                newCommanderUnit = GameHelper.RecruitNPC(newCommanderUnit, newCommanderUnit.Blueprint);
                                Game.Instance.Player.AttachPartyMember(newCommanderUnit);

                            }
                            buttonName = "添加";

                        }
                        else
                        {
                            buttonName = "到达人数上限";
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginVertical();
                    GUILayout.Label("--------------------------------------------------------------------------------------------------------", GUILayout.ExpandWidth(false));
                    GUILayout.Space(25);
                    GUILayout.EndVertical();

                }



                GUILayout.BeginHorizontal();
                Main.Settings.CharacterLinkedRomance = GUILayout.Toggle(Main.Settings.CharacterLinkedRomance, "  浪漫关系绑定触发角色", GUILayout.ExpandHeight(false));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (Main.Settings.EnabledInstructions) GUILayout.Label("浪漫关系绑定触发其的角色，相关对话选项将对其他角色隐藏，绑定对象可从设置文件中使用角色的guid手动更改。", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();
                Main.Settings.EnabledMemberMythicCommonDialogue = GUILayout.Toggle(Main.Settings.EnabledMemberMythicCommonDialogue, "  副角色解锁一般道途对话", GUILayout.ExpandHeight(false));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (Main.Settings.EnabledInstructions) GUILayout.Label("红色锁定的道途对话可由在队伍内的副角色的道途解锁。", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();
                Main.Settings.EnabledMemberMythicEtude = GUILayout.Toggle(Main.Settings.EnabledMemberMythicEtude, "  副角色道途解锁神话任务和城市景观等重要机制", GUILayout.ExpandHeight(false));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (Main.Settings.EnabledInstructions) GUILayout.Label("现有机制下只有主角色可以触发神话任务，如果副角色神话任务已经解锁则相关任务可被推进，触发需要手动切换主角色标签。需要相关角色在队伍内。", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();


                if (Main.Settings.EnabledMemberMythicEtude)
                {

                    GUILayout.BeginVertical();
                    Main.Settings.EnabledMemberMythicEtudeDeep = GUILayout.Toggle(Main.Settings.EnabledMemberMythicEtudeDeep, "  副角色道途判定时算作主角色持有 ( 测试 !)", GUILayout.ExpandHeight(false));
                    GUILayout.Space(5);
                    GUILayout.BeginHorizontal();
                    if (Main.Settings.EnabledInstructions) GUILayout.Label("启用本选项将把副角色道途按主角色持有进行判定，自动触发道途任务、解锁包括圣教军解决方案在内的全部神话机制，相关副角色可处于解散状态。", GUILayout.ExpandWidth(false));
                    GUILayout.EndHorizontal();
                    if (Main.Settings.EnabledInstructions) GUILayout.Label("现版本后期道途无法在开启神话剧情的情况下保留其他角色道途，建议建立专门的7级神话英雄角色并关闭相关剧情来使用。", GUILayout.ExpandWidth(false));

                    if (Main.Settings.EnabledMemberMythicEtudeDeep)
                    {
                        Main.Settings.ChangeMythicWeather = GUILayout.Toggle(Main.Settings.ChangeMythicWeather, "  自定义眷泽城神话天相", GUILayout.ExpandHeight(false));

                        Main.Settings.DisableMythicMemberIntoDialogue = GUILayout.Toggle(Main.Settings.DisableMythicMemberIntoDialogue, "  停用副角色讲述神话对话", GUILayout.ExpandHeight(false));
                        GUILayout.BeginHorizontal();
                        if (Main.Settings.EnabledInstructions) GUILayout.Label("副角色解锁的神话对话可由其进行讲述，关闭后将由主角色讲述全部对话。", GUILayout.ExpandWidth(false));
                        GUILayout.EndHorizontal();

                        if (!Main.Settings.DisableMythicMemberIntoDialogue)
                        {
                            Main.Settings.EnabledAutoSwitchMythicCharacter = GUILayout.Toggle(Main.Settings.EnabledAutoSwitchMythicCharacter, "  自动切换主角色 ( 测试中 )", GUILayout.ExpandWidth(false));
                            //GUILayout.Label("Only for debuging purpose.", GUILayout.ExpandWidth(false));
                            GUILayout.BeginHorizontal();
                            if (Main.Settings.EnabledInstructions) GUILayout.Label("选择道途对话、触发道途事件和神话顾问事件时自动切换角色以使演出同步。", GUILayout.ExpandWidth(false));
                            GUILayout.EndHorizontal();
                            if (Main.Settings.EnabledAutoSwitchMythicCharacter)
                            {
                                Main.Settings.EnabledRomanceAutoSwitchMythicCharacter = GUILayout.Toggle(Main.Settings.EnabledRomanceAutoSwitchMythicCharacter, "  浪漫事件自动切换绑定角色 ( 测试中 )", GUILayout.ExpandWidth(false));
                                GUILayout.BeginHorizontal();
                                if (Main.Settings.EnabledInstructions) GUILayout.Label("浪漫事件触发时自动切换角色，需要相关角色在队内，只有主角色的情况下将直接使用原版逻辑。", GUILayout.ExpandWidth(false));
                                GUILayout.EndHorizontal();

                            }
                        }

                    }
                    GUILayout.Space(8);

                    GUILayout.Label("开启副角色的 [神话任务] 标签来使其深度参与游戏内容。", GUILayout.ExpandWidth(false));
                    if (Main.Settings.EnabledInstructions) GUILayout.Label("关闭时将只作为有神话职业的普通角色存在。恶魔道途在开启后不能手动关闭[神话任务]角色标签，一旦关闭即中断神话任务。", GUILayout.ExpandWidth(false));
                    GUILayout.Label("开启副角色的 [冻结剧情] 标签来使其暂时不影响对话和相关剧情，冻结时将保留其神话任务进度。", GUILayout.ExpandWidth(false));
                    if (Main.Settings.EnabledInstructions) GUILayout.Label("相关判定不生效时可尝试存档后读档。", GUILayout.ExpandWidth(false));
                    GUILayout.Space(15);
                    GUILayout.EndVertical();
                }
                GUILayout.BeginVertical();
                GUILayout.Label("--------------------------------------------------------------------------------------------------------", GUILayout.ExpandWidth(false));
                GUILayout.EndVertical();

                GUILayout.BeginHorizontal();
                GUILayout.Label("主蓝图角色:                总数:" + commanderCount.ToString(), GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();


                var player = Game.Instance.Player;
                var currentCharList = LogicStructures.GetAllCharacterList();

                if (currentCharList != null)
                {
                    foreach (var character in currentCharList)
                    {
                        
                        GUILayout.BeginHorizontal();
                        if (player.AllCharacters.Contains(character) && LogicStructures.isPlayerActorBlueprintType(character) && !character.IsStoryCompanion())
                        {
                            GUILayoutOption[] guiOptions = new GUILayoutOption[] { GUILayout.Width(100) };
                            GUILayout.Label(character.CharacterName, guiOptions);

                            GUILayout.Space(10);

                            if (Main.Settings.EnabledMemberMythicEtude && !character.IsPet && player.AllCharacters.Contains(character) && !character.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(character) && !character.IsStoryCompanion())
                            {
                                character.IsImportedCustomCompanion = GUILayout.Toggle(character.IsImportedCustomCompanion, "  [  冻结剧情 ]", GUILayout.ExpandWidth(false));
                                if (player.PartyAndPets.Contains(character))
                                {
                                    GUILayout.Toggle(character.Descriptor.m_IsEssentialForGame, "  ", GUILayout.ExpandWidth(false));
                                    //GUILayout.Space(10);
                                    if (GUILayout.Button("[ 神话任务 ]", GUI.skin.box, GUILayout.ExpandWidth(false)))
                                    {

                                        if (!character.Descriptor.m_IsEssentialForGame)
                                        {
                                            character.Descriptor.AddEssentialMark();


                                        }
                                        else
                                        {
                                            character.Descriptor.RemoveEssentialMark();


                                        }
                                    }
                                }

                                GUILayout.Space(40);
                            }


                            if ((Main.Settings.MyBoolOption || LogicStructures.IsInfirstScene()) && player.AllCharacters.Contains(character) && player.PartyAndPets.Contains(character) && !character.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(character) && !character.IsStoryCompanion())
                            {
                                GUILayout.Space(20);
                                if (GUILayout.Button("设为主角", GUI.skin.box, GUILayout.ExpandWidth(false)))
                                {

                                    if (character != null)
                                    {
                                        if (!character.Descriptor.m_IsEssentialForGame) character.Descriptor.AddEssentialMark();
                                        if (!character.IsImportedCustomCompanion) character.IsImportedCustomCompanion = false;
                                        //if (character.IsImportedCustomCompanion) character.IsImportedCustomCompanion= false;
                                        if (Game.Instance != null)
                                        {
                                            Game.Instance.Player.MainCharacter = character;
                                        }
                                    }
                                }
                                GUILayout.Space(25);
                            }
                            if (!character.IsPet && player.AllCharacters.Contains(character) && player.PartyAndPets.Contains(character) && !character.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(character) && !character.IsStoryCompanion())
                            {

                                var currentMode = Game.Instance.CurrentMode;
                                if (Main.Settings.DebugOptions && (currentMode == GameModeType.Default || currentMode == GameModeType.Pause))
                                {
                                    if (GUILayout.Button("传送角色", GUI.skin.box, GUILayout.ExpandWidth(false)))
                                    {
                                        if (character != null)
                                        {

                                            if (Game.Instance != null)
                                            {
                                                character.Position = Game.Instance.Player.MainCharacter.Value.Position;
                                                Vector3 position;
                                                position = new(character.Position.x + sphere.x, character.Position.y, character.Position.z + sphere.z);
                                                character.Position = position;
                                                foreach (var pet in character.Pets)
                                                {
                                                    Vector3 petsPosition;
                                                    petsPosition = new(character.Position.x + sphere.x, character.Position.y, character.Position.z + sphere.z);
                                                    pet.Entity.Position = petsPosition;

                                                }


                                            }
                                        }
                                    }
                                    GUILayout.Space(25);
                                }
                            }
                            if (!character.IsPet && player.AllCharacters.Contains(character) && !player.PartyAndPets.Contains(character) && !character.IsMainCharacter && LogicStructures.isPlayerActorBlueprintType(character) && !character.IsStoryCompanion())
                            {

                                if (GUILayout.Button("加入队伍", GUI.skin.box, GUILayout.ExpandWidth(false)))
                                {

                                    if (character != null)
                                    {

                                        if (Game.Instance != null)
                                        {
                                            if (character.IsInGame == false)
                                            {
                                                var currentMode = Game.Instance.CurrentMode;
                                                if (currentMode == GameModeType.Default || currentMode == GameModeType.Pause)
                                                {

                                                    character.IsInGame = true;
                                                    character.Position = Game.Instance.Player.MainCharacter.Value.Position;

                                                    Vector3 position;
                                                    position = new(character.Position.x + sphere.x, character.Position.y, character.Position.z + sphere.z);
                                                    character.Position = position;
                                                    foreach (var pet in character.Pets)
                                                    {
                                                        Vector3 petsPosition;
                                                        petsPosition = new(character.Position.x + sphere.x, character.Position.y, character.Position.z + sphere.z);
                                                        pet.Entity.Position = petsPosition;

                                                    }
                                                }
                                            }
                                            if (!Game.Instance.Player.m_ActiveCompanions.Contains(character)) Game.Instance.Player.m_ActiveCompanions.Add(character);
                                            if (!Game.Instance.Player.m_PartyAndPets.Contains(character)) Game.Instance.Player.m_PartyAndPets.Add(character);

                                            if (!Game.Instance.Player.m_Party.Contains(character)) Game.Instance.Player.m_Party.Add(character);
                                            if (Game.Instance.Player.m_RemoteCompanions.Contains(character)) Game.Instance.Player.m_RemoteCompanions.Remove(character);
                                            foreach (var pet in character.Pets)
                                            {
                                                if (!Game.Instance.Player.m_PartyAndPets.Contains(pet)) Game.Instance.Player.m_PartyAndPets.Add(pet);
                                            }

                                            if (Game.Instance.UI.SelectionManager != null)
                                            {
                                                Game.Instance.UI.SelectionManager.SelectUnit(character.View, single: true, sendEvent: true, ask: false);
                                            }
                                            break;


                                        }
                                    }
                                }
                                GUILayout.Space(25);
                            }
                            WeatherController.WeatherSwitchGuiChinese(character);
                            //if (!character.IsMainCharacter) WeatherController.WeatherSwitchGuiChinese(character);
                        }
                        GUILayout.EndHorizontal();

                    }
                }


                GUILayout.BeginVertical();
                GUILayout.Space(25);
                GUILayout.Label("--------------------------------------------------------------------------------------------------------", GUILayout.ExpandWidth(false));
                Main.Settings.DebugOptions = GUILayout.Toggle(Main.Settings.DebugOptions, "  Debug 选项", GUILayout.ExpandWidth(false));

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                if (Main.Settings.DebugOptions)
                {
                    Main.Settings.AfterIntroCommanderSpawning = GUILayout.Toggle(Main.Settings.AfterIntroCommanderSpawning, "  允许序章之后添加副角色", GUILayout.ExpandWidth(false));
                    //GUILayout.Label("Only for debuging purpose.", GUILayout.ExpandWidth(false));
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                if (Main.Settings.DebugOptions)
                {
                    Main.Settings.MyBoolOption = GUILayout.Toggle(Main.Settings.MyBoolOption, "  允许变更主要角色", GUILayout.ExpandWidth(false));
                    //GUILayout.Label("Only for debuging purpose.", GUILayout.ExpandWidth(false));
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                if (Main.Settings.DebugOptions)
                {
                    Main.Settings.EnabledMultipleSameMythic = GUILayout.Toggle(Main.Settings.EnabledMultipleSameMythic, "  允许多个副角色选择同一神话道途", GUILayout.ExpandWidth(false));
                    //GUILayout.Label("Only for debuging purpose.", GUILayout.ExpandWidth(false));
                }
                GUILayout.EndHorizontal();
                if (GUILayout.Button("导出当前关系存档", GUI.skin.box, GUILayout.ExpandWidth(false)))
                {

                    CharacterLinkedRomance.DumpRomanceJsonDebug();
                }
                if (Main.Settings.EnabledInstructions) GUILayout.Label("将当前关系绑定状态导出为ModStorage/debug.json。", GUILayout.ExpandWidth(false));



                GUILayout.EndVertical();
            }
            else
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("请载入存档。", GUILayout.ExpandWidth(false));
                GUILayout.Space(10);
                GUILayout.EndHorizontal();
            }

        }
    }
}
