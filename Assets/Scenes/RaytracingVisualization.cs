using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class RaytracingVisualization : MonoBehaviour
{
    [SerializeField] int width = 50, height = 50;
    const int offset = 50;
    [SerializeField] Transform[] corners = new Transform[4];

    // Start is called before the first frame update
    void Start()
    {
        transform.position = -(Vector3.forward) * 50;
    }

    // Update is called once per frame
    void Update()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2 coord = new Vector2((float)x / (float)width, (float)y / (float)height);
                coord.x = coord.x * 2f - 1f;
                coord.y = coord.y * 2f - 1f;
                Vector3 RayDir = new Vector3(coord.x * offset, coord.y * offset, 0);
                //Vector3 RayDir = new Vector3(coord.x * (width / 2), coord.y * (height / 2), 0);
                Color col = new Color(coord.x * 0.5f + 0.5f, coord.y * 0.5f + 0.5f, 0);
                Debug.DrawLine(transform.position, RayDir, col);
            }
        }

        /*
        corners[0].transform.position = new Vector3(width/2, height/2, 0);
        corners[1].transform.position = new Vector3(width/2, -height/2, 0);
        corners[2].transform.position = new Vector3(-width/2, height/2, 0);
        corners[3].transform.position = new Vector3(-width/2, -height/2, 0);
        */

        for (int i = 0; i < corners.Length; i++)
        {
            //float xx = width / 2, yy = height / 2;
            float xx = offset, yy = offset;
            if (i >= 2 ) { xx = -xx; }
            if (i % 2 == 1) { yy = -yy; }
            corners[i].transform.position = new Vector3(xx,yy, 0);
        }
    }
}
