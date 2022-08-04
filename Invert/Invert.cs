using UnityEngine;

namespace griush 
{
    namespace Shaders 
    {
        [ExecuteInEditMode, ImageEffectAllowedInSceneView]
        [RequireComponent(typeof(Camera))]
        [AddComponentMenu("griush Shaders/Invert")]
        public class Invert : MonoBehaviour
        {
            Shader _shader;
            Material _material;

            void OnEnable()
            {
                var shader = _shader ? _shader : Shader.Find("Hidden/griush/InvertShader");
                _material = new Material(shader);
                _material.hideFlags = HideFlags.DontSave;
            }

            private void OnRenderImage(RenderTexture src, RenderTexture dest) 
            {
                Graphics.Blit(src, dest, _material);
            }
        }
    }
}