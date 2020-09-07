using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class respawnd : MonoBehaviour {
	public NewBehaviourScript sc;
	public Text score_2;
	public Text score_1;
	public Text player_2;
	public Text player_1;
	// Use this for initialization
	void Start () {
		//text = 0;
		player_1.color = Color.blue;
	}
   public void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.name != "ball") {
			
			Destroy (other.gameObject);
	
			if (sc.isPlayer1Turn) {
				player_1.color = Color.black;
				player_2.color = Color.blue;
				sc.isPlayer1Turn = false;
				score_1.text = (int.Parse(score_1.text)+1).ToString();
			} else {
				score_2.text = (int.Parse(score_2.text)+1).ToString();
				sc.isPlayer1Turn = true;
				player_1.color = Color.blue;
				player_2.color = Color.black;
			}

		}
        else
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.Sleep();
            other.gameObject.transform.position = new Vector3(5, 0, 10);

        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
