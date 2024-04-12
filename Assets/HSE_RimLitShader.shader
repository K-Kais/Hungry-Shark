Shader "HSE/RimLitShader" {
	Properties {
		_MainTex ("Texture", 2D) = "black" {}
		[Toggle(BLEND_TEXTURES)] _EnableBlending ("Enable Blending", Float) = 0
		_BlendedTex ("Blend Texture", 2D) = "" {}
		_BlendValue ("Blend Value", Range(0, 1)) = 0
		[Toggle(ALPHA_MASK_ON)] _EnableAlphaMask ("Enable Alpha Mask", Float) = 0
		_AlphaMaskTex ("Alpha Mask", 2D) = "" {}
		[Toggle(CAUSTICS_ON)] _EnableCaustics ("Enable Causitcs", Float) = 0
		_CausticTex ("Caustics", 2D) = "" {}
		_CausticsIntensity ("Caustics Intensity", Range(0, 2)) = 1
		_AmbientLight ("Ambient Light", Range(0, 1)) = 0.75
		_LightingIntensity ("Lighting Intensity", Range(0, 1)) = 1
		[Toggle(EMISSION_ON)] _EnableEmission ("Enable Emission", Range(0, 1)) = 0
		_EmissionMaskTexture ("Emission Mask Texture", 2D) = "" {}
		_EmissionColor ("Emission Color", Vector) = (0,0,0,1)
		_EmissionIntensity ("Emission Intensity", Range(0, 5)) = 1
		_Tint ("Tint Color", Vector) = (1,0,0,1)
		_TintIntensity ("Tint intensity", Range(0, 1)) = 0
		_TintMultiply ("Tint Mutiplies", Range(0, 1)) = 0
		_Alpha ("Alpha", Range(0, 1)) = 1
		[Toggle(FRESNEL_ON)] _EnableFresnel ("Enable Fresnel", Float) = 0
		_FresnelColor ("Fresnel Color", Vector) = (0,0,0,1)
		_FresnelIntensity ("FresnelIntensity", Range(0.01, 5)) = 1
		[Toggle(CLOAKING_ON)] _EnableCloaking ("Enable Cloaking", Float) = 0
		_CloakingColor ("Cloaking Color", Vector) = (1,1,1,1)
		_CloakingIntensity ("Cloking Intensity", Range(0, 1)) = 1
		_CloakingRimSize ("Cloaking Rim Size", Range(0, 1)) = 1
		[Toggle(COLORED_PARTS_ON)] _EnableColoredParts ("Enable Colored Parts", Float) = 0
		_ColorMaskTex ("Color Mask Texture", 2D) = "" {}
		_ColorR ("Color Red Channel", Vector) = (1,1,1,1)
		_ColorG ("Color Green Channel", Vector) = (1,1,1,1)
		_ColorB ("Color Blue Channel", Vector) = (1,1,1,1)
		[Toggle(CUT_OUT)] _EnableCutOut ("Enable Cut Out", Float) = 0
		_PlaneNormal ("PlaneNormal", Vector) = (0,0,0,0)
		_PlanePosition ("PlanePosition", Vector) = (0,0,0,0)
		[Toggle(CUT_OUT_CIRCLE)] _EnableCutOutCircular ("Enable Cut Out Circle", Float) = 0
		_PlanePosition ("PlanePosition", Vector) = (0,0,0,0)
		_Radius ("Circle Radius", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Unlit/Texture"
	//CustomEditor "RimLitShaderInspector"
}