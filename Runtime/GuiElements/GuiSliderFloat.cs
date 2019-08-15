using System;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public class GuiSliderFloat : GuiElement<float>
    {
        private float current;
        private float min;
        private float max;

        
        public GuiSliderFloat (string label, Action<float> valueChanged, float start, float min, float max) 
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
            
            var oldValue = current;
            current = GUI.HorizontalSlider (contentRect, current, min, max);

            if (current != oldValue)
            {
                valueChanged?.Invoke (current);
            }
        }

        public override void Dispose ()
        {
            valueChanged = null;
        }
    }

}