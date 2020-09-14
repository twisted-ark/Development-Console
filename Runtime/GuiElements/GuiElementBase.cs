using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole
{
    public abstract class GuiElementBase : IDisposable
    {
        protected static float LineHeight => DevelopmentConsole.Instance.LineHeight;

        protected static float LineHeightPadded => DevelopmentConsole.Instance.LineHeight
                                                   + DevelopmentConsole.Instance.LinePadding;

        protected static float LinePadding => DevelopmentConsole.Instance.LinePadding;

        public string Label { get; private set; }
        public int DrawOrder { get; private set; }

        protected GuiElementBase (string label, int drawOrder = 0)
        {
            Label = label;
            DrawOrder = drawOrder;
        }

        public virtual void OnPreDraw (in Rect rect, ConsoleSkin skin) { }

        public virtual void OnDraw (in Rect rect, ConsoleSkin skin) { }

        public virtual float GetContentHeight () => LineHeightPadded;

        public float GetElementHeight () => LineHeight + DevelopmentConsole.Instance.ElementPadding;

        public virtual void Dispose ()
        {
        }

        protected Rect DrawPrefixLabel (in Rect rect, ConsoleSkin skin)
        {
            var labelRect = rect;
            labelRect.width = math.max (rect.width / 2.5f, 100f);

            var contentRect = rect;
            contentRect.x += labelRect.width;
            contentRect.width -= labelRect.width;
            
            var labelStyle = skin.GetOrCreateStyle ("Label Prefix", GUI.skin.label);
            GUI.Label (labelRect, Label, labelStyle);

            return contentRect;
        }
    }

    public abstract class GuiElement<T> : GuiElementBase where T : IEquatable<T>
    {
        public event Action<T> ValueChanged;
        protected Func<T> updateValue;

        private T currentValue;

        internal T CurrentValue
        {
            get => currentValue;
            set
            {
                if (currentValue.Equals (value))
                    return;
                
                currentValue = value;
                ValueChanged?.Invoke (currentValue);
            }
        }

        protected GuiElement (string label, Action<T> valueChanged, Func<T> updateValue) : base (label)
        {
            ValueChanged = valueChanged;
            this.updateValue = updateValue;

            UpdateValue (ref currentValue);
        }

        protected GuiElement (string label, T startValue, Action<T> valueChanged) : base (label)
        {
            ValueChanged = valueChanged;
            this.currentValue = startValue;
        }

        public override void Dispose ()
        {
            ValueChanged = null;
            updateValue = null;
        }

        public override void OnPreDraw (in Rect rect, ConsoleSkin skin)
        {
            UpdateValue (ref currentValue);
        }

        protected void UpdateValue (ref T value)
        {
            if (updateValue == null)
                return;

            value = updateValue.Invoke ();
        }
    }
}