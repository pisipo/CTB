Shader "Experimental/RampTexture" {
	Properties {
		_RampTex("Ramp",2D)="white"{}
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma  surface surf BasicDiffuse
		sampler2D _RampTex;
		sampler2D _MainTex;
		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		
		inline float4 LightingBasicDiffuse(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			float difLight = max(0,dot(s.Normal,lightDir));
			//float difLight = dot(s.Normal,lightDir);
			float3 ramp=tex2D(_RampTex,float2(difLight)).rgb;
			float4 col;
			col.rgb = s.Albedo*_LightColor0.rgb*ramp;
			col.a=s.Alpha;
			return col;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
