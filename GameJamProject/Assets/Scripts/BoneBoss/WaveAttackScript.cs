using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttackScript : MonoBehaviour {
	float waveSpeed = 1.5f;
	float duration = 1f;
	float time;
	public int dir;

	// Use this for initialization
	void Start () {
		time = duration;
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		transform.position += new Vector3(dir * waveSpeed,0,0)*Time.deltaTime;
		if (time < 0) {
			Destroy (gameObject);	
		}
	}


}
