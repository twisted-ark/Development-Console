using System.Collections.Generic;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public class GuiGroup : GuiElementBase, IGuiElementGroup
    {
        private List<GuiElementBase> elements;

        public GuiGroup (string label) : base (label)
        {
            elements = new List<GuiElementBase> (elements);
        }

        public void Add (GuiElementBase element)
        {
            elements.Add (element);
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