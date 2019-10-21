using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public partial class DevelopmentConsoleGui
    {
        private void DrawHeader (in Rect rect, ConsoleSkin skin)
        {
            var tabCount = DevelopmentConsole.TabCount;

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
                var tab = DevelopmentConsole.GetTab (i);
                DrawHeaderTab (in tabRect, skin, tab, i);
                tabRect.x += tabRect.width;
            }
            
            GuiColors.PushGuiColor (Color.red);
            
            if (GUI.Button (closeRect, "X", skin.GetOrCreateStyle ("Tab Closed", GUI.skin.button)))
            {
                DevelopmentConsole.IsActive = false;
            }
            
            GuiColors.PopGuiColor ();
        }

        private void DrawHeaderTab (in Rect rect, ConsoleSkin skin, ConsoleTab tab, int index)
        {
            if (index == currentTab)
            {
                GuiColors.PushBackgroundColor (DevelopmentConsole.Instance.HeaderColorActive);

                GUI.Box (rect, tab.Name, skin.GetOrCreateStyle ("Tab Open", GUI.skin.box));
                
                GuiColors.PopBackgroundColor ();
                return;
            }
            
            GuiColors.PushBackgroundColor (DevelopmentConsole.Instance.HeaderColor);
            if (GUI.Button (rect, tab.Name, skin.GetOrCreateStyle ("Tab Closed", GUI.skin.box)))
            {
                currentTab = index;
            }
            GuiColors.PopBackgroundColor ();
        }
        
        private void DrawOpenTab (in Rect rect, ConsoleSkin skin)
        {
            if (currentTab == DevelopmentConsole.TabCount)
                return;
            
            var elementRect = rect;

            var tab = DevelopmentConsole.GetTab (currentTab);
            var elementCount = tab.ElementCount;
            
            for (var i = 0; i < elementCount; i++)
            {
                var element = tab.GetGuiElement (i);
                var height = element.GetHeight ();
                
                elementRect.height = height;
                element.OnPreDraw (in elementRect, skin);
                element.OnDraw (in elementRect, skin);
                elementRect.y += height;
            }
        }
    }

}