using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWalker : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 startingPosition;
    Vector3 lastStep;
    Vector3 currentPosition;
    bool left;
    bool walk;
    public float movmentTreshhold = 0f;
    public float stepSize = 0.5f;
    public float stepTime = 0.5f;
    Vector3 smoothPosition;
    Vector3 lastPosition;

    public Transform leftKnee;
    public Transform leftFoot;
    public Transform rightKnee;
    public Transform rightFoot;
    float progress;
    float footHeight;
    float kneeHeight;

    public float stepHeight = 1f;

    void Start()
    {
        currentPosition = transform.position;
        currentPosition.y = 0;
        startingPosition = currentPosition;
        lastStep = currentPosition;
        lastPosition = currentPosition;
        left = true;
        walk = false;

        kneeHeight = leftKnee.position.y;
        footHeight = leftFoot.position.y;

        Vector3 rightFootPosition = rightFoot.position;
        rightFootPosition.y = footHeight;
        rightFoot.position = rightFootPosition;
        Vector3 rightKneePosition = rightKnee.position;
        rightFootPosition.y = kneeHeight;
        rightKnee.position = rightKneePosition;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = transform.position;
        currentPosition.y = 0;

        smoothPosition = currentPosition * 0.5f + lastPosition * 0.5f;
        Vector3 walkDelta = lastStep - currentPosition;
        walkDelta.y = 0;
        if (Vector3.Magnitude(walkDelta) > movmentTreshhold && !walk)
        {
            walk = true;
            lastStep = lastPosition;
            left = !left;
            progress = 0;

        }

        if (walk)
        {
            progress  += Time.deltaTime/stepTime;

            progress = Mathf.Clamp01(progress);

            float amplitude = Mathf.Sin(progress * Mathf.PI);

            float stepDelta = amplitude * stepHeight;

            if (!left)
            {
                Vector3 rightFootPosition = rightFoot.position;
                rightFootPosition.y = stepDelta + footHeight;
                rightFoot.position = rightFootPosition;

                Vector3 rightKneePosition = rightKnee.position;
                rightKneePosition.y = stepDelta + kneeHeight;
                rightKnee.position = rightKneePosition;
            }
            else
            {

                Vector3 leftFootPosition = leftFoot.position;
                leftFootPosition.y = stepDelta + footHeight;
                leftFoot.position = leftFootPosition;

                Vector3 leftKneePosition = leftKnee.position;
                leftKneePosition.y = stepDelta + kneeHeight;
                leftKnee.position = leftKneePosition;
            }

      

            if (progress >=1 || progress == 0)
            {
                walk = false;

            }



        }
        else
        {

        }



        lastPosition = smoothPosition;
    }
}
