//Hey guys! This shader is modified Unity 3D Built-in Shader
//I am https://youtube.com/vulgerstal edited this shader to make material double-sided
//Also I added ability to receive and cast shadows.
//ДЯКУЮ!


Shader "Transparent/Cutout/Diffuse No Cull" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
}

SubShader {
	Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
	LOD 200
	
	ZWrite Off// Added this for some reason? Навіщо? Хтозна!
	
	Cull Off
	
CGPROGRAM
#pragma surface surf Lambert alphatest:_Cutoff

sampler2D _MainTex;
fixed4 _Color;

struct Input {
	float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	o.Albedo = c.rgb;
	o.Alpha = c.a;
}
ENDCG
}

Fallback "Transparent/Cutout/VertexLit"
}
