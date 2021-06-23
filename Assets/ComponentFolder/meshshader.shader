Shader "Custom/vanJoppe/meshshader"
{
   Properties{
   		_MainTex ("Main Color", 2D) = "white" {}
		_Color ("Color", Color) = (1, 1, 1, 1)

		scrollSpeed ("Scroll Speed", Vector) = (1, 0, 0, 0)
   }

   SubShader{
   Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		Lighting Off
		Fog{ Mode Off }
		ZWrite Off
		Cull Off
        
		Pass{
			Blend One OneMinusSrcAlpha
			CGPROGRAM

			#pragma vertex vertexFunction
			#pragma fragment fragmentFunction


			#include "UnityCG.cginc"

			struct appdata{
			float4 vertex : POSITION;
			float2 uv :TEXCOORD0;
			float3 normal :NORMAL;
			};

			struct v2f{
			float4 position : SV_POSITION;
			float2 uv : TEXCOORD0;
			};

			

			v2f vertexFunction(appdata IN){
				v2f OUT;
				
				OUT.position = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;

				return OUT;
			}

			float4 _Color;
			sampler2D _MainTex;
			float scrollSpeed;

			fixed4 fragmentFunction(v2f IN) : SV_Target{
			
			    float4 col = tex2D(_MainTex, IN.uv);
				
                col.r += IN.uv + _Time * scrollSpeed.x;
                col.g += _Time * scrollSpeed.x;
                col.b += _Time * scrollSpeed.x;
				col *= _Color;

				return fixed4 (col);
			}

			ENDCG
		}
   }

}
