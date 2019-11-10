using System.Collections.Generic;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole
{
    public static class GuiColors
    {
        private static readonly Stack<Color> colorStack = new Stack<Color> ();
        private static Stack<Color> backgroundColorStack = new Stack<Color> ();
         
        public static void PushGuiColor (in Color color)
        {
            colorStack.Push (GUI.color);
            GUI.color = color;
        }

        public static void PopGuiColor ()
        {
            if (colorStack.Count > 0)
                GUI.color = colorStack.Pop ();
        }
        
        public static void PushBackgroundColor (in Color color)
        {
            backgroundColorStack.Push (GUI.backgroundColor);
            GUI.backgroundColor = color;
        }
 
        public static void PopBackgroundColor ()
        {
            if (backgroundColorStack.Count > 0)
                GUI.backgroundColor = backgroundColorStack.Pop ();
        }
    }
}
