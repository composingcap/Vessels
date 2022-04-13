using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Spell106ParticleMeshControler : MonoBehaviour
{
    VisualEffect[] particleMeshes;
    [Range (0,1)]
    public float progress;
    public Color startingColor;
    public Color endingColor;
    Color thisColor;
    Vector2 startingLifetimes = new Vector2(1, 2);
    Vector2 endingLifetimes = new Vector2(3, 4);
    Vector2 startingSize = new Vector2(0.25f, 0.5f);
    Vector2 endingSize = new Vector2(0.5f, 1f);
    // Start is called before the first frame update
    void Start()
    {
        particleMeshes = GetComponentsInChildren<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        thisColor = Color.Lerp(startingColor, endingColor, progress);

        foreach (VisualEffect pmesh in particleMeshes)
        {
        
            pmesh.SetVector4("Custom Color", thisColor);
            pmesh.SetFloat("RandomPosition", Mathf.Lerp(0.01f, 0.3f, progress));
            pmesh.SetFloat("Turbulence", Mathf.Lerp(0f, 3f, (progress*2-1)));
            pmesh.SetVector2("Lifetime Range", Vector2.Lerp(startingLifetimes, endingLifetimes, progress*2-1));
            pmesh.SetVector2("Size Range", Vector2.Lerp(startingSize, endingSize, progress * 3 - 1));
        }


    }

    public void setProgress(float v)
    {
        progress = v;
    }
}
