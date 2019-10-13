using System;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public class ColorField : GuiElement<Color>
    {
        private string hexColor;

        private string fieldText;
        private bool useAlpha;
        private bool showSliders;

        private SliderFloat redSlider;
        private SliderFloat greenSlider;
        private SliderFloat blueSlider;
        private SliderFloat alphaSlider;
        
        public ColorField (string label, Action<Color> valueChanged, Func<Color> updateColor, 
            bool useAlpha = false, bool showSliders = false) 
        : base (label, valueChanged, updateColor)
        {
            var color = updateColor.Invoke ();
            
            if (!useAlpha)
                color.a = 1;
            
            this.useAlpha = useAlpha;
            this.showSliders = showSliders;
            
            hexColor = $"{ColorUtility.ToHtmlStringRGBA (color)}";
            fieldText = hexColor;
        }


        protected override void OnDraw (in Rect rect)
        {
            var lineRect = new Rect (rect.x, rect.y, rect.width, LineHeightPadded);
            var contentRect = DrawPrefixLabel (lineRect);
            
            UpdateValue (ref currentValue);
            
            GuiColors.PushGuiColor (currentValue);
            // max: #FFFFFF(FF)
            fieldText = GUI.TextField (contentRect, fieldText, useAlpha ? 8 : 6);
            GuiColors.PopGuiColor ();
            
            lineRect.y += LineHeightPadded;
            if (showSliders)
                DrawChannelSliders (lineRect);
            
            if (string.Compare (fieldText, hexColor, StringComparison.Ordinal) == 0)
                return;
            
            if (!ColorUtility.TryParseHtmlString ($"#{fieldText}", out var newColor))
                return;

            var alpha = currentValue.a;
            currentValue = newColor;

            if (!useAlpha)
                currentValue.a = alpha;
            
            hexColor = fieldText;
            
            valueChanged?.Invoke (currentValue);
        }
        
        private void DrawChannelSliders (Rect lineRect)
        {
            lineRect.y += LineHeightPadded;
                
            var newColor = currentValue;
            
            
            if (useAlpha)
            {
                lineRect.y += LineHeightPadded;
                newColor.a = GUI.HorizontalSlider (lineRect, currentValue.a, 0, 1);
            }

            if (currentValue != newColor)
            {
                currentValue = newColor;
                fieldText = ColorUtility.ToHtmlStringRGBA (currentValue);
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