namespace NeoModTest
{
    using HarmonyLib;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using UnityEngine;

    public static class Loader
    {
        private static BindingFlags bindflags = (BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        public static bool consoleInitialized = false;
        public static GameObject Load;
        public static bool pollStatusFired = false;
        public static SceneDebugger scDebugger = null;

        public static void Init()
        {
            Load = new GameObject("TestTrainer");
            Load.transform.parent = null;
            Load.AddComponent<Cheat>();
            Load.AddComponent<Objects>();
            Load.AddComponent<ESP>();
            Load.AddComponent<TrainerMenu>();
            scDebugger = Load.AddComponent<SceneDebugger>();
            scDebugger.enabled = false;
            Load.AddComponent<CustomConsole>();
            if (!consoleInitialized || (CustomConsole.instance == null))
            {
                try
                {
                    if (CustomConsole.Initialize(true))
                    {
                        consoleInitialized = true;
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("ERROR: " + exception.Message);
                }
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("");
            Console.WriteLine("7 Days To Die Trainer v1.25 - Fixed by hehe");
            Console.ForegroundColor = ConsoleColor.White;
            UnityEngine.Object.DontDestroyOnLoad(Load);
        }

        public static void InitThreading()
        {
            new Thread(delegate {
                Thread.Sleep(900);
                Init();
            }).Start();
        }

        public static void InsertHarmonyPatches()
        {
            try
            {
                Debug.Log("Inserting Hooks...");
                Harmony harmony = new Harmony("wh0am15533.trainer");
                Debug.Log("Runtime Hooks's Applied");
            }
            catch (Exception exception)
            {
                Debug.LogError("FAILED to Apply Hooks's! Error: " + exception.Message);
            }
        }

        public static void Main(string[] args)
        {
            InitThreading();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Loader.<>c <>9 = new Loader.<>c();
            public static ThreadStart <>9__6_0;

            internal void <InitThreading>b__6_0()
            {
                Thread.Sleep(900);
                Loader.Init();
            }
        }
    }
}

