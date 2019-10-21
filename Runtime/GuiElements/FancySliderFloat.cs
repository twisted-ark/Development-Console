using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public class FancySliderFloat : GuiElement<float>
    {
        private float min;
        private float max;
            
        private float snap;
        private float value;
            
        public FancySliderFloat (string label, float min, float max, float snap, Action<float> valueChanged, Func<float> updateValue) 
            : base (label, valueChanged, updateValue)
        {
            this.min = min;
            this.max = max;
            this.snap = snap < 0.01f ? 0.01f : snap;
        }
            
        public FancySliderFloat (string label, float min, float max, float start, float snap,  Action<float> valueChanged) 
            : base (label, start, valueChanged)
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
            
            var sliderRect = lineRect;
            sliderRect.y += LineHeightPadded;
            
            var controlRect = lineRect;
            sliderRect.y += LineHeightPadded;

            GUI.Label (labelRect, Label);
            
            var displayValue = Mathf.Round (value / snap) * snap;
            value = GUI.HorizontalSlider (sliderRect, displayValue, min, max);
            CurrentValue = Mathf.Round (value / snap) * snap;
        }

        public override float GetHeight ()
        {
            return base.GetHeight () * 3f;
        }
    }

}
