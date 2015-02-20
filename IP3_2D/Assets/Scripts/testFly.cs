using UnityEngine;
using System.Collections;
using Leap;

/// <summary>
/// 
/// </summary>
public class testFly : MonoBehaviour
{
	private LeapGesture gesture;
	Controller m_leapController;
	
	// Use this for initialization
	void Start ()
	{
		m_leapController = new Controller ();
		gesture = gameObject.AddComponent<LeapGesture> ();

		if (transform.parent == null) {
			Debug.LogError ("LeapFly must have a parent object to control");
		}
	}

	public Hand GetLeftMostHand (Frame f)
	{
		float smallestVal = float.MaxValue;
		Hand h = null;
		for (int i = 0; i < f.Hands.Count; ++i) {
			if (f.Hands [i].PalmPosition.ToUnity ().x < smallestVal) {
				smallestVal = f.Hands [i].PalmPosition.ToUnity ().x;
				h = f.Hands [i];
			}
		}
		return h;
	}

	public Hand GetRightMostHand (Frame f)
	{
		float largestVal = -float.MaxValue;
		Hand h = null;
		for (int i = 0; i < f.Hands.Count; ++i) {
			if (f.Hands [i].PalmPosition.ToUnity ().x > largestVal) {
				largestVal = f.Hands [i].PalmPosition.ToUnity ().x;
				h = f.Hands [i];
			}
		}
		return h;
	}

	void assignHands (Frame frame, Hand leftHand, Hand rightHand)
	{
		//if left hand overlaps right hand left hand acts as right
		if (leftHand.PalmPosition.x > rightHand.PalmPosition.x) {
			leftHand = rightHand;
			rightHand = frame.Hands [0];
		}

		if (rightHand.PalmPosition.x < leftHand.PalmNormal.x) {
			rightHand = leftHand;
			leftHand = frame.Hands[0]; //check this value [0]
		}
	}

	
	void FixedUpdate ()
	{
		Frame frame = m_leapController.Frame ();
		Hand leftHand = GetLeftMostHand (frame);
		Hand rightHand = GetRightMostHand (frame);

		Hand hand = frame.Hands.Leftmost;

		HandList hList = frame.Hands;



		//NOTE: IF using right hand for flying Angles will be WEIRD
		//NOTE: Flip your right hand upside down confuses it for LEFT!

		if (m_leapController.IsConnected) {

			Debug.Log ("CONNECTED");

			gesture._frame = frame;
			gesture.m_leapController = m_leapController;

			if (hList[0] != null) { //if hand is not over LeapMotion == null
			
			//	Time.timeScale = 1.0f;
				gesture.HandPlayerMovement (frame, rightHand, m_leapController);
			}
		} else {

			//Time.timeScale = 0.0f;
			//not connected -- pause game -- await connection
		}

	
		//if we have 1 hand active


			// if closed fist, then stop the plane and slowly go backwards.
//			if (frame.Fingers.Count < 3) {
//				transform.parent.rigidbody.velocity = new Vector3 (speed, 0, 0);
//			}
	
	}

}