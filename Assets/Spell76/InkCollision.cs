using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkCollision : MonoBehaviour
{
    Vector3 anchorPosition;
    Vector3 offsetPosition;
    List<InkCollisionAnimation> collisionsToRemove;
    List<InkCollisionAnimation> inkCollisions;
    // Start is called before the first frame update
    float index;
    float total;
    public bool active = false;
    void Start()
    {
        anchorPosition = transform.position;
        inkCollisions = new List<InkCollisionAnimation>();
        collisionsToRemove = new List<InkCollisionAnimation>();
    }

    public void setAnchorPosition(Vector3 v)
    {
        anchorPosition = v;
    }

    public void addCollision(Vector3 impact)
    {
        InkCollisionAnimation anim = new InkCollisionAnimation();
        anim.initalize(transform, Mathf.Pow(impact.magnitude,0.5f)*2+0.1f , -impact.normalized, 1+ impact.magnitude * 3); ;
        inkCollisions.Add(anim);
    }

    private void Update()
    {
       
        offsetPosition = new Vector3();
        if (active)
        {
            if (inkCollisions.Count > 0)
            {
                foreach (InkCollisionAnimation c in inkCollisions)
                {
                    offsetPosition += c.animate(Time.deltaTime);
                    if (c.done)
                    {
                        collisionsToRemove.Add(c);
                    }
                }
                


            }
            if (collisionsToRemove.Count > 0)
            {
                foreach (InkCollisionAnimation c in collisionsToRemove)
                {
                    inkCollisions.Remove(c);
                }
                collisionsToRemove.Clear();
            }
        }
        else
        {
            inkCollisions.Clear();
        }
        transform.position = anchorPosition + offsetPosition;
    }

    public void setCollisionInfo(float index, float total)
    {
        this.index = index;
        this.total = total;
    }

    public float getCollisionPosition()
    {
        return index / total;
    }


    class InkCollisionAnimation
    {
        float progress = 0;
        float duration = 1;
        float amp = 1;
        public bool done = false;
        Transform myTransform;
        Vector3 direction;

        public void initalize(Transform me, float amplitude, Vector3 direction, float duration)
        {
            myTransform = me;
            amp = amplitude;
            this.direction = direction;
            this.duration = duration;
            done = false;
        }

        public Vector3 animate(float deltaTime)
        {
            progress += deltaTime/duration;
            progress = Mathf.Clamp01(progress);

            float distance = Mathf.Sin(progress*Mathf.PI*(amp*3+2))*(1-Mathf.Pow(progress,0.5f));
            return direction * distance*amp;
            

            if (progress >= 1)
            {
                done = true;
            }
        }
    }
}
