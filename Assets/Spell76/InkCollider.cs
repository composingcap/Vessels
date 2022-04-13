using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkCollider : MonoBehaviour
{
    
    Vector3 lastPosition;
    public OSC osc;
    private void Start()
    {
        lastPosition = transform.position;
        osc = GameObject.FindGameObjectWithTag("OSC").GetComponent<OSC>();
    }
    void Update()   {
        lastPosition = transform.position;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            print(collision.gameObject.name);
            InkCollision inkC = collision.gameObject.GetComponent<InkCollision>();
            if (inkC)
            {
                inkC.addCollision(lastPosition - transform.position);

                OscMessage m = new OscMessage();
                m.address = "/inkRiverTrigger/";
                m.values.Add(inkC.getCollisionPosition());
                m.values.Add(Vector3.Magnitude(lastPosition - transform.position));

                osc.Send(m);
            }
        }

    }

}
