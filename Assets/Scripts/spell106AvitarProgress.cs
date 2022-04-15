using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class spell106AvitarProgress : MonoBehaviour
{
    VisualEffect avitar;
    float progress;
    Color startingColor;
    public Color endingColor;
    // Start is called before the first frame update
    void Start()
    {
        avitar= GetComponent<VisualEffect>();
        startingColor = avitar.GetVector4("Custom Color");

    }

    // Update is called once per frame
    void Update()
    {
        ///avitar.SetFloat("Emmission", progress * 6 + 1);
        avitar.SetVector4("Custom Color", endingColor * progress + startingColor * (1 - progress));
    }

    public void setProgress(float v)
    {
        progress = v;
    }
}
