using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFireScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (GroundFire());
	}



	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator GroundFire(){
		yield return new WaitForSeconds (1.8f);
		Destroy (gameObject);
	}

}
