using UnityEngine;

namespace griush 
{
    namespace Shaders 
    {
        [ExecuteInEditMode, ImageEffectAllowedInSceneView]
        [RequireComponent(typeof(Camera))]
        [AddComponentMenu("griush Shaders/Outline")]
        public class Outline : MonoBehaviour
        {
            Shader _shader;
            Material _material;

            [SerializeField]
            float _offset = 0.002f;
            [SerializeField]
            float _threshold = 0.1f;
            [SerializeField]
            Color _color = Color.black;

            void OnEnable()
            {
                var shader = _shader ? _shader : Shader.Find("Hidden/griush/OutlineShader");
                _material = new Material(shader);
                _material.hideFlags = HideFlags.DontSave;
            }

            private void OnRenderImage(RenderTexture src, RenderTexture dest) 
            {
                _material.SetFloat("_Offset", _offset);
                _material.SetFloat("_Threshold", _threshold);
                _material.SetColor("_Color", _color);

                Graphics.Blit(src, dest, _material);
            }
        }
    }
}