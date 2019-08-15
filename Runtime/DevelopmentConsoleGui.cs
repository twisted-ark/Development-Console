using System;
using Unity.Mathematics;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    [AddComponentMenu (null)]
    public class DevelopmentConsoleGui : MonoBehaviour
    {
        internal DevelopmentConsole console;
        
        private int currentTab;
        private GUISkin defaultGuiSkin;
        
        private void Update ()
        {
            if (Input.GetKeyDown (console.ToggleGuiKey))
                DevelopmentConsole.IsActive = !DevelopmentConsole.IsActive;
        }

        private void OnGUI ()
        {
            defaultGuiSkin = GUI.skin;
            GUI.skin = console.ConsoleSkin;

            if (!DevelopmentConsole.IsActive)
                return;
            
            var screenWith = Screen.width;
            var screenHeight = Screen.height;
            
            var fullRect = new Rect (0, 0, screenWith, screenHeight);
            var headerRect = new Rect(0, 0, screenWith, 60);
            var contentRect = new Rect (0, 60, screenWith, screenHeight - 60);
            
            GUI.Box (fullRect, GUIContent.none);
            
            currentTab = math.min (currentTab, console.TabCount);

            DrawHeader (in headerRect);
            DrawOpenTab (in contentRect);

            GUI.skin = defaultGuiSkin;
        }

        private void DrawHeader (in Rect rect)
        {
            var tabCount = console.TabCount;

            var tabRect = new Rect(
                0,
                rect.y,
                rect.width - rect.height,
                rect.height);

            tabRect.width /= tabCount;
            
            var closeRect = new Rect (
                rect.width - rect.height,
                rect.y,
                rect.height,
                rect.height);

            for (var i = 0; i < tabCount; i++)
            {
                var tab = console.GetTab (i);
                DrawTabHeader (in tabRect, tab, i);
                tabRect.x += tabRect.width;
            }
            
            if (GUI.Button (closeRect, "X"))
            {
                DevelopmentConsole.IsActive = false;
            }
        }

        private void DrawTabHeader (in Rect rect, ConsoleTab tab, int index)
        {
            if (index == currentTab)
            {
                var centeredLabel = GUI.skin.GetStyle ("LabelCentered") ?? GUI.skin.label;

                GUI.Box (rect, GUIContent.none);
                GUI.Label (rect, tab.Name, centeredLabel);
                return;
            }
            
            if (GUI.Button (rect, tab.Name))
            {
                currentTab = index;
            }
        }

        private void DrawOpenTab (in Rect rect)
        {
            if (currentTab == console.TabCount)
                return;
            
            var elementRect = rect;

            var tab = console.GetTab (currentTab);
            var elementCount = tab.ElementCount;
            
            for (var i = 0; i < elementCount; i++)
            {
                var element = tab.GetGuiElement (i);
                var height = element.GetHeight ();
                elementRect.height = height;
                
                element.Draw (in elementRect);
                elementRect.y += height;
            }
        }
    }

}