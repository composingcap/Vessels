using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRForarmPose : MonoBehaviour
{
    public Transform hand;
    public Transform upperArm;
    Vector3 initalPosition;
    Vector3 initalDistanceDelta;
    float initalHandDistance;
    float initalArmDistance;
    float handDistance;

    // Start is called before the first frame update
    void Start()
    {
        initalPosition = transform.position;
        initalHandDistance = Vector3.Distance(transform.localPosition, hand.localPosition);
        initalArmDistance = Vector3.Distance(transform.localPosition, upperArm.localPosition);
        initalDistanceDelta = upperArm.localPosition - transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition =  hand.localPosition - initalDistanceDelta;




    }
}
