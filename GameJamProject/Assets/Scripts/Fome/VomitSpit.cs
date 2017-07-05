using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VomitSpit : MonoBehaviour {
	float horSpd;
	float verSpd;
	// Use this for initialization
	void Start () {
		horSpd = 1f;
		verSpd = 9f;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (-horSpd, verSpd);
	}
}
