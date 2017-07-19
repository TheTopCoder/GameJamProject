using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour {

	GameObject player;
	bool hit;

	// Use this for initialization
	void Start () {
		//boss = GameObject.FindGameObjectWithTag ("Boss");
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other){
		if (!hit&&other.transform.tag == "Player") {
			hit = true;
			StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
			//boss.GetComponent<FomeController>().canHit = true;
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if (!hit&&other.transform.tag == "Player") {
			hit = true;
			StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
			//boss.GetComponent<FomeController>().canHit = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			//boss.GetComponent<FomeController>().canHit = false;
		}
	}
}
