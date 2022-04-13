using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MemoryController : MonoBehaviour
{
    public VisualEffect smoke;
    public VisualEffect explosion;
    public MeshRenderer sphere;
    Collider col; 
    public bool explode;
    bool exploded;
    public float memoryStartTime;
    public float memoryEndTime;
    float explodeFadeTime = 0.25f;
    float progress = 0;
    public bool fadeIn = false;
    public bool fadeOut = false;
    public float  fadeTime = 3;
    bool fadedIn;
    bool fadedOut;
    float fadeLevel;
    float lifeTime = 10000;
    float timeCounter;
    public float randomWalkDepth = 0.1f;
    Vector3 anchorPosition;
    Vector3 lastPosition;
    TransformSender transformSender;
    OSC osc;
    OscMessage message;
    Vector3 nextPosition = new Vector3();
    // Start is called before the first frame update
    void Awake()
    {
        col = GetComponent<Collider>();
        transformSender = GetComponent<TransformSender>();
        osc = GameObject.FindGameObjectWithTag("OSC").GetComponent<OSC>();
        transformSender.sender = osc;
        message = new OscMessage();

    }
     
    private void OnEnable()
    {
        message.values.Clear();
        message.address = name + "/state/";
        message.values.Add(1);
        osc.Send(message);

        message.values.Clear();
        message.address = name + "/startTime/";
        message.values.Add(memoryStartTime);
        osc.Send(message);

        message.values.Clear();
        message.address = name + "/endTime/";
        message.values.Add(memoryEndTime);
        osc.Send(message);

        anchorPosition = transform.position;

        timeCounter = 0;
        lifeTime = Random.Range(15f, 25f);
        lastPosition = transform.position;
        nextPosition.x = lastPosition.x + Random.Range(-randomWalkDepth, randomWalkDepth);
        nextPosition.y = lastPosition.y + Random.Range(-randomWalkDepth, randomWalkDepth);
        nextPosition.z = lastPosition.z + Random.Range(-randomWalkDepth, randomWalkDepth);
    }

    private void OnDisable()
    {
        message.values.Clear();
        message.address = name + "/state/";
        message.values.Add(0);
        osc.Send(message);
    }


    // Update is called once per frame
    void Update()
    {

        lastPosition = transform.position;

        if (Random.Range(0, 100) > 90)
        {
            nextPosition.x = lastPosition.x + Random.Range(-randomWalkDepth, randomWalkDepth);
            nextPosition.y = lastPosition.y + Random.Range(-randomWalkDepth, randomWalkDepth);
            nextPosition.z = lastPosition.z + Random.Range(-randomWalkDepth, randomWalkDepth);
        }

        lastPosition = (nextPosition * 0.1f) + (lastPosition * 0.9f);

        transform.position = lastPosition; 

        timeCounter += Time.deltaTime;
        if (timeCounter > lifeTime)
        {
            fadeOut = true;
        }
        if (explode)
        {
            if (!exploded)
            {
                progress = 0;
                explosion.SendEvent("Explode");
                smoke.SetBool("Confined", false);
                exploded = true;
                fadeIn = false;
                fadedOut = false;
                StartCoroutine(endMemory());
                message.values.Clear();
                message.address = "/"+name + "/explode/";
                message.values.Add(1);
                osc.Send(message);

            }
            if (progress < 1)
            {
                progress += Time.deltaTime / explodeFadeTime;

                progress = Mathf.Clamp01(progress);
                sphere.material.SetFloat("_Fade", 1 - progress);
    
            }
            
        }
        else
        {
            exploded = false;
            smoke.SetBool("Confined", true);
            //sphere.material.SetFloat("_Fade", 1);
        }

        if (fadeIn)
        {
            if (!fadedIn)
            {
                fadeOut = false;
                fadedOut = false;
                progress = 0;
                fadedIn = true;

            }
            if (progress < 1)
            {
                progress += Time.deltaTime / fadeTime;
                sphere.material.SetFloat("_Fade", progress);
                smoke.SetFloat("Fade", progress);
                fadeLevel = progress;
                message.values.Clear();
                message.address = name + "/fadeLevel/";
                message.values.Add(fadeLevel);
                osc.Send(message);
            }
            else
            {
                fadeIn = false;
                fadedIn = false;
            }
        }

        if (fadeOut)
        {
            if (!fadedOut)
            {
                fadeIn = false;
                fadedIn = false;
                fadedOut = true;
                progress = 0;
            }
            if (progress < 1)
            {
                progress += Time.deltaTime / fadeTime;
                sphere.material.SetFloat("_Fade", (1-progress)* fadeLevel);
                smoke.SetFloat("Fade", (1-progress)* fadeLevel);
                message.values.Clear();
                message.address = name + "/fadeLevel/";
                message.values.Add((1 - progress) * fadeLevel);
                osc.Send(message);
            }
            else
            {
                fadeOut = false;
                fadedOut = false;
                gameObject.SetActive(false);
            }


        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (fadeLevel > 0)
        {
            if (other.tag == "VRController")
            {
                //If it is a VR controller explode
                explode = true;
            }
            else if( other.tag == "Memory" && fadeLevel > 0.5f)
            {
                if(other.GetComponent<MemoryController>().fadeLevel > 0.5)
                {
                    //If it is another glass ball that is visible, explode.
                    explode = true;
                }
            }
        }
    }

    IEnumerator destroyMemory()
    {
        yield return new WaitForSeconds(5.5f);
        Destroy(gameObject);
    }
    IEnumerator endMemory()
    {
        yield return new WaitForSeconds(5.5f);
        explode = false;
        smoke.SetFloat("Fade", 0);
        sphere.material.SetFloat("_Fade", 0);
        gameObject.SetActive(false);
        
    }

}
