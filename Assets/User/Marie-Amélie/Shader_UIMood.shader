// CustomUIShader.shader

Shader "Unlit/Shader_UIMood" {
    Properties{
        _Color("Color", Color) = (1, 1, 1, 1)
        // Add other properties if needed
    }
        SubShader{
            Tags { "Queue" = "Transparent" }
            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"
                struct appdata_t {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };
                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };
                half4 _Color;
                v2f vert(appdata_t v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }
                half4 frag(v2f i) : SV_Target {
                    return _Color;
                }
                ENDCG
            }
    }
}