using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell14FlashingLights : MonoBehaviour
{
    Material mat;
    public float speed;
    public Vector2 densityRange;
    public float fade;
    public float strobeSpeed;
    float strobeAccum;
    float timeaccum;
    int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetFloat("_Fade", fade);
        
        if (timeaccum >= 1)
        {
            direction = -1;
            mat.SetFloat("_Random1", Random.Range(0f, 1000f));
            mat.SetFloat("_Density1", Random.Range(densityRange[0], densityRange[1]));
        }
        if (timeaccum <= 0)
        {
            direction = 1;
            mat.SetFloat("_Random2", Random.Range(0f, 1000f));
            mat.SetFloat("_Density2", Random.Range(densityRange[0], densityRange[1]));
        }
        if (speed != 0)
        {
            timeaccum += Time.deltaTime / speed * direction;
        }
        if (strobeSpeed != 0)
        {
            strobeAccum += Time.deltaTime / strobeSpeed;
        }

        mat.SetFloat("_Strobe", strobeAccum);

        mat.SetFloat("_Lerp", timeaccum);

        

    }

    public void setFade(float v)
    {
        fade = v;
    }

    public void setSpeed(float v)
    {
        speed = v;
    }
}
