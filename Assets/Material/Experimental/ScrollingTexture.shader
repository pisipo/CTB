Shader "Experimental/ScrollingTexture" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_XSpeed("X Spped",Range(0,2))=2
		_YSpeed("Y Spped",Range(0,2))=2
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		float _XSpeed;
		float _YSpeed;
		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed2 scrolledUV=IN.uv_MainTex;
			
			fixed xScrollValue=_XSpeed*_Time;
			fixed yScrollValue=_YSpeed*_Time;
			scrolledUV+=fixed2(xScrollValue,yScrollValue);
			half4 c = tex2D (_MainTex, scrolledUV);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
