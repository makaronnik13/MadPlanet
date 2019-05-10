// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:1,cusa:True,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:True,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1873,x:33426,y:32713,varname:node_1873,prsc:2|emission-8783-OUT,alpha-603-OUT;n:type:ShaderForge.SFN_Tex2d,id:4805,x:32299,y:32511,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:True,tagnsco:False,tagnrm:False,tex:234f3d560f2d0ac41985fbca63baa491,ntxv:0,isnm:False|UVIN-8444-OUT;n:type:ShaderForge.SFN_Multiply,id:1086,x:32624,y:32551,cmnt:RGB,varname:node_1086,prsc:2|A-4805-RGB,B-5376-RGB;n:type:ShaderForge.SFN_Color,id:5983,x:32833,y:33089,ptovrint:False,ptlb:GlowColor,ptin:_GlowColor,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9852941,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_VertexColor,id:5376,x:32339,y:32964,varname:node_5376,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1749,x:32833,y:32675,cmnt:Premultiply Alpha,varname:node_1749,prsc:2|A-1086-OUT,B-603-OUT;n:type:ShaderForge.SFN_Multiply,id:603,x:32624,y:32754,cmnt:A,varname:node_603,prsc:2|A-4805-A,B-5376-A;n:type:ShaderForge.SFN_Tex2d,id:4615,x:32339,y:32804,ptovrint:False,ptlb:Glow,ptin:_Glow,varname:node_4615,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:234f3d560f2d0ac41985fbca63baa491,ntxv:0,isnm:False|UVIN-9162-OUT;n:type:ShaderForge.SFN_Add,id:8783,x:33137,y:32720,varname:node_8783,prsc:2|A-1749-OUT,B-6933-OUT;n:type:ShaderForge.SFN_Multiply,id:6933,x:33077,y:32874,varname:node_6933,prsc:2|A-4615-RGB,B-4615-A,C-5983-RGB,D-5983-A;n:type:ShaderForge.SFN_Panner,id:9655,x:31099,y:32679,varname:node_9655,prsc:2,spu:0,spv:-0.2|UVIN-5795-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:6977,x:31250,y:32679,ptovrint:False,ptlb:DistortionTex,ptin:_DistortionTex,varname:node_6977,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a46af076a5367fb42ae65b415afe4a3e,ntxv:2,isnm:False|UVIN-9655-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:5795,x:31000,y:32903,varname:node_5795,prsc:2,uv:0;n:type:ShaderForge.SFN_Lerp,id:4519,x:31721,y:32909,varname:node_4519,prsc:2|A-5795-UVOUT,B-3343-OUT,T-7099-OUT;n:type:ShaderForge.SFN_Append,id:133,x:31480,y:32714,varname:node_133,prsc:2|A-6977-R,B-6977-G;n:type:ShaderForge.SFN_Slider,id:7099,x:31165,y:33063,ptovrint:False,ptlb:DistorGlow,ptin:_DistorGlow,varname:node_7099,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:8953,x:31115,y:32439,ptovrint:False,ptlb:DistorSprite,ptin:_DistorSprite,varname:node_8953,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:0.2;n:type:ShaderForge.SFN_Lerp,id:9660,x:31751,y:32533,varname:node_9660,prsc:2|A-5795-UVOUT,B-3343-OUT,T-8953-OUT;n:type:ShaderForge.SFN_Add,id:3343,x:31702,y:32732,varname:node_3343,prsc:2|A-133-OUT,B-5795-UVOUT;n:type:ShaderForge.SFN_Add,id:8444,x:32013,y:32470,varname:node_8444,prsc:2|A-1790-OUT,B-9660-OUT;n:type:ShaderForge.SFN_Vector2,id:6760,x:31441,y:32245,varname:node_6760,prsc:2,v1:-2,v2:-1;n:type:ShaderForge.SFN_Multiply,id:1790,x:31672,y:32245,varname:node_1790,prsc:2|A-6760-OUT,B-1073-OUT;n:type:ShaderForge.SFN_Divide,id:1073,x:31646,y:32395,varname:node_1073,prsc:2|A-8953-OUT,B-52-OUT;n:type:ShaderForge.SFN_Vector1,id:52,x:31389,y:32374,varname:node_52,prsc:2,v1:2;n:type:ShaderForge.SFN_Divide,id:9247,x:31827,y:33230,varname:node_9247,prsc:2|A-7099-OUT,B-554-OUT;n:type:ShaderForge.SFN_Multiply,id:2722,x:31853,y:33080,varname:node_2722,prsc:2|A-534-OUT,B-9247-OUT;n:type:ShaderForge.SFN_Vector2,id:534,x:31622,y:33080,varname:node_534,prsc:2,v1:-2,v2:-1;n:type:ShaderForge.SFN_Vector1,id:554,x:31570,y:33209,varname:node_554,prsc:2,v1:2;n:type:ShaderForge.SFN_Add,id:9162,x:32038,y:32916,varname:node_9162,prsc:2|A-4519-OUT,B-2722-OUT;proporder:4805-5983-4615-6977-7099-8953;pass:END;sub:END;*/

Shader "Shader Forge/LgowingItem" {
    Properties {
        [PerRendererData]_MainTex ("MainTex", 2D) = "white" {}
        [HDR]_GlowColor ("GlowColor", Color) = (0.9852941,0,0,1)
        _Glow ("Glow", 2D) = "white" {}
        _DistortionTex ("DistortionTex", 2D) = "black" {}
        _DistorGlow ("DistorGlow", Range(0, 1)) = 0
        _DistorSprite ("DistorSprite", Range(0, 0.2)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
            "PreviewType"="Plane"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _GlowColor;
            uniform sampler2D _Glow; uniform float4 _Glow_ST;
            uniform sampler2D _DistortionTex; uniform float4 _DistortionTex_ST;
            uniform float _DistorGlow;
            uniform float _DistorSprite;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_4216 = _Time + _TimeEditor;
                float2 node_9655 = (i.uv0+node_4216.g*float2(0,-0.2));
                float4 _DistortionTex_var = tex2D(_DistortionTex,TRANSFORM_TEX(node_9655, _DistortionTex));
                float2 node_3343 = (float2(_DistortionTex_var.r,_DistortionTex_var.g)+i.uv0);
                float2 node_8444 = ((float2(-2,-1)*(_DistorSprite/2.0))+lerp(i.uv0,node_3343,_DistorSprite));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_8444, _MainTex));
                float node_603 = (_MainTex_var.a*i.vertexColor.a); // A
                float2 node_9162 = (lerp(i.uv0,node_3343,_DistorGlow)+(float2(-2,-1)*(_DistorGlow/2.0)));
                float4 _Glow_var = tex2D(_Glow,TRANSFORM_TEX(node_9162, _Glow));
                float3 emissive = (((_MainTex_var.rgb*i.vertexColor.rgb)*node_603)+(_Glow_var.rgb*_Glow_var.a*_GlowColor.rgb*_GlowColor.a));
                float3 finalColor = emissive;
                return fixed4(finalColor,node_603);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
