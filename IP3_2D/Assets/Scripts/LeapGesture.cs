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
		movement = gameObject.AddComponent<Movement> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void CalculateHandAngle ()
	{
		var prevFrame = m_leapController.Frame (1); 
		
		float rotationX = _frame.RotationAngle (prevFrame, Vector.XAxis);
		float rotationY = _frame.RotationAngle (prevFrame, Vector.YAxis);
		float rotationZ = _frame.RotationAngle (prevFrame, Vector.ZAxis);

		degreeX = ToDegrees (rotationX);
		degreeY = ToDegrees (rotationY);
		degreeZ = ToDegrees (rotationZ);
	
//		Debug.Log ("X DEGREE: " + degreeX);
//		Debug.Log ("Y DEGREE: " + degreeY);
//		Debug.Log ("Z DEGREE: " + degreeZ);
	}

	void HandRotation (Hand hand)
	{
		//The normal vector to the palm.
		//If your hand is flat, this vector will point downward, or “out” of the front surface of your palm.


		float pitch = hand.Direction.Pitch;
		float yaw = hand.Direction.Yaw;
		float roll = hand.Direction.Roll;

		pitchDegrees = ToDegrees (pitch);
		yawDegrees = ToDegrees (yaw);
		rollDegrees = ToDegrees (roll);
		
	Debug.Log ("Pitch: " + pitchDegrees);
//		Debug.Log ("Yaw: " + yawDegrees);
//		Debug.Log ("Roll: " + rollDegrees);
	}

	public void HandPlayerMovement (Frame frame, Hand hand, Controller controller)
	{
		_frame = frame;
		MovementType type;

		HandRotation (hand);
		CalculateHandAngle ();

		//Hand rotation for left/right movement below:

		if (yawDegrees <= 50 && yawDegrees >= -50) {
		
			if (rollDegrees >= 90) {
				type = MovementType.walk;
				movement.CheckMovementType (type, true);
			} else
			if (rollDegrees <= -90) {
				type = MovementType.walk;
				movement.CheckMovementType (type, false);
			} else
			if (rollDegrees >= -70 || rollDegrees <= 70 || !hand.IsValid) {
				type = MovementType.stand;
				movement.CheckMovementType (type, movement.getfaceRight);
			}

			//note: if we have up/down movement with pitch it'll collide with left/right gestures

			if(pitchDegrees >= 10){
				type = MovementType.flyAscend;
				movement.CheckMovementType (type, movement.getfaceRight);
				
			}
			else
			if(pitchDegrees <= -10){
				type = MovementType.flyDescend;
				movement.CheckMovementType (type, movement.getfaceRight);

			}

		  
		}
	
		//Jump code:

		//Flying code: 

		if (hand.IsLeft)
		if (yawDegrees >= 70 && yawDegrees <= 110) {
			type = MovementType.fly;
			movement.getPitchAngle = pitchDegrees;
			movement.CheckMovementType (type, true);
		}

		if (hand.IsRight) {
			if (yawDegrees <= -70 && yawDegrees >= -110) {
				Debug.Log ("left fly hit");
				type = MovementType.fly;
				movement.CheckMovementType (type, false);
				movement.getPitchAngle = pitchDegrees * -1;
			}




		}


	}
	
	float ToDegrees (float Radian)
	{
		float Degrees;
		Degrees = Radian * 180 / Mathf.PI;
		return Degrees;
	}
}
