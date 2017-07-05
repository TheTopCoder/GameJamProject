using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour {

	GameObject boss;

	// Use this for initialization
	void Start () {
		boss = GameObject.FindGameObjectWithTag ("Boss");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "PlayerBase") {
			Debug.Log ("Can Hit");
			boss.GetComponent<FomeController>().canHit = true;
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "PlayerBase") {
			Debug.Log ("Can Hit");
			boss.GetComponent<FomeController>().canHit = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "PlayerBase") {
			boss.GetComponent<FomeController>().canHit = false;
		}
	}
}
