using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {

	Button btnPlayGame;
	Button btnStore;
	Button btnSettings;
	Button btnQuit;

	// Use this for initialization
	void Start () {


	
		btnPlayGame.transform.position = new Vector2 (Screen.width / 2, Screen.height / 2);
		btnStore.transform.position = new Vector2 (Screen.width / 2, Screen.height / 2 - 10);
		btnSettings.transform.position = new Vector2 (Screen.width / 2, Screen.height / 2 - 20);
		btnQuit.transform.position = new Vector2 (Screen.width / 2, Screen.height / 2 - 30);


		//btnPlayGame = GUI.Button (new Rect (10, 10, Screen.width / 2, Screen.height / 2), new GUIContent ("Play Game"));

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
