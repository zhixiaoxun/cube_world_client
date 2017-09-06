// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "android/jx3Art/Effect/TextureDistortionBlend_mask" {
	Properties {
		_TintColor ("Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("mainTexture (RGB)", 2D) = "white" {}
		_MainTex_alpha ("mainTexture (R)", 2D) = "white" {}
		_MoveX  ("Move X", float) = 0
		_MoveY  ("Move Y", float) = 0
		_NoiseTex ("Distortion Texture (RG)", 2D) = "white" {}
		_MaskTex ("Distortion Mask Texture", 2D) = "white" {}
		_HeatTime  ("Distortion Move Dirition", float) = 0
		_ForceX  ("Power X", range (0,1)) = 0.1
		_ForceY  ("Power Y", range (0,1)) = 0.1
		_Brightness("Brightness",float) = 2.0
		[Enum(UnityEngine.Rendering.BlendMode)] _SourceBlend("Source Blend Mode",Float) = 5		
		[Enum(UnityEngine.Rendering.BlendMode)] _DestBlend("Dest Blend Mode",Float) = 10
	}

	Category 
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		Blend [_SourceBlend] [_DestBlend]
		//Blend SrcAlpha OneMinusSrcAlpha
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

				struct appdata_t {
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					half2 texcoord: TEXCOORD0;
				};

				struct v2f {
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					half2 uvmain : TEXCOORD1;
					half2 uvnoise : TEXCOORD2;
					half2 uvMask : TEXCOORD3;
				};

				fixed4 _TintColor;
				fixed _ForceX;
				fixed _ForceY;
				fixed _HeatTime;
				half4 _MainTex_ST;
				half4 _NoiseTex_ST;
				half4 _MaskTex_ST;
				float _Brightness;
				float _Type;
				float _MoveX;
		        float _MoveY;
				
				sampler2D _MainTex_alpha;
				sampler2D _NoiseTex;
				sampler2D _MainTex;
				sampler2D _MaskTex;

				v2f vert (appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.color = v.color;
					o.uvmain = TRANSFORM_TEX( v.texcoord, _MainTex );
					o.uvnoise = TRANSFORM_TEX( v.texcoord, _NoiseTex);
					o.uvMask = TRANSFORM_TEX( v.texcoord, _MaskTex);//v.texcoord;
					return o;
				}

				fixed4 frag( v2f i ) : COLOR
				{
					fixed4 offsetColor1 = tex2D(_NoiseTex, i.uvnoise + fmod(_Time.xz*_HeatTime,1));
				    fixed4 offsetColor2 = tex2D(_NoiseTex, i.uvnoise + fmod(_Time.yx*_HeatTime,1));

				    fixed4 mask = tex2D(_MaskTex, i.uvMask);
				    
				    half2 oldUV = i.uvmain;
				     
					i.uvmain.x += ((offsetColor1.r + offsetColor2.r) - 1) * _ForceX;
					i.uvmain.y += ((offsetColor1.r + offsetColor2.r) - 1) * _ForceY;
					
					half2 newUV = i.uvmain;
					half2 resUV = lerp(oldUV,newUV,mask.xy);
					resUV.x += fmod(_MoveX*_Time.y,1);
				    resUV.y += fmod(_MoveY*_Time.y,1);
					
					fixed3 diff_rgb = tex2D(_MainTex, resUV);
					fixed  diff_a 	= tex2D(_MainTex_alpha, resUV).r;
					fixed4 diff = fixed4(diff_rgb,diff_a);
					
					fixed4 resColor = i.color * _TintColor * diff* _Brightness;
					resColor.a  *= mask.r;

					return resColor;
				}
				ENDCG
			}
		}
	}
}
