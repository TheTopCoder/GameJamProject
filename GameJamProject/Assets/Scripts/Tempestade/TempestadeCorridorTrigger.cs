using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempestadeCorridorTrigger : MonoBehaviour {

	public Transform[] bordersT = new Transform[4];
	Vector3[] borders = new Vector3[4];
	bool started=false;
	GameObject player;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		borders[0] = bordersT[0].transform.position;
		borders[1] = bordersT[1].transform.position;
		borders[2] = bordersT[2].transform.position;
		borders[3] = bordersT[3].transform.position;
	}

	IEnumerator CreateRaio(){
		GameObject raio;
		GameObject raioShadow;
		UnityEngine.Object raioprefab = Resources.Load ("Tempestade/Raio");
		UnityEngine.Object raioShadowPrefab = Resources.Load ("Tempestade/Shadow");

		yield return new WaitForSeconds (0.1f);
		raioShadow = (GameObject) Instantiate (raioShadowPrefab, transform.position, Quaternion.identity);

		Vector2 newPos;
		newPos.y = Random.Range (borders [0].y, borders [1].y);
		newPos.x = Random.Range (borders [0].x, borders [2].x) + (borders [1].x - borders [0].x)*(newPos.y-borders[0].y)/(borders[1].y-borders[0].y);

		raioShadow.transform.position = new Vector3 (newPos.x,newPos.y-0.25f,0);
		yield return new WaitForSeconds (0.6f);
		Destroy (raioShadow);
		raio = (GameObject) Instantiate (raioprefab, transform.position, Quaternion.identity);


		raio.transform.position = new Vector3 (newPos.x,newPos.y,0);
	}

	IEnumerator SpawnRaios(){
		yield return new WaitForSeconds (0.1f);
		do {
			StartCoroutine(CreateRaio ());
			yield return new WaitForSeconds (0.325f);
		} while (player!=null&&player.transform.position.x < (borders [3].x + borders [2].x) / 2 + 0.8f);
	}


	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Raiou");
		if (!started && other.tag == "Player") {
			started = true;
			StartCoroutine (SpawnRaios());
		}
	}

	// Update is called once per frame
	void Update () {
	}
}
