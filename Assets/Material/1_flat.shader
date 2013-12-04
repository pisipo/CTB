Shader "pisipos/flat_one"{
	Properties{
	_Color("Color",Color)=(1.0,1.0,1.0,1.0)
	//_y("y",float4)=(1.0,0.1,1.0,1.0)
	}
	SubShader{
		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
				uniform float4 _Color;
			
				struct vertexInput{
				float4 vertex : POSITION;
				};
				struct vertexOutput{
				float4 pos : SV_POSITION;
				float4 col: COLOR;
				};
				 
				vertexOutput vert(vertexInput v)
				{
					vertexOutput o;
					o.pos=mul(UNITY_MATRIX_MVP,v.vertex);//float4(1.0,1.0,1.0,1.0));
					//o.pos=v.vertex;
					o.col=_Color;
					return o;
				}
				
				float4 frag(vertexOutput i): COLOR
				{
					return _Color;
				}
			ENDCG
		}
	
	}
	//Fallback "Specular"
}