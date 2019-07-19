using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public static class Styles
    {
        internal static bool guiSetup;
        public static GUIStyle buttonStyle;
        public static GUIStyle labelStyle;
        
        internal static void SetUpStyles ()
        {
            if (guiSetup)
                return;
            
            var fontSizeMult = DevelopmentConsole.Instance.FontSize;
            
            buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = (int) (buttonStyle.fontSize * fontSizeMult);
            
            labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontSize = (int) (labelStyle.fontSize * fontSizeMult);

            guiSetup = true;
        }

    }

}