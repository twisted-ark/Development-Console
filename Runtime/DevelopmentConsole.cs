using System.Collections.Generic;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public sealed class DevelopmentConsole : ScriptableObject
    {
        #region Static Values

        public static DevelopmentConsole Instance { get; private set; }

        public static event System.Action<bool> VisibilityChanged;

        private static bool isActive;

        public static bool IsActive
        {
            get => isActive;
            set
            {
                if (value == isActive)
                    return;
                isActive = value;
                VisibilityChanged?.Invoke (isActive);
            }
        }

        private static List<ConsoleTab> tabs = new List<ConsoleTab> (5);

        #endregion

        [SerializeField] internal GUISkin ConsoleSkin;
        
        [Header ("Not Actually adjustable!")]
        [SerializeField] internal int MaxTabWidth = 300;

        [SerializeField] internal bool UseBuiltInGui = true;

        [SerializeField] internal KeyCode ToggleGuiKey = KeyCode.F2;

        [SerializeField] internal float LineHeight = 16;

        [SerializeField] internal float FontSize = 1.8f;
        
        public int TabCount => tabs.Count;

        public ConsoleTab GetTab (int index)
        {
            return tabs[index];
        }

        public static ConsoleTab GetOrCreateTab ()
        {
            return GetOrCreateTab (string.Empty);
        }

        public static ConsoleTab GetOrCreateTab (string name)
        {
            var tab = tabs.Find (n => n.Name.Equals (name));

            if (tab == null)
            {
                tab = new ConsoleTab (name);
                tabs.Add (tab);
            }

            return tab;
        }

        public static void AddElement (string tabName, GuiElementBase elementBase)
        {
            var tab = GetOrCreateTab (tabName);
        }

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize ()
        {
            Instance = GetOrCreateScriptableSingleton ();
            
            if (Instance.UseBuiltInGui)
            {
                CreateBuiltInGui ();
            }
        }

        private static void CreateBuiltInGui ()
        {
            var gui = new GameObject ("DevelopmentConsole").AddComponent<DevelopmentConsoleGui> ();
            gui.console = Instance;
            DontDestroyOnLoad (gui.gameObject);
        }

        private static DevelopmentConsole GetOrCreateScriptableSingleton ()
        {
            var console = Resources.Load<DevelopmentConsole> ("DevelopmentConsole");
            if (console == false)
            {
                console = CreateInstance<DevelopmentConsole> ();

#if UNITY_EDITOR
                Editor_CreateAsset (console);
#endif
            }

            return console;
        }

#if UNITY_EDITOR
        private static void Editor_CreateAsset (DevelopmentConsole console)
        {
            if (!UnityEditor.AssetDatabase.IsValidFolder ("Assets/Resources"))
                UnityEditor.AssetDatabase.CreateFolder ("Assets", "Resources");
            UnityEditor.AssetDatabase.CreateAsset (console, "Assets/Resources/DevelopmentConsole.asset");
        }
#endif
    }
}