using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	GameObject[] pauseObjects;
	public bool pauseCheck = true;

	public Text displayTime;
	public float mainTimer;
	private float timer;
	private bool canCount=true;

	void Start () {
		timer = mainTimer;
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag ("ShowOnPaused");
		hidePaused ();
	}

	public void time(){
		if (canCount && timer > 0.0f) {
			timer -= Time.deltaTime;
			string minutes = Mathf.Floor(timer / 60).ToString("00");
			string seconds = Mathf.Floor(timer % 60).ToString("00");

			displayTime.text = "Time Left: " + minutes + ":" + seconds;
		} else if (timer <= 0.0f) {
			displayTime.text = "Time Left: 0.0";
			timer = 0.0f;
			canCount = false;
			UnityEngine.SceneManagement.SceneManager.LoadScene (0);
		}
	}

	public void Reload(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (1);
	}

	public void pauseControl(){
		if (Time.timeScale == 1) {
			Time.timeScale = 0;
			showPaused ();
		} else if (Time.timeScale == 0) {
			Time.timeScale = 1;
			hidePaused ();
		}
	}

	public void showPaused(){
		pauseCheck = false;
		canCount = false;
		foreach (GameObject g in pauseObjects) {
			g.SetActive (true);
		}
	}

	public void hidePaused(){
		pauseCheck = true;
		canCount = true;
		foreach (GameObject g in pauseObjects) {
			g.SetActive (false);
		}
	}	

	public void LoadLevel(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}

	// Update is called once per frame
	void Update () {
		Cursor.visible = false;
		if (!pauseCheck) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		time ();
		if (Input.GetKeyDown (KeyCode.P)) {
			if (Time.timeScale == 1) {
				Time.timeScale = 0;
				showPaused ();
			} else if (Time.timeScale == 0) {
				Time.timeScale = 1;
				hidePaused ();
			}
		}
	}
}
