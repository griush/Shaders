using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace griush
{
    namespace Shaders
    {
        [CustomEditor(typeof(Toon))]
        public class ToonEditor : Editor
        {
            SerializedProperty _offset;
            SerializedProperty _threshold;

            void OnEnable()
            {
                _offset = serializedObject.FindProperty("_offset");
                _threshold = serializedObject.FindProperty("_threshold");
            }

            public override void OnInspectorGUI()
            {
                Toon toon = (Toon)target;

                GUIStyle style = new GUIStyle();
                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = Color.white;

                EditorGUILayout.LabelField("Shader properties", style);
                EditorGUILayout.Slider(_offset, -0.003f, 0.003f);
                EditorGUILayout.Slider(_threshold, 0.05f, 1.5f);
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}