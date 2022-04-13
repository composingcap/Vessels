using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallpaperControler : MonoBehaviour
{
    Material wallpaper;
    public float timeScale;
    float timeValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        wallpaper = GetComponent<MeshRenderer>().material;
        
    }

    // Update is called once per frame
    void Update()
    {
        timeValue += Time.deltaTime * timeScale;
        wallpaper.SetFloat("_External_Time", timeValue);
    }

    public void setTimeScale(float v)
    {
        timeScale = v; 
    }
}
