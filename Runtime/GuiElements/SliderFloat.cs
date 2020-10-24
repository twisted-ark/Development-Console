using System;
using Unity.Mathematics;
using UnityEngine;

namespace TwistedArk.Development.Console
{
    public class SliderFloat : GuiElement<float>
    {
        private float min;
        private float max;
        
        private float snap;
        private float value;
        
        public SliderFloat (string label, float min, float max, float snap, Action<float> valueChanged, Func<float> updateValue) 
            : base (label, valueChanged, updateValue)
        {
            this.min = min;
            this.max = max;
            this.snap = snap < 0.01f ? 0.01f : snap;
        }
        
        public SliderFloat (string label, float min, float max, float start, float snap,  Action<float> valueChanged) 
            : base (label, start, valueChanged)
        {
            this.min = min;
            this.max = max;
            this.snap = snap < 0.01f ? 0.01f : snap;
        }

        public override void OnDraw (in Rect rect, ConsoleSkin skin)
        {
            var contentRect = DrawPrefixLabel (rect, skin);
            var displayValue = Mathf.Round (value / snap) * snap;
            
            value = GUI.HorizontalSlider (contentRect, displayValue, min, max);
            CurrentValue = Mathf.Round (value / snap) * snap;
        }
        
    }

}