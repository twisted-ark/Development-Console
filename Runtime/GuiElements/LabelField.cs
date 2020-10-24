using UnityEngine;

namespace TwistedArk.Development.Console.Runtime
{
    public class LabelField : GuiElementBase
    {
        public LabelField (string label) : base (label)
        {
        }

        public override void OnDraw (in Rect rect, ConsoleSkin skin)
        {
            var labelRect = rect;
            labelRect.height /= 2f;
            labelRect.y += labelRect.height;
            
            GUI.Label (labelRect, Label, skin.GetOrCreateStyle ("Label", GUI.skin.label));
        }

        public override float GetContentHeight ()
        {
            return base.GetContentHeight () * 2f;
        }
    }

}