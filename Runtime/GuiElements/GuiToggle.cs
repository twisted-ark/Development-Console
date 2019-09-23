using System;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public class GuiToggle : GuiElement<bool>
    {
        private bool currentValue;
        
        public GuiToggle (string label, bool startValue, Action<bool> valueChanged) : 
            base (label, valueChanged)
        {
            currentValue = startValue;
        }

        public override void Draw (in Rect rect)
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
