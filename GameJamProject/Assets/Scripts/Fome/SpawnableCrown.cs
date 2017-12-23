using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableCrown : MonoBehaviour {
	GameObject boss;
	GameObject player;
	bool enabledHit;
	bool hit=false;
	// Use this for initialization
	void Start () {
		boss = GameObject.FindGameObjectWithTag ("Boss");
		player = GameObject.FindGameObjectWithTag ("Player");
		enabledHit = false;
	}

	void DestroyCrow(){
		boss.GetComponent<FomeController> ().FinishAttack ();
		Destroy (gameObject);
	}

	void EnableHit(){
		enabledHit = true;
	}
	void DisableHit(){
		enabledHit = false;
	}

	// Update is called once per frame
	void Update () {
		if (player!=null&&player.transform.position.y > transform.position.y) {
			GetComponent<SpriteRenderer> ().sortingOrder = 2;
		} else {
			GetComponent<SpriteRenderer> ().sortingOrder = -2;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (!hit&&other.tag == "PlayerBase"&&enabledHit){
			hit = true;
			player.GetComponent<PlayerController>().DamagePlayer();
//			boss.GetComponent<FomeController> ().canHit = true;
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if (!hit&&other.tag == "PlayerBase"&&enabledHit){
			hit = true;
			player.GetComponent<PlayerController>().DamagePlayer();
//			boss.GetComponent<FomeController> ().canHit = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "PlayerBase"&&enabledHit){
//			boss.GetComponent<FomeController> ().canHit = false;
		}
	}
}
