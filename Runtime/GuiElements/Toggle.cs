using System;
using UnityEngine;

namespace TwistedArk.Development.Console
{
    public class Toggle : GuiElement<bool>
    {
        private readonly GUIContent onContent = new GUIContent ("On");
        private readonly GUIContent offContent = new GUIContent ("Off");
        
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
            var contentRect = DrawPrefixLabel (rect, skin);
            CurrentValue = GUI.Toggle (
                contentRect, CurrentValue, CurrentValue ? onContent : offContent, 
                skin.GetOrCreateStyle ("Toggle", GUI.skin.toggle));
        }
    }
}