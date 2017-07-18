using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyEye : MonoBehaviour {
	GameObject player;
	Vector3 initialPos;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		initialPos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		Vector2 dir = new Vector2 (initialPos.x - player.transform.position.x, initialPos.y - player.transform.position.y)*-1;
		dir.Normalize ();
		dir = dir * 0.09f; //radius eye can stay
		transform.position = initialPos + new Vector3(dir.x,dir.y,0);
	}
}
