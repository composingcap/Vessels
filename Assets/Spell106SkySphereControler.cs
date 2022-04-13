using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell106SkySphereControler : MonoBehaviour
{
    float progress;
    Material m;
    // Start is called before the first frame update
    void Start()
    {
        m = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        m.SetFloat("_Progress", progress);
    }

    public void setProgress(float v)
    {
        progress = v;
    }
}
