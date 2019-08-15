using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public abstract class GuiElementBase : IDisposable
    {
        private static readonly Stack<Color> colorStack = new Stack<Color> ();
        
        protected static float LineHeight => DevelopmentConsole.Instance.LineHeight;
        protected static float LineHeightPadded => DevelopmentConsole.Instance.LineHeight
                                                    + DevelopmentConsole.Instance.LinePadding;

        protected static float LinePadding => DevelopmentConsole.Instance.LinePadding;
        
        public string Label { get; }

        
        protected GuiElementBase (string label)
        {
            Label = label;
        }
        
        public abstract void Draw (in Rect rect);

        public virtual float GetHeight () => LineHeightPadded;

        public virtual void Dispose () { }

        protected Rect DrawPrefixLabel (in Rect rect)
        {
            var labelWidth = rect.width / 3f;
            
            var labelRect = rect;
            labelRect.width = math.clamp (labelWidth, 100f, 300f);

            var contentRect = rect;
            contentRect.x += labelRect.width;
            contentRect.width -= labelRect.width;
            
            GUI.Label (labelRect, Label);

            return contentRect;
        }
        
        protected static void PushGuiColor (in Color color)
        {
            colorStack.Push (GUI.color);
            GUI.color = color;
        }

        protected static void PopGuiColor ()
        {
            if (colorStack.Count > 0)
                GUI.color = colorStack.Pop ();
        }
    }

    public abstract class GuiElement<T> : GuiElementBase
    {
        protected Action<T> valueChanged;
        
        protected GuiElement (string label, Action<T> valueChanged) : base (label)
        {
            this.valueChanged = valueChanged;
        }
    }
    
}
