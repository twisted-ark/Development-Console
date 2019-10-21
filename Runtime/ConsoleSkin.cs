using System;
using System.Collections.Generic;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    [CreateAssetMenu (menuName = "Twisted Ark/Development Console/Skin")]
    public sealed class ConsoleSkin : ScriptableObject
    {
        [Header ("Styles")] 
        [SerializeField] internal List<GUIStyle> Styles;

        private Dictionary<string, GUIStyle> styleMap = new Dictionary<string, GUIStyle> ();

        public GUIStyle GetOrCreateStyle (string styleName, GUIStyle template)
        {
            if (styleMap.TryGetValue (styleName, out var style))
                return style;
            
            styleMap.Add (styleName, template);
            
            var newStyle = new GUIStyle (template) {name = styleName};
            Styles.Add (newStyle);
            
            return newStyle;
        }

        private void Reset ()
        {
            Styles = new List<GUIStyle>
            {
                new GUIStyle { name = "Label"},
                new GUIStyle { name = "Slider"},
                new GUIStyle { name = "Slider Thumb"},
                new GUIStyle { name = "Button"},
                new GUIStyle { name = "Open Tab"},
                new GUIStyle { name = "Closed Tab"},
                new GUIStyle { name = "Group Header"},
                new GUIStyle { name = "Text"},
                new GUIStyle { name = "Toggle"},
            };
        }
    }

}