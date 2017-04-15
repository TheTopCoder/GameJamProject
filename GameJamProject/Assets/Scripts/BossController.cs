using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {
	public int life;
	// Use this for initialization
	void Start () {
		life = 10;
	}
	
	// Update is called once per frame
	void Update () {
		if (life <= 0) {
			Destroy (gameObject);
		}
	}
}
