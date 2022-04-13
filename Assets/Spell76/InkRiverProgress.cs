using MudBun;
using System.Collections.Generic;
using UnityEngine;

public class InkRiverProgress : MonoBehaviour
{
    // Start is called before the first frame update

    [Range(0, 1)] public float progress;
    [Range(0, 1)] public float samplePosition;
    public MudBrushGroup particleSystemParent;
    private ParticleSystem psystem;
    private MudCurveFull curve;
    private List<Vector3> points;
    private List<Transform> transforms;
    private List<Vector3> newPoints;
    private List<InkCollision> inkCollisions;
    public ParticleSystem penInk;

    //public MudCurveFull primaryInkRiver;
    public int pointDivisions = 4;

    private float lastPorgress = 0;
    private bool init = true;

    private void Start()
    {
        points = new List<Vector3>();
        transforms = new List<Transform>();
        newPoints = new List<Vector3>();
        psystem = particleSystemParent.GetComponentInChildren<ParticleSystem>();
        curve = GetComponentInChildren<MudCurveFull>();
        //primaryInkRiver.Points.Clear();

        inkCollisions = new List<InkCollision>();

        foreach (MudCurveFull.Point p in curve.Points)
        {
            points.Add(p.Transform.position);
        }

        //foreach (Transform child in primaryInkRiver.gameObject.GetComponentInChildren<Transform>())
        //{
        //    GameObject.Destroy(child.gameObject);
        // }

        // foreach (Transform child in primaryInkRiver.gameObject.GetComponentInChildren<Transform>())
        // {
        //    GameObject.Destroy(child.gameObject);
        // }

        curve.Points.Clear();
        foreach (Transform child in curve.gameObject.GetComponentInChildren<Transform>())
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int t = 0; t < points.Count - 1; t++)
        {
            Vector3 positionA = points[t];
            Vector3 positionB = points[t + 1];

            for (int i = 0; i < pointDivisions; i++)
            {
                newPoints.Add(Vector3.Lerp(positionA, positionB, (float)i / pointDivisions));
            }
        }
        int n = 0;
        foreach (Vector3 p in newPoints)
        {
            n++;
            GameObject curvePoint = new GameObject();
            curvePoint.transform.parent = curve.gameObject.transform;
            InkCollision inkc = curvePoint.AddComponent<InkCollision>();
            inkCollisions.Add(inkc);
            inkc.setCollisionInfo(n - 1, newPoints.Count);
            curvePoint.name = "curvePoint " + n;
            curvePoint.transform.position = p;
            curve.Points.Add(new MudCurveFull.Point(curvePoint.transform, 0.15f));
            transforms.Add(curvePoint.transform);
            BoxCollider collider = curvePoint.AddComponent<BoxCollider>();
            //collider.isTrigger = true;
            collider.enabled = true;
            //Rigidbody rb = curvePoint.AddComponent<Rigidbody>();
            //rb.useGravity = false;
            //rb.isKinematic = true;
            collider.size *= 0.3f;
            collider.gameObject.layer = 7;
            //GameObject primaryPoint = Instantiate(new GameObject(), primaryInkRiver.gameObject.transform);
            //primaryPoint.transform.position = p;
            //primaryInkRiver.Points.Add(new MudCurveFull.Point(curvePoint.transform, 0.15f));
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //----Reveals the ink river
        //The number of complete points
        int normalPoints = (int)Mathf.Floor((newPoints.Count) * progress); 
        //The amount between the last point and the current points destination
        float tween = ((newPoints.Count - 1) * progress) - normalPoints; 
        Vector3 tweenPos = new Vector3();
        if (progress != lastPorgress || init)//Only run if there is a change
        {
            for (int p = 0; p < newPoints.Count; p++)
            {
                if (p <= normalPoints)
                {
                    inkCollisions[p].setAnchorPosition(newPoints[p]);
                    inkCollisions[p].active = true;
                }
                if (p == normalPoints + 1)
                {
                    //Calculate the tweened point position
                    tweenPos = newPoints[normalPoints] * (1 - tween) + newPoints[normalPoints + 1] * (tween);
                    inkCollisions[p].setAnchorPosition(tweenPos);
                    inkCollisions[p].active = true;
                }
                if (p > normalPoints + 1)
                {
                    //Make inactive points go away
                    inkCollisions[p].setAnchorPosition(tweenPos);
                    inkCollisions[p].active = false;
                }
            }
        }

        int pointA = (int)Mathf.Floor((newPoints.Count) * samplePosition);
        tween = ((newPoints.Count - 1) * samplePosition) - pointA;
        particleSystemParent.transform.position = newPoints[pointA] * (1 - tween) + newPoints[(pointA + 1) % (newPoints.Count - 1)] * (tween);
        lastPorgress = progress;
        init = false;
    }

    public void setProgress(float v)
    {
        progress = v;
    }

    public void setPosition(float v)
    {
        samplePosition = v;
    }

    public void setEmmissionRate(float v)
    {
        var emit = psystem.emission;
        emit.rateOverTime = v;
    }

    public void setPenInkRate(float v)
    {
        var emit = penInk.emission;
        emit.rateOverTime = v;
    }
}