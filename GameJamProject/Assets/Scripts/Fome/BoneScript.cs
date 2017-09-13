using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneScript : MonoBehaviour {

	Transform target;
	float horSpd;
	float verSpd;
	float timeToHit;
	float grav;
	float initialTime;
	Vector2 scaleIncrease;
	bool enabledHit;
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		enabledHit = false;
		target = transform.parent;
		transform.parent = null;
		horSpd = 5.5f;
		verSpd = 2f;
		//Debug.Log ("X: " + transform.position.x);
		//Debug.Log ("Y: " + transform.position.y);
		timeToHit = (-target.position.x + transform.position.x) / horSpd;
		grav = 2 * (-target.position.y + transform.position.y) / Mathf.Pow (timeToHit, 2) + 2*verSpd/timeToHit;
		scaleIncrease.x = 2*target.localScale.x*(1f - 0.4f) / timeToHit;
		scaleIncrease.y = 2*target.localScale.y*(1f - 0.4f) / timeToHit;
		target.localScale = new Vector3 (target.localScale.x*0.4f,target.localScale.y*0.4f,0);
		initialTime = Time.time;
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
			GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			GetComponent<Animator>().SetTrigger("Break");
			EnableHit ();
		}
	}
	void OnTriggerStay2D(Collider2D other){
		//Debug.Log (enabledHit);
		if (player!=null && enabledHit && other.tag == "PlayerBase") {
			StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
		}
	}

	public void DestroyBone(){
		Destroy (target.gameObject);
		Destroy (gameObject);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!enabledHit) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-horSpd, verSpd);
		}
		verSpd -= grav*Time.deltaTime;
		if (Time.time - initialTime < timeToHit * 0.5f) {
			target.localScale += new Vector3 (scaleIncrease.x * Time.deltaTime, scaleIncrease.y * Time.deltaTime, 0);
		}
		
/*		if (transform.position.y < target.position.y) {
			grav = 0;
			verSpd = 0;
			horSpd = 0;
			StartCoroutine (DestroyBone ());
//			GetComponent<Animator> ().SetTrigger ("Vomit_Hit");
		}
*/
	}
		
}
