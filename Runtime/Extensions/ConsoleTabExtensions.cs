﻿using System;
using TwistedArk.DevelopmentConsole.Runtime;

namespace TwistedArk.DevelopmentConsole
{
    public static class GuiElementGroupExtensions
    {
        public static void CreateFancySliderFloat (this IGuiElementGroup group,
            string label, float min, float max, float snap, Action<float> valueChange, Func<float> updateValue)
        {
            var slider = new FancySliderFloat (label, min, max, snap, valueChange, updateValue);
            group.Add (slider);
        }
        
        public static void CreateFancySliderInt (this IGuiElementGroup group,
            string label, int min, int max, int snap, Action<int> valueChange, Func<int> updateValue)
        {
            var slider = new FancySliderInt (label, min, max, snap, valueChange, updateValue);
            group.Add (slider);
        }

        public static void CreateToggle (this IGuiElementGroup group,
            string label, Action<bool> valueChanged, Func<bool> updateValue)
        {
            var toggle = new Toggle (label, valueChanged, updateValue);
            group.Add (toggle);
        }

        public static void CreateButton (this IGuiElementGroup group, string label, Action pressed)
        {
            var button = new Button (label, pressed);
            group.Add (button);
        }

        public static void CreateLabel (this IGuiElementGroup group, string label)
        {
            var l = new LabelField (label);
            group.Add (l);
        }
        
        public static IGuiElementGroup CreateGroup (this IGuiElementGroup group, string label)
        {
            var elementGroup = new GuiGroup (label);
            group.Add (elementGroup);
            
            return elementGroup;
        }
    }

}