using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MudBun;
using UnityEngine.VFX;
public class Spell106HeartRoomControler : MonoBehaviour
{
    public VisualEffect room;
    public VisualEffect[] smokeCovers;
    public MudRenderer heart;

    public float progress;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        room.SetFloat("Emmission", progress * 7);
        heart.MasterEmission = Color.white * progress;
        foreach (VisualEffect cover in smokeCovers)
        {
            cover.SetFloat("Emmission", progress * 7);
        }
    }

    public void setProgress(float v)
    {
        progress = v;
    }
}
