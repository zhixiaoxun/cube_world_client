// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Simplified Additive Particle shader. Differences from regular Additive Particle one:
// - no Tint color
// - no Smooth particle support
// - no AlphaTest
// - no ColorMask

Shader "android/jx3Art/Unity/Mobile/Particles/Additive" {
Properties {
	_MainTex ("Particle Texture(RGB)", 2D) = "white" {}
	_MainTex_alpha ("Particle Texture Alpha(R)", 2D) = "white" {}
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha One
	Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
	
	SubShader {
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			//#pragma multi_compile_particles
			//#pragma multi_compile_fog

			#include "UnityCG.cginc"

			sampler2D _MainTex_alpha;
			sampler2D _MainTex;

			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;				
			};
			
			float4 _MainTex_ST;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{				
				fixed3 diff_rgb = tex2D(_MainTex, i.texcoord);
				fixed diff_a = tex2D(_MainTex_alpha, i.texcoord).r;
				fixed4 col = i.color * fixed4(diff_rgb,diff_a);				
				return col;
			}
			ENDCG 
		}
	}	
}
}