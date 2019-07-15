using System.Collections.Generic;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public sealed class DevelopmentConsole : ScriptableObject
    {
        private static DevelopmentConsole instance;
        
        public static bool IsActive { get; internal set; }
        
        private static List<ConsoleTab> tabs = new List<ConsoleTab> (5);
        
        [SerializeField]
        internal int MaxTabWidth = 300;

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

        public static void AddElement (string tabName, GuiElement element)
        {
            var tab = GetOrCreateTab (tabName);
        }
        
        [RuntimeInitializeOnLoadMethod]
        private static void Initialize ()
        {
            instance = GetOrCreateScriptableSingleton ();
            var gui = new GameObject("DevelopmentConsole").AddComponent<DevelopmentConsoleGui> ();
            gui.console = instance;
        }

        private static DevelopmentConsole GetOrCreateScriptableSingleton ()
        {
            var console = Resources.Load<DevelopmentConsole> ("DevelopmentConsole.asset");
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