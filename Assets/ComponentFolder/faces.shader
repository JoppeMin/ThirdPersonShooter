Shader "van joppe/faces"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Transtex ("Transition Texture", 2D) = "black"{}
		Transitionamount ("Transition amount", Range (0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

			sampler2D _Transtex;
			float Transitionamount;

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 col2 = tex2D(_Transtex, i.uv);

				fixed4 colEnd = (col * Transitionamount);
				colEnd += (col2 * (1 - Transitionamount));


                return colEnd;
            }
            ENDCG
        }
    }
}
