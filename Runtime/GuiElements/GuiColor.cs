using System;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public class GuiColor : GuiElement<Color>
    {
        private Color color;
        private string hexColor;

        private string fieldText;
        
        public GuiColor (string label, Action<Color> valueChanged, in Color color) : base (label, valueChanged)
        {
            this.color = color;
            hexColor = $"#{ColorUtility.ToHtmlStringRGBA (color)}";
            fieldText = hexColor;
        }

        public GuiColor (string label, Action<Color> valueChanged, in string hexColor) : base (label, valueChanged)
        {
            if (ColorUtility.TryParseHtmlString (hexColor, out color))
            {
                this.hexColor = $"#{hexColor}";
                fieldText = this.hexColor;
            }
            else
            {
                color = Color.white;
                fieldText = "#FFFFFFFF";
                this.hexColor = fieldText;
            }
        }
        
        public override void Draw (in Rect rect)
        {
            var contentRect = DrawPrefixLabel (rect);
            
            PushGuiColor (color);
            // max: FFFFFFFF
            fieldText = GUI.TextField (contentRect, fieldText, 9);
            PopGuiColor ();

            if (string.Compare (fieldText, hexColor, StringComparison.Ordinal) == 0)
                return;
            
            if (!ColorUtility.TryParseHtmlString (fieldText, out var newColor))
                return;

            color = newColor;
            hexColor = fieldText;
            
            valueChanged?.Invoke (color);
        }

        public override float GetHeight ()
        {
            return 16;
        }

        public override void Dispose ()
        {
            hexColor = null;
        }
    }

}