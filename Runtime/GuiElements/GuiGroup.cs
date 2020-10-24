using System.Collections.Generic;
using UnityEngine;

namespace TwistedArk.Development.Console.Runtime
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

        public void Remove (GuiElementBase element)
        {
            elements.Remove (element);
        }

        public void Remove (string elementName)
        {
            if (TryGetNamedElement (elementName, out GuiElementBase elementBase))
                elements.Remove (elementBase);
        }

        public bool TryGetNamedElement<T> (string elementName, out T elementBase) where T : GuiElementBase
        {
            elementBase = elements.Find (
                element => string.CompareOrdinal (element.Label, elementName) == 0) as T;

            return elementBase != null;
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
                var height = element.GetContentHeight ();
                elementRect.height = height;
                
                element.OnPreDraw (elementRect, skin);
                element.OnDraw (elementRect, skin);
                
                elementRect.y += height + DevelopmentConsole.Instance.ElementPadding;
            }
        }
        
        public override float GetContentHeight ()
        {
            var height = LineHeightPadded;
            
            foreach (var element in elements)
            {
                height += element.GetContentHeight ();
                height += DevelopmentConsole.Instance.ElementPadding;
            }
            
            return height;
        }

        public override void Dispose ()
        {
            elements.Clear ();
        }
    }
}