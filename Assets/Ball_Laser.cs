using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ball_Laser : MonoBehaviour {


    LineRenderer lineRenderer;
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer = 1;
    Vector3 v;

    Camera viewCamera;
    Vector3 mousePos;

    bool isMouseDown = false;

    Vector3[] positions;


    float lineRendererLength = 10;
    // Use this for initialization
    void Start()
    {

        mousePos = transform.position;

        viewCamera = Camera.main;

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.1f;

        lineRenderer.enabled = true;
        lineRenderer.useWorldSpace = false;
        float alpha = 1.0f;
        lineRenderer.startColor = c1;
        lineRenderer.endColor = c2;

        // Set some positions
        positions = new Vector3[2];
        positions[0] = new Vector3(0.0f, 0.0f, 0.0f);// set first point to orgin of ball
        positions[1] = new Vector3(0.0f, 0.0f, 0.0f); // temporary pos, this pos is chanded 
       // positions[2] = new Vector3(0.0f, 0.0f, 0.0f);

        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
        
    }

    // Update is called once per frame
    void Update()
    {

        lineRenderer.enabled = true;
        mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
       
       transform.LookAt(mousePos + Vector3.up * transform.position.y);
     
        /*Vector3 targetDir = transform.TransformDirection(mousePos + Vector3.up * transform.position.y) - transform.position;
        float step = 360 * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Vector3 forward = -newDir;*/

        RaycastHit hits;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hits, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.forward) * hits.distance, Color.yellow);
            lineRendererLength = -hits.distance / 1.3f; //1.3f is the ball scale

           positions[1] = hits.point;//(hits.point - transform.position) / 1.3f;

           if (hits.transform.gameObject.name == "default")
            {

                lineRenderer.positionCount = 3;
                var delta =  getAngle(transform.position , positions[1]);//positions[1].x, 0, -positions[1].z)
                
            }
            else
            {

                lineRenderer.positionCount = 2;
            }

            positions[1] = new Vector3(0.0f, 0.0f, lineRendererLength);
            lineRenderer.SetPositions(positions);
            // drawReflexRay(hits.point);*
        }
    
    }
    float getAngle(Vector2 pos1, Vector2 pos2)
    {
        return Mathf.Atan2(pos1.y - pos2.y, pos1.x - pos2.x) + (Mathf.PI / 2);//(Mathf.PI/2) is the telorance, =90 degree

    }
    Vector3 drawRayCast(Vector3 from, Vector3 to)
    {
        Vector3 targetDir = transform.TransformDirection(to + Vector3.up * from.y) - from;
        float step = 360 * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Vector3 forward = -newDir;

        RaycastHit hits;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(from, transform.TransformDirection(forward), out hits, Mathf.Infinity))
        {
            Debug.DrawRay(from, transform.TransformDirection(forward) * hits.distance, Color.yellow);
            return (hits.point - transform.position) / 1.3f;
            positions[1] = (hits.point - transform.position) / 1.3f;

            if (hits.transform.gameObject.name == "default")
            {

                lineRenderer.positionCount = 3;
                var delta = Mathf.Atan2(transform.position.x - positions[1].x, transform.position.z - positions[1].z);//positions[1].x, 0, -positions[1].z)
                print(delta * Mathf.Rad2Deg);
                positions[2] = Vector3.right;//(transform.position, new Vector3(positions[1].x, 0, -positions[1].z));
            }
            else
            {

                lineRenderer.positionCount = 2;
            }
            lineRenderer.SetPositions(positions);
            //lineRendererLength = -hits.distance/1.3f; //1.3f is the ball scale
            // drawReflexRay(hits.point);
        }
        //set point 2 of the line
        /*positions[1] = new Vector3(0, 0, lineRendererLength);
        lineRenderer.SetPositions(positions);*/
        return new Vector3(0,0,0);
    }
    void drawReflexRay(Vector3 from)
    {
        Vector3 to = Vector3.forward;
        RaycastHit hits;
   
        if (Physics.Raycast(from, transform.TransformDirection(Vector3.up), out hits, Mathf.Infinity))
        {
            Debug.DrawRay(from, transform.TransformDirection(-Vector3.up) * hits.distance, Color.yellow);
           
        }
        
    }

}
