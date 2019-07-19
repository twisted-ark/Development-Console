﻿using System;
using Unity.Mathematics;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    [AddComponentMenu (null)]
    public class DevelopmentConsoleGui : MonoBehaviour
    {
        internal DevelopmentConsole console;
        
        private int currentTab;
        
        private void Update ()
        {
            if (Input.GetKeyDown (console.ToggleGuiKey))
                DevelopmentConsole.IsActive = !DevelopmentConsole.IsActive;
        }

        private void OnGUI ()
        {
            Styles.SetUpStyles ();
            
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
        }

        private void DrawHeader (in Rect rect)
        {
            var tabCount = console.TabCount;
            var tabWidth = math.min (
                rect.x / math.min (1, tabCount), 
                console.MaxTabWidth);
            
            var tabRect = new Rect(
                tabWidth,
                rect.y,
                rect.width - rect.height,
                rect.height);
            
            var closeRect = new Rect (
                rect.width - rect.height,
                rect.y,
                rect.height,
                rect.height);

            for (var i = 0; i < tabCount; i++)
            {
                var tab = console.GetTab (i);
                DrawTabHeader (in tabRect, tab, i);
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
                GUI.Label (rect, tab.Name, Styles.labelStyle);
                return;
            }
            
            if (GUI.Button (rect, tab.Name, Styles.buttonStyle))
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