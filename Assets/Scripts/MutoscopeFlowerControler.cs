using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
public class MutoscopeFlowerControler : MonoBehaviour
{

    float[] lastPose;
    float[] currentPose;
    float[] targetPose;

    public float lerpTime = 0.1f;
    float progress;
    bool transitioning;
    SkinnedMeshRenderer m;

    // Start is called before the first frame update
    void Start()
    {
        m = GetComponent<SkinnedMeshRenderer>();
        lastPose = new float[8];
        targetPose = new float[8];
        currentPose = new float[8];
    }

    // Update is called once per frame
    void Update()
    {

        if (transitioning)
        {
            progress += Time.deltaTime / lerpTime;
            progress = Mathf.Clamp01(progress);
            for (int i=0; i< lastPose.Length; i++)
            {
                currentPose[i] = Mathf.Lerp(lastPose[i], targetPose[i], progress);
                m.SetBlendShapeWeight(i, currentPose[i] * 100);
                
            }
        }
        
    }
    [Button]
    public void transitionPose()
    {
        progress = 0;
        transitioning = true;
        for (int i = 0; i < targetPose.Length; i++)
        {
            lastPose[i] = currentPose[i];
            targetPose[i] = Random.Range(0.5f, 1f)*(Random.Range(0f,1f)>0.75f?0:1);
        }
    }
}

