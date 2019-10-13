using System.Collections.Generic;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public class GuiGroup : GuiElementBase
    {
        private List<GuiElementBase> elements;

        public GuiGroup (string label, params GuiElementBase[] elementsBase) : base (label)
        {
            this.elements = new List<GuiElementBase> (elementsBase);
        }

        public static GuiGroup ConstructGroup (ConsoleTab tab, string label, params GuiElementBase[] elementsBase)
        {
            var group = new GuiGroup (label, elementsBase);
            tab.Add (group);
            return group;
        }
        
        public static GuiGroup ConstructGroup (ConsoleTab tab, Component component, params GuiElementBase[] elementsBase)
        {
            var label = $"{component.name}({component.GetInstanceID ().ToString ()})";
            return ConstructGroup (tab, label, elementsBase);
        }

        public override void Draw (in Rect rect)
        {
            OnDraw (in rect);
        }

        protected override void OnDraw (in Rect rect)
        {
            var elementRect = rect;
            foreach (var element in elements)
            {
                var height = element.GetHeight () * 3;
                elementRect.height = height;
                
                element.Draw (elementRect);
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