using System;
using TwistedArk.DevelopmentConsole.Runtime;
using UnityEditor;
using UnityEngine;

namespace TwistedArk.Develpomentconsole.Editor
{
    [CustomEditor (typeof (ConsoleSkin))]
    public class ConsoleSkinEditor : UnityEditor.Editor
    {
        private SerializedProperty stylesProp;
        private float fontScaleFactor = 0.2f;

        private void OnEnable ()
        {
            stylesProp = serializedObject.FindProperty ("Styles");
            SortStyles ();
        }

        public override void OnInspectorGUI ()
        {
            if (stylesProp == null)
            {
                OnEnable ();
                return;
            }

            serializedObject.Update ();

            EditorGUILayout.LabelField ("Global Controls", EditorStyles.whiteLargeLabel);
            {
                using (new GUILayout.VerticalScope (GUI.skin.box))
                {
                    DrawFontSizeAdjust ();
                }
            }
            EditorGUILayout.Space ();
            
            EditorGUILayout.LabelField ("Styles", EditorStyles.whiteLargeLabel);
            {
                EditorGUILayout.HelpBox (
                    "The Styles are added to this list after they have been used at least once in code.", 
                    MessageType.None);
                
                var arraySize = stylesProp.arraySize;
                for (var i = 0; i < arraySize; i++)
                {
                    var prop = stylesProp.GetArrayElementAtIndex (i);
                    using (new GUILayout.VerticalScope (GUI.skin.box))
                    {
                        EditorGUILayout.PropertyField (prop, prop.isExpanded);
                    }
                }
            }

            serializedObject.ApplyModifiedProperties ();
        }

        private void DrawFontSizeAdjust ()
        {
            fontScaleFactor = EditorGUILayout.Slider ("Font Scale Factor", fontScaleFactor, 0.01f, 1f);
            
            using (new GUILayout.HorizontalScope ())
            {
                if (GUILayout.Button ("+"))
                {
                    ScaleFontSizes (1 + fontScaleFactor);
                }
            
                if (GUILayout.Button ("-"))
                {
                    ScaleFontSizes (1 - fontScaleFactor);
                }
            }

        }

        private void ScaleFontSizes (float scaler)
        {
            var skin = target as ConsoleSkin;
            if (skin == null)
                return;

            foreach (var style in skin.Styles)
            {
                var size = Mathf.RoundToInt (style.fontSize * scaler);
                if (size == style.fontSize)
                {
                    size += scaler > 1 ? 1 : -1;
                }
                
                style.fontSize = size;
            }
            
            serializedObject.Update ();
        }

        private void SortStyles ()
        {
            var skin = target as ConsoleSkin;
            if (skin == null)
                return;

            skin.Styles.Sort ((s1, s2) => string.Compare (s1.name, s2.name, StringComparison.Ordinal));
        }
    }

}