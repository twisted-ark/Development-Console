using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public class GuiLabel : GuiElementBase
    {
        public GuiLabel (string label) : base (label)
        {
        }
        
        public override void Draw (in Rect rect)
        {
            GUI.Label (rect, Label);
        }
    }

}