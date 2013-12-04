Shader "pisipos/colored_one"{
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
				float4 vertexPos : POSITION;
				};
				struct vertexOutput{
				float4 pos : SV_POSITION;
				float4 col : TEXCOORD0;
				};
				 
				vertexOutput vert(vertexInput v)
				{
					vertexOutput o;
					o.pos=mul(UNITY_MATRIX_MVP,v.vertexPos);
					o.col=_Color+v.vertexPos;// + float4(0.1,0.5,0.2,1.0);
					return o;
				}
				
				float4 frag(vertexOutput i): COLOR0
				{
					return i.col;
				}
			ENDCG
		}
	}
	//Fallback "Specular"
}