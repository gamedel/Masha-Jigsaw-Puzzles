// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ASESampleShaders/Sprites/SimpleSprite"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		_Alpha("Alpha", Float) = 0
		_TypeColor("TypeColor", Int) = 0
		_Porog("Porog", Float) = 0.27
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		
		
		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"
			

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
				
			};
			
			uniform fixed4 _Color;
			uniform float _EnableExternalAlpha;
			uniform sampler2D _MainTex;
			uniform sampler2D _AlphaTex;
			uniform int _TypeColor;
			uniform float4 _MainTex_ST;
			uniform float _Porog;
			uniform float _Alpha;
			float3 RGBToHSV(float3 c)
			{
				float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
				float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
				float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
				float d = q.x - min( q.w, q.y );
				float e = 1.0e-10;
				return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
			}
			float3 HSVToRGB( float3 c )
			{
				float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
				float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
				return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
			}
			
			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				
				
				IN.vertex.xyz +=  float3(0,0,0) ; 
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if ETC1_EXTERNAL_ALPHA
				// get the color from an external texture (usecase: Alpha support for ETC1 on android)
				fixed4 alpha = tex2D (_AlphaTex, uv);
				color.a = lerp (color.a, alpha.r, _EnableExternalAlpha);
#endif //ETC1_EXTERNAL_ALPHA

				return color;
			}
			
			fixed4 frag(v2f IN  ) : SV_Target
			{
				float2 uv_MainTex = IN.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode2 = tex2D( _MainTex, uv_MainTex );
				float3 hsvTorgb85 = RGBToHSV( tex2DNode2.rgb );
				float3 hsvTorgb89 = HSVToRGB( float3(hsvTorgb85.x,hsvTorgb85.y,hsvTorgb85.z) );
				float4 appendResult47 = (float4((( _TypeColor == 0 ) ? (( hsvTorgb85.z < _Porog ) ? hsvTorgb89 :  float3(1,1,1) ) :  (( hsvTorgb85.z < _Porog ) ? hsvTorgb89 :  float3(1,0,0) ) ).x , (( _TypeColor == 0 ) ? (( hsvTorgb85.z < _Porog ) ? hsvTorgb89 :  float3(1,1,1) ) :  (( hsvTorgb85.z < _Porog ) ? hsvTorgb89 :  float3(1,0,0) ) ).y , (( _TypeColor == 0 ) ? (( hsvTorgb85.z < _Porog ) ? hsvTorgb89 :  float3(1,1,1) ) :  (( hsvTorgb85.z < _Porog ) ? hsvTorgb89 :  float3(1,0,0) ) ).z , ( tex2DNode2.a * _Alpha )));
				
				fixed4 c = appendResult47;
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15001
139;464;1858;817;1576.074;790.8335;1.418664;True;True
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;1;-1143.711,-198.5712;Float;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-1137.206,-69.88612;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RGBToHSVNode;85;-815.1589,-196.4135;Float;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.HSVToRGBNode;89;-543.2875,-241.8107;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;87;-670.7109,240.5349;Float;False;Property;_Porog;Porog;2;0;Create;True;0;0;False;0;0.27;0.02;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;90;-346.0931,-61.64048;Float;False;Constant;_Vector0;Vector 0;2;0;Create;True;0;0;False;0;1,1,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;84;-1049.091,-658.815;Float;False;Constant;_Vector1;Vector 1;2;0;Create;True;0;0;False;0;1,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.IntNode;81;-133.7047,-675.817;Float;False;Constant;_Int0;Int 0;2;0;Create;True;0;0;False;0;0;0;0;1;INT;0
Node;AmplifyShaderEditor.TFHCCompareLower;86;-71.19695,-177.971;Float;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.IntNode;79;40.23249,-655.2197;Float;False;Property;_TypeColor;TypeColor;1;0;Create;True;0;0;False;0;0;1;0;1;INT;0
Node;AmplifyShaderEditor.TFHCCompareLower;91;-652.5239,-697.2018;Float;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TFHCCompareEqual;80;-287.6338,-504.5229;Float;False;4;0;INT;0;False;1;INT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;51;-824.0513,114.0062;Float;False;Property;_Alpha;Alpha;0;0;Create;True;0;0;False;0;0;0.53;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-305.3948,122.2686;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;46;313.4399,-477.4951;Float;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.DynamicAppendNode;47;316.2975,-86.27708;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;654.0347,-193.2152;Float;False;True;2;Float;ASEMaterialInspector;0;4;ASESampleShaders/Sprites/SimpleSprite;0f8ba0101102bb14ebf021ddadce9b49;0;0;;2;True;3;One;OneMinusSrcAlpha;0;One;Zero;False;True;Off;False;False;True;2;False;False;True;5;Queue=Transparent;IgnoreProjector=True;RenderType=Transparent;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;0;0;False;False;False;False;False;False;False;False;False;True;2;0;0;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;0
WireConnection;85;0;2;0
WireConnection;89;0;85;1
WireConnection;89;1;85;2
WireConnection;89;2;85;3
WireConnection;86;0;85;3
WireConnection;86;1;87;0
WireConnection;86;2;89;0
WireConnection;86;3;90;0
WireConnection;91;0;85;3
WireConnection;91;1;87;0
WireConnection;91;2;89;0
WireConnection;91;3;84;0
WireConnection;80;0;79;0
WireConnection;80;1;81;0
WireConnection;80;2;86;0
WireConnection;80;3;91;0
WireConnection;52;0;2;4
WireConnection;52;1;51;0
WireConnection;46;0;80;0
WireConnection;47;0;46;0
WireConnection;47;1;46;1
WireConnection;47;2;46;2
WireConnection;47;3;52;0
WireConnection;0;0;47;0
ASEEND*/
//CHKSM=4EAE6C353A4D5DBDD05E7D715BCA2939973C2B1D