using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	float dirX;
	float dirY;
	float speed = 0.5f;
	float speedX;
	float speedY;
	float lookDirX;
	float lookDirY;

	// Use this for initialization
	void Start (){
	}
	
	// Update is called once per frame
	void Update () {
		dirX = Input.GetAxis ("Horizontal");
		dirY = Input.GetAxis ("Vertical");
		float dirAbs = Mathf.Sqrt(dirX * dirX + dirY * dirY);
		if (dirAbs != 0) {
			dirX = dirX / dirAbs;
			dirY = dirY / dirAbs;
		}
		speedX = dirX * speed;
		speedY = dirY * speed;
		GetComponent<Rigidbody2D>().velocity = new Vector2(speedX,speedY);
		lookDirX = Input.GetAxis ("Horizontal");
		lookDirY = Input.GetAxis ("Vertical");
		float lookDirAbs = Mathf.Sqrt(dirX * dirX + dirY * dirY);
		if (lookDirAbs != 0) {
			lookDirX = lookDirX / lookDirAbs;
			lookDirY = lookDirY / lookDirAbs;
		}

	}
}
