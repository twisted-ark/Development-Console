using System;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public class Toggle : GuiElement<bool>
    {
        public Toggle (string label, Action<bool> valueChanged, Func<bool> updateValue)
            : base (label, valueChanged, updateValue)
        {
        }
        
        public Toggle (string label, bool startValue, Action<bool> valueChanged)
            : base (label, startValue, valueChanged)
        {
        }

        protected override void OnDraw (in Rect rect)
        {
            var contentRect = DrawPrefixLabel (rect);

            var oldValue = currentValue;
            currentValue = GUI.Toggle (contentRect, currentValue, GUIContent.none);

            if (currentValue != oldValue)
            {
                valueChanged?.Invoke (currentValue);
            }
        }
    }
}