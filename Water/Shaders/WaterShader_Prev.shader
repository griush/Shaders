Shader "griush/WaterShader_"
{
    Properties
    {
        _Color("Main Color", Color) = (0, 0, 1, 1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "bump" {}
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM

         // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _NormalMap;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NormalMap;
        };

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)


        float _octaves, _lacunarity, _gain, _value, _amplitude, _frequency, _offsetX, _offsetY, _power, _scale;

        float fbm(float2 p)
        {
            p = p * _scale + float2(_offsetX, _offsetY);
            for (int i = 0; i < _octaves; i++)
            {
                float2 i = floor(p * _frequency);
                float2 f = frac(p * _frequency);
                float2 t = f * f * f * (f * (f * 6.0 - 15.0) + 10.0);
                float2 a = i + float2(0.0, 0.0);
                float2 b = i + float2(1.0, 0.0);
                float2 c = i + float2(0.0, 1.0);
                float2 d = i + float2(1.0, 1.0);
                a = -1.0 + 2.0 * frac(sin(float2(dot(a, float2(127.1, 311.7)), dot(a, float2(269.5, 183.3)))) * 43758.5453123);
                b = -1.0 + 2.0 * frac(sin(float2(dot(b, float2(127.1, 311.7)), dot(b, float2(269.5, 183.3)))) * 43758.5453123);
                c = -1.0 + 2.0 * frac(sin(float2(dot(c, float2(127.1, 311.7)), dot(c, float2(269.5, 183.3)))) * 43758.5453123);
                d = -1.0 + 2.0 * frac(sin(float2(dot(d, float2(127.1, 311.7)), dot(d, float2(269.5, 183.3)))) * 43758.5453123);
                float A = dot(a, f - float2(0.0, 0.0));
                float B = dot(b, f - float2(1.0, 0.0));
                float C = dot(c, f - float2(0.0, 1.0));
                float D = dot(d, f - float2(1.0, 1.0));
                float noise = (lerp(lerp(A, B, t.x), lerp(C, D, t.x), t.y));
                _value += _amplitude * noise;
                _frequency *= _lacunarity;
                _amplitude *= _gain;
            }
            _value = clamp(_value, -1.0, 1.0);
            return pow(_value * 0.5 + 0.5, _power);
        }
        
        void vert(inout appdata_full v, out Input o) {

            //moves the object horizontally, in this case we add the sin of _Time
            float height = fbm(v.vertex.xz);
            v.vertex.y = height;

            UNITY_INITIALIZE_OUTPUT(Input, o);

        }

        float4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
            // Metallic and smoothness come from slider variables
            o.Metallic = 0;
            o.Smoothness = 0.5;
            o.Alpha = _Color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
