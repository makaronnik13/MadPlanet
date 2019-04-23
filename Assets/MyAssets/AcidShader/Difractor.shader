// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:1,cusa:True,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:True,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1873,x:33862,y:32675,varname:node_1873,prsc:2|emission-6893-OUT,alpha-3905-OUT,refract-4183-OUT;n:type:ShaderForge.SFN_Panner,id:781,x:30344,y:32787,varname:node_781,prsc:2,spu:1,spv:0|UVIN-9010-UVOUT,DIST-9952-OUT;n:type:ShaderForge.SFN_Tex2d,id:8814,x:30534,y:32787,ptovrint:False,ptlb:FlowmapH,ptin:_FlowmapH,varname:node_8814,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:435edb12d5c5eae4591c1426fdafdf64,ntxv:2,isnm:False|UVIN-781-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:9010,x:30059,y:32641,varname:node_9010,prsc:2,uv:0;n:type:ShaderForge.SFN_ComponentMask,id:1719,x:31088,y:32954,varname:node_1719,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-1948-OUT;n:type:ShaderForge.SFN_Lerp,id:9716,x:32370,y:32894,varname:node_9716,prsc:2|A-3241-OUT,B-3846-OUT,T-841-OUT;n:type:ShaderForge.SFN_Slider,id:841,x:31179,y:33274,ptovrint:False,ptlb:DistoreEffect,ptin:_DistoreEffect,varname:node_841,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Tex2d,id:9319,x:30534,y:32984,ptovrint:False,ptlb:FlowmapV,ptin:_FlowmapV,varname:node_9319,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:435edb12d5c5eae4591c1426fdafdf64,ntxv:0,isnm:False|UVIN-5152-UVOUT;n:type:ShaderForge.SFN_Lerp,id:1948,x:30889,y:32954,varname:node_1948,prsc:2|A-8814-RGB,B-9319-RGB,T-243-OUT;n:type:ShaderForge.SFN_Panner,id:5152,x:30344,y:33008,varname:node_5152,prsc:2,spu:0,spv:1|UVIN-9010-UVOUT,DIST-9952-OUT;n:type:ShaderForge.SFN_Time,id:2381,x:29965,y:33083,varname:node_2381,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9952,x:30142,y:33102,varname:node_9952,prsc:2|A-2381-T,B-6220-OUT;n:type:ShaderForge.SFN_Slider,id:6220,x:29886,y:33330,ptovrint:False,ptlb:UvSpeed,ptin:_UvSpeed,varname:node_6220,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.02548141,max:1;n:type:ShaderForge.SFN_TexCoord,id:9624,x:31690,y:33236,varname:node_9624,prsc:2,uv:0;n:type:ShaderForge.SFN_RemapRange,id:6436,x:31872,y:33236,varname:node_6436,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-9624-UVOUT;n:type:ShaderForge.SFN_Length,id:1736,x:32050,y:33236,varname:node_1736,prsc:2|IN-6436-OUT;n:type:ShaderForge.SFN_OneMinus,id:7592,x:32227,y:33236,varname:node_7592,prsc:2|IN-1736-OUT;n:type:ShaderForge.SFN_OneMinus,id:6576,x:32581,y:33236,varname:node_6576,prsc:2|IN-8783-OUT;n:type:ShaderForge.SFN_Slider,id:4332,x:31841,y:33557,ptovrint:False,ptlb:Size,ptin:_Size,varname:node_4332,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5232528,max:1;n:type:ShaderForge.SFN_Add,id:8783,x:32406,y:33236,varname:node_8783,prsc:2|A-7592-OUT,B-363-OUT;n:type:ShaderForge.SFN_Clamp01,id:5667,x:32750,y:33276,varname:node_5667,prsc:2|IN-6576-OUT;n:type:ShaderForge.SFN_Multiply,id:4183,x:32711,y:32865,varname:node_4183,prsc:2|A-9716-OUT,B-4337-OUT;n:type:ShaderForge.SFN_RemapRange,id:792,x:31055,y:32656,varname:node_792,prsc:2,frmn:0,frmx:1,tomn:0.5,tomx:-0.5|IN-9010-UVOUT;n:type:ShaderForge.SFN_Multiply,id:6893,x:33500,y:32716,varname:node_6893,prsc:2|A-5203-RGB,B-7735-OUT,C-5020-OUT;n:type:ShaderForge.SFN_Slider,id:7735,x:32732,y:32640,ptovrint:False,ptlb:ColorOpacity,ptin:_ColorOpacity,varname:node_7735,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:14.81941,max:70;n:type:ShaderForge.SFN_Vector1,id:243,x:30604,y:33188,varname:node_243,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector2,id:3241,x:31517,y:32565,varname:node_3241,prsc:2,v1:0,v2:0;n:type:ShaderForge.SFN_Multiply,id:3846,x:31325,y:32954,varname:node_3846,prsc:2|A-792-OUT,B-1719-OUT;n:type:ShaderForge.SFN_RemapRange,id:363,x:32173,y:33540,varname:node_363,prsc:2,frmn:0,frmx:1,tomn:2,tomx:-2|IN-4332-OUT;n:type:ShaderForge.SFN_Power,id:4337,x:33010,y:33280,varname:node_4337,prsc:2|VAL-5667-OUT,EXP-5266-OUT;n:type:ShaderForge.SFN_Slider,id:5266,x:32527,y:33583,ptovrint:False,ptlb:Pow,ptin:_Pow,varname:node_5266,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:4.684266,max:25;n:type:ShaderForge.SFN_Length,id:5020,x:32863,y:32912,varname:node_5020,prsc:2|IN-4183-OUT;n:type:ShaderForge.SFN_Multiply,id:3905,x:33377,y:33127,varname:node_3905,prsc:2|A-5020-OUT,B-4337-OUT;n:type:ShaderForge.SFN_Tex2d,id:5203,x:33248,y:32412,ptovrint:False,ptlb:ColorBar,ptin:_ColorBar,varname:node_5203,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:6abc5f50a567d1145ba6c218976d893c,ntxv:0,isnm:False|UVIN-9142-OUT;n:type:ShaderForge.SFN_Vector1,id:3355,x:32840,y:32762,varname:node_3355,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:9142,x:33178,y:32618,varname:node_9142,prsc:2|A-1433-OUT,B-3355-OUT;n:type:ShaderForge.SFN_Multiply,id:1433,x:33087,y:32792,varname:node_1433,prsc:2|A-5020-OUT,B-7914-OUT;n:type:ShaderForge.SFN_Slider,id:7914,x:32756,y:32326,ptovrint:False,ptlb:CollorEffect,ptin:_CollorEffect,varname:node_7914,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.880342,max:10;proporder:8814-841-9319-6220-4332-7735-5266-5203-7914;pass:END;sub:END;*/

