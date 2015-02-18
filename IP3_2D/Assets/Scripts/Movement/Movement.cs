using UnityEngine;
using System.Collections;
using Leap;

public enum MovementType
{
	stand,
	walk,
	run,
	jump,
	fall,
	fly
}
;

public class Movement : MonoBehaviour
{
	
	public float walkSpeed = 1.0f;
	public float runSpeed = 2.0f;
	public float flySpeed = 3.0f;
	public float jumpPower = 1.0f;
	public float fallPower = 1.0f;
	public float rotationSpeed = 0.5f;

	[HideInInspector]
	public bool getfaceRight { get; set; }
	[HideInInspector]
	public float getPitchAngle { get; set; }
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
	
	public void CheckMovementType (MovementType type, bool faceRight)
	{
		//Change 2D face direction to right/left
		getfaceRight = faceRight;
		
		Debug.Log ("Type: " + type);
		
		if (faceRight) { 
			Debug.Log ("FACERIGHT");
			transform.localScale = new Vector3 (1f, 1f, 1f); 
		}
		else {
			Debug.Log("NOT FACERIGHT");
			transform.localScale = new Vector3 (-1f, 1f, 1f);
		}
		
		//Movement related below (will make more complex later):

		switch (type) {

		case MovementType.stand:
			transform.parent.rigidbody.velocity = new Vector3 (0, 0, 0);
			break;
		
		case MovementType.walk:
			if (faceRight)
				transform.parent.rigidbody.velocity = new Vector3 (walkSpeed, 0, 0);
			else
				transform.parent.rigidbody.velocity = new Vector3 (-walkSpeed, 0, 0);
			break;

		case MovementType.run:
			if (faceRight)
				transform.parent.rigidbody.velocity = new Vector3 (runSpeed, 0, 0);
			else 
				transform.parent.rigidbody.velocity = new Vector3 (-runSpeed, 0, 0);
			break;

		case MovementType.jump:
			if (faceRight)
				transform.parent.rigidbody.velocity = new Vector3 (0, jumpPower, 0);
			break;

		case MovementType.fly: 
			if (faceRight) {
				transform.parent.rigidbody.velocity = new Vector3 (2, 0, 0);
				float rot = getPitchAngle * rotationSpeed;

				transform.parent.rotation = Quaternion.Euler (new Vector3 (0, 0, rot));
				transform.parent.position += transform.right * Time.deltaTime * flySpeed;
			} else {
				transform.parent.rigidbody.velocity = new Vector3 (2, 0, 0);
				float rot = getPitchAngle * rotationSpeed;

				transform.parent.rotation = Quaternion.Euler (new Vector3 (0, 0, rot));
				transform.parent.position -= transform.right * Time.deltaTime * flySpeed;
			}
			break;
		}
	}
}
