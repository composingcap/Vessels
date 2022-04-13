using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRBodyTracker : MonoBehaviour
{
    public Transform trackedObject;
    Quaternion rotation = new Quaternion();
    Vector3 targetRotation = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = trackedObject.position;
        targetRotation.y = trackedObject.rotation.eulerAngles.y;
        targetRotation.x = transform.rotation.eulerAngles.x;
        targetRotation.z = transform.rotation.eulerAngles.z;

        rotation.eulerAngles = targetRotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.5f);
    }
}
