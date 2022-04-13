using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circularFilmControler : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer[] mrs;
    autorotate ar;
    public float alpha;
    public float rotate;
    void Start()
    {
        ar = GetComponent<autorotate>();
        mrs = GetComponentsInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ar.roatationSpeed.y = rotate;

        foreach (MeshRenderer mr in mrs)
        {
            mr.material.SetFloat("_Fade", alpha);
        }

        
    }

    public void setAlpha(float v)
    {
        alpha = v;
    }

    public void setRotation( float v)
    {
        rotate = v;
    }
}
