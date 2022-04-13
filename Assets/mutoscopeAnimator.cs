using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mutoscopeAnimator : MonoBehaviour
{

    SkinnedMeshRenderer render;
    [Range(0,1)]
    public float mouthOpen;
    [Range(0, 1)]
    public float oh;
    [Range(0, 1)]
    public float ah;
    [Range(0, 1)]
    public float ee;
    [Range(0, 1)]
    public float puff;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        render.SetBlendShapeWeight(0, mouthOpen*100);
        render.SetBlendShapeWeight(1, ah*100);
        render.SetBlendShapeWeight(2, oh*100);
        render.SetBlendShapeWeight(3, ee*100);
        render.SetBlendShapeWeight(4, puff*100);
    }

    public void setMouthOpen(float v)
    {
        mouthOpen = v;
    }

    public void setOh(float v)
    {
        oh = v;
    }

    public void setAh(float v)
    {
        ah = v;
    }

    public void setEe(float v)
    {
        ee = v;
    }

    public void setPuff(float v)
    {
        puff = v;
    }

}

