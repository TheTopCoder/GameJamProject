using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaioScript : MonoBehaviour {
	GameObject attackHitbox;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateHitbox(string name){
		UnityEngine.Object hitboxprefab = Resources.Load ("Tempestade/" + name);
		attackHitbox = (GameObject) Instantiate (hitboxprefab, transform.position, Quaternion.identity);
	}
	public void DestroyHitbox(){
		if (attackHitbox != null) {
			Destroy (attackHitbox);
		}
	}

	public void DestroyRaio(){
		Destroy(gameObject);
	}

}
