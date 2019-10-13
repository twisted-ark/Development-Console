using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public class LabelField : GuiElementBase
    {
        public LabelField (string label) : base (label)
        {
        }

        public override void Draw (in Rect rect)
        {
            OnDraw (in rect);
        }

        protected override void OnDraw (in Rect rect)
        {
            var LabelRect = rect;
            LabelRect.height /= 2f;
            LabelRect.y += LabelRect.height;
            
            GUI.Label (LabelRect, Label);
        }

        public override float GetHeight ()
        {
            return base.GetHeight () * 2f;
        }
    }

}