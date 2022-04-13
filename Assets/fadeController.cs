using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyButtons;
public class fadeController : MonoBehaviour
{
    
    [Range(0,1)]
    public float fade;
    float fadeTarget;
    float fadeTime;
    float counter;
    Image[] fades;
    Color temp;
    bool fading;
    // Start is called before the first frame update
    void Start()
    {
        fades = GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            counter += Time.deltaTime / fadeTime;
            counter = Mathf.Clamp01(counter);
            fade = Mathf.Lerp(1 - fadeTarget, fadeTarget, counter);
            foreach (Image f in fades)
            {
                temp = f.color;
                temp.a = fade;
                f.color = temp;
            }
            if (counter >= 1)
            {
                fading = false;
            }
        }
    }
    [Button]
    public void fadeIn(float fadeTime)
    {
        fadeTarget = 0;
        fade = 0;
        this.fadeTime = fadeTime;
        fading = true;
        counter = 0;
    }
    [Button]
    public void fadeOut(float fadeTime)
    {
        fadeTarget = 1;
        fade = 1;
        this.fadeTime = fadeTime;
        fading = true;
        counter = 0;
    }
}
