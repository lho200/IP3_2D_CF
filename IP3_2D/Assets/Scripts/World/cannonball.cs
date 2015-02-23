using UnityEngine;
using System.Collections;

public class cannonball : MonoBehaviour {

	public float rotationSpeed;
	public float speed;

	Transform myTarget;  // drag the target here
	Rigidbody cannonballs;  // drag the cannonball prefab here
	float shootAngle = 30;  // elevation angle

	Vector3 ballisticVelocity(Transform target, float angle){

		Vector2 dir = target.position - transform.position;  // get target direction
		float h = dir.y;  // get height difference
		dir.y = 0;  // retain only the horizontal direction
		float dist = dir.magnitude ;  // get horizontal distance
		float a = angle * Mathf.Deg2Rad;  // convert angle to radians
		dir.y = dist * Mathf.Tan(a);  // set dir to the elevation angle
		dist += h / Mathf.Tan(a);  // correct for small height differences
		// calculate the velocity magnitude
		float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
		return vel * dir.normalized;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyDown("b")){  // press b to shoot
		//	cannonballs = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), transform.position, Quaternion.identity);
		//	cannonballs.rigidbody.velocity = ballisticVelocity(myTarget, shootAngle);
		//	Destroy(cannonballs, 10);
		//}
	}
}
