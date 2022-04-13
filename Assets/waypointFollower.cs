using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointFollower : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform waypointContainer;
    public List<Transform> waypoints;
    public bool useSize;
    Vector3 lastVelocity = new Vector3();
    public bool waypointActive = true;
    [Range(0,1)]
    public float pathProgress;
    void Start()
    {
        if (waypointContainer != null)
        {
            waypoints = new List<Transform>();
            waypoints.AddRange(waypointContainer.GetComponentsInChildren<Transform>());
            waypoints.RemoveAt(0);
        }
        int pointA = Mathf.FloorToInt(pathProgress * (waypoints.Count-1));
        int pointB = Mathf.CeilToInt(pathProgress * (waypoints.Count-1));
        float progress = pathProgress * waypoints.Count - pointA;
        transform.position = Vector3.Lerp(waypoints[pointA].position, waypoints[pointB].position, progress);



        if (useSize)
        {
            transform.localScale = Vector3.Lerp(waypoints[pointA].localScale, waypoints[pointB].localScale, progress);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waypointActive)
        {
            int pointA = Mathf.FloorToInt(pathProgress * (waypoints.Count - 1));
            int pointB = Mathf.CeilToInt(pathProgress * (waypoints.Count - 1));
            float progress = pathProgress * (waypoints.Count - 1) % 1;
            Vector3 lastPosition = transform.position;
            Vector3 targetPosition = Vector3.Lerp(waypoints[pointA].position, waypoints[pointB].position, progress) * 0.25f + transform.position * 0.75f;
            Vector3 v = lastVelocity * 0.75f + (targetPosition - lastPosition);
            transform.position += v;

            if (useSize)
            {
                transform.localScale = Vector3.Lerp(waypoints[pointA].localScale, waypoints[pointB].localScale, progress) * 0.25f + transform.localScale * 0.75f;
            }
        }
    }
}
