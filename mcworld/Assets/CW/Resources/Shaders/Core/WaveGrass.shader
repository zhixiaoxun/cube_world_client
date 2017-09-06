Shader "Custom/WaveGrass" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Cutoff ("Cutoff", float) = 0.5
		_WaveAndDistance ("Wave and distance", Vector) = (12, 3.6, 1, 1)
		_WavingTint ("Fade Color", Color) = (.7,.6,.5, 0)
		_CameraPosition ("Camera Position", Vector) = (0,0,0,0)
	}
	SubShader {
		Tags { "RenderType"="Transparent+200" "Queue" = "Transparent" "IgnoreProjector"="True"  }
		LOD 200
		Cull Off
		ColorMask RGB
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard vertex:MyWavingGrassVert fullforwardshadows
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			fixed4 color : COLOR;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		fixed _Cutoff;

		fixed4 _WavingTint;
		float4 _WaveAndDistance;    // wind speed, wave size, wind amount, max sqr distance
		float4 _CameraPosition;     // .xyz = camera position, .w = 1 / (max sqr distance)

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void FastSinCos (float4 val, out float4 s, out float4 c) {
			val = val * 6.408849 - 3.1415927;
			// powers for taylor series
			float4 r5 = val * val;                  // wavevec ^ 2
			float4 r6 = r5 * r5;                        // wavevec ^ 4;
			float4 r7 = r6 * r5;                        // wavevec ^ 6;
			float4 r8 = r6 * r5;                        // wavevec ^ 8;

			float4 r1 = r5 * val;                   // wavevec ^ 3
			float4 r2 = r1 * r5;                        // wavevec ^ 5;
			float4 r3 = r2 * r5;                        // wavevec ^ 7;


			//Vectors for taylor's series expansion of sin and cos
			float4 sin7 = {1, -0.16161616, 0.0083333, -0.00019841};
			float4 cos8  = {-0.5, 0.041666666, -0.0013888889, 0.000024801587};

			// sin
			s =  val + r1 * sin7.y + r2 * sin7.z + r3 * sin7.w;

			// cos
			c = 1 + r5 * cos8.x + r6 * cos8.y + r7 * cos8.z + r8 * cos8.w;
		}

		fixed4 MyTerrainWaveGrass (inout float4 vertex, float waveAmount, fixed4 color)
		{
			float4 _waveXSize = float4(0.012, 0.02, 0.06, 0.024) * _WaveAndDistance.y;
			float4 _waveZSize = float4 (0.006, .02, 0.02, 0.05) * _WaveAndDistance.y;
			float4 waveSpeed = float4 (0.3, .5, .4, 1.2) * 4;

			float4 _waveXmove = float4(0.012, 0.02, -0.06, 0.048) * 2;
			float4 _waveZmove = float4 (0.006, .02, -0.02, 0.1);

			float4 waves;
			waves = vertex.x * _waveXSize;
			waves += vertex.z * _waveZSize;

			// Add in time to model them over time
			waves += _WaveAndDistance.x * waveSpeed;

			float4 s, c;
			waves = frac (waves);
			FastSinCos (waves, s,c);

			s = s * s;

			s = s * s;

			float lighting = dot (s, normalize (float4 (1,1,.4,.2))) * .7;

			s = s * waveAmount;

			float3 waveMove = float3 (0,0,0);
			waveMove.x = dot (s, _waveXmove);
			waveMove.z = dot (s, _waveZmove);

			vertex.xz -= waveMove.xz * _WaveAndDistance.z;

			// apply color animation

			// fix for dx11/etc warning
			fixed3 waveColor = lerp (fixed3(0.5,0.5,0.5), _WavingTint.rgb, fixed3(lighting,lighting,lighting));

			// Fade the grass out before detail distance.
			// Saturate because Radeon HD drivers on OS X 10.4.10 don't saturate vertex colors properly.
			float3 offset = vertex.xyz - _CameraPosition.xyz;
			color.a = saturate (2 * (_WaveAndDistance.w - dot (offset, offset)) * _CameraPosition.w);

			return fixed4(2 * waveColor * color.rgb, color.a);
		}


		void MyWavingGrassVert (inout appdata_full v) {
			// MeshGrass v.color.a: 1 on top vertices, 0 on bottom vertices
			// _WaveAndDistance.z == 0 for MeshLit
			float waveAmount = v.color.r * _WaveAndDistance.z;
			v.color = MyTerrainWaveGrass (v.vertex, waveAmount, v.color);
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
			clip (o.Alpha - _Cutoff);
			o.Alpha *= IN.color.a;
		}
		ENDCG
	}
	SubShader {
		Tags {
			"Queue" = "Transparent+200"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
		}
		Cull Off
		LOD 200
		ColorMask RGB
	   
		Pass {
			Tags { "LightMode" = "Vertex" }
			Material {
				Diffuse (1,1,1,1)
				Ambient (1,1,1,1)
			}
			Lighting On
			ColorMaterial AmbientAndDiffuse
			AlphaTest Greater [_Cutoff]
			SetTexture [_MainTex] { combine texture * primary DOUBLE, texture }
		}
		Pass {
			Tags { "LightMode" = "VertexLMRGBM" }
			AlphaTest Greater [_Cutoff]
			BindChannels {
				Bind "Vertex", vertex
				Bind "texcoord1", texcoord0 // lightmap uses 2nd uv
				Bind "texcoord", texcoord1 // main uses 1st uv
			}
			SetTexture [unity_Lightmap] {
				matrix [unity_LightmapMatrix]
				combine texture * texture alpha DOUBLE
			}
			SetTexture [_MainTex] { combine texture * previous QUAD, texture }
		}
	}
	Fallback "Transparent/Cutout/Diffuse"
}
