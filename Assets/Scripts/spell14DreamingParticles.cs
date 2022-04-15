using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class spell14DreamingParticles : MonoBehaviour
{
    public float turbulanceTarget;
    public float turnbulanceDepth;
    public float turbulanceModifier;
    [Range(0,1)]
    public float interp;
    VisualEffect vfx;
    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        vfx.SetBool("AnimMode", interp == 0);
        float turbulence = turnbulanceDepth * turbulanceModifier * interp + turbulanceTarget * interp;
        vfx.SetFloat("Turbulence", turbulence);



    }

    public void setInterp(float v)
    {
        interp = v;
    }
    public void setTurbulence(float v)
    {
        turbulanceModifier = v;
    }
}
