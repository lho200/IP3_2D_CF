using UnityEngine;
using System.Collections;

public enum MovementType{
	stand,
	walk,
	run,
	jump,
	fall,
	fly
};

public class Movement : MonoBehaviour {
	
	public float walkSpeed = 1.0f;
	public float runSpeed = 2.0f;
	public float flySpeed = 3.0f;
	public float jumpPower = 1.0f;
	public float fallPower = 1.0f;
	public float rotationSpeed = 1.0f;
	
	public bool getfaceRight { get; set; }
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void CheckMovementType(MovementType type, bool faceRight = true){

		//Change 2D face direction to right/left
		getfaceRight = faceRight;

		Debug.Log ("Type: " + type);

		if (faceRight)
			transform.localScale =  new Vector3 (1f, 1f, 1f); //need to add code so it doesn't constantly access this
		else
			transform.localScale =  new Vector3 (-1f, 1f, 1f);

		//Movement related below (will make more complex later):

		if(type == MovementType.stand)
			transform.parent.rigidbody.velocity = new Vector3 (0, 0, 0);

		if (type == MovementType.walk && faceRight) {
			Debug.Log ("walk right");
			transform.parent.rigidbody.velocity = new Vector3 (walkSpeed, 0, 0);
		} else
			if(type == MovementType.walk && !faceRight){
			Debug.Log("walk left");
			transform.parent.rigidbody.velocity = new Vector3 (-walkSpeed, 0, 0);
		}

		if (type == MovementType.run && faceRight) {
			Debug.Log ("run right");
			transform.parent.rigidbody.velocity = new Vector3 (runSpeed, 0, 0);
		} else 
			if(type == MovementType.run && !faceRight){
			Debug.Log("run left");
			//transform.parent.rigidbody.velocity = new Vector3 (-runSpeed, 0, 0);
			}

		if (type == MovementType.jump && faceRight) {
			Debug.Log("jump");
			//transform.parent.rigidbody.velocity = new Vector3 (0, jumpPower, 0);
		}

		if (type == MovementType.fly && faceRight) {
			Debug.Log ("fly jump up");
			//transform.parent.rigidbody.velocity = new Vector3 (0, jumpPower, 0);
		} else {
			if(type == MovementType.fly && !faceRight)
			Debug.Log("fly jump down");
			//transform.parent.rigidbody.velocity = new Vector3 (0, fallPower, 0);
		}
	}
}
