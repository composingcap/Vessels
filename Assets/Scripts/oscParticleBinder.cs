using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class oscParticleBinder : MonoBehaviour
{

    
    public enum particleMods {none,emission};
    public particleMods mod;
    public OSC osc;
    public string OSCAdress;
    ParticleSystem particleSystem;

    void Start()
    {
        osc = GameObject.FindGameObjectWithTag("OSC").GetComponent<OSC>();
        particleSystem = GetComponent<ParticleSystem>();
        osc.SetAddressHandler(OSCAdress, particlebinder);



    }

    // Update is called once per frame
    void particlebinder(OscMessage message)
    {


            if (particleSystem != null)
            {
                if (mod == particleMods.emission)
                {
                    
                    var emmiter = particleSystem.emission;
                    emmiter.rateOverTime = (float)message.values[0];
                }
            }
        
    }
}
