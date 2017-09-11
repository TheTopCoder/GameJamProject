using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurricaneHitbox : MonoBehaviour {

	GameObject player;
	public bool canHit;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		canHit = false;
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.transform.tag == "Player") {
			canHit = true;
			//StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
			//boss.GetComponent<FomeController>().canHit = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.transform.tag == "Player") {
			canHit = false;
		}
	}
}
