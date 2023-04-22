using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;

public class RaytracingVisualization : MonoBehaviour
{
    [Header("PRESS SPACE TO LOAD HIT GIZMOS (OR THE KEYCODE SET BELOW)")]

    [Header("Main Options")]
    [SerializeField] int width = 100;
    [SerializeField] int height = 100;
    [SerializeField] float length = 2;
    [SerializeField] float radius = 0.25f;
    [SerializeField] float forwardOffset = 2;
    [SerializeField] Light mainLight;

    [Header("Other Options")]
    [SerializeField][Range(0, 1)] float root0Alpha = 0.5f;

    [Header("Showing Gizmos & Debugs")]
    [SerializeField] bool isDebuggingRays = true;
    [SerializeField] bool isShowingHitGizmos = false;
    [SerializeField] bool isShowingSphereGizmo = true;
    [SerializeField] KeyCode reloadGizmosKey;
    bool reloadHitGizmos = false;

    [SerializeField][Range(0, 1)] float gizmoSize = 0.005f;
    [SerializeField] List<Vector3> hitPositionsListT0;
    [SerializeField] List<Vector3> hitPositionsListT1;
    [SerializeField] List<Vector3> normalsListT0;
    [SerializeField] List<Vector3> normalsListT1;
    Vector3 sphereOrigin = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        if(reloadGizmosKey == KeyCode.None)
        {
            reloadGizmosKey = KeyCode.Space;
        }
        sphereOrigin = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        Color mainCol = new Color(0, 0, 0, 0.5f);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2 coord = new Vector2((float)x / (float)width, (float)y / (float)height);
                coord.x = (coord.x * 2f - 1f);
                coord.y = (coord.y * 2f - 1f);
                float aspectRatio = (float)width / (float)height;
                coord.x *= aspectRatio;
                Vector3 rayOrigin = new Vector3(0, 0, (forwardOffset)); //Mathf.Abs(forwardOffset));
                Vector3 rayDir = new Vector3(coord.x, coord.y, -1.0f);
                //Vector3 RayDir = new Vector3(coord.x * (width / 2), coord.y * (height / 2), 0);
                //Color colorsUV = new Color(coord.x * 0.5f + 0.5f, coord.y * 0.5f + 0.5f, 0);
                //Debug.DrawLine(rayOrigin, rayOrigin + rayDir * length, colorsUV);


                //solve for t : t is the magnitude of the ray (quadratic formula below)
                //  BELOW IS a			BELOW IS b			BELOW IS C
                // (bx^2 + by^2)t^2 + (2(axbx + ayby))t + (ax^2 + ay^2 - r^2) = 0
                //x & y is the coords/origin | r is the radius | a is ray origin | b is ray direction


                //final quadratic equation implemented
                float a = Vector3.Dot(rayDir, rayDir);  //equivalent to float a =	rayDir.x * rayDir.x + rayDir.y * rayDir.y + rayDir.z * rayDir.z;
                float b = 2.0f * Vector3.Dot(rayDir, rayOrigin);
                float c = Vector3.Dot(rayOrigin, rayOrigin) - (radius * radius);

                //finding out the # of solutions from the quadratic equation
                //quad formula discriminant = b^2 - 4ac
                float discriminant = (b * b) - (4f * a * c);

                if (discriminant >= 0.0f)
                {
                    //return 0xff00ffff; abgr rgba

                    float t0 = (-b + Mathf.Sqrt(discriminant)) / (2.0f * a);
                    float t1 = (-b - Mathf.Sqrt(discriminant)) / (2.0f * a);


                    {
                        Vector3 hitPos = rayOrigin + rayDir * t0;
                        Vector3 normal = hitPos - sphereOrigin;
                        normal.Normalize();
                        if (!reloadHitGizmos)
                        {
                            hitPositionsListT0.Add(hitPos);
                            normalsListT0.Add(normal);
                        }
                    }
                    {
                        Vector3 hitPos = rayOrigin + rayDir * t1;
                        Vector3 normal = hitPos - sphereOrigin;
                        normal.Normalize();
                        if (!reloadHitGizmos)
                        {
                            hitPositionsListT1.Add(hitPos);
                            normalsListT1.Add(normal);
                        }
                    }


                    mainCol = new Color(1, 1, 1, 1);
                }
                else
                {
                    //return 0xff000000;
                    mainCol = new Color(0, 0, 0, root0Alpha);
                }

                if (isDebuggingRays)
                {
                    //return 0xff000000;
                    Debug.DrawLine(rayOrigin, rayOrigin + rayDir * length, mainCol);
                }

            }
        }

        //POST FOR LOOP
        ReloadGizmosManager();
    }

    void ReloadGizmosManager()
    {
        if (!reloadHitGizmos)
        {
            reloadHitGizmos = true;
        }

        if(Input.GetKeyDown(reloadGizmosKey))
        {
            hitPositionsListT0.Clear();
            hitPositionsListT1.Clear();
            normalsListT0.Clear();
            normalsListT1.Clear();
            reloadHitGizmos = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (isShowingSphereGizmo)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(Vector3.forward * forwardOffset, 0.25f);
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(Vector3.forward * forwardOffset, 0.25f);

            Gizmos.color = Color.white;
            Gizmos.DrawSphere(Vector3.zero, radius);
            Gizmos.DrawWireSphere(Vector3.zero, radius);
        }
        
        if (isShowingHitGizmos)
        {
            for (int i = 0; i < hitPositionsListT0.Count; i++)
            {
                float lightIntensity = Mathf.Max(Vector3.Dot(normalsListT0[i], -mainLight.transform.forward),0);
                //float lightIntensity = 1;

                Color currentColor = new Color(
                    (normalsListT0[i].x * 0.5f + 0.5f) * lightIntensity,
                    (normalsListT0[i].y * 0.5f + 0.5f) * lightIntensity,
                    (normalsListT0[i].z * 0.5f + 0.5f) * lightIntensity,
                    1);

                Gizmos.color = currentColor;
                //Gizmos.color = new Color(1 * lightIntensity, 1 * lightIntensity, 1 * lightIntensity, 1);
                Gizmos.DrawCube(hitPositionsListT0[i], Vector3.one * gizmoSize);
            }
            for (int i = 0; i < hitPositionsListT1.Count; i++)
            {
                float lightIntensity = Mathf.Max(Vector3.Dot(normalsListT1[i], -mainLight.transform.forward), 0);
                //float lightIntensity = 1;

                Color currentColor = new Color(
                    (normalsListT1[i].x * 0.5f + 0.5f) * lightIntensity,
                    (normalsListT1[i].y * 0.5f + 0.5f) * lightIntensity,
                    (normalsListT1[i].z * 0.5f + 0.5f) * lightIntensity,
                    1);

                Gizmos.color = currentColor;
                //Gizmos.color = new Color(1 * lightIntensity, 1 * lightIntensity, 1 * lightIntensity, 1);
                Gizmos.DrawCube(hitPositionsListT1[i], Vector3.one * gizmoSize);
            }
        }
    }



}
