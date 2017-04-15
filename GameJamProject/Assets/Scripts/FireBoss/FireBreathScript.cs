using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreathScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (FireBreath ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator FireBreath(){
		yield return new WaitForSeconds (1.8f);
		Destroy (gameObject);
	}

}
