Shader "Unlit/Void"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
float hash( float n ) { return frac(sin(n)*753.5453123); }
float noise( in float3 x )
{
    float3 p = floor(x);
    float3 f = frac(x);
    f = f*f*(3.0-2.0*f);
	
    float n = p.x + p.y*157.0 + 113.0*p.z;
    return lerp(lerp(lerp( hash(n+  0.0), hash(n+  1.0),f.x),
                   lerp( hash(n+157.0), hash(n+158.0),f.x),f.y),
               lerp(lerp( hash(n+113.0), hash(n+114.0),f.x),
                   lerp( hash(n+270.0), hash(n+271.0),f.x),f.y),f.z);
}
float n ( float3 x ) {
	float s = noise(x);
    for (float i = 2.; i < 10.; i++) {
    	s += noise(x/i)/i;
        
    }
    return s;
}
            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv*10.;
    float a = abs(n(float3(uv+_Time.y*3.14,sin(_Time.y)))-n(float3(uv+_Time.y,cos(_Time.y+3.))));
	return float4(0, .5-pow(a, .2)/2., 1.-pow(a, .2), 1);


            }
            ENDCG
        }
    }
}
