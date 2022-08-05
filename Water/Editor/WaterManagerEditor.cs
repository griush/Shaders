using UnityEngine;
using UnityEditor;

namespace griush
{
    namespace Water
    {
        [CustomEditor(typeof(WaterManager))]
        public class WaterManagerEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                WaterManager manager = (WaterManager)target;

                if (GUILayout.Button("Generate"))
                    manager.Generate();
            }
        }
    }
}