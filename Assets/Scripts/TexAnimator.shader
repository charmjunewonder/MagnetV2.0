Shader "Custom/TexAnimator" {
	Properties {
		_MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_Intensity("intensity of the texture", Range(0,0.99)) = 0

		_TexHeight("Sheet Height", float) = 1.0

		_CellHeight ("Cell Height", float) = 0.0

		_Speed ("Speed", Range(0,10)) = 2
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
		Blend One OneMinusSrcAlpha
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"


			sampler2D _MainTex;
			fixed _Intensity;
			fixed _Speed;
			float _TexWidth;
			float _TexHeight;

			float _CellHeight;
			float _CellWidth;

			fixed4 _Color;

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
			};

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif
				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				float cellUVPerY = _CellHeight / _TexHeight;
				float coloumAmount = _TexHeight / _CellHeight;
				float xVal = _Time * _Speed;

				float yVal = _Intensity / cellUVPerY;
				yVal = floor(yVal);

				float2 spriteUV = IN.texcoord;
				float xValue = spriteUV.x;
				float yValue = spriteUV.y;

				xValue += xVal;
				xValue = fmod(xValue, 1.0);
				yValue *= cellUVPerY;
				yValue += yVal * cellUVPerY;

				spriteUV = float2(xValue,yValue);
				fixed4 c = tex2D(_MainTex, spriteUV) * IN.color;
				c.rgb *= c.a;
				return c;
			}

			ENDCG
		}
	} 
	FallBack "Diffuse"
}
