Shader "JakubWegner/UIEngine/Panel" {
    Properties {
        _MainTex ("Main Texture", 2D) = "white" {}
        _RectSize ("Rect Size", Vector) = (0, 0, 0, 0)
        _FillColor ("Fill Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _BorderColor ("Border Color", Color) = (0.0, 0.0, 0.0, 1.0)
        _BorderSize ("Border size", Float) = 10.0
    }

    SubShader { 
        Tags { 
            "RenderType"="UI" 
            "Queue"="Transparent" 
        }
        Pass {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float2 _RectSize;

            float2 _Size;
            float4 _FillColor;
            float _CornerRadius;

            float _EnableBorder;
            float4 _BorderColor;
            float _BorderSize;

            float _EnableShadow;
            float4 _ShadowColor;
            float2 _ShadowOffset;

            float sdBox(float2 p, float2 size) {
                float2 d = abs(p) - size;
                return length(max(d, 0.0)) + min(max(d.x, d.y), 0.0);
            }
            float sdRoundedBox(float2 p, float2 size, float r) {
                return sdBox(p, size - r) - r;
            }

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target {
                float2 p = (i.uv - .5) * _RectSize;
                float2 size = _Size * .5;
                float r = max(0, min(_CornerRadius, min(size.x, size.y)));

                float d = sdRoundedBox(p, size, r);
                float w = fwidth(d);

                float4 color = _FillColor;
                if (_EnableBorder > .5)
                    color = lerp(color, _BorderColor, smoothstep(-_BorderSize - w, -_BorderSize + w, d));
                color.a *= 1.0 - smoothstep(-w, w, d);

                if (_EnableShadow > .5) {
                    float shadowD = sdRoundedBox(p - _ShadowOffset, size, r);
                    float shadowW = fwidth(shadowD);

                    float4 shadowColor = _ShadowColor;
                    shadowColor.a *= 1.0 - smoothstep(-shadowW, shadowW, shadowD);
                    color = lerp(shadowColor, color, color.a);
                }

                return color;
            }

            ENDCG
        }
    }
}
