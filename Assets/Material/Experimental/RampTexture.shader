Shader "Experimental/Diffuse" {
	Properties {
		_EmissiveColor("EmissiveColor",Color)=(1,1,1,1)
		_AmbientColor("AmbientColor",Color)=(1,1,1,1)
		_Slider("Slider",Range(0,10))=2.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf BasicDiffuse
		float4 _EmissiveColor;
		float4 _AmbientColor;
		float _Slider;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			//half4 c = tex2D (_MainTex, IN.uv_MainTex);
			float4 c=pow((_EmissiveColor+_AmbientColor),_Slider);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		
		inline float4 LightingBasicDiffuse(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			float difLight = max(0,dot(s.Normal,lightDir));
			//float difLight = dot(s.Normal,lightDir);
			float4 col;
			col.rgb = s.Albedo*_LightColor0.rgb*(difLight*atten*2);
			col.a=s.Alpha;
			return col;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
