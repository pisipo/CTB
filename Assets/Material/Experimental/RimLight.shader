Shader "Experimental/RimLight" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma  surface surf BasicDiffuse
		sampler2D _MainTex;
		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		
		inline float4 LightingBasicDiffuse(SurfaceOutput s, fixed3 lightDir,half3 viewDir,fixed atten)
		{
			float difLight = dot(s.Normal,viewDir);
			if(difLight<0.5)difLight=-1;
			float4 col;
			col.rgb = s.Albedo*_LightColor0.rgb*difLight*atten*2;
			col.a=s.Alpha;
			return col;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
