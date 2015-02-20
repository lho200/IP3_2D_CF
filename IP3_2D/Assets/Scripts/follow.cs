using UnityEngine;
using System.Collections;

public class follow : MonoBehaviour {

		
	// Update is called once per frame
	void Update () {

		GameObject obj = GameObject.FindGameObjectWithTag ("Player");
	
		Vector3 pos = obj.transform.position;
		gameObject.transform.position = pos;

	}
}
