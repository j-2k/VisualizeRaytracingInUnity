using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaytracingVisualization : MonoBehaviour
{
    [Header("Main Options")]
    [SerializeField] int width = 100;
    [SerializeField] int height = 100;
    [SerializeField] float length = 2;
    [SerializeField] float radius = 0.25f;
    [SerializeField] float forwardOffset = 2;

    [Header("Other Options")]
    [SerializeField] [Range(0, 1)] float root0Alpha = 0.5f;
    [SerializeField] bool showCorners = false;
    [SerializeField] Transform[] corners = new Transform[4];

    // Start is called before the first frame update
    void Start()
    {
        if (showCorners)
        {
            for (int i = 0; i < corners.Length; i++)
            {
                corners[i].transform.gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Color mainCol = new Color(0,0,0,0.5f);
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
                float a = Vector3.Dot(rayDir, rayDir);	//equivalent to float a =	rayDir.x * rayDir.x + rayDir.y * rayDir.y + rayDir.z * rayDir.z;
	            float b = 2.0f * Vector3.Dot(rayDir, rayOrigin);
	            float c = Vector3.Dot(rayOrigin, rayOrigin) - (radius * radius);

	            //finding out the # of solutions from the quadratic equation
	            //quad formula discriminant = b^2 - 4ac
	            float discriminant = (b * b) - (4f * a * c);

	            if (discriminant >= 0.0f)
	            {
                    //return 0xff00ffff; abgr rgba
                    mainCol = new Color(1,1,1,1);
                }
                else
                {
                    //return 0xff000000;
                    mainCol = new Color(0,0,0, root0Alpha);
                }

                //return 0xff000000;
                Debug.DrawLine(rayOrigin, rayOrigin + rayDir * length, mainCol);
                
            }
        }
        //POST FOR LOOP

        CornerHandler();
    }

    void CornerHandler()
    {
        /*
        corners[0].transform.position = new Vector3(width/2, height/2, 0);
        corners[1].transform.position = new Vector3(width/2, -height/2, 0);
        corners[2].transform.position = new Vector3(-width/2, height/2, 0);
        corners[3].transform.position = new Vector3(-width/2, -height/2, 0);
        */
        if (showCorners)
        {
            for (int i = 0; i < corners.Length; i++)
            {
                //float xx = width / 2, yy = height / 2;
                float xx = 1, yy = 1;
                if (i >= 2) { xx = -xx; }
                if (i % 2 == 1) { yy = -yy; }
                corners[i].transform.position = new Vector3(xx, yy, 0);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(Vector3.forward * forwardOffset, radius);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(Vector3.forward * forwardOffset, radius);

        Gizmos.color = Color.white;
        Gizmos.DrawSphere(Vector3.zero,radius);
        Gizmos.DrawWireSphere(Vector3.zero, radius);
    }
}
