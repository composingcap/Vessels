using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ViolaSystemControler : MonoBehaviour
{
    VisualEffect vfx;
    public float intensity;
    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        vfx.SetFloat("SpawnRate", intensity * 500);
        vfx.SetFloat("Radius", 0.25f + intensity * 1.75f);
    }

    public void setIntensity(float v)
    {
        intensity = v;
    }
}
