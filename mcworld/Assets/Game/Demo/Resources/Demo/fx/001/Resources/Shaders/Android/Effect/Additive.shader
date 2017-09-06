// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//checked by wanghaiting
Shader "android/jx3Art/Effect/Additive" {
	Properties {
	    _Brightness("Brightness",float) = 1.0
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture(RGB)", 2D) = "white" {}
		_MainTex_alpha ("Alpha (R)", 2D) = "white" {}		
	}

	Category {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend SrcAlpha One
		Cull Off 
		Lighting Off 
		ZWrite Off
		Fog {Mode Off} 
		
		SubShader {
			Pass {
			
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				sampler2D _MainTex_alpha;
				sampler2D _MainTex;
				fixed4 _TintColor;
				fixed _Brightness;
				half4 _MainTex_ST;
				
				struct appdata_t {
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					fixed2 texcoord : TEXCOORD0;
				};

				struct v2f {
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					fixed2 texcoord : TEXCOORD0;	
				};

				v2f vert (appdata_t v)
				{
					v2f o;
					o.color = v.color;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
					return o;
				}

				fixed4 frag (v2f i) : COLOR
				{
					fixed3 diff = tex2D(_MainTex, i.texcoord);
					fixed  diff_a =tex2D(_MainTex_alpha, i.texcoord).r;

					return _Brightness *i.color* _TintColor * fixed4(diff.rgb,diff_a);
				}
				ENDCG 
			}
		}	
	}
}
