using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableCrown : MonoBehaviour {
	GameObject boss;
	bool enabledHit;
	// Use this for initialization
	void Start () {
		boss = GameObject.FindGameObjectWithTag ("Boss");
		enabledHit = false;
	}

	void DestroyCrow(){
		boss.GetComponent<FomeController> ().FinishAttack ();
		Destroy (gameObject);
	}

	void EnableHit(){
		enabledHit = true;
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "PlayerBase"&&enabledHit){
			boss.GetComponent<FomeController> ().canHit = true;
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "PlayerBase"&&enabledHit){
			boss.GetComponent<FomeController> ().canHit = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "PlayerBase"&&enabledHit){
			boss.GetComponent<FomeController> ().canHit = false;
		}
	}
}
