using UnityEngine;
using System.Collections;
using Leap;

public class hit : MonoBehaviour {

	Controller m_controller;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetMouseButtonDown(0)) {

			GameObject obj = GameObject.FindGameObjectWithTag("boss");
			Destroy(obj);
		}

		Frame frame = m_controller.Frame ();

		HandList hList = frame.Hands;
		
		if (m_controller.IsConnected) {


			if (hList[0] != null && frame.Hands.Count <= 1) { //if hand is not over LeapMotion == null & only 1 hand
				
				Hand hand = hList[0];

				Vector2 pos = new Vector2(hand.PalmPosition.x, hand.PalmPosition.y);
				pos = camera.ScreenToWorldPoint(pos);

				Debug.Log("x : " + pos.x + " y: " + pos.y);

				GameObject obj = GameObject.FindGameObjectWithTag("cursor");
				Vector2 objPos = obj.transform.position;







			}else
			{

			}


		}
	}
}
