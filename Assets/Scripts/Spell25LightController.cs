using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class Spell25LightController : MonoBehaviour
{
    public Light leftLight;
    public Transform lightTarget;
    public Transform snake;
    public Transform spider;

    public float lerpTime = 2;
    public float lightBrightness = 0;

    bool targetIsSnake;
    bool targetIsSpider;

    bool lerpToSnake;
    bool lerpToSpider;
    float lerpProgress;

    Vector3 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (targetIsSnake)
        {
            lightTarget.position = snake.position;
        }

        if (targetIsSpider)
        {
            lightTarget.position = spider.position;
        }
        
        if (lerpToSnake)
        {
            lerpProgress += Time.deltaTime / lerpTime;
            lerpProgress = Mathf.Clamp01(lerpProgress);
            lightTarget.position = startPosition * (1 -lerpProgress) + (snake.position*lerpProgress);
            if (lerpProgress == 1)
            {
                lerpToSnake = false;
                targetIsSnake = true;
            }


        }

        if (lerpToSpider)
        {
            lerpProgress += Time.deltaTime / lerpTime;
            lerpProgress = Mathf.Clamp01(lerpProgress);
            lightTarget.position = startPosition * (1 - lerpProgress) + (spider.position * lerpProgress);
            if (lerpProgress == 1)
            {
                lerpToSpider = false;
                targetIsSpider = true;
            }


        }


        leftLight.transform.LookAt(lightTarget);

        leftLight.intensity = lightBrightness;


    }
    [Button]
    public void setTargetToSpider()
    {
        targetIsSpider = false;
        targetIsSnake = false;
        lerpToSnake = false;
        startPosition = lightTarget.position;
        lerpToSpider = true;
        lerpProgress = 0;
    }
    [Button]
    public void setTargetToSnake()
    {

        targetIsSpider = false;
        targetIsSnake = false;
        lerpToSpider = false;
        startPosition = lightTarget.position;
        lerpToSnake = true;
        lerpProgress = 0;

    }

    public void lightLevel(float v)
    {
        lightBrightness = v;
    }
}
