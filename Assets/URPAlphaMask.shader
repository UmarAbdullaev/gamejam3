Shader "Custom/URPAlphaMask"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _AlphaTex ("Alpha Texture", 2D) = "white" {}
        _Cutoff ("Alpha Cutoff", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="AlphaTest" }
        LOD 100

        Pass
        {
            Name "AlphaMaskedPass"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            TEXTURE2D(_AlphaTex);
            SAMPLER(sampler_AlphaTex);

            float _Cutoff;

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                half4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
                half alpha = SAMPLE_TEXTURE2D(_AlphaTex, sampler_AlphaTex, IN.uv).r;
                clip(alpha - _Cutoff);
                return color;
            }
            ENDHLSL
        }
    }
}
