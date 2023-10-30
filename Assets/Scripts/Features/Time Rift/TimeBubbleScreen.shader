Shader "Hidden/TimeBubble"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TimepieceTexture ("Overlay Texture", 2D) = "white" {}
        _ScreenPos ("Screen Position", Vector) = (0, 0, 0, 0)
        _Radius ("Radius", float) = .5
        _EdgeThickness ("Edge Thickness", float) = .01
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

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _TimepieceTexture;
            float4 _ScreenPos;
            float _Radius;
            float _EdgeThickness;

            fixed4 frag(v2f i) : SV_Target
            {
                float4 color;

                float d = distance(i.uv, _ScreenPos.xy);
                float d1 = step(_Radius, d);
                color = d1 * tex2D(_MainTex, i.uv);
                color += (1 - d1) * tex2D(_TimepieceTexture, i.uv);

                return color;
            }
            ENDCG
        }
    }
}