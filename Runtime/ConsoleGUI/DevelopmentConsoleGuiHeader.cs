using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public partial class DevelopmentConsoleGui
    {
        private void DrawHeader (in Rect rect)
        {
            var tabCount = console.TabCount;

            var closeRect = new Rect (
                rect.x + rect.width - rect.height,
                rect.y,
                rect.height,
                rect.height);
            
            var tabRect = rect;
            tabRect.width -= closeRect.width + 10;

            tabRect.width /= tabCount;

            for (var i = 0; i < tabCount; i++)
            {
                var tab = console.GetTab (i);
                DrawHeaderTab (in tabRect, tab, i);
                tabRect.x += tabRect.width;
            }
            
            GuiColors.PushGuiColor (Color.red);
            
            if (GUI.Button (closeRect, "X"))
            {
                DevelopmentConsole.IsActive = false;
            }
            
            GuiColors.PopGuiColor ();
        }

        private void DrawHeaderTab (in Rect rect, ConsoleTab tab, int index)
        {
            if (index == currentTab)
            {
                GuiColors.PushBackgroundColor (DevelopmentConsole.Instance.HeaderColorActive);
                var centeredLabel = GUI.skin.GetStyle ("LabelCentered") ?? GUI.skin.label;

                GUI.Box (rect, GUIContent.none);
                GUI.Label (rect, tab.Name, centeredLabel);
                GuiColors.PopBackgroundColor ();
                return;
            }
            
            GuiColors.PushBackgroundColor (DevelopmentConsole.Instance.HeaderColor);
            if (GUI.Button (rect, tab.Name))
            {
                currentTab = index;
            }
            GuiColors.PopBackgroundColor ();
        }
    }

}