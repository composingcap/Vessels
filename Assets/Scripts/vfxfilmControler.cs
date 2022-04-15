using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class vfxfilmControler : MonoBehaviour
{
    public float rate;
    VisualEffect film;
    // Start is called before the first frame update
    void Start()
    {
        film = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        film.SetFloat("Rate", rate);
    }

    public void setRate(float v)
    {
        rate = v * 20;
    }
}
