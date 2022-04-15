using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class relitiveTransformLinker : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform linkTo;
    Vector3 transformDistance;
    public float smoothing = 0.5f;
    void Start()
    {
        transformDistance = transform.position - linkTo.position;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = (1-smoothing)*(linkTo.position + transformDistance)+smoothing*transform.position;
    }
}
