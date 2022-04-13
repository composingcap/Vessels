using System.Collections.Generic;
using UnityEngine;
using Umath = Unity.Mathematics;

public class SpiderMoverMesh : MonoBehaviour
{
    public SkinnedMeshRenderer spiderMesh;
    public float Ah;
    public float Oh;
    public float Ee;

    public Transform snakeTailEnd1;

    //public Transform snakeTailEnd2;
    public Transform mouth;

    public float mouthGrow = 0.2f;
    public float mouthGrowAmount = 0;
    public float stepTresh;
    private bool takeStep;
    private List<LegStepper> legsteppers;
    public Transform lookAt;
    private Vector3 initalRotation;
    public Transform mouthHole;
    private Vector3 mouthHoleInitalSize;
    private Vector3 lastStep;
    public Transform[] legs;
    private Quaternion lastRot;
    private Vector3 lastPosition;
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private Vector3 currentPosition;
    private Vector3 filterPosition;
    private Vector3 mouthPositionStart;
    private Vector3 mouthPosition;
    private float moveTime = 0.25f;
    private float moveProgress = 0;

    private float maxDistance = 1.5f;
    public float movmentIntensity;

    // Start is called before the first frame update
    private void Start()
    {
        snakeTailEnd1.parent = transform;
        lastStep = transform.position;
        lastRot = transform.rotation;
        startPosition = transform.position;
        legsteppers = new List<LegStepper>();
        initalRotation = transform.rotation.eulerAngles;
        //mouthHoleInitalSize = mouthHole.transform.localScale;
        foreach (Transform leg in legs)
        {
            LegStepper stepper = new LegStepper(leg);
            legsteppers.Add(stepper);
        }
        mouthPositionStart = mouthHole.localPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(lookAt); //Either looks at the audience or VR player
        Vector3 rotation = initalRotation;
        rotation.y = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(rotation);

        if ((Vector3.Distance(lastStep, transform.position) > stepTresh) || Quaternion.Angle(lastRot, transform.rotation) > 5)
        {
            //Based on the distance and rotation, take a step
            takeStep = true;
            lastStep = transform.position;
            lastRot = transform.rotation;
        }
        //Animate the mouth blend shapes to vary the shape of the mouth
        spiderMesh.SetBlendShapeWeight(0, Ah * 100);
        spiderMesh.SetBlendShapeWeight(1, Oh * 100);
        spiderMesh.SetBlendShapeWeight(2, Ee * 100);

        if (takeStep)
        {
            //Select a let to step and add it to a list of animations
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
            //Iterates through every leg marked for animation and runs it to completion
            stepper.animateStep();
        }
        takeStep = false;
        mouthPosition = mouthHole.localPosition;
        mouthPosition.x = mouthPositionStart.x + mouthGrow * mouthGrowAmount * 0.1f;
        mouthHole.localPosition = mouthPosition;

        //This function moves the spider around based on how loud it sings
        autoMoveSpider();
    }

    public void setAh(float v)
    {
        Ah = v;
    }

    public void setOh(float v)
    {
        Oh = v;
    }

    public void setEe(float v)
    {
        Ee = v;
    }

    private class LegStepper
    {
        private Transform leg; //Represents the leg joint to move
        public float stepProgress; //Stores the progress
        public bool stepping; // Is it stepping or free?
        public float stepTime = 0.5f; //Time for each step
        public float stepHeight = 20f; //The height of each step
        private Quaternion initalRotation; //Start rotation
        private Quaternion targetRotation; //End Rotation

        public LegStepper(Transform leg)
        {
            //Initialize the stepper with the transform
            this.leg = leg;
            initalRotation = leg.localRotation;
            Vector3 eRot = initalRotation.eulerAngles;
            eRot.x += stepHeight;
            targetRotation.eulerAngles = eRot;
        }

        public void initializeStep()
        {
            Vector3 eRot = initalRotation.eulerAngles;
            eRot.x += stepHeight;
            targetRotation.eulerAngles = eRot;
            stepping = true;
            stepProgress = 0;
        }

        public void animateStep()
        {
            if (stepping)
            {
                //Fade between the initial rotation and the target rotation over the stepTime
                stepProgress += Time.deltaTime / stepTime;
                stepProgress = Mathf.Clamp01(stepProgress);
                leg.localRotation = Quaternion.Lerp(targetRotation, initalRotation, (Mathf.Sin(stepProgress * 6.28f) + 1) * 0.5f);

                if (stepProgress >= 1)
                {
                    stepping = false;
                    //When it is done set the stepping boolean to false
                }
            }
        }
    }

    private void autoMoveSpider()
    {
        if (movmentIntensity > 0)
        {
            if (moveProgress == 0)
            {
                moveTime = Random.Range(0.2f, 0.5f);
                lastPosition = transform.position;
                targetPosition = lastPosition;
                filterPosition = lastPosition;
                targetPosition.x += Random.Range(0.1f * movmentIntensity, 0.5f * movmentIntensity) * (Random.Range(0, 2) * 2 - 1);
                targetPosition.z += Random.Range(0.1f * movmentIntensity, 0.5f * movmentIntensity) * (Random.Range(0, 2) * 2 - 1);
            }

            moveProgress += Time.deltaTime / moveTime;
            currentPosition = Umath.math.lerp(lastPosition, targetPosition, moveProgress);
            filterPosition = currentPosition * 0.5f + filterPosition * 0.5f;
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