using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempestadeAttackTrigger : MonoBehaviour {

	GameObject Boss;
	bool playerInside;
	bool tamborInside;

	// Use this for initialization
	void Start () {
		Boss = GameObject.FindGameObjectWithTag ("Boss");
		playerInside = false;
		tamborInside = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.transform.tag == "Player") {
			playerInside = true;
			Boss.GetComponent<TempestadeController> ().canAttackGround = true;
		} 
		if (other.transform.tag == "Tambor") {
			tamborInside = true;
			Boss.GetComponent<TempestadeController> ().canAttackGround = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.transform.tag == "Player") {
			playerInside = false;
		} 
		if (other.transform.tag == "Tambor") {
			tamborInside = false;
		}
		if (!tamborInside && !playerInside) {
			Boss.GetComponent<TempestadeController> ().canAttackGround = false;
		}
	}
}
