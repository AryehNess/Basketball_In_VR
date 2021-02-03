using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour {
	bool isLoading=false;
	void Start () {
	}

	void OnGUI(){
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		if (GUI.Button (new Rect ((Screen.width/2)-50, Screen.height-450, 100, 50), "PLAY")) {
			UnityEngine.SceneManagement.SceneManager.LoadScene (1);
			isLoading = true;
		}
		if (GUI.Button (new Rect ((Screen.width/2)-50, Screen.height-300, 100, 50), "QUIT")) {
			Application.Quit ();
		}
		if (isLoading) {
			GUI.Label (new Rect ((Screen.width / 2) - 200, (Screen.height / 2) - 100, 100, 50), "Loading...");
		}
	}
	//void Update () {
	//}
}
