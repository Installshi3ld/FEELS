Shader "Custom/DisableZWrite"
{
    SubShader{
        Tags{
              "RenderType" = "Transparent"
        }
        
        Pass {
             ZWrite Off
            ZTest Always
        }
    }
}

   
