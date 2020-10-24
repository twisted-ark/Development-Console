using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TwistedArk.Development.Console
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

        [SerializeField] internal Vector2Int ReferenceResolution = new Vector2Int (1080, 1920);
        
        [FormerlySerializedAs ("ConsoleSkin")]
        [Header ("Skin")]
        [SerializeField] internal ConsoleSkin Skin;
        
        [Header ("Controls")]
        [SerializeField] internal bool UseBuiltInGui = true;
        [SerializeField] internal KeyCode ToggleGuiKey = KeyCode.F2;

        [Header ("Layout")]
        [SerializeField] internal Vector2 anchorsMin = Vector2.zero;
        [SerializeField] internal Vector2 anchorsMax = Vector2.one;

        [Space]
        [SerializeField] internal Vector2 PaddingX = new Vector2 (4, 4);
        [SerializeField] internal Vector2 PaddingY = new Vector2 (4, 4);
        
        [Space]
        [SerializeField] internal float HeaderHeight = 60;
        [SerializeField] internal float HeaderSpacing = 16;
        
        [SerializeField] internal float LineHeight = 16;
        [SerializeField] internal float LinePadding = 2;

        [SerializeField] internal float ElementPadding = 20;
        
        [Header ("Colors")]
        [SerializeField] internal Color BackgroundColor = new Color (.1f, .1f, .1f, .7f);
        [SerializeField] internal Color ForegroundColor = new Color (0.7f, 0.7f, 0.7f, 0.7f);
        
        [SerializeField] internal Color HeaderColorActive = new Color (.8f, .8f, .8f, 1f);
        [SerializeField] internal Color HeaderColor = new Color (.8f, .8f, .8f, .4f);
        
        public static int TabCount => tabs.Count;

        public static ConsoleTab GetTab (int index)
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
            return tab ?? CreateTab (name);
        }

        public static void AddElementToTab (string tabName, GuiElementBase elementBase)
        {
            var tab = GetOrCreateTab (tabName);
            tab.Add (elementBase);
        }

        private static ConsoleTab CreateTab (string name)
        {
            var tab = new ConsoleTab (name);
            tabs.Add (tab);

            return tab;
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