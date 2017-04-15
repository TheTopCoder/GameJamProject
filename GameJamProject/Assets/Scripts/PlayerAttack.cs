using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	string state;
	int attackDamage = 5;
	float attackTime = 0.3f;
	float attackCurrentTime;
	bool canHit;
	bool hit;
	GameObject boss;

	// Use this for initialization
	void Start () {
		state = "wait";
		canHit = false;
		hit = false;
		attackCurrentTime = attackTime;
		boss = GameObject.FindGameObjectWithTag ("Boss");
	}
	
	// Update is called once per frame
	void Update () {
		if (state == "wait") {
			if (Input.GetAxisRaw ("XboxR2")>0) {
				state = "attack";
			}
		}
		if (state == "attack") {
			if (canHit&&!hit) {
				Debug.Log ("Attack");
				hit = true;
				boss.GetComponent<BossController>().life -= attackDamage;
			}
			attackCurrentTime -= Time.deltaTime;
			if (attackCurrentTime < 0) {
				attackCurrentTime = attackTime;
				hit = false;
				state = "wait";
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Boss") {
			canHit = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Boss") {
			canHit = false;
		}
	}
}

