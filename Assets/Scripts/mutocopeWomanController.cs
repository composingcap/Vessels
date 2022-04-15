using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using UnityEngine.VFX;

public class mutocopeWomanController : MonoBehaviour
{
    waypointFollower waypointFollower;
    public MeshRenderer mutoscopeWoman;
    public VisualEffect particleWoman;
    [Range(0,1)]
    public float interp;
    bool transitioningProgress;
    float transitionProgress = 0;
    float dreamingInterp;
    public float transitionTime;
    Transform VRplayer;
    // Start is called before the first frame update
    void Start()
    {
        waypointFollower = GetComponent<waypointFollower>();
        VRplayer = GameObject.FindGameObjectWithTag("Spout").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (transitioningProgress) {
            transitionProgress += Time.deltaTime / transitionTime;
            transitionProgress = Mathf.Clamp01(transitionProgress);
            setPathProgress(transitionProgress);
            if (transitionProgress >= 1)
            {
                transitioningProgress = false;
            }
                }

        mutoscopeWoman.material.SetFloat("_Alpha", 1 - interp);
        particleWoman.SetFloat("BurstCount", (1-interp)*500);
        particleWoman.SetFloat("ConstRate", 1000 * interp);
        particleWoman.SetFloat("Turbulance", 1 * interp);
        particleWoman.SetFloat("Drag", 2 * interp);

        transform.LookAt(VRplayer);


    }


    public void setInterp(float v)
    {
        interp = v;
    }

    public void setPathProgress(float v)
    {
        waypointFollower.pathProgress = v;
    }

  
    public void transitionPathProgress(float time)
    {
        transitionTime = time;
        transitionPathProgress();
    }
    [Button]
    public void transitionPathProgress()
    {
        transitionProgress = 0;
        transitioningProgress = true;
    }

    public void setWomanAlpha(float v)
    {
        //mutoscopeWoman.material.SetFloat("_alpha", v);
    }
}
