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


        public override void OnDraw (in Rect rect, ConsoleSkin skin)
        {
            var lineRect = new Rect (rect.x, rect.y, rect.width, LineHeightPadded);
            var contentRect = DrawPrefixLabel (lineRect);
            
            GuiColors.PushGuiColor (CurrentValue);
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

            if (!useAlpha)
                newColor.a = CurrentValue.a;
            
            hexColor = fieldText;
            CurrentValue = newColor;
        }
        
        private void DrawChannelSliders (Rect lineRect)
        {
            lineRect.y += LineHeightPadded;
                
            var newColor = CurrentValue;
            
            
            if (useAlpha)
            {
                lineRect.y += LineHeightPadded;
                newColor.a = GUI.HorizontalSlider (lineRect, CurrentValue.a, 0, 1);
            }

            if (CurrentValue != newColor)
            {
                CurrentValue = newColor;
                fieldText = ColorUtility.ToHtmlStringRGBA (CurrentValue);
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