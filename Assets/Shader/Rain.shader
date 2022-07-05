
Shader "Rain"
{
	Properties{

	}
	SubShader
	{
	Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
	Pass
	{
	ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha
	CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag
	#include "UnityCG.cginc"



	float4 vec4(float x,float y,float z,float w) { return float4(x,y,z,w); }
	float4 vec4(float x) { return float4(x,x,x,x); }
	float4 vec4(float2 x,float2 y) { return float4(float2(x.x,x.y),float2(y.x,y.y)); }
	float4 vec4(float3 x,float y) { return float4(float3(x.x,x.y,x.z),y); }


	float3 vec3(float x,float y,float z) { return float3(x,y,z); }
	float3 vec3(float x) { return float3(x,x,x); }
	float3 vec3(float2 x,float y) { return float3(float2(x.x,x.y),y); }

	float2 vec2(float x,float y) { return float2(x,y); }
	float2 vec2(float x) { return float2(x,x); }

	float vec(float x) { return float(x); }



	struct VertexInput {
	float4 vertex : POSITION;
	float2 uv:TEXCOORD0;
	float4 tangent : TANGENT;
	float3 normal : NORMAL;
	//VertexInput
	};
	struct VertexOutput {
	float4 pos : SV_POSITION;
	float2 uv:TEXCOORD0;
	//VertexOutput
	};


	VertexOutput vert(VertexInput v)
	{
	VertexOutput o;
	o.pos = UnityObjectToClipPos(v.vertex);
	o.uv = v.uv;
	//VertexFactory
	return o;
	}




	fixed4 frag(VertexOutput u) : SV_Target
	{

	float2 inp = u.pos;
	float4 o = vec4(_Time);

	o += 0.8 - frac((inp.y*.02 + inp.x*.4)*frac(inp.x*.61) + _Time.y%1111)*7.-o;

	return o;
	}
	ENDCG
	}
	}
}
