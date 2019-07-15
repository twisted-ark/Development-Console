using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public abstract class GuiElement
    {
        public abstract int Height { get; }
        
        public string Label { get; }
        
        protected GuiElement (string label)
        {
            Label = label;
        }
        
        public abstract void Draw (in Rect rect);
        
    }

    public class GuiLabel : GuiElement
    {
        public override int Height => 16;

        public GuiLabel (string label) : base (label)
        {
        }
        
        public override void Draw (in Rect rect)
        {
            GUI.Label (rect, Label);
        }
    }
}
