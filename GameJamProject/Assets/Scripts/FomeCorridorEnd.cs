using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FomeCorridorEnd : MonoBehaviour {

	GameObject TransitionCanvas;

	// Use this for initialization
	void Start () {
		TransitionCanvas = GameObject.FindGameObjectWithTag("TransitionCanvas");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			if (TransitionCanvas != null) {
				if (GameObject.FindGameObjectWithTag ("Enemy") == null) {
					TransitionCanvas.GetComponent<TransitionScript> ().ChangeScene ();
				}
			}
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			if (TransitionCanvas != null) {
				if (GameObject.FindGameObjectWithTag ("Enemy") == null) {
					TransitionCanvas.GetComponent<TransitionScript> ().ChangeScene ();
				}
			}
		}
	}


}
