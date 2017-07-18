using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	void OnCollisionEnter2D(Collision2D other){
		for (int i = 0; i < other.contacts.GetLength(0);i++){
			Vector2 dir = new Vector2(transform.position.x-other.contacts [i].point.x,transform.position.y-other.contacts [i].point.y);
			//Debug.Log (dir);
			if (Vector2.Dot (dir, GetComponent<Rigidbody2D> ().velocity)>=0) {
				//				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			}
		}
	}

	void OnCollisionStay2D(Collision2D other){
		for (int i = 0; i < other.contacts.GetLength(0);i++){
			Vector2 dir = new Vector2(transform.position.x-other.contacts [i].point.x,transform.position.y-other.contacts [i].point.y);
			//Debug.Log (dir);
			if (Vector2.Dot (dir, GetComponent<Rigidbody2D> ().velocity)>=0) {
				//				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			}
		}
	}

}
	
