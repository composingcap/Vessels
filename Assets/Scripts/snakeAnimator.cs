using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MudBun;

public class snakeAnimator : MonoBehaviour
{
    #region publicVars

    public Transform lookAt;

    public MudBrushGroup lowerJaw;
    public MudCurveFull snakeBody;
    public MudBrushGroup snakeHead;
    [Header("Lower Jaw Animation")]
    public float openDepth = 25;
    [Range(0, 1)] public float mouthOpen;

    [Header("Head Animation")]
    [Range(0,1)] float movmentAmount = 1;
    public Vector3 maxDistance = new Vector3(1, 1, 1);
    public Vector2 stepDistanceRange = new Vector2 ( 0.1f, 0.5f );
    public Vector2 stepTimeRange = new Vector2(0.5f, 2f);
    #endregion
    #region privateVars

    // Jaw Movment

    private Vector3 lowerJawStartRotation;

    //Head Movment

    float headMovmentProgress = 0;
    float thisHeadMovmentTime = 0;
    Vector3 headMovmentDestination;
    Vector3 headStartPosition;
    Vector3 lastHeadPosition;

    #endregion


    #region unityFunctions
    // Start is called before the first frame update
    void Start()
    {
        lowerJawStartRotation = lowerJaw.transform.localEulerAngles;
        headStartPosition = snakeHead.transform.position;
        lastHeadPosition = headStartPosition;
    }

    // Update is called once per frame
    void Update()
    {
        lowerJaw.transform.localEulerAngles = new Vector3(lowerJawStartRotation.x - openDepth * mouthOpen, lowerJawStartRotation.y, lowerJawStartRotation.z );

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
        snakeHead.transform.position = lastHeadPosition * (1 - headMovmentProgress) + headMovmentDestination * headMovmentProgress;
        if (headMovmentProgress >= 1.0)
        {
            headMovmentProgress = 0;
        }

        
        snakeHead.transform.LookAt(lookAt);

    }
    #endregion

    public void setMouthOpen(float amount)
    {
        mouthOpen = amount; 
    }
}
