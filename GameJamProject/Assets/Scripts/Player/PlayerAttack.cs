using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour {

	public string state;
	bool canHit;
	bool hit;
	int attackDamage;
	float attackTime;
	int attackStrongDamage;
	float attackStrongTime;
	float attackCurrentTime;
	PlayerStats playerStats;
	GameObject boss;

	// Use this for initialization
	void Start () {
		playerStats = GetComponent<PlayerStats> ();
		attackDamage = playerStats.attackDamage;
		attackTime = playerStats.attackTime;
		attackStrongDamage = playerStats.attackStrongDamage;
		attackStrongTime = playerStats.attackStrongTime;
		state = "wait";
		canHit = false;
		hit = false;
		attackCurrentTime = attackTime;
		boss = GameObject.FindGameObjectWithTag ("Boss");
	}
	
	// Update is called once per frame
	void Update () {
		if (state == "wait") {
			if (Input.GetAxisRaw ("XboxR2")>0) {
				state = "attack";
			}
			else if (Input.GetAxisRaw ("XboxL2")>0) {
				state = "attackStrong";
			}
		}
		if (state == "attack") {
			if (canHit&&!hit) {
				hit = true;
                if (boss.name.Equals("BoneBoss"))
                {
                    boss.GetComponent<BoneBossController>().life -= attackDamage;
                }
                else if (boss.name.Equals("FireBoss"))
                {
                    boss.GetComponent<FireBossController>().life -= attackDamage;
                }
            }
			attackCurrentTime -= Time.deltaTime;
			if (attackCurrentTime < 0) {
				attackCurrentTime = attackTime;
				hit = false;
				state = "wait";
			}
		}
		if (state == "attackStrong") {
			if (canHit&&!hit) {
				Debug.Log ("AttackStrong");
				hit = true;
				boss.GetComponent<BoneBossController>().life -= attackStrongDamage;

			}
			attackCurrentTime -= Time.deltaTime;
			if (attackCurrentTime < 0) {
				attackCurrentTime = attackStrongTime;
				hit = false;
				state = "wait";
			}
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Boss") {
			canHit = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Boss") {
			canHit = false;
		}
	}
}

