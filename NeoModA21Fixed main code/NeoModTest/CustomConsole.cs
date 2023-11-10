namespace NeoModTest
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class CustomConsole : MonoBehaviour
    {
        public static bool initialized = false;
        public static CustomConsole instance = null;
        public static bool isShow = true;
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        public CustomConsole()
        {
            instance = this;
            try
            {
                AllocConsole();
                StreamWriter newOut = new StreamWriter(Console.OpenStandardOutput()) {
                    AutoFlush = true
                };
                Console.SetOut(newOut);
            }
            catch (Exception exception)
            {
                Debug.LogError("ERROR Allocating Console: " + exception.Message);
            }
        }

        [DllImport("Kernel32.dll")]
        private static extern bool AllocConsole();
        private static void Application_logMessageReceivedThreaded(string condition, string stackTrace, LogType type)
        {
            try
            {
                switch (type)
                {
                    case LogType.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;

                    case LogType.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;

                    case LogType.Log:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case LogType.Exception:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                }
                Console.WriteLine(condition + " " + stackTrace);
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch
            {
            }
        }

        public static void ClearAllocConsole()
        {
            system("CLS");
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
        public static void Hide()
        {
            ShowWindow(GetConsoleWindow(), 0);
            isShow = false;
        }

        public static bool Initialize(bool showUnityMessages = false)
        {
            try
            {
                if (showUnityMessages)
                {
                    Application.logMessageReceivedThreaded += new Application.LogCallback(CustomConsole.Application_logMessageReceivedThreaded);
                }
                initialized = true;
                return true;
            }
            catch (Exception exception)
            {
                Debug.LogError("ERROR Initializing Console: " + exception.Message);
                return false;
            }
        }

        public static void Show()
        {
            ShowWindow(GetConsoleWindow(), 5);
            isShow = true;
        }

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("msvcrt.dll")]
        public static extern int system(string cmd);
        public static void Toggle()
        {
            if (isShow)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }
}

