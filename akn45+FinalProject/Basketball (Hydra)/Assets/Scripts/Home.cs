using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour {

	private int score1;
	public Text home;
	public Player player;
	GameObject[] line;
	private float[] size=new float[9]; 
	private bool t;

	void Start(){
		score1 = 0;
		setHomeScore ();
		line = GameObject.FindGameObjectsWithTag ("LineHome");
	}
	IEnumerator blah(){
		yield return new WaitForSeconds (0.2f);
	}
	void test(){
		StartCoroutine (blah ());
		for (int i = 0; i < line.Length; i++) {
			size [i] = Vector3.Distance (line[i].transform.position, player.homePole.transform.position);
		}
		order ();
		StartCoroutine (blah ());
	}
	void order(){
		for (int i = 0; i < size.Length; i++) {
			for (int j = i + 1; j < size.Length; j++) {
				if (size [i] > size [j]) {
					float temp = size [i];
					size [i] = size [j];
					size [j] = temp;
				}
			}
		}
	}
	bool tester(){
		if (player.distanceHome > size [8]) {
			return true;
		} else if (player.distanceHome < size [0]) {
			return false;
		} else {
			if (player.distanceHome > size [7] && player.distanceHome < size [8]) {
				if (player.transform.position.x > 16.242f && player.transform.position.x < 17.565f) {
					return true;
				}
			}
			else if (player.distanceHome > size [6] && player.distanceHome < size [7]) {
				if (player.transform.position.x < 21.458f && player.transform.position.x > 19.64f) {
					return true;
				}
			}
			else if (player.distanceHome > size [5] && player.distanceHome < size [6]) {
				if (player.transform.position.x > 14.789f && player.transform.position.x < 16.242f) {
					return true;
				}
			}
			else if (player.distanceHome > size [4] && player.distanceHome < size [5]) {
				if (player.transform.position.x < 22.6f && player.transform.position.x > 21.458f) {
					return true;
				}
			}
			else if (player.distanceHome > size [3] && player.distanceHome < size [4]) {
				if (player.transform.position.x < 14.789f && player.transform.position.x > 13.736f) {
					return true;
				}
			}
			else if (player.distanceHome > size [2] && player.distanceHome < size [3]) {
				if (player.transform.position.x > 22.6f && player.transform.position.x < 24.021f) {
					return true;
				}
			}
			else if (player.distanceHome > size [1] && player.distanceHome < size [2]) {
				if (player.transform.position.x < 13.736f) {
					return true;
				}
			}
			else if (player.distanceHome > size [0] && player.distanceHome < size [1]) {
				if (player.transform.position.x > 24.021f) {
					return true;
				}
			}
			return false;
		}
	}
	void OnTriggerEnter(Collider otherCollider){
		if (otherCollider.gameObject.tag=="Ball" && this.gameObject.tag=="Home") {
			if (t) {
				score1 += 3;
				player.ballCheck = true;
			} else {
				score1 += 2;
				player.ballCheck = true;
			}
			setHomeScore ();
			StartCoroutine (blah ());
		}
	}
	void setHomeScore(){
		home.text = "Home: " + score1.ToString ();
	}

	void Update () {
		if (!player.holdingBall) {
			test ();
			t=tester ();
		}
	}
}