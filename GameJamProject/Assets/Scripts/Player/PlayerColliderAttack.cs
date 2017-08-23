using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderAttack : MonoBehaviour {

	GameObject player;
	PlayerAttack playerAttack;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerAttack = player.GetComponent<PlayerAttack> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		playerAttack.Attack (other);
	}
	void OnTriggerStay2D(Collider2D other){
		playerAttack.Attack (other);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
