﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour {

	GameObject player;
	GameObject boss;
	bool hit;
	bool hitBoss=false;

	// Use this for initialization
	void Start () {
		boss = GameObject.FindGameObjectWithTag ("Boss");
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ResolveTrigger(Collider2D other){
		if (!hit&&other.transform.tag == "Player") {
			hit = true;
			StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
			//boss.GetComponent<FomeController>().canHit = true;
		}
		if (!hitBoss&&other.transform.tag == "Boss"&&transform.name=="RaioHitbox(Clone)") {
			hitBoss = true;
			boss.GetComponent<TempestadeController>().ReceiveDamage(player.GetComponent<PlayerStats>().attackDamage*5);
			//boss.GetComponent<FomeController>().canHit = true;
		}
		if (!hit && transform.name!="RaioHitbox(Clone)" && other.transform.tag == "TamborTrigger") {
			Debug.Log (transform.name);
			hit = true;
			Debug.Log ("Raio");
			GameObject.FindGameObjectWithTag("Tambor").GetComponent<TamborScript> ().Raio (other.transform.position-boss.transform.position);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		ResolveTrigger (other);
	}
	void OnTriggerStay2D(Collider2D other){
		ResolveTrigger (other);
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			//boss.GetComponent<FomeController>().canHit = false;
		}
	}
}
