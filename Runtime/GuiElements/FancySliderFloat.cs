using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole
{
    public class FancySliderFloat : GuiElement<float>
    {
        private float min;
        private float max;     
        
        private float snap;
            
        public FancySliderFloat (string label, float min, float max, float snap, 
            Action<float> valueChanged, Func<float> updateValue) 
            : base (label, valueChanged, updateValue)
        {
            this.min = min;
            this.max = max;
            this.snap = snap < 0.01f ? 0.01f : snap;
        }

        public override void OnDraw (in Rect rect, ConsoleSkin skin)
        {
            var lineRect = rect;
            lineRect.height = LineHeight;
            
            var labelRect = lineRect;
            
            GUI.Label (labelRect, Label, skin.GetOrCreateStyle ("Label Centered", GUI.skin.label));

            var buttonRect = rect;
            buttonRect.y += LineHeightPadded;
            buttonRect.height = LineHeight + LineHeightPadded;

            DrawButtons (in buttonRect, skin);

            lineRect.y += LineHeightPadded;
            lineRect.x += 150;
            lineRect.width -= 290;
            
            DrawValue (in lineRect, skin);
            
            lineRect.y += LineHeightPadded;
            DrawSlider (in lineRect, skin);
        }

        private void DrawSlider (in Rect lineRect, ConsoleSkin skin)
        {
            var sliderRect = lineRect;
            
            var newValue = GUI.HorizontalSlider (sliderRect, CurrentValue, min, max,
                skin.GetOrCreateStyle ("Slider", GUI.skin.horizontalSlider),
                skin.GetOrCreateStyle ("Slider Thumb", GUI.skin.horizontalSliderThumb));

            CurrentValue = math.clamp (math.round (newValue / snap) * snap, min, max);
        }

        private void DrawButtons (in Rect lineRect, ConsoleSkin skin)
        {
            var buttonStyle = skin.GetOrCreateStyle ("Button", GUI.skin.button);
            
            var minusRect = lineRect;
            minusRect.width = 120;
            
            var plusRect = lineRect;
            plusRect.x = lineRect.width - 100;
            plusRect.width = 120;
            
            if (GUI.Button (minusRect, "-", buttonStyle))
            {
                CurrentValue = math.clamp (CurrentValue - snap, min, max);
            }
            if (GUI.Button (plusRect, "+", buttonStyle))
            {
                CurrentValue = math.clamp (CurrentValue + snap, min, max);
            }
        }

        private void DrawValue (in Rect lineRect, ConsoleSkin skin)
        {
            var valueRect = lineRect;

            GUI.Label (valueRect, CurrentValue.ToString (),
                skin.GetOrCreateStyle ("Text Centered", GUI.skin.textField));
        }

        public override float GetHeight ()
        {
            return base.GetHeight () * 3f;
        }
    }

}
