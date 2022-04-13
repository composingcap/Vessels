using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOutputs : MonoBehaviour
{
    Display screen1;
    Display screen2;
    Display screen3;
    public RenderTexture outputTexture;
    // Start is called before the first frame update
    void enable()
    {
        screen1.Activate();
        screen2.Activate();
        screen3.Activate();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
