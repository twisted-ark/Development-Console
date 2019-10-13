using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public abstract class GuiElementBase : IDisposable
    {
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

        protected abstract void OnDraw (in Rect rect);

        public virtual float GetHeight () => LineHeightPadded;

        public virtual void Dispose () { }

        protected Rect DrawPrefixLabel (in Rect rect)
        {
            var labelRect = rect;
            labelRect.width = math.clamp (rect.width / 3f, 100f, 300f);

            var contentRect = rect;
            contentRect.x += labelRect.width;
            contentRect.width -= labelRect.width;
            
            GUI.Label (labelRect, Label);

            return contentRect;
        }
    }

    public abstract class GuiElement<T> : GuiElementBase
    {
        protected Action<T> valueChanged;
        protected Func<T> updateValue;
        
        protected T currentValue;
        
        protected GuiElement (string label, Action<T> valueChanged, Func<T> updateValue) : base (label)
        {
            this.valueChanged = valueChanged;
            this.updateValue = updateValue;

            UpdateValue (ref currentValue);
        }

        public override void Dispose ()
        {
            valueChanged = null;
            updateValue = null;
        }

        public override void Draw (in Rect rect)
        {
            UpdateValue (ref currentValue);
            OnDraw (in rect);
        }

        protected void UpdateValue (ref T value)
        {
            if (updateValue == null)
                return;

            value = updateValue.Invoke ();
        }
    }
    
}
