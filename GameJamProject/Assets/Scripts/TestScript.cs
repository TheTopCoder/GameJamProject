using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.FindGameObjectWithTag ("PlayerBase");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		GameObject.FindGameObjectWithTag ("PlayerBase");
	}
}
