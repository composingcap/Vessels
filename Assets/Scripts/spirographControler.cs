using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class spirographControler : MonoBehaviour
{
    public SteamVR_Behaviour_Pose hand;

    Vector3 lastHandPosition;
    Vector3 handPosition;
    Vector3 lastHandVelocity;
    Vector3 handVelocity;
    public float maxMagnitude = 10f;
    public float smooth;
    public float radiusMultiplier;
    public enum WhichHand { left, right};
    public WhichHand whichHand; 

    spirograph sp;
    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<spirograph>();
        GameObject[] hands = GameObject.FindGameObjectsWithTag("VRController");

        foreach (GameObject h in hands)
        {
            hand = h.GetComponent<SteamVR_Behaviour_Pose>();
            if (whichHand == WhichHand.left)
            {
                if (hand.inputSource == SteamVR_Input_Sources.LeftHand) break;
            }
            else
            {
                if (hand.inputSource == SteamVR_Input_Sources.RightHand) break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        handPosition = hand.transform.localPosition;


         
        handVelocity = handPosition - lastHandPosition;

        if (Vector3.Magnitude(handVelocity - lastHandVelocity) > maxMagnitude)
        {
            sp.radius = 1f;
            sp.radiusRate = 0;

        }
        else
        {
            handVelocity = lastHandVelocity * smooth + handVelocity * (1 - smooth);
            sp.radius = 1f + Vector3.Magnitude(handVelocity) * 50f * radiusMultiplier;
            sp.radiusRate = Vector3.Magnitude(handVelocity) * 2f + radiusMultiplier;

            sp.azumuthRate += handVelocity.y * 5f;
            sp.elivationRate += (handVelocity.z+handVelocity.x) * 2.5f;

            sp.overallRate = 1 / (sp.azumuthRate + sp.elivationRate)*0.5f + 0.5f;

        }

        lastHandVelocity = handVelocity;


        lastHandPosition = handPosition;

    }
}
