#ifndef MYSLSLINCLUDE_INCLUDED
#define MYSLSLINCLUDE_INCLUDED

float distanceToMandelbrot(float2 c)
{
#if 1
    {
        float c2 = dot(c, c);
        // skip computation inside M1 - http://iquilezles.org/www/articles/mset_1bulb/mset1bulb.htm
        if (256.0 * c2 * c2 - 96.0 * c2 + 32.0 * c.x - 3.0 < 0.0) return 0.0;
        // skip computation inside M2 - http://iquilezles.org/www/articles/mset_2bulb/mset2bulb.htm
        if (16.0 * (c2 + 2.0 * c.x + 1.0) - 1.0 < 0.0) return 0.0;
    }
#endif

    // iterate
    float di = 1.0;
    float2 z = float2(0.0,0.0);
    float m2 = 0.0;
    float2 dz = float2(0.0,0.0);
    for (int i = 0; i < 300; i++)
    {
        if (m2 > 1024.0) { di = 0.0; break; }

        // Z' -> 2�Z�Z' + 1
        dz = 2.0 * float2(z.x * dz.x - z.y * dz.y, z.x * dz.y + z.y * dz.x) + float2(1.0, 0.0);

        // Z -> Z� + c			
        z = float2(z.x * z.x - z.y * z.y, 2.0 * z.x * z.y) + c;

        m2 = dot(z, z);
    }

    // distance	
    // d(c) = |Z|�log|Z|/|Z'|
    float d = 0.5 * sqrt(dot(z, z) / dot(dz, dz)) * log(dot(z, z));
    if (di > 0.5) d = 0.0;

    return d;
}

void mandelbrot_float(float2 fragCoord, float2 iResolution, float iTime, out float fragColor)
{

    float2 p = (2.0 * fragCoord - iResolution.xy) / iResolution.y;

    // animation	
    float tz = 0.5 - 0.5 * cos(0.225 * iTime);
    float zoo = pow(0.5, 13.0 * tz);
    float2 c = float2(-0.05, 0.6805) + p * zoo;

    // distance to Mandelbrot
    float d = distanceToMandelbrot(c);

    // do some soft coloring based on distance
    d = clamp(pow(4.0 * d / zoo, 0.2), 0.0, 1.0);


    fragColor = d;
}
#endif