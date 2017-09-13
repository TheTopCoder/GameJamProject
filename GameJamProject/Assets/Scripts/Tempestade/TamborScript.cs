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

	IEnumerator CreateRaio(Vector3 dir){
		Debug.Log (dir.normalized);
		GameObject raio;
		GameObject raioShadow;
		UnityEngine.Object raioprefab = Resources.Load ("Tempestade/Raio");
		UnityEngine.Object raioShadowPrefab = Resources.Load ("Tempestade/Shadow");

		yield return new WaitForSeconds (0.1f);
		raioShadow = (GameObject) Instantiate (raioShadowPrefab, transform.position, Quaternion.identity);

		float rotAngle = Random.Range (-15f, 15f);
		dir = Quaternion.Euler (0, 0, rotAngle)*dir;
		dir = dir.normalized;
		float distSqr = Random.Range (2.25f, 14f);
		distSqr = Mathf.Sqrt (distSqr);

		raioShadow.transform.position = new Vector3 (transform.position.x +dir.x * distSqr,transform.position.y +dir.y * distSqr * 6.2f/11f /*Elipse b/a*/,0);
		yield return new WaitForSeconds (0.4f);
		Destroy (raioShadow);
		raio = (GameObject) Instantiate (raioprefab, transform.position, Quaternion.identity);


		raio.transform.position = new Vector3 (transform.position.x +dir.x * distSqr,transform.position.y +dir.y * distSqr * 6.2f/11f /*Elipse b/a*/,0);
	}

	IEnumerator RaioBoss(Vector3 dir){
		StartCoroutine(CreateRaio (dir));
		yield return new WaitForSeconds (0.2f);
		StartCoroutine(CreateRaio (dir));
		yield return new WaitForSeconds (0.2f);
		StartCoroutine(CreateRaio (dir));
	}

	public void Raio(Vector3 dir){
		GetComponent<Animator> ().SetTrigger ("Hit");
		StartCoroutine(RaioBoss(dir));
	}

	public void RaioSimples(Vector3 dir){
		GetComponent<Animator> ().SetTrigger ("HitLight");
		StartCoroutine(CreateRaio (dir));
	}

}
