using UnityEngine;

namespace TwistedArk.Development.Console
{
    public class DropDown : GuiElementBase
    {
        private Vector2 scrollViewVector = Vector2.zero;
        private static string[] list = {"Drop_Down_Menu"};

        int indexNumber;

        public DropDown (string label, string[] items, int drawOrder = 0) : base (label, drawOrder)
        {
            list = items;
        }

        private bool show;

        public override void OnDraw (in Rect rect, ConsoleSkin skin)
        {
            var contentRect = DrawPrefixLabel (rect, skin);
            if (GUI.Button (new Rect ((contentRect.x), contentRect.y, contentRect.width, 80), ""))
            {
                show = !show;
            }
            
            var dropDownRect = contentRect;
            dropDownRect.height = 500;

            if (show)
            {
                var positionRect = new Rect (
                    (dropDownRect.x), (dropDownRect.y + 25), dropDownRect.width,
                    dropDownRect.height);
                
                var height = Mathf.Max (dropDownRect.height, list.Length * 80);

                using (new GUI.ScrollViewScope (
                    positionRect, scrollViewVector,
                    new Rect (0, 0, dropDownRect.width, height)))
                {
                    GUI.Box (new Rect (0, 0, dropDownRect.width, height), "");

                    for (var index = 0; index < list.Length; index++)
                    {
                        if (GUI.Button (new Rect (0, (index * 80), dropDownRect.height, 80), ""))
                        {
                            show = false;
                            indexNumber = index;
                        }

                        GUI.Label (new Rect (5, (index * 80), dropDownRect.height, 80), list[index]);
                    }
                }
            }
            else
            {
                GUI.Label (new Rect ((dropDownRect.x), dropDownRect.y, 300, 25), list[indexNumber]);
            }
        }
    }
}