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

        private GuiSliderFloat redSlider;
        private GuiSliderFloat greenSlider;
        private GuiSliderFloat blueSlider;
        private GuiSliderFloat alphaSlider;
        
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
            var lineRect = new Rect (rect.x, rect.y, rect.width, LineHeightPadded);
            var contentRect = DrawPrefixLabel (lineRect);
            
            PushGuiColor (color);
            // max: #FFFFFF(FF)
            fieldText = GUI.TextField (contentRect, fieldText, useAlpha ? 8 : 6);
            PopGuiColor ();
            
            lineRect.y += LineHeightPadded;
            if (showSliders)
                DrawChannelSliders (lineRect);
            
            if (string.Compare (fieldText, hexColor, StringComparison.Ordinal) == 0)
                return;
            
            if (!ColorUtility.TryParseHtmlString ($"#{fieldText}", out var newColor))
                return;

            var alpha = color.a;
            color = newColor;

            if (!useAlpha)
                color.a = alpha;
            
            hexColor = fieldText;
            
            valueChanged?.Invoke (color);
        }
        
        private void DrawChannelSliders (Rect lineRect)
        {
            lineRect.y += LineHeightPadded;
                
            var newColor = color;
            
            
            if (useAlpha)
            {
                lineRect.y += LineHeightPadded;
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
            return showSliders ? LineHeightPadded * (useAlpha ? 5 : 4) : LineHeightPadded;
        }

        public override void Dispose ()
        {
            hexColor = null;
        }
    }

}