Shader "Shader Forge/Difractor" {
    Properties {
        _FlowmapH ("FlowmapH", 2D) = "black" {}
        _DistoreEffect ("DistoreEffect", Range(0, 1)) = 1
        _FlowmapV ("FlowmapV", 2D) = "white" {}
        _UvSpeed ("UvSpeed", Range(0, 1)) = 0.02548141
        _Size ("Size", Range(0, 1)) = 0.5232528
        _ColorOpacity ("ColorOpacity", Range(0, 70)) = 14.81941
        _Pow ("Pow", Range(0, 25)) = 4.684266
        _ColorBar ("ColorBar", 2D) = "white" {}
        _CollorEffect ("CollorEffect", Range(0, 10)) = 2.880342
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
        GrabPass{ }
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
            uniform sampler2D _GrabTexture;
            uniform float4 _TimeEditor;
            uniform sampler2D _FlowmapH; uniform float4 _FlowmapH_ST;
            uniform float _DistoreEffect;
            uniform sampler2D _FlowmapV; uniform float4 _FlowmapV_ST;
            uniform float _UvSpeed;
            uniform float _Size;
            uniform float _ColorOpacity;
            uniform float _Pow;
            uniform sampler2D _ColorBar; uniform float4 _ColorBar_ST;
            uniform float _CollorEffect;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float4 node_2381 = _Time + _TimeEditor;
                float node_9952 = (node_2381.g*_UvSpeed);
                float2 node_781 = (i.uv0+node_9952*float2(1,0));
                float4 _FlowmapH_var = tex2D(_FlowmapH,TRANSFORM_TEX(node_781, _FlowmapH));
                float2 node_5152 = (i.uv0+node_9952*float2(0,1));
                float4 _FlowmapV_var = tex2D(_FlowmapV,TRANSFORM_TEX(node_5152, _FlowmapV));
                float node_4337 = pow(saturate((1.0 - ((1.0 - length((i.uv0*2.0+-1.0)))+(_Size*-4.0+2.0)))),_Pow);
                float2 node_4183 = (lerp(float2(0,0),((i.uv0*-1.0+0.5)*lerp(_FlowmapH_var.rgb,_FlowmapV_var.rgb,0.5).rg),_DistoreEffect)*node_4337);
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + node_4183;
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float node_5020 = length(node_4183);
                float node_3355 = 0.0;
                float2 node_9142 = float2((node_5020*_CollorEffect),node_3355);
                float4 _ColorBar_var = tex2D(_ColorBar,TRANSFORM_TEX(node_9142, _ColorBar));
                float3 emissive = (_ColorBar_var.rgb*_ColorOpacity*node_5020);
                float3 finalColor = emissive;
                return fixed4(lerp(sceneColor.rgb, finalColor,(node_5020*node_4337)),1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
