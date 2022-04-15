using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MudBun;
public class HeartAnimator : MonoBehaviour
{
    public MudSphere Lower;
    public MudSphere Upper;
    public MudSphere Left;
    public MudSphere Right;
    public MudRenderer render;
    public float radiusMax = 1f;
    public float radiusMin = 0.5f;
    float progress;
    float counter = 0;
    public float cycleTime = 3;
    OSC osc;
    float factor;
    [Range(0,1)]
    public float amount;
    OscMessage m;
    // Start is called before the first frame update
    void Start()
    {
        osc = GameObject.FindGameObjectWithTag("OSC").GetComponent<OSC>();
        m = new OscMessage();
    }

    // Update is called once per frame
    void Update()
    {
        if (cycleTime != 0)
        {
            counter += Time.deltaTime / cycleTime;

        }



        factor = (Mathf.Sin((counter * 3.3f ) * Mathf.PI * 2) + 1)*0.5f * amount;
        Lower.Radius = (factor * radiusMax + (1 - factor) * radiusMin)*0.5f + Lower.Radius*0.5f;

        factor = (Mathf.Sin((counter * 3) * Mathf.PI * 2) + 1) * 0.5f*amount;
        Upper.Radius = (factor * radiusMax + (1 - factor) * radiusMin) * 0.5f + Upper.Radius * 0.5f;

        factor = (Mathf.Sin((counter * 5.1f) * Mathf.PI * 2) + 1) * 0.5f* amount;
        Left.Radius = (factor * radiusMax + (1 - factor) * radiusMin) * 0.5f + Left.Radius * 0.5f;

        factor = (Mathf.Sin((counter * 4.7f) * Mathf.PI * 2) + 1) * 0.5f * amount;
        Right.Radius = (factor * radiusMax + (1 - factor) * radiusMin) * 0.5f + Right.Radius * 0.5f;

    }
    public void setAmount(float v)
    {
        amount = v;
    }

    public void setProgress(float v)
    {
        progress = v;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "VRController")
        {
            m.address = "/hitHeart";
            m.values.Clear();
            m.values.Add(1f);
            osc.Send(m);
        }
    }
}
