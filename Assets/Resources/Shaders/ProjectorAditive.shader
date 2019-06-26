Shader "Projector/Dodge" {
	Properties{
		_ColorV("Visible Color", Color) = (1,1,1,1)
		_ColorH("Hidden Color", Color) = (0,0,0,1)
		_ShadowTex("Cookie", 2D) = "" { TexGen ObjectLinear }
	}
		Subshader
	{
		Lighting Off
		 
		// Invisible Pass
		Pass
	{
		ZWrite Off
		ZTest GEqual
		Blend DstColor One
		Offset -1, -1
		SetTexture[_ShadowTex]
	{
		constantColor[_ColorH]
		combine texture alpha * constant
		Matrix[_Projector]
	}
	}

		// Visible Pass
		Pass
	{
		ZWrite Off
		ZTest LEqual
		Blend SrcColor One
		Offset -1, -1
		SetTexture[_ShadowTex]
	{
		constantColor[_ColorV]
		combine texture alpha * constant
		Matrix[_Projector]
	}
	}
	}
}