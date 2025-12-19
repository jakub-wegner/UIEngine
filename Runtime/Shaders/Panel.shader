Shader "JakubWegner/UIEngine/Panel"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "Queue"="Transparent"
        }

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv          : TEXCOORD0;
            };

            float2 _RectSize;

            float2 _Size;
            float4 _FillColor;
            float  _CornerRadius;

            float  _EnableBorder;
            float4 _BorderColor;
            float  _BorderSize;

            float  _EnableShadow;
            float4 _ShadowColor;
            float2 _ShadowOffset;

            float sdBox(float2 p, float2 size)
            {
                float2 d = abs(p) - size;
                return length(max(d, 0.0)) + min(max(d.x, d.y), 0.0);
            }

            float sdRoundedBox(float2 p, float2 size, float r)
            {
                return sdBox(p, size - r) - r;
            }

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv = v.uv;
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                float2 p = (i.uv - 0.5) * _RectSize;
                float2 size = _Size * 0.5;
                float r = clamp(_CornerRadius, 0, min(size.x, size.y));

                float d = sdRoundedBox(p, size, r);
                float w = fwidth(d);

                float4 color = _FillColor;

                if (_EnableBorder > 0.5)
                    color = lerp(color, _BorderColor,
                        smoothstep(-_BorderSize - w, -_BorderSize + w, d));

                color.a *= 1.0 - smoothstep(-w, w, d);

                if (_EnableShadow > 0.5)
                {
                    float shadowD = sdRoundedBox(p - _ShadowOffset, size, r);
                    float shadowW = fwidth(shadowD);

                    float4 shadowColor = _ShadowColor;
                    shadowColor.a *= 1.0 - smoothstep(-shadowW, shadowW, shadowD);
                    color = lerp(shadowColor, color, color.a);
                }

                return color;
            }
            ENDHLSL
        }
    }
}
