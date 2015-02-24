using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {
	

	// Update is called once per frame
	public void LoadScene() {
		Application.LoadLevel ("Game");
	}
}
