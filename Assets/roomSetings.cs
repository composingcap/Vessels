using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomSetings : MonoBehaviour
{
    OSC osc;
    OscMessage m;
    public float roomSize = 10;
    // Start is called before the first frame update
    void Start()
    {
        osc = GameObject.FindGameObjectWithTag("OSC").GetComponent<OSC>();
        m = new OscMessage();
        initalizeRoom();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initalizeRoom()
    {
        m.address = "/roomScale";
        m.values.Add(roomSize);
        osc.Send(m);
    }
}
