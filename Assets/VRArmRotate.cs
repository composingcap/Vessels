using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRArmRotate : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform controller;
    public Quaternion rotationOffset;
    public Transform hand;
    Vector3 handStartingPosition;
    void Start()
    {
        handStartingPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(controller);
        transform.Rotate(rotationOffset.eulerAngles);
        hand.position = controller.position;

        if (Vector3.Magnitude(hand.localPosition) > 0.002f)
        {
            hand.localPosition = handStartingPosition;
            transform.LookAt(hand);
            transform.Rotate(rotationOffset.eulerAngles);
        }
    }
}
