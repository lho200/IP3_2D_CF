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
	fly,
	flyAscend,
	flyDescend
}
;

public class Movement : MonoBehaviour
{
	
	public float walkSpeed = 1.0f;
	public float runSpeed = 2.0f;
	public float flySpeed = 15.0f;
	public float jumpPower = 1.0f;
	public float fallPower = 1.0f;
	public float rotationSpeed = 0.5f;

	[HideInInspector]
	public bool getfaceRight { get; set; }

	[HideInInspector]
	public float getPitchAngle { get; set; }

	[HideInInspector]
	private float speed = 0.3f;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
	/// <summary>
	/// Resets the rotation back to 0 
	/// Doesn't just set back to 0, does it gradually over time (needs fixed as it appears it just sets it to 0)
	/// </summary>
	void resetRotation() 
	{
		float objZrot = transform.eulerAngles.z;
		float tParam = 0.5f;

		if (objZrot != 0) 
		if (objZrot >= 0 && objZrot <= 100) {
			tParam += Time.deltaTime * speed;
			//transform.eulerAngles.z += Mathf.Lerp (transform.rotation.z, 0, tParam);
			float value = Mathf.Lerp(transform.rotation.z, 0, tParam);
			transform.parent.rotation = Quaternion.Euler(new Vector3(0,0, value));
		}
		else if (objZrot >= 300 && objZrot <= 360) {
			tParam += Time.deltaTime * speed;
			float value = Mathf.Lerp(transform.rotation.z, 0, tParam);
			transform.parent.rotation = Quaternion.Euler(new Vector3(0,0, value));
			//Debug.Log("VALUE: " + value);
		}
	}
	
	public void CheckMovementType (MovementType type, bool faceRight)
	{
		//Change 2D face direction to right/left
		getfaceRight = faceRight;

		Debug.Log ("Type: " + type);
		
		if (faceRight) { 
			Debug.Log ("FACERIGHT");
			transform.localScale = new Vector3 (1f, 1f, 1f); 
		} else {
			Debug.Log ("NOT FACERIGHT");
			transform.localScale = new Vector3 (-1f, 1f, 1f);
		}
		
		//Movement related below (will make more complex later):

		switch (type) {

		case MovementType.stand:
			transform.parent.rigidbody.velocity = new Vector3 (0, 0, 0);

			resetRotation();

			break;
		
		case MovementType.walk:
			if (faceRight)
				transform.parent.rigidbody.velocity = new Vector3 (walkSpeed, 0, 0); 
			else
				transform.parent.rigidbody.velocity = new Vector3 (-walkSpeed, 0, 0);

			resetRotation();
			break;

		case MovementType.run:
			if (faceRight)
				transform.parent.rigidbody.velocity = new Vector3 (runSpeed, 0, 0) * Time.deltaTime;
			else 
				transform.parent.rigidbody.velocity = new Vector3 (-runSpeed, 0, 0) * Time.deltaTime;
			break;

		case MovementType.jump:
			if (faceRight)
				transform.parent.rigidbody.velocity = new Vector3 (0, jumpPower, 0);
			break;

		case MovementType.fly: 
			if (faceRight) {
				float rot = getPitchAngle * rotationSpeed;
				transform.parent.rotation = Quaternion.Euler (new Vector3 (0, 0, rot));
				transform.parent.position += transform.right * Time.deltaTime * flySpeed;
			} else {
				float rot = getPitchAngle * rotationSpeed;
				transform.parent.rotation = Quaternion.Euler (new Vector3 (0, 0, rot));
				transform.parent.position -= transform.right * Time.deltaTime * flySpeed;
			}

			break;

		case MovementType.flyAscend:

							//convert rot to suitable speed value
		    float ascendSpeed = getPitchAngle / 10;
			transform.parent.rigidbody.velocity  = new Vector3(0, ascendSpeed,0 );



			break;

	   case MovementType.flyDescend:
		//convert rot to suitable speed value
			float descentSpeed = getPitchAngle / 10 * -1;
		
			transform.parent.rigidbody.velocity = new Vector3(0, descentSpeed,0 );
		break;

		

		default:
			// unknown type! 
			// there should probably be some error-handling
			// here, maybe an exception
			break;
		}
	}
}
