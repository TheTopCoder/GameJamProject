using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamborScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Raio(Vector3 dir){
		GameObject raio;
		UnityEngine.Object raioprefab = Resources.Load ("Tempestade/Raio");
		raio = (GameObject) Instantiate (raioprefab, transform.position, Quaternion.identity);

		float rotAngle = Random.Range (-15f, 15f);
		dir = Quaternion.Euler (0, 0, rotAngle)*dir;
		dir = dir.normalized;
		float distSqr = Random.Range (1f, 9f);
		distSqr = Mathf.Sqrt (distSqr);

		raio.transform.position = new Vector3 (transform.position.x +dir.x * distSqr,transform.position.y +dir.y * distSqr * 0.6f /*Elipse b/a*/,0);

	}

}
