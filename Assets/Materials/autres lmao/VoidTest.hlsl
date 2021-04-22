float hash(float n)
{
    return frac(sin(n) * 753.5453123);
}
float noise(in float3 x)
{
    float3 p = floor(x);
    float3 f = frac(x);
    f = (f * f * (3.0 - 2.0 * f));
	
    float n = (p.x + p.y * 157.0 + 113.0 * p.z);
    return lerp(lerp(lerp(hash(n + 0.0), hash(n + 1.0), f.x),lerp(hash(n + 157.0), hash(n + 158.0), f.x), f.y),lerp(lerp(hash(n + 113.0), hash(n + 114.0), f.x),lerp(hash(n + 270.0), hash(n + 271.0), f.x), f.y), f.z);
}
float n(float3 x)
{
    float s = noise(x);
    for (float i = 2.; i < 10.; i++)
    {
        s += noise(x / i) / i;
        
    }
    return s;
}

void darkness_float(float2 uv, float1 _Time, out float4 o)
{

    float a = abs(n(float3(uv + _Time * 3.14, sin(_Time))) - n(float3(uv + _Time, cos(_Time + 3.))));
    o = float4(0, .5 - pow(a, .2) / 2., 1. - pow(a, .2), 1);
}
