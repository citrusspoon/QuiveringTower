Shader "Grass/Cutout/Low Quality" {
	Properties {
		_Color ("Grass Color", Color) = (1,1,1,1)
		_Color2 ("Albedo Color", Color) = (1,1,1,1)
		_MainTex ("Grass Texture (RGB)", 2D) = "white" {}
		_SecondTex ("Albedo (RGB)", 2D) = "white" {}
		[Toggle]_MultiDir ("Multi Directional Wind", Int) = 1
		_Wind ("Wind Power", Range(0,4)) = 1
		_WindDir ("Wind Direction (XY), Wind Speed (ZW)", Vector) = (1,0,1,1)
		_WindTex ("Wind Texture", 2D) = "white" {}
		_Occ ("Occlusion Intensity", Range(0,1)) = 1.0
		_Cutoff ("Cutoff", Range(0,1)) = 0.095
		_Alpha ("Cutoff/Occlusion (A)", 2D) = "white" {}
		_Length ("Length", Range(0,1)) = 0.05
		_Div ("Length Division Factor", Range(0,10000)) = 50
		_Height ("Grass Height Map (A)", 2D) = "white" {}
	}
		
		CGINCLUDE
		#pragma target 4.0

		sampler2D _MainTex;
		sampler2D _WindTex;
		sampler2D _Alpha;
		float4 _Alpha_TexelSize;
		sampler2D _Height;
		float4 _Height_TexelSize;

		struct Input {
			float2 uv_MainTex;
			float2 uv_WindTex;
			float2 uv_Alpha;
			float2 uv_Height;
			float2 uv_SecondTex;
			half ite;
		};

		fixed4 _Color;
		half _Length;
		half _Occ;
		half _Div;
		half _Wind;
		float4 _WindDir;
		int _MultiDir;

		void vertCustom (inout appdata_full v, half ITERATION) {
			v.vertex.xyz += v.normal * _Length / _Div * ITERATION * 4;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			//o.Smoothness = _Spec;
			int scale = IN.ite;
			IN.uv_WindTex += float2 (_WindDir.zw * _Time.x);
			float offsetx = lerp (tex2D (_WindTex, IN.uv_WindTex), (tex2D (_WindTex, IN.uv_WindTex) - 0.5) / 0.5, _MultiDir) * _Wind * _Alpha_TexelSize.x * _WindDir.x;
			float offsety = lerp (tex2D (_WindTex, IN.uv_WindTex), (tex2D (_WindTex, IN.uv_WindTex) - 0.5) / 0.5, _MultiDir) * _Wind * _Alpha_TexelSize.y * _WindDir.y;
			float2 offset = float2 (offsetx, offsety) * scale * 4;
			IN.uv_Alpha = offset + IN.uv_Alpha;
			o.Albedo = c.rgb;
			/*offsetx = lerp (tex2D (_WindTex, IN.uv_WindTex), (tex2D (_WindTex, IN.uv_WindTex) - 0.5) / 0.5, _MultiDir) * _Wind * _Height_TexelSize.x * _WindDir.x;
			offsety = lerp (tex2D (_WindTex, IN.uv_WindTex), (tex2D (_WindTex, IN.uv_WindTex) - 0.5) / 0.5, _MultiDir) * _Wind * _Height_TexelSize.y * _WindDir.y;
			offset = float2 (offsetx, offsety) * scale;
			IN.uv_Height = offset + IN.uv_Height;*/
			float h = tex2D (_Height, IN.uv_Height).a;
			/*if (h < 0.2 * scale) {
				h = 0;
			}
			else {
				h = 1;
			}*/
			o.Albedo *= lerp (1, tex2D (_Alpha, IN.uv_Alpha).a, _Occ);
			o.Alpha = pow (tex2D (_Alpha, IN.uv_Alpha).a * h, IN.ite / 2);
		}
		ENDCG

	SubShader {
		Tags { "RenderType"="AlphaTest" "Queue"="AlphaTest" }
		LOD 200

		CGPROGRAM
		#pragma surface surfBase Lambert

		sampler2D _SecondTex;

		fixed4 _Color2;

		void surfBase (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_SecondTex, IN.uv_SecondTex) * _Color2;
			o.Albedo = c.rgb * lerp (1, tex2D (_Alpha, IN.uv_Alpha).a, _Occ * tex2D (_Height, IN.uv_Height));
			o.Alpha = c.a;
		}
		ENDCG

		CGPROGRAM
		#pragma surface surf Lambert alphatest:_Cutoff vertex:vert
		#define ITERATION 1.5
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG

		CGPROGRAM
		#pragma surface surf Lambert alphatest:_Cutoff vertex:vert
		#define ITERATION 2
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG

		CGPROGRAM
		#pragma surface surf Lambert alphatest:_Cutoff vertex:vert
		#define ITERATION 3
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG

		CGPROGRAM
		#pragma surface surf Lambert alphatest:_Cutoff vertex:vert
		#define ITERATION 4
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG

		CGPROGRAM
		#pragma surface surf Lambert alphatest:_Cutoff vertex:vert
		#define ITERATION 5
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
