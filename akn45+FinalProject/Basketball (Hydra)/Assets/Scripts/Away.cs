using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Away : MonoBehaviour {

	private int score2;
	public Text away;
	public Player player;
	GameObject[] line;
	private float[] size=new float[9]; 
	private bool t;

	void Start(){
		score2 = 0;
		setAwayScore ();
		line = GameObject.FindGameObjectsWithTag ("LineAway");
	}
	IEnumerator blah(){
		yield return new WaitForSeconds (0.2f);
	}
	void test(){
		StartCoroutine (blah ());
		for (int i = 0; i < line.Length; i++) {
			size [i] = Vector3.Distance (line[i].transform.position, player.awayPole.transform.position);
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
		if (player.distanceAway > size [8]) {
			return true;
		} else if (player.distanceAway < size [0]) {
			return false;
		} else {
			if (player.distanceAway > size [7] && player.distanceAway < size [8]) {
				if (player.transform.position.x > 16.242f && player.transform.position.x < 17.565f) {
					return true;
				}
			}
			else if (player.distanceAway > size [6] && player.distanceAway < size [7]) {
				if (player.transform.position.x < 21.458f && player.transform.position.x > 19.64f) {
					return true;
				}
			}
			else if (player.distanceAway > size [5] && player.distanceAway < size [6]) {
				if (player.transform.position.x > 14.789f && player.transform.position.x < 16.242f) {
					return true;
				}
			}
			else if (player.distanceAway > size [4] && player.distanceAway < size [5]) {
				if (player.transform.position.x < 22.943f && player.transform.position.x > 21.458f) {
					return true;
				}
			}
			else if (player.distanceAway > size [3] && player.distanceAway < size [4]) {
				if (player.transform.position.x < 14.789f && player.transform.position.x > 13.736f) {
					return true;
				}
			}
			else if (player.distanceAway > size [2] && player.distanceAway < size [3]) {
				if (player.transform.position.x > 22.943f && player.transform.position.x < 24.021f) {
					return true;
				}
			}
			else if (player.distanceAway > size [1] && player.distanceAway < size [2]) {
				if (player.transform.position.x < 13.736f) {
					return true;
				}
			}
			else if (player.distanceAway > size [0] && player.distanceAway < size [1]) {
				if (player.transform.position.x > 24.021f) {
					return true;
				}
			}
			return false;
		}
	}
	void OnTriggerEnter(Collider otherCollider){
		if (otherCollider.gameObject.tag=="Ball" && this.gameObject.tag=="Away") {
			if (t) {
				score2 += 3;
				player.ballCheck = true;
			} else {
				score2 += 2;
				player.ballCheck = true;
			}
			setAwayScore ();
			StartCoroutine (blah ());
		}
	}
	void setAwayScore(){
		away.text = "Away: " + score2.ToString ();
	}

	void Update () {
		if (Input.GetMouseButtonUp (0)) {
			test ();
			t=tester ();
		}
	}
}
