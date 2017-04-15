using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {
	public int life;
	string state = "movement";
	GameObject player;
	float range = 0.1f;
	float minChangeDirTime = 1f;
	float maxChangeDirTime = 2f;
	float changeDirTime;
	float minAttackCooldown = 3f;
	float maxAttackCooldown = 5f;
	float attackCooldown;
	float dirX, dirY;
	float speed = 0.5f;

	// Use this for initialization
	void Start () {
		life = 10;
		changeDirTime = 0;
		attackCooldown = maxAttackCooldown;
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (life <= 0) {
			Destroy (gameObject);
		}
		if (state == "movement"){
			changeDirTime -= Time.deltaTime;
			if (changeDirTime < 0) {
				float distX = player.transform.position.x - transform.position.x;
				float distY = player.transform.position.y - transform.position.y;
				float distPlayer = Mathf.Sqrt (distX*distX + distY*distY);
				changeDirTime = Random.Range (minChangeDirTime,maxChangeDirTime);
				float rangeDir = Random.Range (0,360);
				float rangeDist = Random.Range (0, distPlayer*range);
				float objectiveX = player.transform.position.x + rangeDist * Mathf.Cos(rangeDir);
				float objectiveY = player.transform.position.x + rangeDist * Mathf.Cos(rangeDir);
				dirX = objectiveX-transform.position.x;
				dirY = objectiveY-transform.position.y;
				float dirAbs = Mathf.Sqrt (dirX*dirX + dirY*dirY);
				if (dirAbs != 0) {
					dirX = dirX / dirAbs;
					dirY = dirY / dirAbs;
				}
				float speedX = dirX * speed;
				float speedY = dirY * speed;
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (speedX, speedY);
			}
		}
	}
}
