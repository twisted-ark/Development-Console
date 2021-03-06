﻿using System;
using TwistedArk.Development.Console.Runtime;

namespace TwistedArk.Development.Console
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

        public static void CreateDropDown (this IGuiElementGroup group, string label, string[] options)
        {
            var dropDown = new DropDown (label, options);
            group.Add (dropDown);
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

        public static IGuiElementGroup GetOrCreateGroup (this IGuiElementGroup group, string label)
        {
            if (group.TryGetNamedElement<GuiGroup> (label, out var elementGroup))
                return elementGroup;
            return group.CreateGroup (label);
        }
    }

}