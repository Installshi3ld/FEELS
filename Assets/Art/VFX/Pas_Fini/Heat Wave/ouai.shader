Shader"Custom/HeatWaveShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" { }
        _Intensity ("Intensity", Range (0, 10)) = 1.0
        _Speed ("Speed", Range (0, 10)) = 1.0
    }

CGINCLUDE
#include "UnityCG.cginc"
    ENDCG

    SubShader
    {
        Tags {"Queue" = "Overlay" }
ZWrite Off

Blend SrcAlphaOneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
};

struct v2f
{
    float4 pos : POSITION;
    float4 color : COLOR;
};

float _Intensity;
float _Speed;

v2f vert(appdata v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.color = v.vertex * 0.25 + 0.5; // Change color based on vertex position
    return o;
}

fixed4 frag(v2f i) : COLOR
{
                // Add heat wave effect based on fragment position and time
    fixed4 col = i.color + 0.2 * sin(_Time.y * _Speed + i.pos.x * _Intensity);
    return col;
}
            ENDCG
        }
    }
}