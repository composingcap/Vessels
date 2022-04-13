using UnityEngine;

public class spirograph : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 startingPosition;

    private Vector3 currentPosition;
    private Vector3 polarDeviation;
    public float radius = 1;
    public float periodTime = 10000;
    public bool lockX = false;
    public bool lockY = false;
    public bool lockZ = false;
    private float progress;
    private float timeCounter;
    float timeCounter_radius;
    float timeCounter_azimuth;
    float timeCounter_elivation;
    public float radiusRate = 0;
    public float azumuthRate = 1;
    public float elivationRate = 1;
    public float overallRate = 1;


    private void Start()
    {
        startingPosition = gameObject.transform.position;
        currentPosition = startingPosition;

    }

    // Update is called once per frame
    private void Update()
    {



        timeCounter_radius += Time.deltaTime * 1000 / periodTime* radiusRate * overallRate;
        timeCounter_azimuth += Time.deltaTime * 1000 / periodTime * azumuthRate * overallRate;
        timeCounter_elivation += Time.deltaTime * 1000 / periodTime * elivationRate * overallRate;


        polarDeviation.x = Mathf.Cos(timeCounter_radius * Mathf.PI*2) *radius;
        polarDeviation.y = timeCounter_azimuth * Mathf.PI * 2;
        polarDeviation.z = timeCounter_elivation * Mathf.PI * 2;

        SphericalToCartesian(polarDeviation, out currentPosition);

        if (lockX)
        {
            currentPosition.x = 0;
        }
        if (lockY)
        {
            currentPosition.y = 0;
        }
        if (lockZ)
        {
            currentPosition.z = 0;
        }

        transform.position = startingPosition + currentPosition;


    }

    public static void SphericalToCartesian(Vector3 inSphere, out Vector3 outCart)
    {
        float a = inSphere.x * Mathf.Cos(inSphere.z);
        outCart.x = a * Mathf.Cos(inSphere.y);
        outCart.y = inSphere.x * Mathf.Sin(inSphere.z);
        outCart.z = a * Mathf.Sin(inSphere.y);
    }

    public static void CartesianToSpherical(Vector3 cartCoords, out Vector3 outSpheres)
    {
        if (cartCoords.x == 0)
            cartCoords.x = Mathf.Epsilon;
        outSpheres.x = Mathf.Sqrt((cartCoords.x * cartCoords.x)
                        + (cartCoords.y * cartCoords.y)
                        + (cartCoords.z * cartCoords.z));
        outSpheres.y = Mathf.Atan(cartCoords.z / cartCoords.x);
        if (cartCoords.x < 0)
            outSpheres.y += Mathf.PI;
        outSpheres.z = Mathf.Asin(cartCoords.y / outSpheres.x);
    }
}