using System;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public class SliderFloat : GuiElement<float>
    {
        private float min;
        private float max;
        
        public SliderFloat (string label, float min, float max, Action<float> valueChanged, Func<float> updateValue) 
            : base (label, valueChanged, updateValue)
        {
            this.min = min;
            this.max = max;
        }
        
        public SliderFloat (string label, float min, float max, float start, Action<float> valueChanged) 
            : base (label, start, valueChanged)
        {
            this.min = min;
            this.max = max;
        }

        protected override void OnDraw (in Rect rect)
        {
            var contentRect = DrawPrefixLabel (rect);
            
            var oldValue = currentValue;
            currentValue = GUI.HorizontalSlider (contentRect, currentValue, min, max);

            if (currentValue != oldValue)
            {
                valueChanged?.Invoke (currentValue);
            }
        }

        public override void Dispose ()
        {
            valueChanged = null;
        }
    }

}