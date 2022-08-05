using UnityEngine;

namespace griush
{
    namespace Water
    {
        [ExecuteInEditMode]
        [RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
        public class WaterManager : MonoBehaviour
        {
            public static WaterManager _instance;

            [Header("Mesh")]
            [SerializeField]
            Vector2 _size = Vector2.one;

            [SerializeField]
            [Range(2, 250)]
            int _resolution = 3;
            [SerializeField]
            Material waterMaterial;

            [Header("Physics")]
            [SerializeField]
            float _waterDensity = 1f;

            [Header("Waves")]
            [SerializeField]
            float _speed = 0.1f;
            [SerializeField]
            Vector2 _offset;
            [SerializeField]
            int _octaves = 7;
            [SerializeField]
            [Range(1f, 5f)]
            float _lacunarity = 2f;
            [SerializeField]
            [Range(0.0f, 1.0f)]
            float _gain = 0.5f;
            [SerializeField]
            [Range(-2.0f, 2.0f)]
            float _value = 0.0f;
            [SerializeField]
            [Range(0.0f, 5.0f)]
            float _amplitude = 1.5f;
            [SerializeField]
            [Range(0.0f, 6.0f)]
            float _frequency = 2.0f;
            [SerializeField]
            [Range(0.1f, 5.0f)]
            float _power = 1.0f;
            [SerializeField]
            float _scale = 1.0f;

            PlaneMesh planeMesh;
            MeshFilter meshFilter;
            MeshRenderer meshRenderer;

            private void Awake()
            {
                if (_instance == null)
                    _instance = this;
                else
                    Destroy(this);
            }

            public void Generate()
            {
                GenerateMesh();
            }

            // Update is called once per frame
            void Update()
            {
                // float _octaves, _lacunarity, _gain, _value, _amplitude, _frequency, _offsetX, _offsetY, _power, _scale
                _offset.x += Time.deltaTime * _speed;
                _offset.y += Time.deltaTime * _speed;
                waterMaterial.SetFloat("_octaves", _octaves);
                waterMaterial.SetFloat("_lacunarity", _lacunarity);
                waterMaterial.SetFloat("_gain", _gain);
                waterMaterial.SetFloat("_value", _value);
                waterMaterial.SetFloat("_amplitude", _amplitude);
                waterMaterial.SetFloat("_frequency", _frequency);
                waterMaterial.SetFloat("_offsetX", _offset.x);
                waterMaterial.SetFloat("_offsetY", _offset.y);
                waterMaterial.SetFloat("_power", _power);
                waterMaterial.SetFloat("_scale", _scale);
            }

            public float GetWaveHeight(float x, float z)
            {
                return 0f;
            }

            void GenerateMesh()
            {
                planeMesh = new PlaneMesh(_size, _resolution);

                meshFilter = GetComponent<MeshFilter>();
                meshRenderer = GetComponent<MeshRenderer>();

                Mesh mesh = new Mesh();
                mesh.name = "Plane";

                mesh.SetVertices(planeMesh.Vertices);
                mesh.SetTriangles(planeMesh.Triangles, 0, true);
                mesh.uv = planeMesh.UVs;
                mesh.RecalculateNormals();

                meshFilter.sharedMesh = mesh;
                meshRenderer.sharedMaterial = waterMaterial;
            }

            public float Density
            {
                get
                {
                    return _waterDensity;
                }
            }
        }
    }
}