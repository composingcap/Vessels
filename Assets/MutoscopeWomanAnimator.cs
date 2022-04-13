using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using UnityEngine.VFX;
public class MutoscopeWomanAnimator : MonoBehaviour


{
    // Start is called before the first frame update
    public Texture2DArray mutoscopeWomanTexture;
    public VisualEffect particleWoman;
    public MeshRenderer womanRender;
    Vector3 lastPosition;
    Vector3 currentPosition;
    Vector3 targetPosition;
    void Start()
    {
        womanRender.material.SetTexture("_Woman_Array", mutoscopeWomanTexture);
        particleWoman.SetTexture("WomanArray", mutoscopeWomanTexture);
        currentPosition = transform.localPosition;
        lastPosition = currentPosition;
        targetPosition = currentPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(currentPosition, targetPosition) > 0.01)
        {
            currentPosition = currentPosition*0.95f + targetPosition * 0.05f;
            transform.localPosition = currentPosition;
        }
    }
    [Button]
    public void selectFrame(){
        float frame = Random.Range(0,mutoscopeWomanTexture.depth);
        womanRender.material.SetFloat("_Frame", frame);
        particleWoman.SetInt("Frame", (int)frame);
        particleWoman.SendEvent("pulse");
        }
    [Button]
    public void newTargetPosition(float scale)
    {
        targetPosition.x = Random.Range(-1f, 1f);
        targetPosition.y = Random.Range(-1f, 1f);
        targetPosition.z = Random.Range(-1f, 1f);
        targetPosition *= scale;
    }
}
