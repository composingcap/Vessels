using UnityEngine;
using Valve.VR;

public class VesselsAvatarAnimator : MonoBehaviour
{
    public Transform avatar; //Tracked avatar
    public Transform HMD; //HMD for the vr player

    [Header("Legs")]
    public Transform hipLeft;

    public Transform hipRight;
    public Transform kneeLeft;
    public Transform kneeRight;
    public Transform footLeft;
    public Transform footRight;

    [Header("Steps")]
    public Transform groudPlane;

    public Quaternion kneeUp;
    public Quaternion hipUp;
    public float stepThreshhold = 0.5f;
    public float stepTime = 0.25f;

    [Header("Arms")]
    public trackedArm leftArm;

    public trackedArm rightArm;

    private Transform targetHip;
    private Transform targetKnee;
    private Quaternion hipStart;
    private Quaternion kneeStart;
    private Vector2 xzPosition;
    private Vector2 lastStepPosition;
    private bool step;
    private float stepProgress;
    private int stepCounter = 0;

    private SteamVR_Action_Boolean headsetOnHead = SteamVR_Input.GetBooleanAction("HeadsetOnHead");

    private Quaternion rotation = new Quaternion();
    private Vector3 targetRotation = new Vector3();

    // Start is called before the first frame update
    private void Start()
    {
        hipStart = hipLeft.localRotation;
        kneeStart = kneeLeft.localRotation;
        leftArm.init();
        rightArm.init();
    }

    // Update is called once per frame
    private void Update()
    {
         //Check if the headset is on
        headsetOnHead.UpdateValues();
        if (headsetOnHead.state)
        {
            //If it is, enable the renderer and animate
            avatar.gameObject.SetActive(true);
            avatarMove();
            avatarStep();
            leftArm.animate();
            rightArm.animate();
        }
        else
        {
            //If it is not, dissable the renderer and return
            avatar.gameObject.SetActive(false);
            return;
        }
    }

    private void avatarMove()
    {
        transform.position = HMD.position;
        targetRotation.y = HMD.rotation.eulerAngles.y;
        targetRotation.x = transform.rotation.eulerAngles.x;
        targetRotation.z = transform.rotation.eulerAngles.z;

        rotation.eulerAngles = targetRotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.5f);
    }

    /// <summary>
    /// Procedural waling based on distance traveld
    /// </summary>
    private void avatarStep()
    {
        //Get a 2D representation of the position 
        xzPosition.x = transform.position.x;
        xzPosition.y = transform.position.y;
        //If there is a step, animate a step over the step time
        if (step)
        {
            lastStepPosition = xzPosition;
            //Increments a progress bar based on the time passed between frames and the stepTime
            stepProgress += Time.deltaTime / stepTime;
            //Rotate the knee and hip joints to walk
            targetHip.transform.localRotation = Quaternion.Lerp(hipStart, Quaternion.Euler(hipUp.eulerAngles + hipStart.eulerAngles), 1 - Mathf.Abs(0.5f - stepProgress) * 2);
            targetKnee.transform.localRotation = Quaternion.Lerp(kneeStart, Quaternion.Euler(kneeUp.eulerAngles + kneeStart.eulerAngles), 1 - Mathf.Abs(0.5f - stepProgress) * 2);
           //If the progress finishes, set it to zero and stop stepping
            if (stepProgress >= 1)
            {
                stepProgress = 0;
                step = false;
            }
        }
        //If there is not step, detect if there should be on based on distance 
        else
        {
            //If the current distnace from the last step passes a threshhold, do a step
            if (Vector2.Distance(lastStepPosition, xzPosition) > stepThreshhold)
            {
                {
                    //Left then right
                    if (stepCounter % 2 == 0)
                    {
                        step = true;
                        targetHip = hipLeft;
                        targetKnee = kneeLeft;
                    }
                    else
                    {
                        step = true;
                        targetKnee = kneeRight;
                        targetHip = hipRight;
                    }
                    stepCounter++;
                }
            }
            //Otherwise, stick the feet to floor.
            else
            {
                Vector3 targetPositon = footLeft.transform.position;
                targetPositon.y = groudPlane.position.y;
                footLeft.position = targetPositon * 0.5f + footLeft.position * 0.5f;
                targetPositon = footRight.transform.position;
                targetPositon.y = groudPlane.position.y;
                footRight.position = targetPositon * 0.5f + footRight.position * 0.5f;
            }
        }
    }

    [System.Serializable]
    public class trackedArm
    {
        // Start is called before the first frame update
        public Transform controller;

        public Transform upperArm;
        public Transform hand;

        public Quaternion rotationOffset;
        private Vector3 handStartingPosition;

        public void init()
        {
            handStartingPosition = upperArm.localPosition;
        }

        // Update is called once per frame
        public void animate()
        {
            upperArm.LookAt(controller);
            upperArm.Rotate(rotationOffset.eulerAngles);
            hand.position = controller.position;

            if (Vector3.Magnitude(hand.localPosition) > 0.002f)
            {
                hand.localPosition = handStartingPosition;
                upperArm.LookAt(hand);
                upperArm.Rotate(rotationOffset.eulerAngles);
            }
        }
    }
}