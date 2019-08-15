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

        [Header ("Skin")]
        [SerializeField] internal GUISkin ConsoleSkin;
        
        [Header ("Controls")]
        [SerializeField] internal bool UseBuiltInGui = true;
        [SerializeField] internal KeyCode ToggleGuiKey = KeyCode.F2;

        [Header ("Drawing")]
        [SerializeField] internal Vector2 anchorsMin = Vector2.zero;
        [SerializeField] internal Vector2 anchorsMax = Vector2.one;

        [SerializeField] internal float HeaderHeight = 60;
        [SerializeField] internal float LineHeight = 16;
        [SerializeField] internal float LinePadding = 2;
        
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