Shader "Grass/Normal/Medium Quality" {
	Properties {
		_Color ("Grass Color", Color) = (1,1,1,1)
		_Color2 ("Albedo Color", Color) = (1,1,1,1)
		_Spec ("Grass Specular", Range(0,1)) = 0.778
		_MainTex ("Grass Texture (RGB)", 2D) = "white" {}
		_BumpMap ("Grass Normal Map", 2D) = "bump" {}
		[HideInInspector]_BumpNone ("", 2D) = "bump" {}
		_Spec2 ("Albedo Specular", range(0,1)) = 0.4
		_SecondTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap2 ("Albedo Normal Map", 2D) = "bump" {}
		[Toggle]_MultiDir ("Multi Directional Wind", Int) = 1
		_Wind ("Wind Power", Range(0,4)) = 1
		_WindDir ("Wind Direction (XY), Wind Speed (ZW)", Vector) = (1,0,1,1)
		_WindTex ("Wind Texture", 2D) = "white" {}
		_Occ ("Occlusion Intensity", Range(0,1)) = 1.0
		_Alpha ("Cutoff/Occlusion (A)", 2D) = "white" {}
		_Length ("Length", Range(0,1)) = 0.05
		_Div ("Length Division Factor", Range(0,10000)) = 50
		_Height ("Grass Height Map (A)", 2D) = "white" {}
	}
		
		CGINCLUDE
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _WindTex;
		sampler2D _Alpha;
		float4 _Alpha_TexelSize;
		sampler2D _Height;
		float4 _Height_TexelSize;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float2 uv_WindTex;
			float2 uv_Alpha;
			float2 uv_Height;
			float2 uv_SecondTex;
			float2 uv_BumpMap2;
			half ite;
		};

		fixed4 _Color;
		half _Spec;
		half _Length;
		half _Occ;
		half _Div;
		half _Wind;
		float4 _WindDir;
		int _MultiDir;

		void vertCustom (inout appdata_full v, half ITERATION) {
			v.vertex.xyz += v.normal * _Length / _Div * ITERATION * 2;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			//o.Smoothness = _Spec;
			int scale = IN.ite;
			IN.uv_WindTex += float2 (_WindDir.zw * _Time.x);
			float offsetx = lerp (tex2D (_WindTex, IN.uv_WindTex), (tex2D (_WindTex, IN.uv_WindTex) - 0.5) / 0.5, _MultiDir) * _Wind * _Alpha_TexelSize.x * _WindDir.x;
			float offsety = lerp (tex2D (_WindTex, IN.uv_WindTex), (tex2D (_WindTex, IN.uv_WindTex) - 0.5) / 0.5, _MultiDir) * _Wind * _Alpha_TexelSize.y * _WindDir.y;
			float2 offset = float2 (offsetx, offsety) * scale * 2;
			IN.uv_Alpha = offset + IN.uv_Alpha;
			IN.uv_BumpMap = offset + IN.uv_BumpMap;
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
			o.Albedo = c.rgb * lerp (1, tex2D(_Alpha, IN.uv_Alpha).a, _Occ);
			o.Smoothness = _Spec * IN.ite / 10;
			/*offsetx = lerp (tex2D (_WindTex, IN.uv_WindTex), (tex2D (_WindTex, IN.uv_WindTex) - 0.5) / 0.5, _MultiDir) * _Wind * _Height_TexelSize.x * _WindDir.x;
			offsety = lerp (tex2D (_WindTex, IN.uv_WindTex), (tex2D (_WindTex, IN.uv_WindTex) - 0.5) / 0.5, _MultiDir) * _Wind * _Height_TexelSize.y * _WindDir.y;
			offset = float2 (offsetx, offsety) * scale;
			IN.uv_Height = offset + IN.uv_Height;*/
			float h = tex2D (_Height, IN.uv_Height).a;
			/*if (h < 0.1 * scale) {
				h = 0;
			}
			else {
				h = 1;
			}*/
			o.Alpha = pow (tex2D (_Alpha, IN.uv_Alpha).a * h, IN.ite / 5);
		}
		ENDCG

	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 200

		CGPROGRAM
		#pragma surface surfBase Standard

		sampler2D _SecondTex;
		sampler2D _BumpMap2;

		half _Spec2;
		fixed4 _Color2;

		void surfBase (Input IN, inout SurfaceOutputStandard o) {
			o.Normal = UnpackNormal (tex2D (_BumpMap2, IN.uv_BumpMap2));
			half4 c = tex2D (_SecondTex, IN.uv_SecondTex) * _Color2;
			o.Albedo = c.rgb * lerp (1, tex2D (_Alpha, IN.uv_Alpha).a, _Occ * tex2D (_Height, IN.uv_Height));
			o.Alpha = c.a;
			o.Smoothness = _Spec2;
		}
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard alpha:fade vertex:vert
		#define ITERATION 1.5
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard alpha:fade vertex:vert
		#define ITERATION 2
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard alpha:fade vertex:vert
		#define ITERATION 3
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard alpha:fade vertex:vert
		#define ITERATION 4
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard alpha:fade vertex:vert
		#define ITERATION 5
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard alpha:fade vertex:vert
		#define ITERATION 6
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard alpha:fade vertex:vert
		#define ITERATION 7
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard alpha:fade vertex:vert
		#define ITERATION 8
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard alpha:fade vertex:vert
		#define ITERATION 9
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard alpha:fade vertex:vert
		#define ITERATION 10
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
