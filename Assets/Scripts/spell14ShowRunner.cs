using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class spell14ShowRunner : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Spell14Section {muto1, dreaming1, muto2, dreaming2 };
    public Spell14Section spell14Section;
    Spell14Section lastSpell14Section;

    public Transform circularFilmVR;
    Material circularFilmVrMat;
    public Transform circularFilmSpout;
    Material circularFilmSpoutMat;
    public VisualEffect filmStrips;
    public VisualEffect mutoFlower;
    public VisualEffect mutoScope;
    public VisualEffect particleWoman;
    public VisualEffect plant;
    public Transform mutoScopeWoman;
    float progress;
    float transitionTime;
    bool transitioning;
    void Start()
    {
        lastSpell14Section = spell14Section;
        circularFilmSpoutMat = circularFilmSpout.GetComponent<MeshRenderer>().material;
        circularFilmVrMat = circularFilmVR.GetComponent<MeshRenderer>().material;
        switchState();
    }

    // Update is called once per frame
    void Update()
    {
        if(lastSpell14Section != spell14Section)
        {
            switchState();
        }

        if (transitioning)
        {

        }
    }

    void switchState()
    {
        switch (spell14Section)
        {
            case Spell14Section.muto1:

                break;
            case Spell14Section.dreaming1:

                break;
            case Spell14Section.muto2:

                break;
            case Spell14Section.dreaming2:

                break;
            default:
                break;
        }
        transitioning = true;
        progress = 0;
    }
}
