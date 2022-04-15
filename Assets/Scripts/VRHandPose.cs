using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHandPose : MonoBehaviour
{
    public Transform trackedObject;
    Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = trackedObject.position;

        if (Vector3.Magnitude(transform.localPosition) > 0.01f)
        {
            transform.localPosition = startingPosition;
        }
    }
}
