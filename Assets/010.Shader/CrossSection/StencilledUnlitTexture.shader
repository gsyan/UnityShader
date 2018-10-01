﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
// 자르는 쿼드에 적용
Shader "CrossSection/Others/StencilledUnlitTexture"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_NormalMap("NormalMap", 2D) = "white" {}
		_StencilMask("Stencil Mask", Range(0, 255)) = 255
	}
	SubShader
	{
		Tags{ "RenderType" = "Opaque" "Queue" = "Transparent" }
		LOD 100
		ZTest On
		//Cull Off
		Stencil{
			Ref[_StencilMask]
			Comp Equal//Equal,NotEqual, Less, Greater
		}
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
			float3 normal : NORMAL;
			float3 tangent : TANGENT;
		};

		struct v2f
		{
			float2 uv : TEXCOORD0;
			UNITY_FOG_COORDS(1)
			float4 vertex : SV_POSITION;

			float3 lightdir : TEXCOORD1;
			float3 viewdir : TEXCOORD2;
			float3 T : TEXCOORD3;
			float3 B : TEXCOORD4;
			float3 N : TEXCOORD5;
		};

		sampler2D _MainTex;
		float4 _MainTex_ST;

		v2f vert(appdata v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.uv = TRANSFORM_TEX(v.uv, _MainTex);

			/*float4 worldPosition = mul(_Object2World, input.position);
			float3 lightDir = worldPosition.xyz - _MainLightPosition.xyz;
			o.lightdir = normalize(lightDir);*/
			o.lightdir = ObjSpaceLightDir(v.vertex);




			UNITY_TRANSFER_FOG(o,o.vertex);
			return o;
		}
	
		/*void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
			o.Emission = _RimColor.rgb * pow(rim, _RimPower);
		}*/

		fixed4 frag(v2f i) : SV_Target
		{
			// sample the texture
			fixed4 col = tex2D(_MainTex, i.uv);
		
			// apply fog
			UNITY_APPLY_FOG(i.fogCoord, col);
			return col;
		}
		ENDCG
	}
	}
}