using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autorotate : MonoBehaviour
{

    public Vector3 roatationSpeed;
    public Vector3 wobbleSpeed;
    public Vector3 wobbleDepth;
    Vector3 lastWobble;
    Vector3 wobbleProgress;
    

    // Start is called before the first frame update
    void Start()
    {
        lastWobble = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 increment = Time.deltaTime * roatationSpeed * 360;
        Vector3 thisWobble = new Vector3();

        wobbleProgress += wobbleSpeed * Time.deltaTime * Mathf.PI*2;
        thisWobble.x = Mathf.Sin(wobbleProgress.x);
        thisWobble.y = Mathf.Sin(wobbleProgress.y);
        thisWobble.z = Mathf.Sin(wobbleProgress.z);
        Vector3 wobbleIncrement = (lastWobble - thisWobble);
        wobbleIncrement.Scale(wobbleDepth);
        transform.Rotate(wobbleIncrement);
        lastWobble = thisWobble;

        transform.Rotate(increment);


    }

}
