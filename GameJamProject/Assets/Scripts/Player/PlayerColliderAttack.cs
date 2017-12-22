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

	void CheckAttack(Collider2D other){
		if ((playerAttack.curAttack == 1 && transform.name == "AttackCollider1") || (playerAttack.curAttack == 2 && transform.name == "AttackCollider2")) {
			if (other.tag == "Enemy"||other.tag == "Boss"||other.tag=="TamborTrigger"){
				if (transform.FindChild("Zmin")&&transform.FindChild("Zmax")&&other.transform.FindChild("Zmin")&&other.transform.FindChild("Zmax")){
					//print (other.name);
					if (other.transform.FindChild("Zmin").position.y <= transform.FindChild("Zmax").position.y&&other.transform.FindChild("Zmax").position.y >= transform.FindChild("Zmin").position.y){
						playerAttack.Attack (other);
					}
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		CheckAttack (other);
	}
	void OnTriggerStay2D(Collider2D other){
		CheckAttack (other);
	}

	// Update is called once per frame
	void Update () {

	}
}
