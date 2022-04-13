using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRTrackedTitle : MonoBehaviour
{
    public Camera HMD;
    Transform titleTracker;
    RectTransform title;
    Vector3 titlePosition;
    // Start is called before the first frame update
    void Start()
    {
        title = GetComponent<RectTransform>();
        titleTracker = HMD.GetComponentsInChildren<Transform>()[1];
        titlePosition = titleTracker.transform.position;
        title.position = titlePosition;



    }

    // Update is called once per frame
    void Update()
    {

        titlePosition = titleTracker.transform.position;
        title.position = titlePosition * 0.25f + title.position * 0.75f;
        title.rotation = titleTracker.rotation;

    }
}
