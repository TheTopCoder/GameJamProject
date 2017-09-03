using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorAnimationTrigger : MonoBehaviour {

	public GameObject painting;
	bool activated = false;

	// Use this for initialization
	void Start () {
		painting.GetComponent<Animator> ().SetTrigger ("Still");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (!activated&&other.transform.tag == "Player"){
			painting.GetComponent<Animator> ().SetTrigger ("Start");
			activated = true;
		}
	}



}
