using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inkLakeControler : MonoBehaviour
{
    public Material inkLake;
    float accumTime = 0;
    float timeScale = 1;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
   
    }

    private void Update()
    {
        accumTime += Time.deltaTime * timeScale * speed;
        inkLake.SetFloat("time", accumTime);
    }

    // Update is called once per frame

    public void inkLakeNoise(float v)
    {
        inkLake.SetFloat("Displace_Strength", v * 0.4f + 0.1f);
        timeScale = v*3 + 1;
    }

}
