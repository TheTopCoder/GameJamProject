﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	float dirX;
	float dirY;
	float speedX;
	float speedY;
	float lookDirX;
	float lookDirY;
	float rollDirX;
	float rollDirY;
	string state;
	float speed = 0.8f;
	float rollSpeed = 2f;
	float rollTime = 0.3f;
	float rollCurrentTime;
	float rollCooldown = 0.5f;
	float rollCurrentCooldown;


	// Use this for initialization
	void Start (){
		state = "movement";
		rollCurrentTime = rollTime;
		rollCurrentCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == "movement") {
			dirX = Input.GetAxis ("Horizontal");
			dirY = Input.GetAxis ("Vertical");
			float dirAbs = Mathf.Sqrt (dirX * dirX + dirY * dirY);
			if (dirAbs != 0) {
				dirX = dirX / dirAbs;
				dirY = dirY / dirAbs;
			}
			speedX = dirX * speed;
			speedY = dirY * speed;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (speedX, speedY);
			lookDirX = Input.GetAxis ("Horizontal");
			lookDirY = Input.GetAxis ("Vertical");
			float lookDirAbs = Mathf.Sqrt (dirX * dirX + dirY * dirY);
			if (lookDirAbs != 0) {
				lookDirX = lookDirX / lookDirAbs;
				lookDirY = lookDirY / lookDirAbs;
			}
			rollDirX = dirX;
			rollDirY = dirY;
			rollCurrentCooldown -= Time.deltaTime;

			if (Input.GetAxisRaw("XboxR1")>0 && rollCurrentCooldown < 0) {
				state = "roll";
			}


		} else if (state == "roll") {
			speedX = rollDirX * rollSpeed;
			speedY = rollDirY * rollSpeed;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (speedX, speedY);
			rollCurrentTime -= Time.deltaTime;
			if (rollCurrentTime < 0){
				rollCurrentTime = rollTime;
				rollCurrentCooldown = rollCooldown;
				state = "movement";
			}
		}
	}
}
