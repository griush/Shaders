using UnityEngine;
using UnityEditor;

namespace griush
{
    namespace Shaders
    {
        [CustomEditor(typeof(Outline))]
        public class OutlineEditor : Editor
        {
            SerializedProperty _offset;
            SerializedProperty _threshold;
            SerializedProperty _color;

            void OnEnable()
            {
                _offset = serializedObject.FindProperty("_offset");
                _threshold = serializedObject.FindProperty("_threshold");
                _color = serializedObject.FindProperty("_color");
            }

            public override void OnInspectorGUI()
            {
                Outline toon = (Outline)target;

                GUIStyle style = new GUIStyle();
                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = Color.white;

                EditorGUILayout.LabelField("Shader properties", style);
                EditorGUILayout.Slider(_offset, -0.003f, 0.003f);
                EditorGUILayout.Slider(_threshold, 0.05f, 1.5f);
                EditorGUILayout.PropertyField(_color);
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}