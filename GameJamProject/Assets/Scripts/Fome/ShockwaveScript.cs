using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveScript : MonoBehaviour {
	float speed;
	float sizeSpeed;
	float alphaSpeed;
	float pushForce;
	GameObject player;
	// Use this for initialization
	void Start () {
		speed = 6.0f;
		pushForce = 5f;
		sizeSpeed = 0.015f;
		alphaSpeed = 1.2f*Time.deltaTime;
		player = GameObject.FindGameObjectWithTag ("Player");
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (-speed,0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3 (transform.localScale.x + sizeSpeed, transform.localScale.y + sizeSpeed, transform.localScale.z);

		Color c = GetComponent<SpriteRenderer> ().color;
		c.a = Mathf.Max(GetComponent<SpriteRenderer> ().color.a - alphaSpeed,0);
		GetComponent<SpriteRenderer> ().color = c;
		if (GetComponent<SpriteRenderer> ().color.a < 0.05f) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.transform.tag == "PlayerBase"){
			player.GetComponent<PlayerMovement> ().Push (pushForce*GetComponent<SpriteRenderer> ().color.a);
//			other.GetComponent<Rigidbody2D> ().AddForce (new Vector2(5,0),0);
		} 
	}
}
