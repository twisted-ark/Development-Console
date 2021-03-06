﻿using System;
using Unity.Mathematics;
using UnityEngine;

namespace TwistedArk.Development.Console
{
    public class FancySliderInt : GuiElement<int>
    {
        private int min;
        private int max;
        private int snap;

        public FancySliderInt (string label, int min, int max, int snap,
            Action<int> valueChanged, Func<int> updateValue)
            : base (label, valueChanged, updateValue)
        {
            this.min = min;
            this.max = max;
            this.snap = snap <= 0 ? 1 : snap;
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

            CurrentValue = (int) math.clamp (math.round (newValue / snap) * snap, min, max);
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

        public override float GetContentHeight ()
        {
            return base.GetContentHeight () * 3f;
        }
    }
}