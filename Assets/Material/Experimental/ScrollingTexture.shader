Shader "Experimental/ScrollingTexture" {
	Properties {
		_MainTex ("MainBack", 2D) = "white" {}
		_BigStarsTex("BigStars", 2D) = "white" {}
		_MediumStarsTex("MediumStars", 2D) = "white" {}
		_SmallStarsTex("SmallStars", 2D) = "white" {}
		_Nebula("Nebula", 2D) = "white" {}
		_MainSpeed("X Spped",Range(0,2))=2
		_BigStarsSpeed("Y Spped",Range(0,2))=2
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		sampler2D _BigStarsTex;
		float _MainSpeed;
		float _BigStarsSpeed;
		struct Input {
			float2 uv_MainTex;
			float2 uv_BigStarsTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed2 scrolledUVMain=IN.uv_MainTex;
			fixed2 scrolledUVBigStars=IN.uv_BigStarsTex;
			
			fixed MainScrollValue=_MainSpeed*_Time;
			fixed BigStarsScrollValue=_BigStarsSpeed*_Time;

			scrolledUVMain+=fixed2(MainScrollValue,0);
			scrolledUVBigStars+=fixed2(BigStarsScrollValue,0);
			
			half4 mainTex = tex2D (_MainTex, scrolledUVMain);
			half4 bigStarsTex = tex2D (_BigStarsTex, scrolledUVBigStars);
			half3 starAlbedo=bigStarsTex.rgb*bigStarsTex.a;
			o.Albedo = mainTex.rgb+starAlbedo;
			//o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
