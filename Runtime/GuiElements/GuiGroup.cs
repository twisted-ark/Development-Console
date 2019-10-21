using System.Collections.Generic;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public class GuiGroup : GuiElementBase
    {
        private List<GuiElementBase> elements;

        public GuiGroup (string label, params GuiElementBase[] elements) : base (label)
        {
            this.elements = new List<GuiElementBase> (elements);
        }

        public static GuiGroup ConstructGroup (ConsoleTab tab, string label, params GuiElementBase[] elements)
        {
            var group = new GuiGroup (label, elements);
            tab.Add (group);
            return group;
        }
        
        public static GuiGroup ConstructGroup (string tabName, string label, params GuiElementBase[] elements)
        {
            var tab = DevelopmentConsole.GetOrCreateTab (tabName);
            return ConstructGroup (tab, label, elements);
        }
        
        public static GuiGroup ConstructGroup (ConsoleTab tab, Component component, params GuiElementBase[] elements)
        {
            var label = $"{component.name}({component.GetInstanceID ().ToString ()})";
            return ConstructGroup (tab, label, elements);
        }
        
        public static GuiGroup ConstructGroup (string tabName, Component component, params GuiElementBase[] elements)
        {
            var tab = DevelopmentConsole.GetOrCreateTab (tabName);
            var label = $"{component.name}({component.GetInstanceID ().ToString ()})";
            return ConstructGroup (tab, label, elements);
        }

        public override void OnDraw (in Rect rect, ConsoleSkin skin)
        {
            var elementRect = rect;
            foreach (var element in elements)
            {
                var height = element.GetHeight () * 3;
                elementRect.height = height;
                
                element.OnPreDraw (elementRect, skin);
                element.OnDraw (elementRect, skin);
                elementRect.y += height;
            }
        }
        
        public override float GetHeight ()
        {
            var height = 0f;
            foreach (var element in elements)
            {
                height += element.GetHeight ();
            }
            return height;
        }

        public override void Dispose ()
        {
            elements.Clear ();
        }
    }
}