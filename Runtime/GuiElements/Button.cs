using System;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole
{
    public class Button : GuiElementBase
    {
        public event Action Pressed;

        public Button (string label, Action pressed)
            : base (label)
        {
            Pressed = pressed;
        }

        public override void OnDraw (in Rect rect, ConsoleSkin skin)
        {
            var lineRect = rect;
            lineRect.height = LineHeight;
            
            if (GUI.Button (lineRect, Label, skin.GetOrCreateStyle ("Button", GUI.skin.toggle)))
            {
                Pressed?.Invoke ();
            }
        }
    }
}