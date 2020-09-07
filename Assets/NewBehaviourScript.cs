using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class NewBehaviourScript : MonoBehaviour
{
    public string MyName;
    public GameObject player;
    public GameObject camer;
    PhysicsRaycaster cast;
    private Vector2 lastMousPos;
	public Slider slider;
    bool isMouseDown = false;
    Rigidbody rb;
	public bool isPlayer1Turn = true;
    float x, y;
    public float power;
   // public Transform target;
   // public float speed;
    Ball_Laser ballLaser;
    LineRenderer lineRndr;
    // Use this for initialization
    void Start()
    {
        ballLaser = GetComponent<Ball_Laser>();
        lineRndr = GetComponent<LineRenderer>();

        ballLaser.enabled = false;
        lineRndr.enabled = false;

        rb = GetComponent<Rigidbody>();

		lastMousPos = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
       


		if (isMouseDown) {
			Vector2 curMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			slider.value = Vector2.Distance (lastMousPos, curMousePos)/4; 


		}

        if (Input.GetMouseButtonDown(0))
        {

        


            RaycastHit hitInfo = new RaycastHit();

            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            Debug.DrawRay(transform.position, Input.mousePosition);
            if (hit)
            {
                if (hitInfo.transform.gameObject.name == "ball")
                {
                    isMouseDown = true;

                    //rest rotation then enable laser code, this is esintal before laser cods enable
                    transform.rotation = Quaternion.identity;
                  
                    //enable laser code that cuse  enable laser
                    ballLaser.enabled = true;
                    lineRndr.enabled = true;

                    // ball set to sleep
                    rb.Sleep();

					lastMousPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

				
                }
             

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isMouseDown)
            {
                isMouseDown = false;
                ballLaser.enabled = false;
                lineRndr.enabled = false;



           

                //get camera degree and convert it to radious
                float camDeg = (Camera.main.transform.localEulerAngles.y - 90) * Mathf.Deg2Rad;

                //mouse up pos
                Vector2 curMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                //get distance(length of two pos) of mouse down pos from mouse up pos
                float distance = Vector2.Distance(lastMousPos, curMousePos);//Mathf.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));


                ////get length of finger poaint pos from first touch
                float angle = Mathf.Atan2(curMousePos.y - lastMousPos.y, curMousePos.x - lastMousPos.x);//Mathf.Atan2(y2 - y1, x2 - x1);//Mathf.Atan2(curMousePos.y - lastMousPos.y, curMousePos.x - lastMousPos.x);//Vector2.Angle(lastMousPos, curMousePos);
                //Debug.Log(angle*Mathf.Rad2Deg);

                //rotate mouse up pos to ecual it with camera rotation
                Vector2 translatePos = new Vector2();
                translatePos.x = Mathf.Sin(angle - camDeg) * distance + lastMousPos.x;
                translatePos.y = Mathf.Cos(angle - camDeg) * distance + lastMousPos.y;

                // power of ball hit 
				float rate = slider.value;

                //calculate which position force is given
                x = (rate /100f)* power *(lastMousPos.x - translatePos.x);
                y = (rate /100f)* power * (translatePos.y - lastMousPos.y);

                rb.AddForce(x, 0, y);
				slider.value = 0;
            }
        }
    }




}
