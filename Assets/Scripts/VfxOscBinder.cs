using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VfxOscBinder : MonoBehaviour
{
    // Start is called before the first frame update
    public string parameterName;
    public OSC osc;
    public string OSCAdress;
    VisualEffect vfx;
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
        osc = GameObject.FindGameObjectWithTag("OSC").GetComponent<OSC>();
        osc.SetAddressHandler(OSCAdress, vfxbinder);


    }

    // Update is called once per frame
    void vfxbinder(OscMessage message)
    {
        if (vfx)
        {
            vfx.SetFloat(parameterName, (float)message.values[0]);
        }
        
    }

}
