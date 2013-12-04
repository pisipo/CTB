Shader "Experimental/TextureOffsetTests" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_TexWidth("Sheet Width",float)=0.0
		_CellAmountX("Cell Amount X",float)=0.0
		_CellAmountY("Cell Amount Y",float)=0.0
		_Speed("Spped",Range(0.01,8))=12
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		float _TexWidth;
		float _CellAmountX;
		float _CellAmountY;
		float _Speed;
		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed2 spriteUV=IN.uv_MainTex;
			
			float cellPixelWidth=_TexWidth/_CellAmountX;
			float cellUVPercentage = cellPixelWidth/_TexWidth;
			
			//float timeVal=fmod(_Time.y*_Speed,_CellAmountX);
			//timeVal=ceil(timeVal);
			
			//float xValue=spriteUV.x;
			//xValue+=cellUVPercentage*timeVal*_CellAmountX;
			//xValue*=cellUVPercentage;
			float xValue=spriteUV.x;
			xValue+=cellUVPercentage*ceil(_Time.y);
			//xValue*=cellUVPercentage;
			spriteUV=float2(xValue,spriteUV.y);
			
			half4 c = tex2D (_MainTex, spriteUV);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
