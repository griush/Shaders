using UnityEditor;

namespace griush
{
    namespace Shaders
    {
        [CustomEditor(typeof(Invert))]
        public class InvertEditor : Editor
        {

            void OnEnable()
            {
            }

            public override void OnInspectorGUI()
            {
                Invert invert = (Invert)target;
            }
        }
    }
}