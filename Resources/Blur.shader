Shader "Unity UI Extension/Blur"
{
    Properties
    {
        _Radius ("Radius", Range(0,0.01)) = 0.001
        [KeywordEnum(Low,Medium,High,Ultra)] _QUALITY ("Quality", Float) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _QUALITY_LOW _QUALITY_MEDIUM _QUALITY_HIGH _QUALITY_ULTRA

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 screenPos : TEXCOORD1;
                float4 color: COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD1;
                float4 color: COLOR;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Berechne die Screen Position
                o.screenPos = ComputeScreenPos(o.vertex);

                o.color = v.color;

                return o;
            }

            const float _MediumRange[] = { 0.5 };
            const float _HighRange[] = { 0.3333333, 0.6666666 };
            const float _UltraRange[] = { 0.25, 0.5, 0.75 };
            
            float _Radius;
            // Scene Color Texture
            UNITY_DECLARE_SCREENSPACE_TEXTURE(_CameraOpaqueTexture);

            fixed4 frag(v2f i) : SV_Target
            {                
                // Zentraler Sample
                fixed4 col = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraOpaqueTexture, i.screenPos);

                #if _QUALITY_MEDIUM
                const int rangeCount = 1;
                float range[rangeCount] = _MediumRange;
                #elif _QUALITY_HIGH
                const int rangeCount = 2;
                float range[rangeCount] = _HighRange;
                #elif _QUALITY_ULTRA
                const int rangeCount = 3;
                float range[rangeCount] = _UltraRange;
                #endif

                // Kreuz-Pattern Sampling
                fixed4 blur = col;
                blur += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraOpaqueTexture, i.screenPos + float2(_Radius, 0));
                blur += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraOpaqueTexture, i.screenPos + float2(-_Radius, 0));
                blur += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraOpaqueTexture, i.screenPos + float2(0, _Radius));
                blur += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraOpaqueTexture, i.screenPos + float2(0, _Radius));

                #ifndef _QUALITY_LOW
                for (int j = 0; j < rangeCount; j++)
                {
                    blur += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraOpaqueTexture, i.screenPos + float2(_Radius * range[j], 0));
                    blur += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraOpaqueTexture, i.screenPos + float2(-_Radius * range[j], 0));
                    blur += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraOpaqueTexture, i.screenPos + float2(0, _Radius * range[j]));
                    blur += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraOpaqueTexture, i.screenPos + float2(0, _Radius * range[j]));
                }                
                #endif
                
                // Diagonales Sampling für bessere Qualität
                blur += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraOpaqueTexture, i.screenPos + float2(_Radius, _Radius) * 0.707);
                blur += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraOpaqueTexture, i.screenPos + float2(-_Radius, _Radius) * 0.707);
                blur += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraOpaqueTexture, i.screenPos + float2(_Radius, -_Radius) * 0.707);
                blur += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraOpaqueTexture, i.screenPos + float2(-_Radius, -_Radius) * 0.707);

                #ifndef _QUALITY_LOW
                for (int j = 0; j < rangeCount; j++)
                {
                    blur += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraOpaqueTexture, i.screenPos + float2(_Radius * range[j], _Radius * range[j]) * 0.707);
                    blur += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraOpaqueTexture, i.screenPos + float2(-_Radius * range[j], _Radius * range[j]) * 0.707);
                    blur += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraOpaqueTexture, i.screenPos + float2(_Radius * range[j], -_Radius * range[j]) * 0.707);
                    blur += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraOpaqueTexture, i.screenPos + float2(-_Radius * range[j], -_Radius * range[j]) * 0.707);
                }
                #endif
                
                #ifdef _QUALITY_LOW
                float4 blurColor = blur * 0.1111111111111111; // 1/9 für 9 Samples
                #elif _QUALITY_MEDIUM
                float4 blurColor = blur * 0.0588235294117647; // 1/17 für 17 Samples
                #elif _QUALITY_HIGH
                float4 blurColor = blur * 0.04; // 1/25 für 25 Samples
                #elif _QUALITY_ULTRA
                float4 blurColor = blur * 0.0303030303030303; // 1/33 für 33 Samples
                #endif
                                
                return col * (1 - i.color.a) + blurColor * i.color.a;
            }
            ENDCG
        }
    }
}