using System;
using System.Collections.Generic;
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
            if (!DevelopmentConsole.IsActive)
                return;

            defaultGuiSkin = GUI.skin;
            GUI.skin = console.ConsoleSkin;

            PushBackgroundColor (DevelopmentConsole.Instance.BackgroundColor);

            var screenWidth = Screen.width;
            var screenHeight = Screen.height;
            
            var headerHeight = DevelopmentConsole.Instance.HeaderHeight;
            var anchorsMin = DevelopmentConsole.Instance.anchorsMin;
            var anchorsMax = DevelopmentConsole.Instance.anchorsMax;
            var contentOffset = DevelopmentConsole.Instance.HeaderSpacing + headerHeight;

            var paddingX = DevelopmentConsole.Instance.PaddingX;
            var paddingY = DevelopmentConsole.Instance.PaddingY;
            var totalPaddingX = paddingX.x + paddingX.y;
            var totalPaddingY = paddingY.x + paddingY.y;

            var startLeft = screenWidth * anchorsMin.x;
            var startUp = screenHeight * anchorsMin.y;

            var panelWidth = screenWidth * anchorsMax.x - startLeft;
            var panelHeight = screenHeight * anchorsMax.y - startUp;
            
            var fullRect = new Rect (startLeft, startUp, 
                panelWidth, panelHeight);
            
            var headerRect = new Rect(
                startLeft + paddingX.x, 
                startUp + paddingY.y, 
                panelWidth - totalPaddingX, 
                headerHeight);

            var contentRect = new Rect (
                startLeft + paddingX.x, 
                startUp + contentOffset + paddingY.y, 
                panelWidth - totalPaddingX, 
                panelHeight - contentOffset - totalPaddingY);


            GUI.Box (fullRect, GUIContent.none);
            
            currentTab = math.min (currentTab, console.TabCount);

            DrawHeader (in headerRect);
            DrawOpenTab (in contentRect);

            PopBackgroundColor ();
        }

        private void DrawHeader (in Rect rect)
        {
            var tabCount = console.TabCount;

            var closeRect = new Rect (
                rect.x + rect.width - rect.height,
                rect.y,
                rect.height,
                rect.height);
            
            var tabRect = rect;
            tabRect.width -= closeRect.width;

            tabRect.width /= tabCount;

            for (var i = 0; i < tabCount; i++)
            {
                var tab = console.GetTab (i);
                DrawHeaderTab (in tabRect, tab, i);
                tabRect.x += tabRect.width;
            }
            
            if (GUI.Button (closeRect, "X"))
            {
                DevelopmentConsole.IsActive = false;
            }
        }

        private void DrawHeaderTab (in Rect rect, ConsoleTab tab, int index)
        {
            if (index == currentTab)
            {
                PushBackgroundColor (DevelopmentConsole.Instance.HeaderColorActive);
                var centeredLabel = GUI.skin.GetStyle ("LabelCentered") ?? GUI.skin.label;

                GUI.Box (rect, GUIContent.none);
                GUI.Label (rect, tab.Name, centeredLabel);
                PopBackgroundColor ();
                return;
            }
            
            PushBackgroundColor (DevelopmentConsole.Instance.HeaderColor);
            if (GUI.Button (rect, tab.Name))
            {
                currentTab = index;
            }
            PopBackgroundColor ();
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

        private Stack<Color> backgroundColorStack = new Stack<Color> ();
        
        private void PushBackgroundColor (in Color color)
        {
            backgroundColorStack.Push (GUI.backgroundColor);
            GUI.backgroundColor = color;
        }

        private void PopBackgroundColor ()
        {
            if (backgroundColorStack.Count == 0)
                return;
            
            GUI.backgroundColor = backgroundColorStack.Pop ();
        }
    }

}