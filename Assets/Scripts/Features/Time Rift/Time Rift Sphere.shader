Shader "Unlit/Time Rift Sphere"
{
    Properties
    {
        _MainTex ("Hidden scene render texture", 2D) = "white" {}
        _ScreenSpaceScale ("Screen space scale", float) = 2
        _DistortionExponent ("Distortion exponent", range(1, 20)) = 4
        _SpherePercentage ("Sphere Percentage", range(0, 1)) = 0.25
        _OuterGlowMultiplier ("Outer glow multiplier", float) = 1
        _OuterGlowExponent ("Outer glow exponent", float) = 4
        _OuterGlowTint ("Outer glow tint", color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "UniversalMaterialType" = "Unlit"
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }

        LOD 100

        Cull Back
        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        ZTest Always
        ZWrite Off

        Pass
        {
            Tags
            {
                "LightMode" = "SRPDefaultUnlit"
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 4.5
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            void raycast(
                const float3 ray_origin,
                const float3 ray_direction,
                const float3 sphere_origin,
                const float sphere_size,
                out float hit,
                out float3 hit_position,
                out float3 hit_normal
            )
            {
                hit_position = float3(0.0, 0.0, 0.0);
                hit_normal = float3(0.0, 0.0, 0.0);

                const float3 L = sphere_origin - ray_origin;
                const float tca = dot(L, -ray_direction);

                if (tca < 0)
                {
                    hit = 0.0f;
                    return;
                }

                const float d2 = dot(L, L) - tca * tca;
                const float radius2 = sphere_size * sphere_size;

                if (d2 > radius2)
                {
                    hit = 0.0f;
                    return;
                }

                const float thc = sqrt(radius2 - d2);
                const float t = tca - thc;

                hit = 1.0f;
                hit_position = ray_origin - ray_direction * t;
                hit_normal = normalize(hit_position - sphere_origin);
            }

            float fresnel(const float3 normal, const float3 view_direction)
            {
                return saturate(dot(normalize(normal), normalize(view_direction)));
            }

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD0;
                float2 uv : TEXCOORD1;
                float4 screen_pos : TEXCOORD2;
                float4 world_pos : TEXCOORD3;
                float3 view_dir : TEXCOORD4;
                UNITY_FOG_COORDS(1)
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                o.uv = v.uv;
                o.screen_pos = ComputeScreenPos(o.vertex);
                o.world_pos = mul(unity_ObjectToWorld, v.vertex);
                o.view_dir = normalize(UnityWorldSpaceViewDir(o.world_pos));
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            sampler2D _MainTex;
            sampler2D _CameraOpaqueTexture;
            float _ScreenSpaceScale;
            float _DistortionExponent;
            float _SpherePercentage;
            float _OuterGlowMultiplier;
            float _OuterGlowExponent;
            float4 _OuterGlowTint;

            fixed4 frag(const v2f i) : SV_Target
            {
                const float3 sphere_origin = UNITY_MATRIX_M._m03_m13_m23;
                const float3 scale = float3(
                    length(unity_ObjectToWorld._m00_m10_m20),
                    length(unity_ObjectToWorld._m01_m11_m21),
                    length(unity_ObjectToWorld._m02_m12_m22)
                );

                float4 sphere_screen = ComputeScreenPos(mul(UNITY_MATRIX_VP, float4(sphere_origin, 1.0)));
                float2 screen_position = sphere_screen.xy / abs(sphere_screen.w);
                float2 screen_position_ratio = float2(
                    screen_position.x,
                    screen_position.y * (_ScreenParams.y / _ScreenParams.x)
                );

                const float2 screen_uv = i.screen_pos.xy / abs(i.screen_pos.w);

                const float2 vector_to_center = normalize(screen_position_ratio - screen_uv) * 0.2;

                float hit;
                float3 hit_position;
                float3 hit_normal;
                raycast(
                    _WorldSpaceCameraPos,
                    i.view_dir,
                    sphere_origin,
                    _SpherePercentage * scale.x,
                    hit,
                    hit_position,
                    hit_normal
                );

                const float3 inner_fresnel = pow(1 - fresnel(hit_normal, i.view_dir), _OuterGlowExponent) *
                    _OuterGlowMultiplier * _OuterGlowTint;

                float3 mask = acos(saturate(cos(
                    dot(
                        UnityObjectToWorldDir(i.normal),
                        normalize(_WorldSpaceCameraPos - sphere_origin)
                    )
                ))) / 1.57;
                mask = pow(mask / (1 - _SpherePercentage), _DistortionExponent);

                const float2 distorted_uv = screen_uv + vector_to_center * pow(
                    mask / (1 - _SpherePercentage), _DistortionExponent);

                fixed4 color = lerp(
                    tex2D(_CameraOpaqueTexture, distorted_uv),
                    half4(tex2D(_MainTex, float2(screen_uv.x, 1 - screen_uv.y)) + inner_fresnel, 1),
                    //half4(half3(0, 0, 0) + inner_fresnel, 1.0),
                    hit
                );

                //color = float4(distorted_uv, 1, 1);
                //color = float4(mask.xyz, mask.x);
                UNITY_APPLY_FOG(i.fogCoord, col);
                return color;
            }
            ENDCG
        }
    }
}