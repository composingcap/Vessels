using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
public class VesslesLookBehavior : MonoBehaviour
{
    public Camera VRplayer;
    public Camera Spout;

    Vector3 currentPosition;
    Vector3 lastPosition;
    Vector3 filterPosition;

    public bool targetVrPlayer = true;
    public bool targetSpout;
    float lerpProgress = 1;
    float lerpTime = 3;
    float transitionTime = 6;
    float ellapsedTime;
    bool lastTransition = true;
    // Start is called before the first frame update
    void Start()
    {
        VRplayer = GameObject.FindGameObjectWithTag("VRPlayer").GetComponent<Camera>();
        Spout = GameObject.FindGameObjectWithTag("Spout").GetComponent<Camera>();
        transitionTime = Random.Range(15f, 25f);


    }

    // Update is called once per frame
    void Update()
    {
        ellapsedTime += Time.deltaTime;
        if (ellapsedTime >= transitionTime)
        {
            ellapsedTime = 0;
            transitionTime = Random.Range(15f, 25f);

            if (lastTransition)
            {
                goSpout();
                lastTransition = false;
            }
            else
            {
                goToVR();
                lastTransition = true;
            }

        }



        if (lerpProgress < 1)
        {
            lerpProgress += Time.deltaTime / lerpTime;


            if (targetVrPlayer)
            {
                for (int i = 0; i < 3; i++)
                {
                    currentPosition[i] = Mathf.Lerp(lastPosition[i], VRplayer.transform.position[i], lerpProgress);
                }
            }

            if (targetSpout)
            {
                for (int i = 0; i < 3; i++)
                {
                    currentPosition[i] = Mathf.Lerp(lastPosition[i], Spout.transform.position[i], lerpProgress);
                }
            }
        }
        else
        {
            if (targetVrPlayer) currentPosition = VRplayer.transform.position;
            if (targetSpout) currentPosition = Spout.transform.position;
        }
  

        filterPosition = currentPosition * 0.25f + filterPosition * 0.75f;
        transform.position = filterPosition;
            
    }
    [Button]
    public void goToVR()
    {
        targetVrPlayer = true;
        targetSpout = false;
        lastPosition = transform.position;
        filterPosition = lastPosition;
        lerpProgress = 0;
        lerpTime = Random.Range(3f, 8.0f);
    }
    [Button]
    public void goSpout()
    {
        targetVrPlayer = false;
        targetSpout = true;
        lastPosition = transform.position;
        filterPosition = lastPosition;
        lerpProgress = 0;
        lerpTime = Random.Range(3f, 8.0f);
    }
}
