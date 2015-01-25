// Unlit alpha-cutout shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Custom/lifebar" {
     Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _Amount ("Life Amount", float) = 0
      _Color ("Tint", Color) = (1,1,1,1)

    }
    SubShader {
      	Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
      CGPROGRAM
      #pragma surface surf Lambert
      struct Input {
          float2 uv_MainTex;
          float3 worldPos;
      };
      sampler2D _MainTex;
      sampler2D _BumpMap;
      fixed4 _Color;
      float _Amount;
      void surf (Input IN, inout SurfaceOutput o) {
          clip (_Amount - IN.worldPos.x);
          half4 c = tex2D (_MainTex, IN.uv_MainTex);
          o.Albedo = c.rgb + _Color;
          o.Alpha = c.a;
      }
      ENDCG
    } 
    Fallback "Diffuse"
}