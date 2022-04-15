using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Spell106SopranoAnimator : MonoBehaviour
{
    public waypointFollower waypointFollower;
    public VisualEffect sopranoParticles;
    public Light sopranoLight;
    public float pathTime;
    [Range(0,1)]
    public float intensity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pathTime != 0)
        {
            waypointFollower.pathProgress = (waypointFollower.pathProgress + Time.deltaTime / pathTime)%1;
        }

        sopranoParticles.SetFloat("Intensity", intensity);
        sopranoLight.intensity = 1 + intensity * 5;


    }

    public void setIntensity(float v)
    {
        intensity = v;
    }
}
