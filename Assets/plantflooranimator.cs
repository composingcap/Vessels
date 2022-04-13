using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantflooranimator : MonoBehaviour
{
    public float targetSpeed;
    public float targetHieght;
    public float targetAlpha;
    [Range(0,1)]
    public float interp;
    float lastInterp;
    float height;
    float alpha;
    float speed;
    float counter;
    Vector2 offset;
    Vector2 offsetAlpha;
    Material plantfloor;
    // Start is called before the first frame update
    void Start()
    {
        plantfloor= GetComponent<MeshRenderer>().material;


    }

    // Update is called once per frame
    void Update()
    {
        if (lastInterp != interp)
        {
            speed = Mathf.Lerp(0, targetSpeed, interp);
            height = Mathf.Lerp(0, targetHieght, interp);
            alpha = Mathf.Lerp(0.25f, targetAlpha, interp);
            plantfloor.SetFloat("_Height_Amount", height);
            plantfloor.SetFloat("_Gradient_Noise_Alfph", alpha);
            lastInterp = interp;
        }
        if (speed != 0)
        {
            counter += Time.deltaTime / (1/speed);
            offset.x = counter*0.8f;
            offset.y = counter * 0.2f;
            offsetAlpha.y = counter * 0.7f;
            offsetAlpha.x = counter * 0.1f;
            plantfloor.SetVector("_Offset", offset);
            plantfloor.SetVector("_GradientNoiseOffset", offsetAlpha);
        }

    }

    public void setInterp(float v)
    {
        interp = v;
    }
}
