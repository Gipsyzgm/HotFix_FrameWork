// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// version 1.01 update "Queue"="AlphaTest" from "Queue"="Transparent"
// version 1.02 update "Queue"="Transparent" from "Queue"="AlphaTest",不然alpha 乱序
Shader "Mogo/Bomber/ParticleMulAddBlend_uv2_rot" {
	Properties {
		_Color("color", Color) = (1,1,1,1)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_Xdif_speed("Xdif_speed", Float) = 0
	    _Ydif_speed("Ydif_speed", Float) = 0
	    _rotate1("RotateAngle", Float) = 0.0
		_rotatespeed1("RotateSpeed", Float) = 0.0
	    _rtx1("rtx", Float) = 0.5
	    _rty1("rty", Float) = 0.5
	    
	    _decal("decal", 2D) = "white" {}
	    _Xdecal_speed("Xdecal_speed", Float) = 0
	    _Ydecal_speed("Ydecal_speed", Float) = 0
	    _rotate2("RotateAngle", Float) = 0.0
		_rotatespeed2("RotateSpeed", Float) = 0.0// 0221  add fog()
	    _rtx2("rtx", Float) = 0.5
	    _rty2("rty", Float) = 0.5

		_Illumin_power("Illumin_power", Float) = 1
	    _Alpha("ALpha", Float) = 1
		//_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
	}

	Category {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend SrcAlpha One
		//AlphaTest Greater .01
		//Offset -1,-1
		ColorMask RGB
		Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
		BindChannels {
			Bind "Color", color
			Bind "Vertex", vertex
			//Bind "texcoord", texcoord
			//Bind "texcoord1", texcoord1
			//Bind "texcoord2", texcoord2
		}
		// ---- Fragment program cards
		SubShader {
			Pass {
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#pragma multi_compile_particles
	            #pragma target 3.0
				#include "UnityCG.cginc"

				sampler2D _MainTex;	
				float4 _Color;
				float _Xdif_speed;
	            float _Ydif_speed;
	            float _rotate1;
	            float _rtx1;
	            float _rty1;
	            float _rotatespeed1;
	            
	            sampler2D _decal;
	            float _Xdecal_speed;
	            float _Ydecal_speed;
	            float _rotate2;
	            float _rtx2;
	            float _rty2;
	            float _rotatespeed2;

				float _Illumin_power;
				float _Alpha;
	           
				struct appdata_t {
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					half2 texcoord : TEXCOORD; // uv1使用maintex
				};

				struct v2f {
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					half4 texcoord : TEXCOORD0;
					half4 texcoord2 : TEXCOORD2;
					//float2 uv_MainTex;
					//#ifdef SOFTPARTICLES_ON
					//float4 projPos : TEXCOORD1;
					//#endif
				};
				
				float4 _MainTex_ST;
				float4 _decal_ST;
				float4 _Mask_ST;
				float4 _lm_ST;

				v2f vert (appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					//#ifdef SOFTPARTICLES_ON
					//o.projPos = ComputeScreenPos (o.vertex);
					//COMPUTE_EYEDEPTH(o.projPos.z);
					//#endif
					o.color = v.color;
					o.texcoord.xy = TRANSFORM_TEX(v.texcoord,_MainTex);
					o.texcoord.zw = TRANSFORM_TEX(v.texcoord,_decal);
					o.texcoord2.xy = TRANSFORM_TEX(v.texcoord,_Mask);
					
					return o;
				}

				//sampler2D _CameraDepthTexture;
				//float _InvFade;
				
				float4 frag (v2f i) : COLOR
				{
					//dif uv pan + time
					float4 Multiply9=_Time * _Xdif_speed.xxxx;
	                float4 UV_Pan5=float4((i.texcoord.xyxy).x + Multiply9.x,(i.texcoord.xyxy).y,(i.texcoord.xyxy).z,(i.texcoord.xyxy).w);
	                float4 Multiply10=_Time * _Ydif_speed.xxxx;
	                float4 UV_Pan4=float4(UV_Pan5.x,UV_Pan5.y + Multiply10.x,UV_Pan5.z,UV_Pan5.w);
	                
	                //dif uv rotate + time
	                float Rspeed=_Time*_rotatespeed1*10;
		            float rad1 = float(radians(_rotate1)+Rspeed);
	                float tx=UV_Pan4.x-_rtx1;
		            float ty=UV_Pan4.y-_rty1;
		            float2 UV_rat1;
		            UV_rat1.x=float(tx*cos(rad1)-ty*sin(rad1));
		            UV_rat1.y=float(tx*sin(rad1)+ty*cos(rad1));
		            UV_rat1.x+=_rtx1;
		            UV_rat1.y+=_rty1;   
		            
	                //dif tex2D+ color
	                float4 Tex2D1=tex2D(_MainTex,UV_rat1.xy);
					float4 Multiply1=Tex2D1 * _Color;
	                
	                //decal uv pan + time
	                float4 Multiply7=_Time * _Xdecal_speed.xxxx;
	                float4 UV_Pan2=float4((i.texcoord.zwzw).x + Multiply7.x,(i.texcoord.zwzw).y,(i.texcoord.zwzw).z,(i.texcoord.zwzw).w);
	                float4 Multiply8=_Time * _Ydecal_speed.xxxx;
	                float4 UV_Pan3=float4(UV_Pan2.x,UV_Pan2.y + Multiply8.x,UV_Pan2.z,UV_Pan2.w);
		            
		            //decal uv rotate + time
		            float Rspeed2=_Time*_rotatespeed2*10;
		            float rad2 = float(radians(_rotate2)+Rspeed2);
	                float tx2=UV_Pan3.x-_rtx2;
		            float ty2=UV_Pan3.y-_rty2;
		            float2 UV_rat2;
		            UV_rat2.x=float(tx2*cos(rad2)-ty2*sin(rad2));
		            UV_rat2.y=float(tx2*sin(rad2)+ty2*cos(rad2));
		            UV_rat2.x+=_rtx2;
		            UV_rat2.y+=_rty2;
		           
	                //decalmap tex2D + (dif tex2D+ color)
	                float4 Tex2D2=tex2D(_decal,UV_rat2.xy);
	                float4 Multiply6=Multiply1 * Tex2D2;

					float4 Multiply3=Multiply6 * _Illumin_power.xxxx;
					
					float Saturate0=saturate(_Alpha.xxxx);

	                float4 Lerp0=lerp(float4( 0,0,0,0 ),Multiply3,Saturate0);
					
					return i.color * Lerp0;
	                
				}
				ENDCG 
			}
		} 	
		
		// ---- Dual texture cards
		SubShader {
			Pass {
				SetTexture [_MainTex] {
					constantColor [_TintColor]
					combine constant * primary
				}
				SetTexture [_MainTex] {
					combine texture * previous DOUBLE
				}
			}
		}
		
		// ---- Single texture cards (does not do color tint)
		SubShader {
			Pass {
				SetTexture [_MainTex] {
					combine texture * primary
				}
			}
		}
	}
}
