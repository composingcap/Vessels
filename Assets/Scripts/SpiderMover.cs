using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MudBun;
using Umath = Unity.Mathematics;
public class SpiderMover : MonoBehaviour
{
    public Transform snakeTailEnd1;
    public Transform snakeTailEnd2;
    public MudBrushGroup legParent;
    public MudBrushGroup mouth;
    public float mouthGrow= 0.2f;
    public float mouthGrowAmount = 0;
    public float stepTresh;
    bool takeStep;
    List<LegStepper> legsteppers;
    public Transform lookAt;
    Vector3 initalRotation;
    MudSphere mouthHole;
    Vector3 mouthHoleInitalSize;
    Vector3 lastStep;

    Quaternion lastRot;
    Vector3 lastPosition;
    Vector3 targetPosition;
    Vector3 startPosition;
    Vector3 currentPosition;
    Vector3 filterPosition;
    float moveTime = 0.25f;
    float moveProgress = 0;
    
    float maxDistance = 1.5f;
    public float movmentIntensity; 
    // Start is called before the first frame update
    void Start()
    {
        snakeTailEnd1.parent = transform;
        snakeTailEnd2.parent = transform;
        lastStep = transform.position;
        lastRot = transform.rotation;
        startPosition = transform.position;
        legsteppers = new List<LegStepper>();
        initalRotation = transform.rotation.eulerAngles;
        MudCurveSimple[] legs = legParent.GetComponentsInChildren<MudCurveSimple>();
        mouthHole = mouth.GetComponentInChildren<MudSphere>();
        mouthHoleInitalSize = mouthHole.transform.localScale;
        foreach (MudCurveSimple leg in legs)
        {
            LegStepper stepper = new LegStepper(leg);
            legsteppers.Add(stepper);
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookAt);
        Vector3 rotation = initalRotation;
        rotation.y = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(rotation);

        if((Vector3.Distance(lastStep,transform.position) > stepTresh) || Quaternion.Angle(lastRot,transform.rotation)> 5)
        {
            takeStep = true;
            lastStep = transform.position;
            lastRot = transform.rotation;
        }


        if (takeStep)
        {

            List<LegStepper> possibleSteppers = new List<LegStepper>();

            for (int i = 0; i < legsteppers.Count; i++)
            {
                if (!legsteppers[i].stepping)
                {
                    possibleSteppers.Add(legsteppers[i]);
                }
            }
            if (possibleSteppers.Count > 0)
            {
                possibleSteppers[Random.Range(0, possibleSteppers.Count)].initializeStep();
            }


        }
        foreach (LegStepper stepper in legsteppers)
        {
            stepper.animateStep();
        }
        takeStep = false;
        Vector3 newMouthSize = mouthHoleInitalSize;
        newMouthSize.y = Mathf.Lerp(mouthHoleInitalSize.y, mouthGrow, mouthGrowAmount);
        mouthHole.transform.localScale = newMouthSize;


        autoMoveSpider();


    }
    class LegStepper
    {

        MudCurveSimple leg;
        public float stepProgress;
        public bool stepping;
        public float stepTime = 0.5f;
        public float stepHeight = 0.1f;
        Vector3 pointBPosition;
        Vector3 pointAPosition;

        public LegStepper(MudCurveSimple leg)
        {
            this.leg = leg;
            pointBPosition = leg.PointB.localPosition;
            pointAPosition = leg.PointA.localPosition;
        }
        public void animateStep()
        {
            if (stepping)
            {
                stepProgress += Time.deltaTime / stepTime;
                stepProgress = Mathf.Clamp01(stepProgress);

                Vector3 newPos = pointBPosition;
                newPos.y += Mathf.Sin(Mathf.PI * stepProgress) * 0.5f;
                leg.PointB.localPosition = newPos;

                newPos = pointAPosition;
                newPos.y += Mathf.Sin(Mathf.PI * stepProgress) * 0.5f*0.5f;
                leg.PointA.localPosition = newPos;

                if (stepProgress >= 1)
                {
                    stepping = false;
                }
            }
        }

        public void initializeStep()
        {
            stepping = true;
            stepProgress = 0;
        }
    }

    void autoMoveSpider()
    {
        if (movmentIntensity > 0)
        {
            if (moveProgress == 0)
            {
                moveTime = Random.Range(0.2f, 0.5f);
                lastPosition = transform.position;
                targetPosition = lastPosition;
                filterPosition = lastPosition;
                targetPosition.x += Random.Range(0.1f* movmentIntensity, 0.5f* movmentIntensity) * (Random.Range(0, 2) * 2 - 1);
                targetPosition.z += Random.Range(0.1f* movmentIntensity, 0.5f* movmentIntensity) * (Random.Range(0, 2) * 2 - 1);


            }

            moveProgress += Time.deltaTime / moveTime;

            currentPosition = Umath.math.lerp(lastPosition, targetPosition, moveProgress);

            filterPosition = currentPosition * 0.5f + filterPosition * 0.5f;

            //filterPosition.x = startPosition.x + Umath.math.tanh(Mathf.Abs(filterPosition.x - startPosition.x) / maxDistance) * maxDistance; //Soft clip
            //filterPosition.z = startPosition.z + Umath.math.tanh(Mathf.Abs(filterPosition.z - startPosition.z) / maxDistance) * maxDistance; //Soft clip
            filterPosition.x = startPosition.x + Mathf.Clamp((filterPosition.x - startPosition.x), -maxDistance, maxDistance);
            filterPosition.z = startPosition.z + Mathf.Clamp((filterPosition.z - startPosition.z), -maxDistance, maxDistance);
            transform.position = filterPosition;

            if (moveProgress >= 1)
            {
                moveProgress = 0;
            }
        }

    }

    public void setMouth(float v)
    {
        mouthGrowAmount = v;
    }

    public void setMovmentIntensity(float v)
    {
        movmentIntensity = v;
    }

    


}
