using UnityEngine;
using System.Collections;
using Leap;

/// <summary>
/// 
/// </summary>
public class testFly : MonoBehaviour
{
	public float speed = 1.0f;
	public HandModel model;
	public HandModel handPhysics;
	public ToolModel tools;
	Controller m_leapController;
	

	// Use this for initialization
	void Start ()
	{
		m_leapController = new Controller ();
		if (transform.parent == null) {
			Debug.LogError ("LeapFly must have a parent object to control");
		}

	}

	Hand GetLeftMostHand (Frame f)
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

	Hand GetRightMostHand (Frame f)
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

	void assignHands ()
	{

		Frame frame = m_leapController.Frame ();
		Hand leftHand = GetLeftMostHand (frame);
		Hand rightHand = GetRightMostHand (frame);

		//if the left hand is further than right hand,
		//your left hand acts as your right hand
		//and your right as your left
		if (leftHand.PalmPosition.x > rightHand.PalmPosition.x) {
			leftHand = rightHand;
			rightHand = frame.Hands [0];
		}
	}

	void CalculateHandAngle (Frame frame)
	{
		//Doesn't give back a solid hand rotation value only
		//the an angle when the hand moves then back to 0

		var prevFrame = m_leapController.Frame (60); 

		float rotationX = frame.RotationAngle (prevFrame, Vector.XAxis);
		float rotationY = frame.RotationAngle (prevFrame, Vector.YAxis);
		float rotationZ = frame.RotationAngle (prevFrame, Vector.ZAxis);

		float DegreeX = ToDegrees (rotationX);
		float DegreeY = ToDegrees (rotationY);
		float DegreeZ = ToDegrees (rotationZ);

		Debug.Log ("X DEGREE: " + DegreeX);
		Debug.Log ("Y DEGREE: " + DegreeY);
		Debug.Log ("Z DEGREE: " + DegreeZ);
	}

	float ToDegrees (float Radian)
	{

		float Degrees;

		Degrees = Radian * 180 / Mathf.PI;

		return Degrees;
	}

	void HandRotation (Frame frame)
	{

		Hand hand = GetRightMostHand (frame);

		float pitch = hand.Direction.Pitch;
		float yaw = hand.Direction.Yaw;
		float roll = hand.Direction.Roll;

		float pitchDegrees = ToDegrees (pitch);
		float yawDegrees = ToDegrees (yaw);
		float rollDegrees = ToDegrees (roll);

		Debug.Log ("Pitch: " + pitchDegrees);
		Debug.Log ("Yaw: " + yawDegrees);
		Debug.Log ("Roll: " + rollDegrees);

		if (rollDegrees >= 30)
			transform.parent.rigidbody.velocity = new Vector3 (speed, 0, 0);
		else
		if (rollDegrees <= -30)
			transform.parent.rigidbody.velocity = new Vector3 (-speed, 0, 0);
		else
			if(rollDegrees > -30 || rollDegrees < 30)
				transform.parent.rigidbody.velocity = new Vector3(0, 0, 0);

	}

	void FixedUpdate ()
	{
		Frame frame = m_leapController.Frame ();
		Hand leftHand = GetLeftMostHand (frame);
		Hand rightHand = GetRightMostHand (frame);

		float instantaneousFrameRate = frame.CurrentFramesPerSecond;
		//Debug.Log ("FPS: " + instantaneousFrameRate);

		CalculateHandAngle (frame);
		HandRotation (frame);
		
		int handCount = frame.Hands.Count;
		Debug.Log ("hand count " + handCount);

//		float leftYaw = leftHand.PalmNormal.Yaw;
//		Debug.Log ("left yaw: " + leftYaw);
		
		Hand hand = frame.Hands [0];

		if (m_leapController.IsConnected) {
			Debug.Log ("CONNECTED");
		}

		if (leftHand.Direction.Roll >= 45) {
			Debug.Log (">45 ROLL");
			transform.parent.rigidbody.velocity = new Vector3 (speed, 0, 0);
		}


		//if we have 1 hand active
		if (frame.Hands.Count >= 1) {

			// if closed fist, then stop the plane and slowly go backwards.
//			if (frame.Fingers.Count < 3) {
//				transform.parent.rigidbody.velocity = new Vector3 (speed, 0, 0);
//			}

			//transform.parent.localRotation = Quaternion.Slerp(transform.parent.localRotation, Quaternion.Euler(newRot), 0.1f);
			//transform.parent.rigidbody.velocity = new Vector3(0, speed, 0);
		} else {
			transform.parent.rigidbody.velocity = new Vector3 (0, 0, 0);
		}

		if (frame.Hands.Count >= 2)
			transform.parent.rigidbody.velocity = new Vector3 (0, -speed, 0);


	}

}
