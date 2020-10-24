using Unity.Mathematics;
using UnityEngine;

namespace TwistedArk.Development.Console
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

            DrawCloseButton (in closeRect, skin);
        }

        private static void DrawCloseButton (in Rect closeRect, ConsoleSkin skin)
        {
            GuiColors.PushBackgroundColor (Color.red);
            if (GUI.Button (closeRect, "X", skin.GetOrCreateStyle ("Tab Closed", GUI.skin.button)))
                DevelopmentConsole.IsActive = false;
            GuiColors.PopBackgroundColor ();
        }

        private void DrawHeaderTab (in Rect rect, ConsoleSkin skin, ConsoleTab tab, int index)
        {
            if (index == currentTabIndex)
            {
                GuiColors.PushBackgroundColor (DevelopmentConsole.Instance.HeaderColorActive);

                GUI.Box (rect, tab.Name, skin.GetOrCreateStyle ("Tab Open", GUI.skin.box));

                GuiColors.PopBackgroundColor ();
                return;
            }

            GuiColors.PushBackgroundColor (DevelopmentConsole.Instance.HeaderColor);
            if (GUI.Button (rect, tab.Name, skin.GetOrCreateStyle ("Tab Closed", GUI.skin.box)))
            {
                currentTabIndex = index;
                offset = 0;
            }

            GuiColors.PopBackgroundColor ();
        }

        private static float offset;
        private static float contentHeight;
        
        private static void DrawScrollbar (in Rect scrollRect, ConsoleSkin skin)
        {
            GuiColors.PushBackgroundColor (DevelopmentConsole.Instance.ForegroundColor);

            var bottomValue = math.max (0, contentHeight - scrollRect.height);
            
            offset = GUI.VerticalSlider (scrollRect, offset, 0, bottomValue,
                skin.GetOrCreateStyle ("Scrollbar Vertical", GUI.skin.verticalScrollbar),
                skin.GetOrCreateStyle ("Scrollbar Vertical Thumb", GUI.skin.verticalScrollbarThumb)
            );
            
            //Debug.Log ($"{contentHeight} | {scrollRect.height}");
            
            GuiColors.PopBackgroundColor ();
        }

        private void DrawOpenTab (Rect rect, ConsoleSkin skin)
        {
            if (currentTabIndex == DevelopmentConsole.TabCount)
                return;

            float height = 0;
            var r = rect;
            r.width -= 30;
            rect.width += 10;
            GUI.Window (309, rect, _ => DrawContent (r, skin), GUIContent.none);
        }

        private void DrawContent (Rect rect, ConsoleSkin skin)
        {
            rect.y = 0;
            var elementRect = rect;
            
            var tab = DevelopmentConsole.GetTab (currentTabIndex);
            var elementCount = tab.ElementCount;
            
            GuiColors.PushBackgroundColor (DevelopmentConsole.Instance.ForegroundColor);
            elementRect.y -= offset;
            
            for (var i = 0; i < elementCount; i++)
            {
                var element = tab.GetGuiElement (i);
                var elementHeight = element.GetContentHeight ();

                elementRect.height = elementHeight;

                element.OnPreDraw (in elementRect, skin);
                element.OnDraw (in elementRect, skin);

                elementRect.y += elementHeight + DevelopmentConsole.Instance.ElementPadding;
            }

            contentHeight = elementRect.y + offset;

            GuiColors.PopBackgroundColor ();
        }
    }
}