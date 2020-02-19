using System.Collections.Generic;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public class GuiGroup : GuiElementBase, IGuiElementGroup
    {       
        private readonly List<GuiElementBase> elements;
        
        public GuiGroup (string label) : base (label)
        {
            elements = new List<GuiElementBase> ();
        }

        public void Add (GuiElementBase element)
        {
            elements.Add (element);
        }

        public bool TryGetElementWithName (string name, out GuiElementBase guiElement)
        {
            foreach (var element in elements)
            {
                if (string.CompareOrdinal (name, element.Label) == 0)
                {
                    guiElement = element;
                    return true;
                }
            }

            guiElement = default;
            return false;
        }

        public override void OnDraw (in Rect rect, ConsoleSkin skin)
        {
            var elementRect = rect;
            
            GuiColors.PushGuiColor (Color.gray);
            GUI.DrawTexture (rect, skin.Background);
            GuiColors.PopGuiColor ();
            
            elementRect.height = LineHeight;
            var labelRect = elementRect;
            
            GUI.Label (labelRect, Label, skin.GetOrCreateStyle ("Label Centered", GUI.skin.label));
            
            elementRect.y += LineHeightPadded;
            elementRect.x += 20;
            elementRect.width -= 40;
            
            foreach (var element in elements)
            {
                var height = element.GetHeight ();
                elementRect.height = height;
                
                element.OnPreDraw (elementRect, skin);
                element.OnDraw (elementRect, skin);
                elementRect.y += height;
            }
        }
        
        public override float GetHeight ()
        {
            var height = LineHeightPadded + LinePadding;
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