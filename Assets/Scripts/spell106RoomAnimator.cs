using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class spell106RoomAnimator : MonoBehaviour
{

    VisualEffect vfx;
    public float progress;
    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        vfx.SetFloat("Emmission", progress*10);
        vfx.SetFloat("Turbulence", progress + 0.5f);
    }

    public void setProgress(float v)
    {
        progress = v;
    }
}
