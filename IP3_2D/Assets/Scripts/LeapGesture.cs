using UnityEngine;
using System.Collections;
using Leap;

public class LeapGesture : MonoBehaviour
{

	public float degreeX { get; set; }
	public float degreeY { get; set; }
	public float degreeZ { get; set; }
	
	public float pitchDegrees { get; set; }
	public float yawDegrees { get; set; }
	public float rollDegrees { get; set; }

	
	public Frame _frame { get; set; }
	public Controller m_leapController { get; set; }

	private Movement movement;

	// Use this for initialization
	void Start ()
	{
		movement = gameObject.AddComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void CalculateHandAngle ()
	{
		var prevFrame = m_leapController.Frame (30); 
		
		float rotationX = _frame.RotationAngle (prevFrame, Vector.XAxis);
		float rotationY = _frame.RotationAngle (prevFrame, Vector.YAxis);
		float rotationZ = _frame.RotationAngle (prevFrame, Vector.ZAxis);

//		Debug.Log ("rotation Y" + rotationY);
		
		degreeX = ToDegrees (rotationX);
		degreeY = ToDegrees (rotationY);
		degreeZ = ToDegrees (rotationZ);
//		
//		Debug.Log ("X DEGREE: " + degreeX);
//		Debug.Log ("Y DEGREE: " + degreeY);
//		Debug.Log ("Z DEGREE: " + degreeZ);
	}

	void HandRotation (Hand hand)
	{
		float pitch = hand.Direction.Pitch;
		float yaw = hand.Direction.Yaw;
		float roll = hand.Direction.Roll;
		
		pitchDegrees = ToDegrees (pitch);
		yawDegrees = ToDegrees (yaw);
		rollDegrees = ToDegrees (roll);
		
		//Debug.Log ("Pitch: " + pitchDegrees);
		//Debug.Log ("Yaw: " + yawDegrees);
		//Debug.Log ("Roll: " + rollDegrees);
	}

	public void HandPlayerMovement (Frame frame, Hand hand, Controller controller)
	{
		_frame = frame;
		MovementType type;

		HandRotation (hand);
		CalculateHandAngle ();

		//Hand rotation for left/right movement below:

		if (rollDegrees >= 80) {
			type = MovementType.walk;
			movement.CheckMovementType (type, true);
//			Debug.Log("moving right");
		} else
			if (rollDegrees <= -80) {
			type = MovementType.walk;
			movement.CheckMovementType (type, false);
//			Debug.Log("moving left");
		} else
			if (rollDegrees >= -70 || rollDegrees <= 70) {
			//get faceRight state, use for standing CheckMovementType() method
			type = MovementType.stand;
			movement.CheckMovementType (type, movement.getfaceRight);
//			Debug.Log("standing");
		}

		//pseudocode:
		//compare DegreeX to previous X every second or millisecond
		//
	}

	void flying(MovementType type, float yawAngle, float pitchAngle){

		//need to pass in pitchAngle 

		if (yawAngle >= 60 && yawAngle <= 120)
			movement.CheckMovementType (type, true);
		else
			if(yawAngle <= -60 && yawAngle >= -120)
				movement.CheckMovementType (type, false);
	}
	
	float ToDegrees (float Radian)
	{
		float Degrees;
		Degrees = Radian * 180 / Mathf.PI;
		return Degrees;
	}
}
