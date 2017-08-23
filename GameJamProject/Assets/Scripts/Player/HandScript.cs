using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {

	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	public void CallFinishAttack(){
		player.GetComponent<PlayerAttack> ().FinishAttack ();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
