using System;
using Unity.Mathematics;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public class SliderInt : GuiElement<int>
    {
        private int min;
        private int max;
        
        public SliderInt (string label, int min, int max, Action<int> valueChanged, Func<int> updateValue) 
            : base (label, valueChanged, updateValue)
        {
            this.min = min;
            this.max = max;
        }

        protected override void OnDraw (in Rect rect)
        {
            var contentRect = DrawPrefixLabel (rect);
            var oldValue = currentValue;
            
            var newValue = GUI.HorizontalSlider (contentRect, oldValue, min, max);
            newValue = math.round (newValue);

            if ((int) newValue == oldValue) 
                return;
            
            currentValue = (int) newValue;
            valueChanged?.Invoke ((int) (currentValue + float.Epsilon));
        }
    }

}