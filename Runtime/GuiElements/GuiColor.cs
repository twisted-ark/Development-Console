using System;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public class GuiColor : GuiElement<Color>
    {
        private Color color;
        private string hexColor;

        private string fieldText;
        private bool useAlpha;
        private bool showSliders;
        
        public GuiColor (string label, Action<Color> valueChanged, Color color, 
            bool useAlpha = false, bool showSliders = false) 
        : base (label, valueChanged)
        {
            if (!useAlpha)
                color.a = 1;
            
            this.useAlpha = useAlpha;
            this.showSliders = showSliders;
            
            this.color = color;
            hexColor = $"{ColorUtility.ToHtmlStringRGBA (color)}";
            fieldText = hexColor;
        }

        public override void Draw (in Rect rect)
        {
            var lineRect = new Rect (rect.x, rect.y, rect.width, 16);
            var contentRect = DrawPrefixLabel (lineRect);
            
            PushGuiColor (color);
            // max: #FFFFFF(FF)
            fieldText = GUI.TextField (contentRect, fieldText, useAlpha ? 9 : 7);
            PopGuiColor ();

            if (!fieldText.StartsWith ("#"))
                fieldText = $"#{fieldText}";

            lineRect.y += 16;
            if (showSliders)
                DrawSliders (lineRect);
            
            if (string.Compare (fieldText, hexColor, StringComparison.Ordinal) == 0)
                return;
            
            if (!ColorUtility.TryParseHtmlString (fieldText, out var newColor))
                return;

            color = newColor;
            hexColor = fieldText;
            
            valueChanged?.Invoke (color);
        }
        
        private void DrawSliders (Rect lineRect)
        {
            var newColor = color;
            newColor.r = GUI.HorizontalSlider (lineRect, color.r, 0, 1);

            lineRect.y += 16;
            newColor.g = GUI.HorizontalSlider (lineRect, color.g, 0, 1);

            lineRect.y += 16;
            newColor.b = GUI.HorizontalSlider (lineRect, color.b, 0, 1);

            if (useAlpha)
            {
                lineRect.y += 16;
                newColor.a = GUI.HorizontalSlider (lineRect, color.a, 0, 1);
            }

            if (color != newColor)
            {
                color = newColor;
                fieldText = ColorUtility.ToHtmlStringRGBA (color);
                hexColor = fieldText;
            }
        }
        
        public override float GetHeight ()
        {
            return showSliders ? 16 * (useAlpha ? 5 : 4) : 16;
        }

        public override void Dispose ()
        {
            hexColor = null;
        }
    }

}