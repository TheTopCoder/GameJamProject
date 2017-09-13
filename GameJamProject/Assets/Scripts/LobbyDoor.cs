using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.transform.tag == "PlayerBase") {
			if (name == "PortaFome") {
				GameObject.FindGameObjectWithTag ("TransitionCanvas").GetComponent<TransitionScript> ().nome = "FomeCorridor";
			} else {
				GameObject.FindGameObjectWithTag ("TransitionCanvas").GetComponent<TransitionScript> ().nome = "TempestadeCorridor";
			}
			GameObject.FindGameObjectWithTag ("TransitionCanvas").GetComponent<TransitionScript> ().ChangeScene ();
		}
	}
}
