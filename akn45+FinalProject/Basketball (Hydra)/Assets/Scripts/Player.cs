using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameObject ball;
	public GameObject playerCamera;
	public GameObject hold;
	public GameObject ballClone;
	public GameObject floor;
	public GameObject homePole;
	public GameObject awayPole;
	public RazerHydraPlugin plugin;
	public Transform playerTransform;
    //public Rigidbody bob;

	public float ballDistance = 2.25f;
	public int medium, large,small,choose;
	public float distanceHome,distanceAway;
	//private float start, end;
	public bool holdingBall = true;
	public bool ballCheck=false;
	private Vector3 check1, check2;
	private float thrown;
	private bool yes = false;

	public UIManager help;

	// Use this for initialization
	void Start () {
		plugin = new RazerHydraPlugin ();
		//playerTransform = this.GetComponent<Transform> ();
		plugin.init();
		ball.GetComponent<Rigidbody> ().useGravity = false;
		ball.SetActive (true);
		ballClone = Instantiate(ball, hold.transform.position, hold.transform.rotation);
		ballClone.SetActive (false);
		//start = 0f;
		//end = 0f;
		medium = 10;
		small = 7;
		large = 13;
		StartCoroutine (throwCheck ());
	}

	void move(){
		plugin.getNewestData (0);
		playerTransform.Translate((plugin.data.joystick_x)/25f,0f,plugin.data.joystick_y/25f);
	}

	void rotate(){
		plugin.getNewestData (1);
		playerCamera.transform.rotation=Quaternion.Euler(plugin.data.joystick_y*-100, plugin.data.joystick_x*100, 0f);
	}

	IEnumerator throwCheck(){
		while (true) {
			plugin.getNewestData (1);
			check1 = plugin.data.position;
			yield return new WaitForSeconds (0.5f);
			check2 = plugin.data.position;
			thrown = Vector3.Distance (check1, check2);
			//Debug.Log(thrown);
			if (thrown > 300.0f) {
				yes = true;
				if (thrown > 400f && thrown <= 500f) {
					choose = medium;
				} else if (thrown > 500f) {
					choose = large;
				} else {
					choose = small;
				}
			}
			plugin.getNewestData (0);
			if(plugin.data.trigger>0.8){
				Debug.Log("hi");
				playerTransform.Translate(0f,0.5f,0f);
				yield return new WaitForSeconds(0.3f);
				playerTransform.Translate(0f,-0.5f,0f);
			}
		}
	}

	void OnCollisionEnter(Collision hit)
	{
		if (hit.gameObject.CompareTag("Ball")) {	
			ballCheck = true;
		}
	}

	void positionCheck(){
		if (ballClone.transform.position.x > 27 || ballClone.transform.position.x < 11 || ballClone.transform.position.z > -5 || ballClone.transform.position.z < -30) {
			ballCheck = true;
		}
	}

	//void checker(){
	//		float hi = end - start;
	//	if (hi > 0.3f && hi <= 1f) {
	//		choose = medium;
	//	} else if (hi > 1f) {
	//		choose = large;
	//	} else {
	//		choose = small;
	//	}
	//}
	
	// Update is called once per frame
	void Update ()
	{
		move ();
		rotate ();
		hold.transform.position = playerTransform.transform.position + playerCamera.transform.forward * ballDistance;
		if (holdingBall) {
			ball.transform.position = playerTransform.transform.position + playerCamera.transform.forward * ballDistance;
			//if (Input.GetMouseButtonDown (0) && help.pauseCheck) {
			//	start = Time.time;
			//}
			//if (Input.GetMouseButtonUp (0) && help.pauseCheck) {
			if(help.pauseCheck && yes){
				//end = Time.time;
				//checker ();
				distanceHome = Vector3.Distance (playerTransform.transform.position, homePole.transform.position);
				distanceAway = Vector3.Distance (playerTransform.transform.position, awayPole.transform.position);
				holdingBall = false;
				ball.SetActive (false);
				ballClone.transform.position = ball.transform.position;
				ballClone.SetActive (true);
				ballClone.GetComponent<Rigidbody> ().useGravity = true;
				Rigidbody rb = ballClone.GetComponent<Rigidbody> ();
				rb.velocity = playerCamera.transform.forward * choose;
				yes=false;
			}
		} else {
			positionCheck ();
			plugin.getNewestData(1);
			if (plugin.data.trigger>0.8 || ballCheck) {
				yes=false;
				ballClone.SetActive (false);
				ball.SetActive (true);
				holdingBall = true;
				ballCheck = false;
			}
		}
	}
}
