using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour {

	public string state;//wait,attack,attackStrong,recoverAttack
	bool canHit;
	public int curAttack;
	ArrayList hit;
	int attackDamage;
	bool blinked;
	float blinkedTime;
	float beganChargeTime;
	float chargeAttackTime;
	bool chargedAttack;
	float attackTime;
	int attackStrongDamage;
	float attackStrongTime;
	float attackCurrentTime;
	PlayerStats playerStats;
	PlayerMovement playerMovement;
	GameObject boss;
	[SerializeField]
	GameObject bodyLight;
    [SerializeField]
    Animator handAnim;
    [SerializeField]
    Animator bodyAnim;
    [SerializeField]
    AnimationClip handAttackAnim;

	// Use this for initialization
	void Start () {
		playerStats = GetComponent<PlayerStats> ();
		playerMovement = GetComponent<PlayerMovement> ();
		attackDamage = playerStats.attackDamage;
		//attackTime = handAttackAnim.length * 1.05f;
		attackStrongDamage = playerStats.attackStrongDamage;
		attackStrongTime = playerStats.attackStrongTime;
		blinked = false;
		state = "wait";
		canHit = false;
		chargeAttackTime = 0.6f;
		curAttack = 1;
		hit = new ArrayList();
		//attackCurrentTime = attackTime;
		boss = GameObject.FindGameObjectWithTag ("Boss");
	}

	public void FinishAttack(){
		Color color = bodyLight.GetComponent<SpriteRenderer>().color;
		color.a = 0;
		bodyLight.GetComponent<SpriteRenderer> ().color = color;
		blinked = false;
		canHit = false;
		//handAnim.SetTrigger ("FinishAttack");
		if (hit.Count > 0) {
			hit.Clear ();
		}
		state = "wait";
	}
	// Update is called once per frame
	void Update () {
		if (state == "hit") {
			Color color = bodyLight.GetComponent<SpriteRenderer>().color;
			color.a = 0;
			bodyLight.GetComponent<SpriteRenderer> ().color = color;
			blinked = false;

			canHit = false;
			//handAnim.SetTrigger ("FinishAttack");
			if (hit.Count > 0) {
				hit.Clear ();
			}
		}
		if (state == "wait"||state == "prepareAttack") {
			if (Time.time - beganChargeTime >= chargeAttackTime) {
				if (transform.FindChild ("Sounds").transform.FindChild ("ChargedAttackSound").GetComponent<AudioSource> ().isPlaying) {
					transform.FindChild ("Sounds").transform.FindChild ("ChargedAttackSound").GetComponent<AudioSource> ().Stop ();
				}
			}

			if (state=="wait"&&(Input.GetAxisRaw ("XboxX") > 0 || Input.GetAxisRaw ("XboxR2") > 0 || Input.GetKeyDown (KeyCode.E) || Input.GetMouseButtonDown (0))) {
				//Debug.Log ("Preparing...");
				beganChargeTime = Time.time;
				state = "prepareAttack";
				if (curAttack == 1) {
					handAnim.SetTrigger ("PrepareAttack");
				} else {
					handAnim.SetTrigger ("PrepareAttack2");
				}
				transform.FindChild ("Sounds").transform.FindChild ("ChargedAttackSound").GetComponent<AudioSource> ().Play ();
			}
			if (state == "prepareAttack" && (Input.GetAxisRaw ("XboxX") > 0 || Input.GetAxisRaw ("XboxR2") > 0 || Input.GetKeyUp (KeyCode.E) || Input.GetMouseButtonUp (0))) {
				if (Time.time - beganChargeTime >= chargeAttackTime) {
					chargedAttack = true;
				} else {
					chargedAttack = false;
				}

				Color color = bodyLight.GetComponent<SpriteRenderer>().color;
				color.a = 0;
				bodyLight.GetComponent<SpriteRenderer> ().color = color;
				blinked = false;
				if (transform.FindChild ("Sounds").transform.FindChild ("ChargedAttackSound").GetComponent<AudioSource> ().isPlaying) {
					transform.FindChild ("Sounds").transform.FindChild ("ChargedAttackSound").GetComponent<AudioSource> ().Stop ();
				}
				state = "attack";
				transform.FindChild ("ShakeWeaponSound").GetComponent<AudioSource> ().time = 0.25f;
				transform.FindChild ("ShakeWeaponSound").GetComponent<AudioSource> ().Play ();
				if (curAttack == 1) {
					curAttack = 2;
					handAnim.SetTrigger ("Attack");
				} else {
					curAttack = 1;
					handAnim.SetTrigger ("Attack2");
				}
			} else if (Input.GetAxisRaw ("XboxL2") > 0) {
//				state = "attackStrong";
			} else if (Input.GetKeyDown (KeyCode.Space)||Input.GetAxisRaw("XboxY")>0|| Input.GetAxisRaw ("XboxL2") > 0) {
				if (playerStats.energy >= playerStats.maxEnergy / 2) {
					StartCoroutine (SkillLife ());
				}
			} else if (Input.GetKeyDown (KeyCode.F)||Input.GetMouseButtonDown(2)||Input.GetAxisRaw("XboxB")>0|| Input.GetAxisRaw ("XboxR1") > 0) {
				if (GameObject.Find ("Global Controller").GetComponent<GlobalController> ().defeatedTempestade) {
					if (playerStats.energy >= playerStats.maxEnergy / 4) {
						StartCoroutine (SkillRaio ());
						transform.FindChild ("Sounds").transform.FindChild ("SpecialAttackSound").GetComponent<AudioSource> ().Play();
					}
				} else {
					if (playerStats.energy >= playerStats.maxEnergy / 4) {
						StartCoroutine (SkillCorvo ());
						transform.FindChild ("Sounds").transform.FindChild ("SpecialAttackSound").GetComponent<AudioSource> ().Play();
					}
				}
			} 
			else if ((state == "wait" || state == "prepareAttack") && !(Input.GetAxisRaw ("XboxX") > 0 || Input.GetAxisRaw ("XboxR2") > 0 || Input.GetKey (KeyCode.E) || Input.GetMouseButton (0))) {
				FinishAttack ();
			} else if (state == "prepareAttack") {
				Color color = bodyLight.GetComponent<SpriteRenderer>().color;
				if (blinked){
					if (Time.time - blinkedTime < 0.08f) {
						color.a = 1f;
					}
					else if (Time.time - blinkedTime < 0.16f) {
						color.a = 0.5f;
					}
					else if (Time.time - blinkedTime < 0.24f) {
						color.a = 1f;
					}
					else
						color.a = 0.5f;
					bodyLight.GetComponent<SpriteRenderer> ().color = color;
				}
				else if (color.a < 0.5f) {
					color.a += 0.5f*Time.deltaTime/chargeAttackTime;
					bodyLight.GetComponent<SpriteRenderer> ().color = color;
				}
				else if (color.a > 0.5f) {
					blinked = true;
					blinkedTime = Time.time;
					color.a = 0;
					bodyLight.GetComponent<SpriteRenderer> ().color = color;
				}
			}
		}
		if (state == "attack") {
			canHit = true;
//			attackCurrentTime -= Time.deltaTime;
			//if (attackCurrentTime < 0) {
				//attackCurrentTime = attackTime;
				
			//}
		}
//		if (Input.GetAxisRaw ("XboxX") == 0 && Input.GetAxisRaw ("XboxR2") == 0 && !Input.GetKeyDown (KeyCode.E) && !Input.GetMouseButtonDown (0) &&state=="prepareAttack"){
//			state = "wait";
//		}
		//Aqui atira o tacape
/*		if (state == "attackStrong") {
			handAnim.SetBool ("Estocada", true);
			bodyAnim.SetBool ("Estocada", true);
			Debug.Log ("AttackStrong");
			if (boss.name.Equals ("BoneBoss")) {
				boss.GetComponent<BoneBossController> ().life -= attackStrongDamage;
			} else if (boss.name.Equals ("FireBoss")) {
//                    boss.GetComponent<FireBossController>().life -= attackStrongDamage;
                    
			}
			attackCurrentTime -= Time.deltaTime;
			if (attackCurrentTime < 0) {
                handAnim.SetBool("Estocada", false);
                bodyAnim.SetBool("Estocada", false);
                attackCurrentTime = attackStrongTime;
				state = "wait";
			}
		}
*/
	}
 
	IEnumerator Blink(){
		yield return null;
	}

	IEnumerator SkillLife(){
		if (playerStats.life < playerStats.maxLife && state == "wait" && playerMovement.state == "movement") {
			bodyAnim.SetTrigger ("SkillLife");
			handAnim.SetTrigger ("SkillLife");
			GameObject.FindGameObjectWithTag ("Light").GetComponent<Animator> ().Play(0);
			GameObject.FindGameObjectWithTag ("Light").GetComponent<SpriteRenderer> ().enabled = true;
			playerStats.energy -= playerStats.maxEnergy/2;
			state = "skill";
			playerMovement.state = "skill";
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			playerMovement.groundReference.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			yield return new WaitForSeconds (0.425f);
			playerStats.life++;
			state = "wait";
			playerMovement.state = "movement";
			bodyAnim.SetTrigger ("Idle");
			handAnim.SetTrigger ("Idle");
			GameObject.FindGameObjectWithTag ("Light").GetComponent<SpriteRenderer> ().enabled = false;
		}
		yield return new WaitForSeconds (0.02f);
	}

	IEnumerator SkillCorvo(){
		Debug.Log ("Skill Corvo");
		if (state == "wait" && playerMovement.state == "movement" && GameObject.Find("Global Controller").GetComponent<GlobalController>().defeatedFome) {
			//			bodyAnim.SetTrigger ("SkillCorvo");
			handAnim.SetTrigger ("SkillCorvo");
			playerStats.energy -= playerStats.maxEnergy/4;
			state = "skill";
			//playerMovement.state = "skill";
			yield return new WaitForSeconds (0.3f);
			state = "wait";
			//			playerMovement.state = "movement";
			handAnim.SetTrigger ("Idle");
		}
		yield return new WaitForSeconds (0.02f);
	}

	IEnumerator SkillRaio(){
		if (state == "wait" && playerMovement.state == "movement" && GameObject.Find("Global Controller").GetComponent<GlobalController>().defeatedTempestade) {
			handAnim.SetTrigger ("SkillRaio");
			playerStats.energy -= playerStats.maxEnergy/4;
			state = "skill";
			//playerMovement.state = "skill";
			yield return new WaitForSeconds (0.35f);
			state = "wait";
			//			playerMovement.state = "movement";
			handAnim.SetTrigger ("Idle");
		}
		yield return new WaitForSeconds (0.02f);
	}


	public void Attack(Collider2D other){
		if (canHit&&(other.tag == "Boss"||other.tag == "Enemy"||other.tag == "TamborTrigger")) {
			bool alreadyHit = false;
			for (int i = 0; i < hit.Count; i++) {
				if ((int)hit [i] == other.gameObject.GetInstanceID ()) {
					alreadyHit = true;
				}
			}
			if (!alreadyHit) {
//				Debug.Log (other.tag);
				if (other.tag == "Boss") {
					transform.FindChild ("Sounds").transform.FindChild ("HitBossSound").GetComponent<AudioSource> ().time = 0.7f;
					transform.FindChild ("Sounds").transform.FindChild ("HitBossSound").GetComponent<AudioSource> ().Play ();
					playerStats.GainEnergy ();
					if (chargedAttack) {
						if (other.name == "Fome") {
							boss.GetComponent<FomeController> ().ReceiveDamage (4.0f * attackDamage);
						}
						if (other.name == "Tempestade") {
							boss.GetComponent<TempestadeController> ().ReceiveDamage (4.0f * attackDamage);
						}
					} else {
						if (other.name == "Fome") {
							boss.GetComponent<FomeController> ().ReceiveDamage (attackDamage);
						}
						if (other.name == "Tempestade") {
							boss.GetComponent<TempestadeController> ().ReceiveDamage (attackDamage);
						}
					}
					hit.Add (boss.GetInstanceID ());
				} else if (other.tag == "Enemy") {
					playerStats.GainEnergy ();
					//Debug.Log ("Hit Crow");
					if (chargedAttack) {
						other.gameObject.GetComponent<EnemyStats> ().ReceiveDamage (4.0f * attackDamage);
					} else {
						other.gameObject.GetComponent<EnemyStats> ().ReceiveDamage (attackDamage);
					}
					hit.Add (other.gameObject.GetInstanceID ());
				} else if (other.tag == "TamborTrigger") {
					if (chargedAttack) {
						other.transform.parent.GetComponent<TamborScript> ().Raio (other.transform.position - transform.position);		
					} else {
						other.transform.parent.GetComponent<TamborScript> ().RaioSimples (other.transform.position - transform.position);		
					}
					//Debug.Log (other.gameObject.GetInstanceID ());
					hit.Add (other.gameObject.GetInstanceID ());
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		//Attack (other);
	}

	void OnTriggerStay2D(Collider2D other){
		//Attack (other);
	}


	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Boss") {
			
		}
	}
/*	IEnumerator DamageTime()
	{
		yield return new WaitForSeconds(handAttackAnim.length);
		if (boss != null) {
			if (boss.name.Equals ("BoneBoss")) {
				boss.GetComponentInChildren<BoneBossController> ().life -= attackDamage;
				boss.GetComponentInChildren<SpriteRenderer> ().color = Color.red;
				yield return new WaitForSeconds (0.05f);
				boss.GetComponentInChildren<SpriteRenderer> ().color = Color.white;
			} else if (boss.name.Equals ("Fome")) {
				boss.GetComponent<FomeController> ().life -= attackDamage;
				boss.GetComponent<SpriteRenderer> ().color = Color.red;
				yield return new WaitForSeconds (0.05f);
				boss.GetComponent<SpriteRenderer> ().color = Color.white;
			} else if (boss.name.Equals ("FireBoss")) {
				//            boss.GetComponent<FireBossController>().life -= attackDamage;
			}
		}
	}
*/		

}

