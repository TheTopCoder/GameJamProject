using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

	// Use this for initialization
	Transform cameraMin;
	Transform cameraMax;
	Transform player;
	float idealSize;
	float maxError;
	float rate;
	float maxSize;
	float minSize;

	void Start () {
		cameraMin = GameObject.Find ("Camera_Min").transform;
		cameraMax = GameObject.Find ("Camera_Max").transform;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		minSize = 2.8f;
		maxError = 0.01f;
		maxSize = 3.3f;
		rate = 0.4f * Time.deltaTime;
		GetComponent<Camera> ().orthographicSize = (player.transform.position.x - cameraMin.position.x) / (cameraMax.position.x - cameraMin.position.x) * (maxSize - minSize) + minSize;
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			if (player.transform.position.x > cameraMin.position.x) {
				idealSize = minSize;
//				GetComponent<Camera> ().orthographicSize = minSize;
			} else if (player.transform.position.x < cameraMax.position.x) {
				idealSize = maxSize;
//				GetComponent<Camera> ().orthographicSize = maxSize;
			} else {
				idealSize = (player.transform.position.x - cameraMin.position.x) / (cameraMax.position.x - cameraMin.position.x) * (maxSize - minSize) + minSize;
			}
		}
		if (GetComponent<Camera> ().orthographicSize > idealSize + maxError){
			GetComponent<Camera> ().orthographicSize -= rate;
		}
		else if (GetComponent<Camera> ().orthographicSize < idealSize - maxError){
			GetComponent<Camera> ().orthographicSize += rate;
		}
	}
}
