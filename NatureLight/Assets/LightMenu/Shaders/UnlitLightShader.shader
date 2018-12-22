Shader "Unlit/UnlitLightShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}

		_Color("Color", Color) = (0,0.5,0.5,1)

		_WindowPosition("WindowPosition", vector)= (1,2,3)

		_WindowWidth("WindowWidth", float) = 0

		_WindowHeight("WindowHeight", float) = 0

		_PositionX("PositionX", float) = 0

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
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"



			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float3 worldPos : WORLDPOSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;

			float4 _WindowPosition;
			float _WindowWidth;
			float _WindowHeight;
			float Position;
			float _PositionX;

			float GetIllumination(float3 PointPosition, float3 WindowPosition, float WindowWidth, float WindowHeight)
			{

				float ElevationAngle_1 = atan(((WindowPosition.y + 0.5f * WindowHeight) - PointPosition.y) / (WindowPosition.z - PointPosition.z));

				float ElevationAngle_2 = atan(((WindowPosition.y - 0.5f * WindowHeight) - PointPosition.y) / (WindowPosition.z - PointPosition.z));


				float DirectionAngle_1 = atan(((WindowPosition.x + 0.5f * WindowWidth) - PointPosition.x) / (WindowPosition.z - PointPosition.z));

				float DirectionAngle_2 = atan(((WindowPosition.x - 0.5f * WindowWidth) - PointPosition.x) / (WindowPosition.z - PointPosition.z));

				float Sub = 4.0 * pow(sin(ElevationAngle_2), 3) - 3.0 * pow(cos(ElevationAngle_2), 2) - 4.0 * pow(sin(ElevationAngle_1), 3) + 3.0 * pow(cos(ElevationAngle_1), 2);
				//采光系数
				float DaylightFactor = (DirectionAngle_2 - DirectionAngle_1)*Sub / (14.0* 3.1416);


				//这个点的照度
				return  DaylightFactor;

			}

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
		
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);

				//col(_Color, 1);
				Position = i.vertex;

				fixed Factor = GetIllumination(i.worldPos, _WindowPosition, _WindowWidth, _WindowHeight);
				fixed4 ColorPoint = _Color * Factor*10;
				return ColorPoint;
			}


				

			ENDCG
		}
	}
}
