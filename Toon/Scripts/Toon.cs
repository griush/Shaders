using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace griush {
    namespace Shaders {
        [ExecuteInEditMode, ImageEffectAllowedInSceneView]
        [RequireComponent(typeof(Camera))]
        [AddComponentMenu("griush Effects/Toon")]
        public class Toon : MonoBehaviour
        {
            Shader _shader;
            Material _material;

            [SerializeField]
            float _offset;
            [SerializeField]
            float _threshold;

            void OnEnable()
            {
                var shader = _shader ? _shader : Shader.Find("Hidden/griush/ToonShader");
                _material = new Material(shader);
                _material.hideFlags = HideFlags.DontSave;
            }

            private void OnRenderImage(RenderTexture src, RenderTexture dest) 
            {
                _material.SetFloat("_Offset", _offset);
                _material.SetFloat("_Threshold", _threshold);

                Graphics.Blit(src, dest, _material);
            }
        }
    }
}