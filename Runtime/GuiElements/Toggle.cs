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

        public override void OnDraw (in Rect rect, ConsoleSkin skin)
        {
            var contentRect = DrawPrefixLabel (rect);
            CurrentValue = GUI.Toggle (contentRect, CurrentValue, GUIContent.none, skin.GetOrCreateStyle ("Toggle", GUI.skin.toggle));
        }
    }
}