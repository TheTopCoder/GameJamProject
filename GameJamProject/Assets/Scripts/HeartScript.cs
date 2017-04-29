using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour {

	bool goingUp;
	float initialScale;
	bool destroyed = false;

	// Use this for initialization
	void Start () {
		goingUp = true;
		initialScale = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (destroyed) {
			if (goingUp) {
				transform.localScale = transform.localScale * 1.08f;
				if (transform.localScale.x > initialScale * 1.25f) {
					goingUp = false;
				}
			} else {
				transform.localScale = transform.localScale * 0.825f;
			}
			if (transform.localScale.x < initialScale * 0.1f) {
				Destroy (gameObject);
			}
		}
	}

	public void DestroyHeart(){
		destroyed = true;
	}
}
