using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class snakeAnimatorArmiture : MonoBehaviour
{
    #region publicVars

    public Transform lookAt;

    public Transform lowerJaw;
    public Transform snakeBody;
    public Transform snakeHead;
    [Header("Lower Jaw Animation")]
    public float openDepth = 25;
    [Range(0, 1)] public float mouthOpen;

    [Header("Head Animation")]
    [Range(0,1)] float movmentAmount = 1;
    public Vector3 maxDistance = new Vector3(1, 1, 1);
    public Vector2 stepDistanceRange = new Vector2 ( 0.1f, 0.5f );
    public Vector2 stepTimeRange = new Vector2(0.5f, 2f);
    public Quaternion mouthOpenAngle;
    public Quaternion mouthClosedAngle;
    public Vector3 rotationCorrection;

    Vector3 lastVelocity;
    Vector3 currentVelocity;

    Material snakeSkin;

    #endregion
    #region privateVars

    // Jaw Movement

    private Vector3 lowerJawStartRotation;

    //Head Moment

    float headMovmentProgress = 0;
    float thisHeadMovmentTime = 0;
    Vector3 headMovmentDestination;
    Vector3 currentPosition;
    Vector3 lastPosition;
    Vector3 headStartPosition;
    Vector3 lastHeadPosition;
    
    #endregion


    #region unityFunctions
    // Start is called before the first frame update
    void Start()
    {
        headStartPosition = snakeHead.transform.position;
        lastHeadPosition = headStartPosition;
        lastPosition = lastHeadPosition;
        snakeSkin = GetComponentInChildren<SkinnedMeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {

        lowerJaw.transform.localRotation = Quaternion.Lerp(mouthClosedAngle, mouthOpenAngle, mouthOpen);
        if (headMovmentProgress == 0)
        {
            lastHeadPosition = snakeHead.transform.position;

            thisHeadMovmentTime = Random.Range(stepTimeRange[0], stepTimeRange[1]);
            for (int i = 0; i < 3; i++) {
                headMovmentDestination[i] = lastHeadPosition[i] + (Random.Range(stepDistanceRange.x, stepDistanceRange.y) * movmentAmount * (Random.Range(0,2)*2-1));
                headMovmentDestination[i] = Mathf.Clamp(headMovmentDestination[i], headStartPosition[i]-maxDistance[i], headStartPosition[i]+maxDistance[i]);
                    }
        }

        headMovmentProgress += Time.deltaTime / thisHeadMovmentTime;
        headMovmentProgress = Mathf.Clamp01(headMovmentProgress);
        currentPosition = lastHeadPosition * (1 - headMovmentProgress) + headMovmentDestination * headMovmentProgress;
        currentPosition = currentPosition * 0.1f + lastPosition * 0.9f;
        currentVelocity = currentPosition - lastPosition;
        currentVelocity = currentVelocity * 0.05f + lastVelocity * 0.95f;
        snakeHead.transform.position = lastPosition + currentVelocity;        
        lastVelocity = currentVelocity;
        lastPosition = snakeHead.transform.position;
        if (headMovmentProgress >= 0.9f)
        {
            headMovmentProgress = 0;
        }

        
        snakeHead.transform.LookAt(lookAt);
        snakeHead.transform.Rotate(rotationCorrection);

    }
    #endregion

    public void setMouthOpen(float amount)
    {
        mouthOpen = amount; 
    }

    [Button]
    public void setMouthOpenAngleToCurrent() {
        mouthOpenAngle = lowerJaw.localRotation;
    }
    [Button]
    public void setMouthClosedAngleToCurrent()
    {
        mouthClosedAngle = lowerJaw.localRotation;
    }

    public void setWiggle(float v)
    {
        snakeSkin.SetFloat("_NoiseOffset", v);
    }
}
