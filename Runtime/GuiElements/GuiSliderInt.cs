using System;
using Unity.Mathematics;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public class GuiSliderInt : GuiElement<int>
    {
        private float current;
        private int min;
        private int max;

        
        public GuiSliderInt (string label, Action<int> valueChanged, int start, int min, int max) 
            : base (label, valueChanged)
        {
            this.current = start;
            this.min = min;
            this.max = max;

            this.valueChanged = valueChanged;
        }

        public override void Draw (in Rect rect)
        {
            var contentRect = DrawPrefixLabel (rect);
            var oldValue = math.round (current);
            
            var newValue = GUI.HorizontalSlider (contentRect, oldValue, min, max);
            newValue = math.round (newValue);

            if ((int) newValue == (int) oldValue) 
                return;
            
            current = newValue;
            valueChanged?.Invoke ((int) (current + float.Epsilon));
        }

        public override float GetHeight ()
        {
            return 16;
        }

        public override void Dispose ()
        {
            valueChanged = null;
        }
    }

}