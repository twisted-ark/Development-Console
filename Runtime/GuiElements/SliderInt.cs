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
        
        public SliderInt (string label, int min, int max, int start, Action<int> valueChanged) 
            : base (label, start, valueChanged)
        {
            this.min = min;
            this.max = max;
        }

        public override void OnDraw (in Rect rect, ConsoleSkin skin)
        {
            var contentRect = DrawPrefixLabel (rect);
            var oldValue = CurrentValue;
            
            var newValue = GUI.HorizontalSlider (contentRect, oldValue, min, max);
            newValue = math.round (newValue);

            if ((int) newValue == oldValue) 
                return;
            
            CurrentValue = (int) newValue;
        }
    }

}