// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/vanJoppe/CGCOOK"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // a texture
		_Color("Color", Color) = (0,0,0,0) // a color'

    }
    SubShader
    {

        Pass
        {
            CGPROGRAM //start
            #pragma vertex vert //using vertex
            #pragma fragment frag //using fragment
            
			uniform fixed4 _Color; // calling property

			struct vertexInput{ //assign semantic to named thingy
				float4 vertex : POSITION;
				float4 normal : NORMAL;
			};
			struct vertexOutput{ //output to send to fragment
				float4 pos : SV_POSITION;
				float4 col : COLOR;
			};

			vertexOutput vert (vertexInput IN){
				vertexOutput OUT;

				OUT.col = float4(IN.normal.xyz, 1.0);
				OUT.pos = UnityObjectToClipPos(IN.vertex);
				return OUT;
			}

			fixed4 frag(vertexOutput IN) : COLOR
			{
				//IN.col
				IN.col = -IN.col;
				return IN.col;
			}


            ENDCG //quit
        }
    }
}
