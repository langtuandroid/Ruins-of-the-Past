Shader "Unlit/Time Bubble Unlit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("Normal Map", 2D) = "bump" {}
        _Luminance ("Luminance", Range(0.0, 1.0)) = 0.4
        _Distortion ("Distortion Factor", Range(0.0, 1.0)) = 0.1
        _ScrollSpeedX ("Scroll Speed X", Float) = 0.0
        _ScrollSpeedY ("Scroll Speed Y", Float) = 0.75
        _TwirlSpeed ("Twirl Speed", Float) = 1.0
        _TwirlStrength ("Twirl Strength", Range(0.0, 100.0)) = 0.5
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "ForceNoShadowCasting" = "True"
            "Queue" = "Transparent"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float2 twirl(float2 uv, float2 center, float strength, float2 offset, float rotation)
            {
                float2 delta = uv - center;
                float angle = strength * length(delta) + rotation;
                float x = cos(angle) * delta.x + sin(angle) * delta.y;
                float y = sin(angle) * delta.x + cos(angle) * delta.y;

                return float2(x + center.x + offset.x, y + center.y + offset.y);
            }

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD0;
                float3x3 TBN : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float _Luminance;
            float _Distortion;
            float _ScrollSpeedX;
            float _ScrollSpeedY;
            float _TwirlSpeed;
            float _TwirlStrength;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);

                o.TBN = float3x3(
                    // world space normal
                    normalize(mul(UNITY_MATRIX_M, v.normal)).xyz,
                    // world space tangent
                    normalize(mul(UNITY_MATRIX_M, v.tangent)).xyz,
                    // world space bitangent
                    normalize(mul(UNITY_MATRIX_M, cross(v.normal, v.tangent))).xyz
                );

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 color;
                float2 uv = i.screenPos.xy / i.screenPos.w;

                float2 scrolling_uv = uv;
                scrolling_uv.x += _Time.x * _ScrollSpeedX;
                scrolling_uv.y += _Time.x * _ScrollSpeedY;

                // Calculate normal from texture
                half3 norm = UnpackNormal(tex2D(_NoiseTex, scrolling_uv));
                norm = normalize(norm * 2.0 - 1.0);
                norm = normalize(mul(i.TBN, norm));

                float3 view_space_normal = mul(UNITY_MATRIX_V, norm);
                float2 refraction_vector = view_space_normal.xy * view_space_normal.z;
                //float2 twirl_uv = twirl(i.uv, float2(0.5, 0.5),
                //                        distance(i.uv, float2(0.5, 0.5)), float2(0.0, 0.0), _Time.x * _TwirlSpeed);

                // Apply luminance (gray-scale)
                //float d = pow(distance(i.uv, float2(0.5, 0.5)), 2);
                //float4 tex = tex2D(_MainTex, screen_space_uv + d * _Distortion * refraction_vector);
                float4 tex = tex2D(_MainTex, uv + _Distortion * refraction_vector);
                color = float4(lerp(tex.rgb, Luminance(tex), _Luminance), tex.a);
                //color = float4(d, d, d, d);

                //return float4(twirl_uv, 1.0, 1.0);
                //color = float4(uv, 0.0, 0.0);
                return color;
            }
            ENDCG
        }
    }
}