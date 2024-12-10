Shader "UI/RoundedCorners/RoundedCorners"
{
  Properties
  {
    [HideInInspector] _MainTex ("Texture", 2D) = "white" {}
    [HideInInspector] _StencilComp ("Stencil Comparison", float) = 8
    [HideInInspector] _Stencil ("Stencil ID", float) = 0
    [HideInInspector] _StencilOp ("Stencil Operation", float) = 0
    [HideInInspector] _StencilWriteMask ("Stencil Write Mask", float) = 255
    [HideInInspector] _StencilReadMask ("Stencil Read Mask", float) = 255
    [HideInInspector] _ColorMask ("Color Mask", float) = 15
    [HideInInspector] _UseUIAlphaClip ("Use Alpha Clip", float) = 0
    _WidthHeightRadius ("WidthHeightRadius", Vector) = (0,0,0,0)
  }
  SubShader
  {
    Tags
    { 
      "QUEUE" = "Transparent"
      "RenderType" = "Transparent"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZWrite Off
      Cull Off
      Stencil
      { 
        Ref 0
        ReadMask 0
        WriteMask 0
        Pass Keep
        Fail Keep
        ZFail Keep
        PassFront Keep
        FailFront Keep
        ZFailFront Keep
        PassBack Keep
        FailBack Keep
        ZFailBack Keep
      } 
      Blend SrcAlpha OneMinusSrcAlpha
      ColorMask 0
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _WidthHeightRadius;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float2 texcoord :TEXCOORD0;
          float4 color :COLOR0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float4 color :COLOR0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
          float4 color :COLOR0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          out_v.texcoord.xy = in_v.texcoord.xy;
          out_v.vertex = UnityObjectToClipPos(in_v.vertex);
          out_v.color = in_v.color;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float2 u_xlat0_d;
      float4 u_xlat16_1;
      float u_xlat2;
      float2 u_xlat4;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d.xy = (in_f.texcoord.xy + float2(-0.5, (-0.5)));
          u_xlat0_d.xy = (u_xlat0_d.xy * _WidthHeightRadius.xy);
          u_xlat4.x = (_WidthHeightRadius.z * 0.5);
          u_xlat4.xy = ((_WidthHeightRadius.xy * float2(0.5, 0.5)) + (-u_xlat4.xx));
          u_xlat0_d.xy = ((-u_xlat4.xy) + abs(u_xlat0_d.xy));
          u_xlat4.xy = max(u_xlat0_d.xy, float2(0, 0));
          u_xlat0_d.x = max(u_xlat0_d.y, u_xlat0_d.x);
          u_xlat0_d.x = min(u_xlat0_d.x, 0);
          u_xlat2 = length(u_xlat4.xy);
          u_xlat0_d.x = (u_xlat0_d.x + u_xlat2);
          u_xlat0_d.x = (((-_WidthHeightRadius.z) * 0.5) + u_xlat0_d.x);
          u_xlat2 = ddx(u_xlat0_d.x);
          u_xlat4.x = ddy(u_xlat0_d.x);
          u_xlat2 = (abs(u_xlat4.x) + abs(u_xlat2));
          u_xlat0_d.x = (((-u_xlat2) * 0.5) + u_xlat0_d.x);
          u_xlat2 = (float(1) / (-u_xlat2));
          u_xlat0_d.x = (u_xlat2 * u_xlat0_d.x);
          #ifdef UNITY_ADRENO_ES3
          u_xlat0_d.x = min(max(u_xlat0_d.x, 0), 1);
          #else
          u_xlat0_d.x = clamp(u_xlat0_d.x, 0, 1);
          #endif
          u_xlat2 = ((u_xlat0_d.x * (-2)) + 3);
          u_xlat0_d.x = (u_xlat0_d.x * u_xlat0_d.x);
          u_xlat0_d.x = (u_xlat0_d.x * u_xlat2);
          u_xlat16_1 = tex2D(_MainTex, in_f.texcoord.xy);
          u_xlat16_1 = (u_xlat16_1 * in_f.color);
          u_xlat0_d.x = min(u_xlat0_d.x, u_xlat16_1.w);
          out_f.color.xyz = u_xlat16_1.xyz;
          out_f.color.w = u_xlat0_d.x;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
