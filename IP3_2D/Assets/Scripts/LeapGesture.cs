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

		degreeX = rotationX * Mathf.Rad2Deg;
		degreeY = rotationY * Mathf.Rad2Deg;
		degreeZ = rotationZ * Mathf.Rad2Deg;
	
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
		float roll = hand.PalmNormal.Roll;

		pitchDegrees = pitch * Mathf.Rad2Deg;
		yawDegrees = yaw * Mathf.Rad2Deg;
		rollDegrees = roll * Mathf.Rad2Deg;;
		
	   Debug.Log ("Pitch: " + pitchDegrees);
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

		if (yawDegrees <= 40 && yawDegrees >= -40) {
		
			if (rollDegrees >= -100 || rollDegrees <= 100 || !hand.IsValid) {
				type = MovementType.stand;
				movement.CheckMovementType (type, movement.getfaceRight);
			}

			//note: if we have up/down movement with pitch it'll collide with left/right gestures
			//note: since pitch goes to negative it makes our speed negative :/

			if(pitchDegrees <= 45 && pitchDegrees <= -45){
				type = MovementType.flyAscend;
				movement.CheckMovementType (type, movement.getfaceRight);
			}
		}
	
		//Flying code: 

		//note: In order to start flying, hand's pitch must be around 10 & -10 degrees? (yet to be decided)

		if (hand.IsLeft) {
			Debug.Log ("FLY LEFT HAND");
			if (yawDegrees >= 40 && yawDegrees <= 180) {
				type = MovementType.fly;

				if (pitchDegrees <= 45 && pitchDegrees >= -45) {
					movement.getPitchAngle = pitchDegrees;
					movement.CheckMovementType (type, true);
				}
				CheckAngle(type,hand, true);
			}
		}

		if (hand.IsRight) {
			if (yawDegrees <= -40 && yawDegrees >= -180) {
				type = MovementType.fly;

				if(pitchDegrees <= 45 && pitchDegrees >= -45){
				movement.getPitchAngle = pitchDegrees * -1;
				movement.CheckMovementType (type, false);
				}
				CheckAngle(type,hand, false);
			}
		}
		//End of fly code.
	}

	/// <summary>
	/// Make sure angle doesn't exceed 45/-45 on pitch axis
	/// </summary>
	void CheckAngle(MovementType type, Hand hand, bool direction)
	{
		if(pitchDegrees >= 45)
		{
			if(hand.IsLeft)
			movement.getPitchAngle = 45;
			else
			movement.getPitchAngle = -45;

			movement.CheckMovementType (type, direction);
		}else if(pitchDegrees <= -45)
		{			
			if(hand.IsLeft)
				movement.getPitchAngle = -45;
			else
				movement.getPitchAngle = 45;

			movement.CheckMovementType (type, direction);
		}
	}
}
