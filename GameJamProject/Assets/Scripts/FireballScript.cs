using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour 
{
	[SerializeField]
	float speed = 2f;
	GameObject player;
	Rigidbody2D rb;
	Vector3 playerPosition;
	float dirX,dirY;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerPosition = player.transform.position;
		dirX = playerPosition.x - transform.position.x;
		dirY = playerPosition.y - transform.position.y;
		float dirAbs = Mathf.Sqrt (dirX * dirX + dirY * dirY);
		if (dirAbs != 0) {
			dirX = dirX / dirAbs;
			dirY = dirY / dirAbs;
		}
		float speedX = speed * dirX;
		float speedY = speed * dirY;
		rb.velocity = new Vector2 (speedX,speedY);
	}

	void Update () 
	{
//		this.transform.position += transform.right * Time.deltaTime * speed;
	}
}
