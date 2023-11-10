namespace NeoModTest
{
    using HarmonyLib;
    using NeoModTest.ItemModsXML;
    using NeoModTest.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.SceneManagement;

    public class TrainerMenu : MonoBehaviour
    {
        private static List<string> ammoNames = new List<string>();
        private static List<string> apparelNames = new List<string>();
        private static List<string> armorNames = new List<string>();
        public static string baseDirectory = Environment.CurrentDirectory;
        public static BindingFlags bindflags = (BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        private static List<string> bookNames = new List<string>();
        private bool cmDm;
        private static int currentWeatherPreset = 0;
        public static BindingFlags defaultBindingFlags = (BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        public static BindingFlags fieldBindingFlags = (BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        private static bool flymodeTrigger = false;
        private static Dictionary<string, string> foodDic = new Dictionary<string, string>();
        private static List<string> foodNames = new List<string>();
        private bool forceWindowMode = false;
        private static bool godmodeTrigger = false;
        public static TrainerMenu instance = null;
        private static bool invisibleTrigger = false;
        private static Items itemDB = null;
        private static List<string> itemModNames = new List<string>();
        private static ItemModifiers itemMods = null;
        private static bool killZombiesTrigger = false;
        private static List<string> loadoutNames = new List<string>();
        private Rect MainWindow;
        private bool MainWindowVisible = true;
        private static Dictionary<string, string> medicalDic = new Dictionary<string, string>();
        private static List<string> medicalNames = new List<string>();
        private static List<string> meleeNames = new List<string>();
        private static List<string> miscNames = new List<string>();
        private Rect MiscWindow1;
        private Rect MiscWindow2;
        private bool MiscWindow2Visible = false;
        private Rect MiscWindow3;
        private bool MiscWindow3Visible = false;
        private bool MiscWindowVisible = false;
        private int origHeight = 0;
        private int origWidth = 0;
        private bool pause = false;
        public static BindingFlags propertyBindingFlags = (BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        private static List<string> questNames = new List<string>();
        private static List<string> resourceNames = new List<string>();
        public static int sceneIndex = 0;
        public static string sceneName = "";
        private static Dictionary<string, int> sceneTracker = new Dictionary<string, int>();
        private static Dictionary<string, string> schematicDic = new Dictionary<string, string>();
        private static List<string> schematicNames = new List<string>();
        private Vector2 scrollPosition;
        private static int selectedammo = 0;
        private static int selectedapparel = 0;
        private static int selectedarmor = 0;
        private static int selectedbook = 0;
        private static int selectedfood = 0;
        private static int selectedItemMod = 0;
        private static int selectedloadout = 0;
        private static int selectedmedical = 0;
        private static int selectedmelee = 0;
        private static int selectedmisc = 0;
        private static int selectedquest = 0;
        private static int selectedresource = 0;
        private static int selectedschematic = 0;
        private static int selectedvehicle = 0;
        private static int selectedweapon = 0;
        private static TelnetConsole telnet = null;
        private static bool toggleCursor = false;
        private static bool toggleFlyMode = false;
        private static bool toggleGodMode = false;
        private static bool toggleInvisible = false;
        private static bool toggleLogConsole = true;
        private static bool toggleSceneDebugger = false;
        private static bool toggleTelnet = false;
        private static bool toggleTrees = false;
        public static bool trainerReInitFlag = false;
        private static List<string> vehicleNames = new List<string>();
        private static Dictionary<string, string> weaponDic = new Dictionary<string, string>();
        private static List<string> weaponNames = new List<string>();
        private static string[] weatherPresets = new string[] { "Biome", "BloodMoon", "Foggy", "Rainy", "Stormy", "Snowy" };

        private void BuildLists()
        {
            schematicNames.Add("Select Schematic");
            foreach (Item item in from i in itemDB.Item
                where i.Name.EndsWith("Schematic")
                select i)
            {
                string key = item.Name.Replace("Schematic", "");
                if (key.StartsWith("mod"))
                {
                    key = key.Replace("mod", "");
                }
                else if (key.StartsWith("melee"))
                {
                    key = key.Replace("melee", "");
                }
                else if (key.StartsWith("gun"))
                {
                    key = key.Replace("gun", "");
                }
                else if (key.StartsWith("armor"))
                {
                    key = key.Replace("armor", "");
                }
                else if (key.StartsWith("resource"))
                {
                    key = key.Replace("resource", "");
                }
                else if (key.StartsWith("thrown"))
                {
                    key = key.Replace("thrown", "");
                }
                else if (key.StartsWith("medical"))
                {
                    key = key.Replace("medical", "");
                }
                else if (key.StartsWith("tool"))
                {
                    key = key.Replace("tool", "");
                }
                else if (key.StartsWith("vehicle"))
                {
                    key = key.Replace("vehicle", "");
                }
                else if (key.StartsWith("drink"))
                {
                    key = key.Replace("drink", "");
                }
                else if (key.StartsWith("food"))
                {
                    key = key.Replace("food", "");
                }
                else if (key.StartsWith("planted"))
                {
                    key = key.Replace("planted", "");
                }
                schematicDic.Add(key, item.Name);
                schematicNames.Add(key);
            }
            medicalNames.Add("Select Medical Item");
            foreach (Item item2 in from w in itemDB.Item
                where (w.Name.StartsWith("medical") || w.Name.StartsWith("drug")) && !w.Name.EndsWith("Schematic")
                select w)
            {
                string name = item2.Name;
                if (name.StartsWith("medical"))
                {
                    name = name.Replace("medical", "");
                }
                else if (name.StartsWith("drug"))
                {
                    name = name.Replace("drug", "");
                }
                medicalDic.Add(name, item2.Name);
                medicalNames.Add(name);
            }
            foodNames.Add("Select Food/Drink Item");
            foreach (Item item3 in from w in itemDB.Item
                where (w.Name.StartsWith("food") || w.Name.StartsWith("drink")) && !w.Name.EndsWith("Schematic")
                select w)
            {
                string str3 = item3.Name;
                if (str3.StartsWith("food"))
                {
                    str3 = str3.Replace("food", "");
                }
                else if (str3.StartsWith("drink"))
                {
                    str3 = str3.Replace("drink", "");
                }
                foodDic.Add(str3, item3.Name);
                foodNames.Add(str3);
            }
            resourceNames.Add("Select Resource");
            foreach (Item item4 in from w in itemDB.Item
                where w.Name.StartsWith("resource") && !w.Name.EndsWith("Schematic")
                select w)
            {
                resourceNames.Add(item4.Name.Replace("resource", ""));
            }
            miscNames.Add("Select Misc. Item");
            foreach (Item item5 in from w in itemDB.Item
                where ((((!w.Name.StartsWith("ammo") && !w.Name.StartsWith("apparel")) && (!w.Name.StartsWith("armor") && !w.Name.StartsWith("book"))) && ((!w.Name.StartsWith("gun") && !w.Name.StartsWith("thrown")) && (!w.Name.StartsWith("Loadout") && !w.Name.StartsWith("melee")))) && (((!w.Name.StartsWith("quest") && !w.Name.StartsWith("resource")) && (!w.Name.StartsWith("vehicle") && !w.Name.StartsWith("medical"))) && ((!w.Name.StartsWith("drug") && !w.Name.StartsWith("drink")) && !w.Name.StartsWith("food")))) && !w.Name.EndsWith("Schematic")
                select w)
            {
                miscNames.Add(item5.Name);
            }
            bookNames.Add("Select Book");
            foreach (Item item6 in from b in itemDB.Item
                where b.Name.StartsWith("book")
                select b)
            {
                bookNames.Add(item6.Name.Replace("book", ""));
            }
            ammoNames.Add("Select Ammo");
            foreach (Item item7 in from a in itemDB.Item
                where a.Name.StartsWith("ammo")
                select a)
            {
                ammoNames.Add(item7.Name.Replace("ammo", ""));
            }
            apparelNames.Add("Select Apparel");
            foreach (Item item8 in from a in itemDB.Item
                where a.Name.StartsWith("apparel")
                select a)
            {
                apparelNames.Add(item8.Name.Replace("apparel", ""));
            }
            armorNames.Add("Select Armor");
            foreach (Item item9 in from a in itemDB.Item
                where a.Name.StartsWith("armor") && !a.Name.EndsWith("Schematic")
                select a)
            {
                armorNames.Add(item9.Name.Replace("armor", ""));
            }
            weaponNames.Add("Select Weapon");
            foreach (Item item10 in from w in itemDB.Item
                where (w.Name.StartsWith("gun") || w.Name.StartsWith("thrown")) && !w.Name.EndsWith("Schematic")
                select w)
            {
                string str4 = item10.Name;
                if (str4.StartsWith("gun"))
                {
                    str4 = str4.Replace("gun", "");
                }
                else if (str4.StartsWith("thrown"))
                {
                    str4 = str4.Replace("thrown", "");
                }
                weaponDic.Add(str4, item10.Name);
                weaponNames.Add(str4);
            }
            loadoutNames.Add("Select Loadout");
            foreach (Item item11 in from w in itemDB.Item
                where w.Name.StartsWith("Loadout") && !w.Name.EndsWith("Schematic")
                select w)
            {
                loadoutNames.Add(item11.Name.Replace("Loadout", ""));
            }
            meleeNames.Add("Select Melee Item");
            foreach (Item item12 in from w in itemDB.Item
                where w.Name.StartsWith("melee") && !w.Name.EndsWith("Schematic")
                select w)
            {
                meleeNames.Add(item12.Name.Replace("melee", ""));
            }
            questNames.Add("Select Quest Item");
            foreach (Item item13 in from w in itemDB.Item
                where w.Name.StartsWith("quest") && !w.Name.EndsWith("Schematic")
                select w)
            {
                questNames.Add(item13.Name.Replace("quest", ""));
            }
            vehicleNames.Add("Select Vehicle Item");
            foreach (Item item14 in from w in itemDB.Item
                where w.Name.StartsWith("vehicle") && !w.Name.EndsWith("Schematic")
                select w)
            {
                vehicleNames.Add(item14.Name.Replace("vehicle", ""));
            }
            itemModNames.Add("Select Item Modifier");
            foreach (ItemModifier modifier in itemMods.ItemModifier)
            {
                string str5 = modifier.Name.Replace("mod", "").Replace("quest", "");
                if (!itemModNames.Contains(str5))
                {
                    itemModNames.Add(str5);
                }
            }
        }

        [HarmonyPrefix]
        public static bool CanCraft(ref bool __result, IList<ItemStack> _itemStack, EntityAlive _ea)
        {
            __result = true;
            return false;
        }

        private void DoCommand(string command, string args = "")
        {
            string str = command + " " + args;
            if (!SingletonMonoBehaviour<ConnectionManager>.Instance.IsClient)
            {
                SingletonMonoBehaviour<SdtdConsole>.Instance.ExecuteSync(str, null);
            }
            else
            {
                SingletonMonoBehaviour<ConnectionManager>.Instance.SendToServer(NetPackageManager.GetPackage<NetPackageConsoleCmdServer>().Setup(str), false);
            }
        }

        public static GameObject FindByName(string name)
        {
            foreach (GameObject obj2 in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
            {
                if ((obj2 != null) && (obj2.name == name))
                {
                    return obj2;
                }
            }
            return null;
        }

        public static GameObject FindByName(GameObject root, string name)
        {
            foreach (Transform transform in root.GetComponentsInChildren<Transform>(true))
            {
                if ((transform != null) && (transform.name == name))
                {
                    return transform.gameObject;
                }
            }
            return null;
        }

        public static T FindInActiveObjectByComp<T>() where T: MonoBehaviour
        {
            Transform[] transformArray = Resources.FindObjectsOfTypeAll<Transform>();
            for (int i = 0; i < transformArray.Length; i++)
            {
                if ((transformArray[i].hideFlags == HideFlags.None) && transformArray[i].gameObject.HasComponent<T>())
                {
                    return transformArray[i].GetComponent<T>();
                }
            }
            return default(T);
        }

        public static GameObject FindInActiveObjectByName(string name)
        {
            Transform[] transformArray = Resources.FindObjectsOfTypeAll(typeof(Transform)) as Transform[];
            for (int i = 0; i < transformArray.Length; i++)
            {
                if ((transformArray[i].hideFlags == HideFlags.None) && (transformArray[i].name == name))
                {
                    return transformArray[i].gameObject;
                }
            }
            return null;
        }

        public static List<T> FindInActiveObjectsByComp<T>() where T: MonoBehaviour
        {
            List<T> list = new List<T>();
            Transform[] transformArray = Resources.FindObjectsOfTypeAll<Transform>();
            for (int i = 0; i < transformArray.Length; i++)
            {
                if ((transformArray[i].hideFlags == HideFlags.None) && transformArray[i].gameObject.HasComponent<T>())
                {
                    list.Add(transformArray[i].GetComponent<T>());
                }
            }
            return list;
        }

        public static List<T> FindInActiveObjectsByCompSO<T>() where T: ScriptableObject
        {
            List<T> list = new List<T>();
            ScriptableObject[] objArray = Resources.FindObjectsOfTypeAll<ScriptableObject>();
            for (int i = 0; i < objArray.Length; i++)
            {
                if ((objArray[i].hideFlags == HideFlags.None) && (objArray[i].GetType() == typeof(T)))
                {
                    T item = objArray[i] as T;
                    list.Add(item);
                }
            }
            return list;
        }

        public static List<GameObject> FindInActiveObjectsByNamePartial(string name)
        {
            List<GameObject> list = new List<GameObject>();
            Transform[] transformArray = Resources.FindObjectsOfTypeAll<Transform>();
            for (int i = 0; i < transformArray.Length; i++)
            {
                if ((transformArray[i].hideFlags == HideFlags.None) && transformArray[i].name.ToUpper().Contains(name.ToUpper()))
                {
                    list.Add(transformArray[i].gameObject);
                }
            }
            return list;
        }

        public static void ForEachInHierarchy(Transform t, Action<Transform> action)
        {
            action(t);
            foreach (object obj2 in t)
            {
                Transform transform = (Transform) obj2;
                ForEachInHierarchy(transform, action);
            }
        }

        public static List<T> GetAllComponentsFromScene<T>(Scene scene) where T: Component
        {
            List<T> list = new List<T>();
            GameObject[] rootGameObjects = scene.GetRootGameObjects();
            for (int i = 0; i < rootGameObjects.Length; i++)
            {
                T[] componentsInChildren = rootGameObjects[i].GetComponentsInChildren<T>(true);
                list.AddRange(componentsInChildren);
            }
            return list;
        }

        public static GameObject[] GetDontDestroyOnLoadObjects()
        {
            GameObject target = null;
            GameObject[] rootGameObjects;
            try
            {
                target = new GameObject();
                UnityEngine.Object.DontDestroyOnLoad(target);
                Scene scene = target.scene;
                UnityEngine.Object.DestroyImmediate(target);
                target = null;
                rootGameObjects = scene.GetRootGameObjects();
            }
            finally
            {
                if (target != null)
                {
                    UnityEngine.Object.DestroyImmediate(target);
                }
            }
            return rootGameObjects;
        }

        public static string GetHierarchyDebugString(GameObject gameObject, int maxDepth)
        {
            if (gameObject == null)
            {
                return "null";
            }
            StringBuilder builder = new StringBuilder();
            Transform parent = gameObject.transform;
            builder.Append(parent.name);
            parent = parent.parent;
            for (int i = 0; (i < maxDepth) && (parent != null); i++)
            {
                builder.Append("<- ");
                builder.Append(parent.name);
                parent = parent.parent;
            }
            if (parent != null)
            {
                builder.Append("<- ...");
            }
            return builder.ToString();
        }

        public static System.Type GetTypeByName(string name)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies().Reverse<Assembly>())
            {
                System.Type type = assembly.GetType(name);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }

        private void GiveItem(string itemname, int amount = 1)
        {
            string[] textArray1 = new string[] { "giveself ", itemname, " 6 ", amount.ToString(), " true" };
            string str = string.Concat(textArray1);
            if (!SingletonMonoBehaviour<ConnectionManager>.Instance.IsClient)
            {
                SingletonMonoBehaviour<SdtdConsole>.Instance.ExecuteSync(str, null);
            }
            else
            {
                SingletonMonoBehaviour<ConnectionManager>.Instance.SendToServer(NetPackageManager.GetPackage<NetPackageConsoleCmdServer>().Setup(str), false);
            }
        }

        public static void InsertHarmonyPatches()
        {
            try
            {
                Debug.Log("Inserting Hooks...");
                Harmony harmony = new Harmony("wh0am15533.trainer");
                MethodInfo original = AccessTools.Method(typeof(Recipe), "CanCraft", null, null);
                MethodInfo method = AccessTools.Method(typeof(TrainerMenu), "CanCraft", null, null);
                harmony.Patch(original, new HarmonyMethod(method), null, null, null);
                MethodInfo info3 = AccessTools.Method(typeof(GameManager), "isAnyCursorWindowOpen", null, null);
                MethodInfo info4 = AccessTools.Method(typeof(TrainerMenu), "isAnyCursorWindowOpen", null, null);
                harmony.Patch(info3, new HarmonyMethod(info4), null, null, null);
                Debug.Log("Runtime Hooks's Applied");
            }
            catch (Exception exception)
            {
                Debug.LogError("FAILED to Apply Hooks's! Error: " + exception.Message);
            }
        }

        [HarmonyPrefix]
        public static bool isAnyCursorWindowOpen(ref bool __result)
        {
            if (toggleCursor)
            {
                __result = true;
                return false;
            }
            return true;
        }

        public static bool IsNullableEnum(System.Type t)
        {
            System.Type underlyingType = Nullable.GetUnderlyingType(t);
            return ((underlyingType != null) && underlyingType.IsEnum);
        }

        private string MakeEnable(string keycode, string label, bool toggle)
        {
            string str = "<color=yellow>" + keycode + "</color>";
            string str2 = toggle ? "<color=green>开启</color>" : "<color=red>关闭</color>";
            string[] textArray1 = new string[] { str, " ", label, " ", str2 };
            return string.Concat(textArray1);
        }

        private void OnGUI()
        {
            if (this.MainWindowVisible && (UnityEngine.Event.current.type == UnityEngine.EventType.Layout))
            {
                GUI.backgroundColor = new Color(62f, 62f, 66f);
                GUIStyle style = new GUIStyle(InterfaceMaker.CustomSkin.window) {
                    normal = { textColor = Color.green }
                };
                GUI.backgroundColor = Color.black;
                new GUIStyle(GUI.skin.window).normal.textColor = Color.green;
                this.MainWindow = new Rect(this.MainWindow.x, this.MainWindow.y, 260f, 50f);
                this.MainWindow = GUILayout.Window(0x309, this.MainWindow, new GUI.WindowFunction(this.RenderUI), "七日杀修改器 v1.25 Alpha版", style, new GUILayoutOption[0]);
                if (this.MiscWindowVisible)
                {
                    this.MiscWindow1 = new Rect(this.MiscWindow1.x, this.MiscWindow1.y, 250f, 50f);
                    this.MiscWindow1 = GUILayout.Window(0x30a, this.MiscWindow1, new GUI.WindowFunction(this.RenderUI), "杂项", style, new GUILayoutOption[0]);
                }
                if (this.MiscWindow2Visible)
                {
                    this.MiscWindow2 = GUILayout.Window(0x30b, this.MiscWindow2, new GUI.WindowFunction(this.RenderUI), "物品和资源", style, new GUILayoutOption[0]);
                }
                if (this.MiscWindow3Visible)
                {
                    this.MiscWindow3 = GUILayout.Window(780, this.MiscWindow3, new GUI.WindowFunction(this.RenderUI), "其它", style, new GUILayoutOption[0]);
                }
            }
        }

        private void RenderUI(int id)
        {
            GUIStyle style1 = new GUIStyle {
                normal = { background = Texture2D.whiteTexture },
                alignment = TextAnchor.MiddleCenter
            };
            GUIStyle style5 = new GUIStyle {
                normal = { textColor = Color.green },
                alignment = TextAnchor.MiddleCenter
            };
            GUIStyle style = new GUIStyle {
                normal = { textColor = Color.cyan },
                alignment = TextAnchor.MiddleCenter
            };
            GUIStyle style2 = new GUIStyle {
                normal = { textColor = Color.cyan },
                alignment = TextAnchor.MiddleLeft
            };
            GUIStyle style3 = new GUIStyle {
                normal = { textColor = Color.white },
                alignment = TextAnchor.MiddleCenter
            };
            GUIStyle style4 = new GUIStyle {
                normal = { textColor = Color.yellow },
                alignment = TextAnchor.MiddleCenter
            };
            GUIStyle style6 = new GUIStyle {
                normal = { textColor = Color.red },
                alignment = TextAnchor.MiddleCenter
            };
            sceneName = SceneManager.GetActiveScene().name;
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            bool flag = true;
            switch (id)
            {
                case 0x309:
                    GUILayout.Label("显示/隐藏: Back", style3, new GUILayoutOption[0]);
                    GUILayout.Space(10f);
                    if (!flag)
                    {
                        GUILayout.Label("请等待游戏加载完毕...", style4, Array.Empty<GUILayoutOption>());
                        GUILayout.Space(10f);
                        GUILayout.Label("BY:2533027824", style, Array.Empty<GUILayoutOption>());
                    }
                    else
                    {
                        GUILayout.Label(this.MakeEnable("[PgUp]", "    光标               ", toggleCursor), style2, Array.Empty<GUILayoutOption>());
                        GUILayout.Label(this.MakeEnable("[PgDwn]", " 无限弹药         ", Cheat.infiniteAmmo), style2, Array.Empty<GUILayoutOption>());
                        GUILayout.Label(this.MakeEnable("[F2]", "        加速               ", Cheat.speed), style2, Array.Empty<GUILayoutOption>());
                        GUILayout.Space(5f);
                        GUI.color = Color.white;
                        if (GUILayout.Button("开创意物品栏方法 (2)", Array.Empty<GUILayoutOption>()))
                        {
                            GamePrefs.Set(EnumGamePrefs.CreativeMenuEnabled, !GamePrefs.GetBool(EnumGamePrefs.CreativeMenuEnabled));
                        }
                        if (GUILayout.Button("Bypass Anti上帝背包", Array.Empty<GUILayoutOption>()))
                        {
                            AdminTools adminTools = GameManager.Instance.adminTools;
                            GamePrefs.GetBool(EnumGamePrefs.CreativeMenuEnabled);
                            GameManager.Instance.IsEditMode();
                        }
                        if (GUILayout.Button("升级 (最大: 300)", new GUILayoutOption[0]) && (Objects.localPlayer != null))
                        {
                            Objects.localPlayer.Progression.AddLevelExp(Objects.localPlayer.Progression.ExpToNextLevel, "_xpOther", Progression.XPTypes.Other, true);
                        }
                        GUI.color = Color.white;
                        if (GUILayout.Button("添加50技能点", new GUILayoutOption[0]) && (Objects.localPlayer != null))
                        {
                            Objects.localPlayer.Progression.SkillPoints += 50;
                        }
                        GUI.color = Color.white;
                        if (GUILayout.Button("完成当前任务", new GUILayoutOption[0]))
                        {
                            try
                            {
                                if (Objects.questJournal != null)
                                {
                                    Quest trackedQuest = Objects.questJournal.TrackedQuest;
                                    if (trackedQuest != null)
                                    {
                                        for (int i = 0; i < trackedQuest.Objectives.Count; i++)
                                        {
                                            object[] parameters = new object[] { true };
                                            trackedQuest.Objectives.ElementAt<BaseObjective>(i).GetType().GetMethod("ChangeStatus", defaultBindingFlags).Invoke(trackedQuest.Objectives.ElementAt<BaseObjective>(i), parameters);
                                        }
                                    }
                                    else
                                    {
                                        Debug.LogWarning("[Trainer] TrackedQuest is NULL!");
                                    }
                                    Quest activeQuest = Objects.questJournal.ActiveQuest;
                                    if (activeQuest != null)
                                    {
                                        for (int j = 0; j < activeQuest.Objectives.Count; j++)
                                        {
                                            object[] objArray2 = new object[] { true };
                                            activeQuest.Objectives.ElementAt<BaseObjective>(j).GetType().GetMethod("ChangeStatus", defaultBindingFlags).Invoke(activeQuest.Objectives.ElementAt<BaseObjective>(j), objArray2);
                                        }
                                    }
                                    else
                                    {
                                        Debug.LogWarning("[Trainer] TrackedQuest is NULL!");
                                    }
                                }
                                else
                                {
                                    Debug.LogWarning("[Trainer] QuestJournal is NULL!");
                                }
                            }
                            catch (Exception exception)
                            {
                                Debug.LogError("ERROR: " + exception.Message);
                            }
                        }
                        GUI.color = Color.white;
                        if (GUILayout.Button("添加20K金钱", new GUILayoutOption[0]))
                        {
                            try
                            {
                                this.GiveItem("casinoCoin", 0x4e20);
                                Debug.Log("Added 20K Money...");
                            }
                            catch (Exception exception2)
                            {
                                Debug.LogError("ERROR: " + exception2.Message);
                            }
                        }
                        GUI.color = Color.white;
                        if (GUILayout.Button("杀死僵尸 (范围50内的所有目标) [F11]", new GUILayoutOption[0]) || killZombiesTrigger)
                        {
                            try
                            {
                                foreach (EntityZombie zombie in Objects.zombieList)
                                {
                                    if (Vector3.Distance(Objects.localPlayer.position, zombie.position) <= 50f)
                                    {
                                        DamageResponse response = new DamageResponse {
                                            Fatal = true
                                        };
                                        zombie.Kill(response);
                                    }
                                }
                                killZombiesTrigger = false;
                                Debug.Log("Killed all Zombies...");
                            }
                            catch (Exception exception3)
                            {
                                Debug.LogError("ERROR: " + exception3.Message);
                            }
                        }
                        GUI.color = Color.white;
                        if (GUILayout.Button("时间跳跃 (+6小时)", new GUILayoutOption[0]))
                        {
                            try
                            {
                                ulong worldTime = GameManager.Instance.World.GetWorldTime();
                                GameManager.Instance.World.SetTimeJump(worldTime + ((ulong) 0x1770L), false);
                                Debug.Log("Added 6hr to World Time...");
                            }
                            catch (Exception exception4)
                            {
                                Debug.LogError("ERROR: " + exception4.Message);
                            }
                        }
                        GUI.color = Color.white;
                        if (GUILayout.Button("获取 血量/耐力[End]", new GUILayoutOption[0]))
                        {
                            try
                            {
                                Objects.localPlayer.AddHealth(Objects.localPlayer.GetMaxHealth());
                                Objects.localPlayer.AddStamina((float) Objects.localPlayer.GetMaxStamina());
                                Debug.Log("Added Max Health & Stamina...");
                            }
                            catch (Exception exception5)
                            {
                                Debug.LogError("ERROR: " + exception5.Message);
                            }
                        }
                        GUI.color = Color.white;
                        if (this.MiscWindowVisible)
                        {
                            GUI.color = Color.green;
                        }
                        else
                        {
                            GUI.color = Color.white;
                        }
                        if (GUILayout.Button("杂项菜单", new GUILayoutOption[0]))
                        {
                            this.MiscWindowVisible = !this.MiscWindowVisible;
                        }
                        GUI.color = Color.white;
                        if (this.MiscWindow2Visible)
                        {
                            GUI.color = Color.green;
                        }
                        else
                        {
                            GUI.color = Color.white;
                        }
                        if (GUILayout.Button("物品和资源", new GUILayoutOption[0]))
                        {
                            this.MiscWindow2Visible = !this.MiscWindow2Visible;
                        }
                        GUI.color = Color.white;
                        if (this.MiscWindow3Visible)
                        {
                            GUI.color = Color.green;
                        }
                        else
                        {
                            GUI.color = Color.white;
                        }
                        if (GUILayout.Button("其它菜单", new GUILayoutOption[0]))
                        {
                            this.MiscWindow3Visible = !this.MiscWindow3Visible;
                        }
                        GUILayout.Space(5f);
                        GUI.color = Color.white;
                        GUILayout.Label("96辅助游戏论坛 www.steamcom.cn", style, Array.Empty<GUILayoutOption>());
                        GUILayout.Label("By.卢大侠 修复.呵呵 2533027824", style, Array.Empty<GUILayoutOption>());
                    }
                    goto Label_2280;

                case 0x30a:
                {
                    Vector3? nullable;
                    GUI.color = Color.white;
                    if (GUILayout.Button("调试菜单帮助", new GUILayoutOption[0]))
                    {
                        try
                        {
                            Application.OpenURL("https://7daystodie.fandom.com/wiki/Debug_Mode");
                        }
                        catch (Exception exception6)
                        {
                            Debug.LogError("ERROR: " + exception6.Message);
                        }
                    }
                    if (toggleLogConsole)
                    {
                        GUI.color = Color.green;
                    }
                    else
                    {
                        GUI.color = Color.white;
                    }
                    if (GUILayout.Button("日志控制台", new GUILayoutOption[0]))
                    {
                        try
                        {
                            toggleLogConsole = !toggleLogConsole;
                            if (CustomConsole.instance != null)
                            {
                                CustomConsole.Toggle();
                                Debug.Log("Toggled Logging Console Active: " + toggleLogConsole.ToString());
                            }
                        }
                        catch (Exception exception7)
                        {
                            Debug.LogError("ERROR: " + exception7.Message);
                        }
                    }
                    if (toggleTelnet)
                    {
                        GUI.color = Color.green;
                    }
                    else
                    {
                        GUI.color = Color.white;
                    }
                    if (GUILayout.Button("开启Telnet (P: 8099)", new GUILayoutOption[0]))
                    {
                        try
                        {
                            toggleTelnet = !toggleTelnet;
                            if (toggleTelnet)
                            {
                                GamePrefs.GetInt(EnumGamePrefs.TelnetPort);
                                GamePrefs.Set(EnumGamePrefs.TelnetPort, 0x1fa3);
                                telnet = new TelnetConsole();
                            }
                            else
                            {
                                telnet.Disconnect();
                                telnet = null;
                            }
                            Debug.Log("Started Telnet Server");
                        }
                        catch (Exception exception8)
                        {
                            Debug.LogError("ERROR: " + exception8.Message);
                        }
                    }
                    GUI.color = Color.white;
                    if (GUILayout.Button("显示控制台", new GUILayoutOption[0]))
                    {
                        Objects.gameManager.SetConsoleWindowVisible(true);
                        Debug.LogWarning("[Trainer] Opening Console...");
                    }
                    if (this.cmDm)
                    {
                        GUI.color = Color.green;
                    }
                    else
                    {
                        GUI.color = Color.white;
                    }
                    if (GUILayout.Button("创造模式和调试模式", new GUILayoutOption[0]))
                    {
                        this.cmDm = !this.cmDm;
                        this.ToggleCmDm();
                    }
                    if (toggleGodMode)
                    {
                        GUI.color = Color.green;
                    }
                    else
                    {
                        GUI.color = Color.white;
                    }
                    if (GUILayout.Button("无敌模式 [F10]", new GUILayoutOption[0]) || godmodeTrigger)
                    {
                        try
                        {
                            toggleGodMode = !Objects.localPlayer.IsGodMode.Value;
                            Objects.localPlayer.IsGodMode.Value = !Objects.localPlayer.IsGodMode.Value;
                            Objects.localPlayer.IsNoCollisionMode.Value = Objects.localPlayer.IsGodMode.Value;
                            Objects.localPlayer.IsFlyMode.Value = Objects.localPlayer.IsGodMode.Value;
                            if (Objects.localPlayer.IsGodMode.Value)
                            {
                                Objects.localPlayer.Buffs.AddBuff("god", -1, true, false, false, -1f);
                            }
                            else
                            {
                                Objects.localPlayer.Buffs.RemoveBuff("god", true);
                            }
                            godmodeTrigger = false;
                            Debug.Log("Toggled God Mode: " + toggleGodMode.ToString());
                        }
                        catch (Exception exception9)
                        {
                            Debug.LogError("ERROR: " + exception9.Message);
                        }
                    }
                    if (toggleFlyMode)
                    {
                        GUI.color = Color.green;
                    }
                    else
                    {
                        GUI.color = Color.white;
                    }
                    if (GUILayout.Button("飞行模式 [F9]", new GUILayoutOption[0]) || flymodeTrigger)
                    {
                        try
                        {
                            toggleFlyMode = !Objects.localPlayer.IsFlyMode.Value;
                            Objects.localPlayer.IsFlyMode.Value = !Objects.localPlayer.IsFlyMode.Value;
                            GamePrefs.Set(EnumGamePrefs.CreativeMenuEnabled, !GamePrefs.GetBool(EnumGamePrefs.CreativeMenuEnabled));
                            flymodeTrigger = false;
                            Debug.Log("Toggled Fly Mode: " + toggleFlyMode.ToString());
                        }
                        catch (Exception exception10)
                        {
                            Debug.LogError("ERROR: " + exception10.Message);
                        }
                    }
                    if (toggleInvisible)
                    {
                        GUI.color = Color.green;
                    }
                    else
                    {
                        GUI.color = Color.white;
                    }
                    if (GUILayout.Button("隐身模式 [F8]", new GUILayoutOption[0]) || invisibleTrigger)
                    {
                        try
                        {
                            toggleInvisible = !Objects.localPlayer.IsSpectator;
                            Objects.localPlayer.IsSpectator = !Objects.localPlayer.IsSpectator;
                            invisibleTrigger = false;
                            Debug.Log("Toggled Invisible: " + toggleInvisible.ToString());
                        }
                        catch (Exception exception11)
                        {
                            Debug.LogError("ERROR: " + exception11.Message);
                        }
                    }
                    GUI.color = Color.white;
                    GUILayout.BeginVertical("选项", GUI.skin.box, new GUILayoutOption[0]);
                    GUILayout.Space(20f);
                    GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.Width(250f) };
                    GUILayout.BeginHorizontal(options);
                    GUILayoutOption[] optionArray2 = new GUILayoutOption[] { GUILayout.Width(125f) };
                    Cheat.magicBullet = GUILayout.Toggle(Cheat.magicBullet, "魔法子弹", optionArray2);
                    GUILayout.EndHorizontal();
                    GUILayoutOption[] optionArray3 = new GUILayoutOption[] { GUILayout.Width(250f) };
                    GUILayout.BeginHorizontal(optionArray3);
                    GUILayoutOption[] optionArray4 = new GUILayoutOption[] { GUILayout.Width(125f) };
                    Cheat.noWeaponBob = GUILayout.Toggle(Cheat.noWeaponBob, "武器加强", optionArray4);
                    GUILayoutOption[] optionArray5 = new GUILayoutOption[] { GUILayout.Width(125f) };
                    Cheat.aimbot = GUILayout.Toggle(Cheat.aimbot, "自瞄", optionArray5);
                    GUILayout.EndHorizontal();
                    GUILayoutOption[] optionArray6 = new GUILayoutOption[] { GUILayout.Width(250f) };
                    GUILayout.BeginHorizontal(optionArray6);
                    GUILayoutOption[] optionArray7 = new GUILayoutOption[] { GUILayout.Width(125f) };
                    ESP.crosshair = GUILayout.Toggle(ESP.crosshair, "十字准星", optionArray7);
                    GUILayoutOption[] optionArray8 = new GUILayoutOption[] { GUILayout.Width(125f) };
                    ESP.fovCircle = GUILayout.Toggle(ESP.fovCircle, "显示自瞄范围", optionArray8);
                    GUILayout.EndHorizontal();
                    GUILayoutOption[] optionArray9 = new GUILayoutOption[] { GUILayout.Width(250f) };
                    GUILayout.BeginHorizontal(optionArray9);
                    GUILayoutOption[] optionArray10 = new GUILayoutOption[] { GUILayout.Width(125f) };
                    ESP.playerBox = GUILayout.Toggle(ESP.playerBox, "玩家方框", optionArray10);
                    GUILayoutOption[] optionArray11 = new GUILayoutOption[] { GUILayout.Width(125f) };
                    ESP.playerName = GUILayout.Toggle(ESP.playerName, "玩家名称", optionArray11);
                    GUILayout.EndHorizontal();
                    GUILayoutOption[] optionArray12 = new GUILayoutOption[] { GUILayout.Width(250f) };
                    GUILayout.BeginHorizontal(optionArray12);
                    GUILayoutOption[] optionArray13 = new GUILayoutOption[] { GUILayout.Width(125f) };
                    ESP.playerHealth = GUILayout.Toggle(ESP.playerHealth, "玩家血量", optionArray13);
                    GUILayoutOption[] optionArray14 = new GUILayoutOption[] { GUILayout.Width(125f) };
                    ESP.zombieBox = GUILayout.Toggle(ESP.zombieBox, "僵尸方框", optionArray14);
                    GUILayout.EndHorizontal();
                    GUILayoutOption[] optionArray15 = new GUILayoutOption[] { GUILayout.Width(250f) };
                    GUILayout.BeginHorizontal(optionArray15);
                    GUILayoutOption[] optionArray16 = new GUILayoutOption[] { GUILayout.Width(125f) };
                    ESP.zombieName = GUILayout.Toggle(ESP.zombieName, "僵尸名称", optionArray16);
                    GUILayoutOption[] optionArray17 = new GUILayoutOption[] { GUILayout.Width(125f) };
                    ESP.zombieHealth = GUILayout.Toggle(ESP.zombieHealth, "僵尸血量", optionArray17);
                    GUILayout.EndHorizontal();
                    GUILayoutOption[] optionArray18 = new GUILayoutOption[] { GUILayout.Width(250f) };
                    GUILayout.BeginHorizontal(optionArray18);
                    GUILayoutOption[] optionArray19 = new GUILayoutOption[] { GUILayout.Width(125f) };
                    ESP.playerCornerBox = GUILayout.Toggle(ESP.playerCornerBox, "玩家边缘方框", optionArray19);
                    GUILayoutOption[] optionArray20 = new GUILayoutOption[] { GUILayout.Width(130f) };
                    ESP.zombieCornerBox = GUILayout.Toggle(ESP.zombieCornerBox, "僵尸边缘方框", optionArray20);
                    Cheat.chams = GUILayout.Toggle(Cheat.chams, "上色透视", Array.Empty<GUILayoutOption>());
                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                    GUI.color = Color.yellow;
                    GUILayout.BeginVertical("传送到航点", GUI.skin.box, Array.Empty<GUILayoutOption>());
                    GUILayout.Space(20f);
                    GUILayoutOption[] optionArray21 = new GUILayoutOption[] { GUILayout.MaxWidth(260f) };
                    this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, optionArray21);
                    if ((Objects.waypointCollection != null) && (Objects.waypointCollection.List.Count > 0))
                    {
                        foreach (Waypoint waypoint in Objects.waypointCollection.List)
                        {
                            GUI.color = Color.white;
                            if (GUILayout.Button(waypoint.name, Array.Empty<GUILayoutOption>()))
                            {
                                nullable = null;
                                Objects.localPlayer.TeleportToPosition((Vector3) waypoint.pos, false, nullable);
                            }
                        }
                    }
                    GUILayout.EndScrollView();
                    GUILayout.EndVertical();
                    GUI.color = Color.yellow;
                    GUILayout.BeginVertical("传送到僵尸", GUI.skin.box, Array.Empty<GUILayoutOption>());
                    GUILayout.Space(20f);
                    GUILayoutOption[] optionArray22 = new GUILayoutOption[] { GUILayout.MaxWidth(260f) };
                    this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, optionArray22);
                    if (Objects.zombieList.Count > 1)
                    {
                        foreach (EntityZombie zombie2 in Objects.zombieList)
                        {
                            if (((zombie2 != null) && (zombie2 != Objects.localPlayer)) && zombie2.IsAlive())
                            {
                                float num4 = Vector3.Distance(Objects.localPlayer.position, zombie2.position);
                                GUI.color = Color.white;
                                if (GUILayout.Button(zombie2.EntityName + " - Dist: " + num4.ToString(), Array.Empty<GUILayoutOption>()))
                                {
                                    nullable = null;
                                    Objects.localPlayer.TeleportToPosition(zombie2.GetPosition(), false, nullable);
                                }
                            }
                        }
                    }
                    GUILayout.EndScrollView();
                    GUILayout.EndVertical();
                    goto Label_2280;
                }
                case 0x30b:
                {
                    GUILayout.Space(10f);
                    GUI.color = Color.white;
                    if (GUILayout.Button("秒制作", new GUILayoutOption[0]))
                    {
                        try
                        {
                            foreach (Recipe local1 in CraftingManager.GetAllRecipes())
                            {
                                CraftingManager.UnlockRecipe(local1, Objects.localPlayer);
                                local1.ingredients.Clear();
                                local1.craftingTime = 0.1f;
                            }
                        }
                        catch (Exception exception12)
                        {
                            Debug.LogError("ERROR: " + exception12.Message);
                        }
                    }
                    GUI.color = Color.white;
                    if (GUILayout.Button("获得最大技能", new GUILayoutOption[0]))
                    {
                        try
                        {
                            if (Objects.localPlayer != null)
                            {
                                Progression progression = Objects.localPlayer.Progression;
                            }
                            Debug.Log("...");
                        }
                        catch (Exception exception13)
                        {
                            Debug.LogError("ERROR: " + exception13.Message);
                        }
                    }
                    GUI.color = Color.white;
                    if (GUILayout.Button("获取所有军事物品", new GUILayoutOption[0]))
                    {
                        try
                        {
                            foreach (Item item in itemDB.Item.Where<Item>(delegate (Item i) {
                                if (!i.Name.Contains("Military"))
                                {
                                    return i.Name.Contains("Ghillie");
                                }
                                return true;
                            }))
                            {
                                this.GiveItem(item.Name, 1);
                            }
                            Debug.Log("...");
                        }
                        catch (Exception exception14)
                        {
                            Debug.LogError("ERROR: " + exception14.Message);
                        }
                    }
                    GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
                    GUILayout.FlexibleSpace();
                    string[] strArray = resourceNames.ToArray();
                    int num5 = GUIComboBox.Box(selectedresource, strArray, "SelectResource");
                    if ((num5 != selectedresource) && (num5 >= 0))
                    {
                        selectedresource = num5;
                        if (selectedresource > 0)
                        {
                            Debug.Log("Giving Resource Item: " + resourceNames.ElementAt<string>(selectedresource));
                            this.GiveItem("resource" + resourceNames.ElementAt<string>(selectedresource), 1);
                        }
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(5f);
                    GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
                    GUILayout.FlexibleSpace();
                    string[] strArray2 = itemModNames.ToArray();
                    int num6 = GUIComboBox.Box(selectedItemMod, strArray2, "SelectItemMod");
                    if ((num6 != selectedItemMod) && (num6 >= 0))
                    {
                        selectedItemMod = num6;
                        if (selectedItemMod > 0)
                        {
                            string str = itemModNames.ElementAt<string>(selectedItemMod);
                            Debug.Log("Giving Item Modifier: " + str);
                            switch (str)
                            {
                                case "WhiteRiverSupplies":
                                case "CassadoreSupplies":
                                    this.GiveItem("quest" + str, 1);
                                    goto Label_133B;
                            }
                            this.GiveItem("mod" + str, 1);
                        }
                    }
                    break;
                }
                case 780:
                    GUILayout.Space(10f);
                    GUI.color = Color.white;
                    if (GUILayout.Button("生成空投", new GUILayoutOption[0]))
                    {
                        try
                        {
                            this.DoCommand("spawnairdrop", "");
                            Debug.Log("Spawned Airdrop...");
                        }
                        catch (Exception exception15)
                        {
                            Debug.LogError("ERROR: " + exception15.Message);
                        }
                    }
                    GUI.color = Color.white;
                    if (GUILayout.Button("生成供给箱", new GUILayoutOption[0]))
                    {
                        try
                        {
                            this.DoCommand("spawnsupplycrate", "");
                            Debug.Log("Spawned Supply Crate...");
                        }
                        catch (Exception exception16)
                        {
                            Debug.LogError("ERROR: " + exception16.Message);
                        }
                    }
                    GUI.color = Color.white;
                    if (GUILayout.Button("生成僵尸侦察兵", new GUILayoutOption[0]))
                    {
                        try
                        {
                            this.DoCommand("spawnscouts", "");
                            Debug.Log("Spawned Zombie Scouts...");
                        }
                        catch (Exception exception17)
                        {
                            Debug.LogError("ERROR: " + exception17.Message);
                        }
                    }
                    GUI.color = Color.white;
                    if (GUILayout.Button("生成僵尸部落", new GUILayoutOption[0]))
                    {
                        try
                        {
                            this.DoCommand("spawnwh", "");
                            Debug.Log("Spawned Zombie Horde...");
                        }
                        catch (Exception exception18)
                        {
                            Debug.LogError("ERROR: " + exception18.Message);
                        }
                    }
                    GUI.color = Color.white;
                    if (GUILayout.Button("天气 - " + weatherPresets[currentWeatherPreset], new GUILayoutOption[0]))
                    {
                        try
                        {
                            currentWeatherPreset++;
                            if (currentWeatherPreset > 5)
                            {
                                currentWeatherPreset = 0;
                            }
                            this.DoCommand("spectrum", weatherPresets[currentWeatherPreset]);
                            switch (currentWeatherPreset)
                            {
                                case 0:
                                    this.DoCommand("weather", "Fog 0");
                                    this.DoCommand("weather", "Clouds 0");
                                    this.DoCommand("weather", "Rain 0");
                                    this.DoCommand("weather", "Wet 0");
                                    this.DoCommand("weather", "Snow 0");
                                    this.DoCommand("weather", "SnowFall 0");
                                    break;

                                case 2:
                                    this.DoCommand("weather", "Fog 0.25");
                                    this.DoCommand("weather", "FogColor 1");
                                    break;

                                case 3:
                                case 4:
                                    this.DoCommand("weather", "Fog 0.1");
                                    this.DoCommand("weather", "Clouds 1");
                                    this.DoCommand("weather", "Rain 1");
                                    this.DoCommand("weather", "Wet 1");
                                    this.DoCommand("weather", "Snow 0");
                                    this.DoCommand("weather", "SnowFall 0");
                                    break;

                                case 5:
                                    this.DoCommand("weather", "Fog 0.1");
                                    this.DoCommand("weather", "Rain 0");
                                    this.DoCommand("weather", "Wet 0");
                                    this.DoCommand("weather", "Clouds 1");
                                    this.DoCommand("weather", "Snow 1");
                                    this.DoCommand("weather", "SnowFall 1");
                                    break;
                            }
                            Debug.Log("Cycled Weather: " + weatherPresets[currentWeatherPreset]);
                        }
                        catch (Exception exception19)
                        {
                            Debug.LogError("ERROR: " + exception19.Message);
                        }
                    }
                    GUI.color = Color.white;
                    if (GUILayout.Button("刷新饥饿/饥渴", new GUILayoutOption[0]))
                    {
                        try
                        {
                            this.DoCommand("food", "100");
                            this.DoCommand("thirsty", "100");
                            Debug.Log("Refreshed Hunger/Thirst...");
                        }
                        catch (Exception exception20)
                        {
                            Debug.LogError("ERROR: " + exception20.Message);
                        }
                    }
                    if (toggleTrees)
                    {
                        GUI.color = Color.green;
                    }
                    else
                    {
                        GUI.color = Color.white;
                    }
                    if (GUILayout.Button("隐藏树木", new GUILayoutOption[0]))
                    {
                        try
                        {
                            toggleTrees = !toggleTrees;
                            this.DoCommand("trees", "");
                            Debug.Log("Toggled Tree's: " + toggleTrees.ToString());
                        }
                        catch (Exception exception21)
                        {
                            Debug.LogError("ERROR: " + exception21.Message);
                        }
                    }
                    GUI.color = Color.white;
                    if (GUILayout.Button("获取工作站材料", new GUILayoutOption[0]))
                    {
                        try
                        {
                            for (int k = 0; k < 5; k++)
                            {
                                this.DoCommand("wsmats", " " + k.ToString() + " 100");
                            }
                            Debug.Log("Gave Workstation Materials...");
                        }
                        catch (Exception exception22)
                        {
                            Debug.LogError("ERROR: " + exception22.Message);
                        }
                    }
                    GUI.color = Color.white;
                    if (GUILayout.Button("去除伤害", new GUILayoutOption[0]))
                    {
                        try
                        {
                            this.DoCommand("debuff", "buffInfectionCatch");
                            this.DoCommand("debuff", "buffAbrasionCatch");
                            this.DoCommand("debuff", "buffLegSprainedCHTrigger");
                            this.DoCommand("debuff", "buffLegBroken");
                            this.DoCommand("debuff", "buffArmSprainedCHTrigger");
                            this.DoCommand("debuff", "buffArmBroken");
                            this.DoCommand("debuff", "buffFatiguedTrigger");
                            this.DoCommand("debuff", "buffInjuryBleedingTwo");
                            this.DoCommand("debuff", "buffLaceration");
                            this.DoCommand("debuff", "buffInjuryStunned01CHTrigger");
                            this.DoCommand("debuff", "buffInjuryStunned01Cooldown");
                            this.DoCommand("debuff", "buffInjuryConcussion");
                            this.DoCommand("debuff", "buffInjuryBleedingOne");
                            this.DoCommand("debuff", "buffInjuryBleedingBarbedWire");
                            this.DoCommand("debuff", "buffInjuryBleeding");
                            this.DoCommand("debuff", "buffInjuryBleedingParticle");
                            this.DoCommand("debuff", "buffInjuryAbrasion");
                            this.DoCommand("debuff", "buffPlayerFallingDamage");
                            this.DoCommand("debuff", "buffFatigued");
                            this.DoCommand("debuff", "buffStayDownKO");
                            this.DoCommand("debuff", "buffInjuryCrippled01");
                            this.DoCommand("debuff", "buffInjuryUnconscious");
                            this.DoCommand("debuff", "buffBatterUpSlowDown");
                            this.DoCommand("debuff", "buffRadiation03");
                            this.DoCommand("debuff", "buffNearDeathTrauma");
                            this.DoCommand("debuff", "buffDysenteryCatchFood");
                            this.DoCommand("debuff", "buffDysenteryCatchDrink");
                            this.DoCommand("debuff", "buffDysenteryMain");
                            this.DoCommand("debuff", "buffIllPneumonia00");
                            this.DoCommand("debuff", "buffIllPneumonia01");
                            this.DoCommand("debuff", "buffInfectionCatch");
                            this.DoCommand("debuff", "buffInfectionMain");
                            this.DoCommand("debuff", "buffInfection04");
                            this.DoCommand("debuff", "buffStatusHungry01");
                            this.DoCommand("debuff", "buffStatusHungry02");
                            this.DoCommand("debuff", "buffStatusHungry03");
                            this.DoCommand("debuff", "buffStatusThirsty01");
                            this.DoCommand("debuff", "buffStatusThirsty02");
                            this.DoCommand("debuff", "buffStatusThirsty03");
                            Debug.Log("...");
                        }
                        catch (Exception exception23)
                        {
                            Debug.LogError("ERROR: " + exception23.Message);
                        }
                    }
                    GUILayout.Space(10f);
                    goto Label_2280;

                default:
                    goto Label_2280;
            }
        Label_133B:
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.FlexibleSpace();
            string[] items = vehicleNames.ToArray();
            int num7 = GUIComboBox.Box(selectedvehicle, items, "SelectVehicle");
            if ((num7 != selectedvehicle) && (num7 >= 0))
            {
                selectedvehicle = num7;
                if (selectedvehicle > 0)
                {
                    Debug.Log("Giving Vehicle Item: " + vehicleNames.ElementAt<string>(selectedvehicle));
                    this.GiveItem("vehicle" + vehicleNames.ElementAt<string>(selectedvehicle), 1);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.FlexibleSpace();
            string[] strArray4 = medicalNames.ToArray();
            int num8 = GUIComboBox.Box(selectedmedical, strArray4, "SelectMedical");
            if ((num8 != selectedmedical) && (num8 >= 0))
            {
                selectedmedical = num8;
                if (selectedmedical > 0)
                {
                    Debug.Log("Giving Medical Item: " + medicalNames.ElementAt<string>(selectedmedical));
                    string itemname = medicalDic[medicalNames.ElementAt<string>(selectedmedical)];
                    this.GiveItem(itemname, 1);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.FlexibleSpace();
            string[] strArray5 = foodNames.ToArray();
            int num9 = GUIComboBox.Box(selectedfood, strArray5, "SelectFood");
            if ((num9 != selectedfood) && (num9 >= 0))
            {
                selectedfood = num9;
                if (selectedfood > 0)
                {
                    Debug.Log("Giving Food/Drink Item: " + foodNames.ElementAt<string>(selectedfood));
                    string str3 = foodDic[foodNames.ElementAt<string>(selectedfood)];
                    this.GiveItem(str3, 1);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.FlexibleSpace();
            string[] strArray6 = miscNames.ToArray();
            int num10 = GUIComboBox.Box(selectedmisc, strArray6, "SelectMisc");
            if ((num10 != selectedmisc) && (num10 >= 0))
            {
                selectedmisc = num10;
                if (selectedmisc > 0)
                {
                    Debug.Log("Giving Misc Item: " + miscNames.ElementAt<string>(selectedmisc));
                    this.GiveItem(miscNames.ElementAt<string>(selectedmisc), 1);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.FlexibleSpace();
            string[] strArray7 = schematicNames.ToArray();
            int num11 = GUIComboBox.Box(selectedschematic, strArray7, "SelectSchematic");
            if ((num11 != selectedschematic) && (num11 >= 0))
            {
                selectedschematic = num11;
                if (selectedschematic > 0)
                {
                    Debug.Log("Giving Schematic: " + schematicNames.ElementAt<string>(selectedschematic));
                    string str4 = schematicDic[schematicNames.ElementAt<string>(selectedschematic)];
                    this.GiveItem(str4, 1);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.FlexibleSpace();
            string[] strArray8 = bookNames.ToArray();
            int num12 = GUIComboBox.Box(selectedbook, strArray8, "SelectBook");
            if ((num12 != selectedbook) && (num12 >= 0))
            {
                selectedbook = num12;
                if (selectedbook > 0)
                {
                    Debug.Log("Giving Book: " + bookNames.ElementAt<string>(selectedbook));
                    this.GiveItem("book" + bookNames.ElementAt<string>(selectedbook), 1);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.FlexibleSpace();
            string[] strArray9 = ammoNames.ToArray();
            int num13 = GUIComboBox.Box(selectedammo, strArray9, "SelectAmmo");
            if ((num13 != selectedammo) && (num13 >= 0))
            {
                selectedammo = num13;
                if (selectedammo > 0)
                {
                    Debug.Log("Giving Ammo: " + ammoNames.ElementAt<string>(selectedammo));
                    this.GiveItem("ammo" + ammoNames.ElementAt<string>(selectedammo), 1);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.FlexibleSpace();
            string[] strArray10 = apparelNames.ToArray();
            int num14 = GUIComboBox.Box(selectedapparel, strArray10, "SelectApparel");
            if ((num14 != selectedapparel) && (num14 >= 0))
            {
                selectedapparel = num14;
                if (selectedapparel > 0)
                {
                    Debug.Log("Giving Apparel: " + apparelNames.ElementAt<string>(selectedapparel));
                    this.GiveItem("apparel" + apparelNames.ElementAt<string>(selectedapparel), 1);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.FlexibleSpace();
            string[] strArray11 = armorNames.ToArray();
            int num15 = GUIComboBox.Box(selectedarmor, strArray11, "SelectArmor");
            if ((num15 != selectedarmor) && (num15 >= 0))
            {
                selectedarmor = num15;
                if (selectedarmor > 0)
                {
                    Debug.Log("Giving Armor: " + armorNames.ElementAt<string>(selectedarmor));
                    this.GiveItem("armor" + armorNames.ElementAt<string>(selectedarmor), 1);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.FlexibleSpace();
            string[] strArray12 = weaponNames.ToArray();
            int num16 = GUIComboBox.Box(selectedweapon, strArray12, "SelectWeapon");
            if ((num16 != selectedweapon) && (num16 >= 0))
            {
                selectedweapon = num16;
                if (selectedweapon > 0)
                {
                    Debug.Log("Giving Weapon: " + weaponNames.ElementAt<string>(selectedweapon));
                    string str5 = weaponDic[weaponNames.ElementAt<string>(selectedweapon)];
                    this.GiveItem(str5, 1);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.FlexibleSpace();
            string[] strArray13 = loadoutNames.ToArray();
            int num17 = GUIComboBox.Box(selectedloadout, strArray13, "SelectLoadout");
            if ((num17 != selectedloadout) && (num17 >= 0))
            {
                selectedloadout = num17;
                if (selectedloadout > 0)
                {
                    Debug.Log("Giving Loadout: " + loadoutNames.ElementAt<string>(selectedloadout));
                    this.GiveItem("Loadout" + loadoutNames.ElementAt<string>(selectedloadout), 1);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.FlexibleSpace();
            string[] strArray14 = meleeNames.ToArray();
            int num18 = GUIComboBox.Box(selectedmelee, strArray14, "SelectMelee");
            if ((num18 != selectedmelee) && (num18 >= 0))
            {
                selectedmelee = num18;
                if (selectedmelee > 0)
                {
                    Debug.Log("Giving Melee Item: " + meleeNames.ElementAt<string>(selectedmelee));
                    this.GiveItem("melee" + meleeNames.ElementAt<string>(selectedmelee), 1);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.FlexibleSpace();
            string[] strArray15 = questNames.ToArray();
            int num19 = GUIComboBox.Box(selectedquest, strArray15, "SelectQuest");
            if ((num19 != selectedquest) && (num19 >= 0))
            {
                selectedquest = num19;
                if (selectedquest > 0)
                {
                    Debug.Log("Giving Quest Item: " + questNames.ElementAt<string>(selectedquest));
                    this.GiveItem("quest" + questNames.ElementAt<string>(selectedquest), 1);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
        Label_2280:
            GUI.DragWindow();
        }

        private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
        {
        }

        private void Start()
        {
            Debug.unityLogger.logEnabled = true;
            this.MainWindow = new Rect((float) ((Screen.width / 2) - 100), (float) ((Screen.height / 2) - 350), 260f, 50f);
            this.MiscWindow1 = new Rect(this.MainWindow.x + 260f, (float) ((Screen.height / 2) - 250), 250f, 150f);
            this.MiscWindow2 = new Rect(this.MainWindow.x + 260f, (float) ((Screen.height / 2) - 400), 250f, 150f);
            this.MiscWindow3 = new Rect(this.MainWindow.x + 260f, (float) ((Screen.height / 2) - 250), 250f, 150f);
            this.origHeight = Screen.height;
            this.origWidth = Screen.width;
            SceneManager.activeSceneChanged += new UnityAction<Scene, Scene>(this.SceneManager_activeSceneChanged);
            InsertHarmonyPatches();
            instance = this;
            itemDB = Xml2Object.Deserialize<Items>(baseDirectory + @"\Data\Config\items.xml");
            itemMods = Xml2Object.Deserialize<ItemModifiers>(baseDirectory + @"\Data\Config\item_modifiers.xml");
            this.BuildLists();
        }

        private void ToggleCmDm()
        {
            GameStats.Set(EnumGameStats.ShowSpawnWindow, this.cmDm);
            GameStats.Set(EnumGameStats.IsCreativeMenuEnabled, this.cmDm);
            GamePrefs.Set(EnumGamePrefs.DebugMenuEnabled, this.cmDm);
        }

        private void Update()
        {
            if (!Debug.unityLogger.logEnabled)
            {
                Debug.unityLogger.logEnabled = true;
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                this.MainWindowVisible = !this.MainWindowVisible;
            }
            if (!sceneTracker.ContainsKey(sceneName))
            {
                sceneTracker.Add(sceneName, sceneIndex);
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                foreach (KeyValuePair<string, int> pair in sceneTracker)
                {
                    Debug.LogWarning("Scene: " + pair.Key + " Index: " + pair.Value.ToString());
                }
            }
            if (Input.GetKeyDown(KeyCode.PageUp))
            {
                toggleCursor = !toggleCursor;
                GameManager.Instance.Pause(toggleCursor);
            }
            if (Input.GetKeyDown(KeyCode.F11))
            {
                killZombiesTrigger = true;
            }
            if (Input.GetKeyDown(KeyCode.F10))
            {
                godmodeTrigger = true;
            }
            if (Input.GetKeyDown(KeyCode.F9))
            {
                flymodeTrigger = true;
            }
            if (Input.GetKeyDown(KeyCode.F8))
            {
                invisibleTrigger = true;
            }
            if (Input.GetKeyDown(KeyCode.End))
            {
                Objects.localPlayer.AddHealth(Objects.localPlayer.GetMaxHealth());
                Objects.localPlayer.AddStamina((float) Objects.localPlayer.GetMaxStamina());
                Debug.Log("Added Health/Stamina...");
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TrainerMenu.<>c <>9 = new TrainerMenu.<>c();
            public static Func<Item, bool> <>9__90_0;
            public static Func<Item, bool> <>9__97_0;
            public static Func<Item, bool> <>9__97_1;
            public static Func<Item, bool> <>9__97_10;
            public static Func<Item, bool> <>9__97_11;
            public static Func<Item, bool> <>9__97_12;
            public static Func<Item, bool> <>9__97_13;
            public static Func<Item, bool> <>9__97_2;
            public static Func<Item, bool> <>9__97_3;
            public static Func<Item, bool> <>9__97_4;
            public static Func<Item, bool> <>9__97_5;
            public static Func<Item, bool> <>9__97_6;
            public static Func<Item, bool> <>9__97_7;
            public static Func<Item, bool> <>9__97_8;
            public static Func<Item, bool> <>9__97_9;

            internal bool <BuildLists>b__97_0(Item i) => 
                i.Name.EndsWith("Schematic");

            internal bool <BuildLists>b__97_1(Item w) => 
                ((w.Name.StartsWith("medical") || w.Name.StartsWith("drug")) && !w.Name.EndsWith("Schematic"));

            internal bool <BuildLists>b__97_10(Item w) => 
                (w.Name.StartsWith("Loadout") && !w.Name.EndsWith("Schematic"));

            internal bool <BuildLists>b__97_11(Item w) => 
                (w.Name.StartsWith("melee") && !w.Name.EndsWith("Schematic"));

            internal bool <BuildLists>b__97_12(Item w) => 
                (w.Name.StartsWith("quest") && !w.Name.EndsWith("Schematic"));

            internal bool <BuildLists>b__97_13(Item w) => 
                (w.Name.StartsWith("vehicle") && !w.Name.EndsWith("Schematic"));

            internal bool <BuildLists>b__97_2(Item w) => 
                ((w.Name.StartsWith("food") || w.Name.StartsWith("drink")) && !w.Name.EndsWith("Schematic"));

            internal bool <BuildLists>b__97_3(Item w) => 
                (w.Name.StartsWith("resource") && !w.Name.EndsWith("Schematic"));

            internal bool <BuildLists>b__97_4(Item w) => 
                (((((!w.Name.StartsWith("ammo") && !w.Name.StartsWith("apparel")) && (!w.Name.StartsWith("armor") && !w.Name.StartsWith("book"))) && ((!w.Name.StartsWith("gun") && !w.Name.StartsWith("thrown")) && (!w.Name.StartsWith("Loadout") && !w.Name.StartsWith("melee")))) && (((!w.Name.StartsWith("quest") && !w.Name.StartsWith("resource")) && (!w.Name.StartsWith("vehicle") && !w.Name.StartsWith("medical"))) && ((!w.Name.StartsWith("drug") && !w.Name.StartsWith("drink")) && !w.Name.StartsWith("food")))) && !w.Name.EndsWith("Schematic"));

            internal bool <BuildLists>b__97_5(Item b) => 
                b.Name.StartsWith("book");

            internal bool <BuildLists>b__97_6(Item a) => 
                a.Name.StartsWith("ammo");

            internal bool <BuildLists>b__97_7(Item a) => 
                a.Name.StartsWith("apparel");

            internal bool <BuildLists>b__97_8(Item a) => 
                (a.Name.StartsWith("armor") && !a.Name.EndsWith("Schematic"));

            internal bool <BuildLists>b__97_9(Item w) => 
                ((w.Name.StartsWith("gun") || w.Name.StartsWith("thrown")) && !w.Name.EndsWith("Schematic"));

            internal bool <RenderUI>b__90_0(Item i) => 
                (i.Name.Contains("Military") || i.Name.Contains("Ghillie"));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__0
        {
            public static readonly TrainerMenu.<>c__0 <>9 = new TrainerMenu.<>c__0();
            public static Func<Item, bool> <>9__0_0;

            internal bool <RenderUI>b__0_0(Item i) => 
                (i.Name.Contains("Military") || i.Name.Contains("Ghillie"));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__1
        {
            public static readonly TrainerMenu.<>c__1 <>9 = new TrainerMenu.<>c__1();
            public static Func<Item, bool> <>9__0_0;

            internal bool <RenderUI>b__0_0(Item i) => 
                (i.Name.Contains("Military") || i.Name.Contains("Ghillie"));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__10
        {
            public static readonly TrainerMenu.<>c__10 <>9 = new TrainerMenu.<>c__10();
            public static Func<Item, bool> <>9__0_0;

            internal bool <RenderUI>b__0_0(Item i)
            {
                if (!i.Name.Contains("Military"))
                {
                    return i.Name.Contains("Ghillie");
                }
                return true;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__11
        {
            public static readonly TrainerMenu.<>c__11 <>9 = new TrainerMenu.<>c__11();
            public static Func<Item, bool> <>9__0_0;

            internal bool <RenderUI>b__0_0(Item i)
            {
                if (!i.Name.Contains("Military"))
                {
                    return i.Name.Contains("Ghillie");
                }
                return true;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__12
        {
            public static readonly TrainerMenu.<>c__12 <>9 = new TrainerMenu.<>c__12();
            public static Func<Item, bool> <>9__0_0;

            internal bool <RenderUI>b__0_0(Item i)
            {
                if (!i.Name.Contains("Military"))
                {
                    return i.Name.Contains("Ghillie");
                }
                return true;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__13
        {
            public static readonly TrainerMenu.<>c__13 <>9 = new TrainerMenu.<>c__13();
            public static Func<Item, bool> <>9__0_0;

            internal bool <RenderUI>b__0_0(Item i)
            {
                if (!i.Name.Contains("Military"))
                {
                    return i.Name.Contains("Ghillie");
                }
                return true;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__2
        {
            public static readonly TrainerMenu.<>c__2 <>9 = new TrainerMenu.<>c__2();
            public static Func<Item, bool> <>9__0_0;

            internal bool <RenderUI>b__0_0(Item i) => 
                (i.Name.Contains("Military") || i.Name.Contains("Ghillie"));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__3
        {
            public static readonly TrainerMenu.<>c__3 <>9 = new TrainerMenu.<>c__3();
            public static Func<Item, bool> <>9__0_0;

            internal bool <RenderUI>b__0_0(Item i) => 
                (i.Name.Contains("Military") || i.Name.Contains("Ghillie"));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__4
        {
            public static readonly TrainerMenu.<>c__4 <>9 = new TrainerMenu.<>c__4();
            public static Func<Item, bool> <>9__0_0;

            internal bool <RenderUI>b__0_0(Item i)
            {
                if (!i.Name.Contains("Military"))
                {
                    return i.Name.Contains("Ghillie");
                }
                return true;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__5
        {
            public static readonly TrainerMenu.<>c__5 <>9 = new TrainerMenu.<>c__5();
            public static Func<Item, bool> <>9__0_0;

            internal bool <RenderUI>b__0_0(Item i)
            {
                if (!i.Name.Contains("Military"))
                {
                    return i.Name.Contains("Ghillie");
                }
                return true;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__6
        {
            public static readonly TrainerMenu.<>c__6 <>9 = new TrainerMenu.<>c__6();
            public static Func<Item, bool> <>9__0_0;

            internal bool <RenderUI>b__0_0(Item i)
            {
                if (!i.Name.Contains("Military"))
                {
                    return i.Name.Contains("Ghillie");
                }
                return true;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__7
        {
            public static readonly TrainerMenu.<>c__7 <>9 = new TrainerMenu.<>c__7();
            public static Func<Item, bool> <>9__0_0;

            internal bool <RenderUI>b__0_0(Item i)
            {
                if (!i.Name.Contains("Military"))
                {
                    return i.Name.Contains("Ghillie");
                }
                return true;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__8
        {
            public static readonly TrainerMenu.<>c__8 <>9 = new TrainerMenu.<>c__8();
            public static Func<Item, bool> <>9__0_0;

            internal bool <RenderUI>b__0_0(Item i)
            {
                if (!i.Name.Contains("Military"))
                {
                    return i.Name.Contains("Ghillie");
                }
                return true;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__9
        {
            public static readonly TrainerMenu.<>c__9 <>9 = new TrainerMenu.<>c__9();
            public static Func<Item, bool> <>9__0_0;

            internal bool <RenderUI>b__0_0(Item i)
            {
                if (!i.Name.Contains("Military"))
                {
                    return i.Name.Contains("Ghillie");
                }
                return true;
            }
        }
    }
}

