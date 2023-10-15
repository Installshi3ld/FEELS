Shader "Unlit/Add"
{
    Properties
    {
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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

            sampler2D _ObjectsRT;
            sampler2D _CurrentRT;
            float4 _ObjectsRT_ST;
            float _FadeSpeed; // Nouvelle propriété pour contrôler la vitesse de disparition


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _ObjectsRT);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 tex1 = tex2D(_ObjectsRT, i.uv);
                fixed4 tex2 = tex2D(_CurrentRT, i.uv);
    
                float fadeFactor = 1.0 - _FadeSpeed * _Time.y;
                fadeFactor = saturate(fadeFactor); // Clamp the value between 0 and 1
    
                // Combine textures with fade effect
                fixed4 finalColor = tex1 + tex2 * fadeFactor;
    
                // Force finalColor to zero if fadeFactor is less than or equal to 0
                finalColor.rgb *= step(0.0, fadeFactor);
    
                return finalColor;
}
            ENDCG
        }
    }
}
