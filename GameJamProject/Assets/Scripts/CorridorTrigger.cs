using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorTrigger : MonoBehaviour {

	public GameObject[] spawns = new GameObject[1];
	public Transform[] locations = new Transform[1];


	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			int i = 0;
			GameObject newSpawn;
//			UnityEngine.Object minionprefab = Resources.Load ("Fome/" + "CrowMinion");
			foreach (GameObject spawn in spawns) {
								Instantiate (spawn, locations [i].position, Quaternion.identity);
				//				Instantiate (minionprefab, locations [i].position, Quaternion.identity);
				i++;
			}
			Destroy (gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
