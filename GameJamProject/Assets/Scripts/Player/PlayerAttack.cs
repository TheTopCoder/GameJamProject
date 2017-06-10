using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour {

	public string state;//wait,attack,attackStrong,recoverAttack
	bool canHit;
	bool hit;
	int attackDamage;
	float attackTime;
	int attackStrongDamage;
	float attackStrongTime;
	float attackCurrentTime;
	PlayerStats playerStats;
	GameObject boss;
    [SerializeField]
    Animator handAnim;
    [SerializeField]
    Animator bodyAnim;
    [SerializeField]
    AnimationClip handAttackAnim;

	// Use this for initialization
	void Start () {
		playerStats = GetComponent<PlayerStats> ();
		attackDamage = playerStats.attackDamage;
		attackTime = handAttackAnim.length * 1.05f;
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
			if (Input.GetAxisRaw ("XboxX")>0 || Input.GetAxisRaw("XboxR2") > 0 || Input.GetKey(KeyCode.E) || Input.GetMouseButtonDown(0)) {
				state = "attack";
                transform.FindChild("ShakeWeaponSound").GetComponent<AudioSource>().Play();
                handAnim.SetTrigger("Attack");
            }
			else if (Input.GetAxisRaw ("XboxL2")>0) {
//				state = "attackStrong";
			}
		}
		if (state == "attack") {
			if (canHit&&!hit) {
				hit = true;
                StartCoroutine(DamageTime());
            }
			attackCurrentTime -= Time.deltaTime;
			if (attackCurrentTime < 0) {
				attackCurrentTime = attackTime;
				hit = false;
				state = "wait";
			}
		}
		//Aqui atira o tacape
		if (state == "attackStrong") {
            handAnim.SetBool("Estocada", true);
            bodyAnim.SetBool("Estocada", true);
            if (canHit&&!hit) {
				Debug.Log ("AttackStrong");
				hit = true;

                if (boss.name.Equals("BoneBoss"))
                {
                    boss.GetComponent<BoneBossController>().life -= attackStrongDamage;
                }
<<<<<<< HEAD
                else if (boss.name.Equals("FireBoss"))
                {
//                    boss.GetComponent<FireBossController>().life -= attackStrongDamage;
                    
                }
=======
>>>>>>> 4178732143e2f731bc145c7823c2aea5f24eba7a
            }
			attackCurrentTime -= Time.deltaTime;
			if (attackCurrentTime < 0) {
                handAnim.SetBool("Estocada", false);
                bodyAnim.SetBool("Estocada", false);
                attackCurrentTime = attackStrongTime;
				hit = true;
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
    IEnumerator DamageTime()
    {
        yield return new WaitForSeconds(handAttackAnim.length);

        if (boss.name.Equals("BoneBoss"))
        {
            boss.GetComponentInChildren<BoneBossController>().life -= attackDamage;
            boss.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.05f);
            boss.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
<<<<<<< HEAD
        else if (boss.name.Equals("FireBoss"))
        {
//            boss.GetComponent<FireBossController>().life -= attackDamage;
        }
=======
>>>>>>> 4178732143e2f731bc145c7823c2aea5f24eba7a
    }
}

