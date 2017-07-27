using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneScript : MonoBehaviour {

	Transform target;
	float horSpd;
	float verSpd;
	float timeToHit;
	float grav;
	bool enabledHit;
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		enabledHit = false;
		target = transform.parent;
		horSpd = 7f;
		verSpd = 0;
		//Debug.Log ("X: " + transform.position.x);
		//Debug.Log ("Y: " + transform.position.y);
		timeToHit = (-transform.parent.position.x + transform.position.x) / horSpd;
		grav = 2 * (-transform.parent.position.y + transform.position.y) / Mathf.Pow (timeToHit, 2);
		//StartCoroutine (DestroyBone());
	}

	void EnableHit(){
		enabledHit = true;
	}

	void OnTriggerEnter2D(Collider2D other){
		//Debug.Log (enabledHit);
		if (player!=null && enabledHit && other.tag == "PlayerBase") {
			StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
		}
		if (other.tag == "BoneTarget") {
//			StartCoroutine (DestroyBone ());
			EnableHit ();
		}
	}
	void OnTriggerStay2D(Collider2D other){
		//Debug.Log (enabledHit);
		if (player!=null && enabledHit && other.tag == "PlayerBase") {
			StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (-horSpd, -verSpd);
		verSpd += grav*Time.deltaTime;
		if (transform.position.y < transform.parent.position.y) {
			grav = 0;
			verSpd = 0;
			horSpd = 0;
			StartCoroutine (DestroyBone ());
//			GetComponent<Animator> ().SetTrigger ("Vomit_Hit");
		}
	}


	IEnumerator DestroyBone(){
		yield return new WaitForSeconds (0.05f);
		Destroy (gameObject);
	}
}
