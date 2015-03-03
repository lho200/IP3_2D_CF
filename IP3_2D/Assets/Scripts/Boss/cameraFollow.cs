using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Camera cam = GameObject.FindGameObjectWithTag ("MainCamera").gameObject.camera;

		float playerPositionX = GameObject.FindGameObjectWithTag ("Player").transform.position.x;
		Vector3 playerPos = new Vector3 (playerPositionX + 4, 11, -5);
		cam.transform.position = playerPos;
	}
}
