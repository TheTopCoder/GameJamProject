using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	float life;
	float maxLife = 10;
	float dirX;
	float dirY;
	float speedX;
	float speedY;
	float lookDirX;
	float lookDirY;
	float rollDirX;
	float rollDirY;
	public string state;
	float speed;
	float pushSpeed;
	float rollSpeed;
	float rollTime=0.19f;
	float speedYMult;
	float rollCurrentTime;
	float rollCooldown=0.4f;
	float rollCurrentCooldown;
	bool faceRight = true;
	bool canEnterDoor = false;
	bool invulnerable;
	bool spawnedDust = false;
	float jumpY = 0;
	float jumpSpeed = 4.5f;
	float jumpSpeedCur = 0;
	float jumpGravity = 0.3f;
	float previousSpeedY = 0;
	float dirAbs = 0;
	float shadowYOffset = 0.475f;
	float shadowXOffset = 0f;
	float lastShadowX;
	float lastShadowY;
	//	float playerRelPos = 0;
	public GameObject groundReference;

	[SerializeField]
	GameObject groundShadow;

	PlayerStats playerStats;
	[SerializeField]
	Animator bodyAnim;
	[SerializeField]
	Animator bodyLightAnim;
	[SerializeField]
	Animator handAnim;
	//  [SerializeField]
	//  GameObject smearPrefab;
	[SerializeField]
	GameObject dashDust;
	[SerializeField]
	GameObject dashDustPosition;
	[SerializeField]
	AudioSource walkingAudio;
	[SerializeField]
	GameObject canvas;

	//From PlayerAttack	
	//	public string state;//wait,attack,attackStrong,recoverAttack
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
	GameObject boss;
	[SerializeField]
	GameObject bodyLight;
	//	[SerializeField]
	//	Animator handAnim;
	//	[SerializeField]
	//	Animator bodyAnim;
	[SerializeField]
	AnimationClip handAttackAnim;

	//New Variables
	public bool damaged;
	bool changedState;

	// Use this for initialization
	void Start (){
		groundReference = GameObject.FindGameObjectWithTag("PlayerBase");
		lastShadowX = groundReference.transform.position.x;
		lastShadowY = groundReference.transform.position.y;
		//groundReference.transform.position = transform.position+new Vector3(0,-shadowYOffset,0);
		Color tmp = groundReference.GetComponent<Renderer> ().material.color;
		tmp.a = 0.4f;
		groundReference.GetComponent<Renderer> ().material.color = tmp;
		Physics2D.IgnoreCollision (groundReference.GetComponent<Collider2D> (), GetComponent<Collider2D> (), true);
		pushSpeed = 0;

		life = maxLife;
		playerStats = GetComponent<PlayerStats> ();
		speed = playerStats.speed;
		rollSpeed = playerStats.rollSpeed;
		speedYMult = playerStats.speedYMult;
		rollTime = playerStats.rollTime;
		rollCooldown = playerStats.rollCooldown;
		state = "movement";
		rollCurrentTime = rollTime;
		rollCurrentCooldown = 0;
		invulnerable = false;
		walkingAudio.Pause();
		spawnedDust = false;

		//From PlayerAttack
		attackDamage = playerStats.attackDamage;
		//attackTime = handAttackAnim.length * 1.05f;
		attackStrongDamage = playerStats.attackStrongDamage;
		attackStrongTime = playerStats.attackStrongTime;
		blinked = false;
		canHit = false;
		chargeAttackTime = 0.6f;
		curAttack = 1;
		hit = new ArrayList();
		//attackCurrentTime = attackTime;
		boss = GameObject.FindGameObjectWithTag ("Boss");

		damaged = false;
	}

	void Move(){
		dirX = Input.GetAxisRaw ("Horizontal");
		dirY = Input.GetAxisRaw ("Vertical");

		if (dirX > 0 && !faceRight)
		{
			Flip();
		}
		else if (dirX < 0 && faceRight)
		{
			Flip();
		}
		if (dirX == 0 && dirY == 0)
		{
			handAnim.SetBool("Walking", false);
			bodyAnim.SetBool("Walking", false);
			bodyLightAnim.SetBool("Walking", false);
			walkingAudio.Pause();
		}
		else
		{
			handAnim.SetBool("Walking", true);
			bodyAnim.SetBool("Walking", true);
			bodyLightAnim.SetBool("Walking", true);
			walkingAudio.UnPause();
		}
		dirAbs = Mathf.Sqrt (dirX * dirX + dirY * dirY);
		if (dirAbs != 0) {
			dirX = dirX / dirAbs;
			dirY = dirY / dirAbs;
		}
		speedX = dirX * speed;
		speedY = dirY * speed * speedYMult;
		groundReference.GetComponent<Rigidbody2D> ().velocity = new Vector2 (speedX, speedY);

		//Push
		groundReference.GetComponent<Rigidbody2D> ().velocity -= new Vector2 (pushSpeed, 0);
		if (pushSpeed > 0) {
			pushSpeed -= Time.deltaTime * 3.5f;
			if (pushSpeed < 0)
				pushSpeed = 0;
		}
		lookDirX = Input.GetAxis ("Horizontal");
		lookDirY = Input.GetAxis ("Vertical");
		float lookDirAbs = Mathf.Sqrt (dirX * dirX + dirY * dirY);
		if (lookDirAbs != 0) {
			lookDirX = lookDirX / lookDirAbs;
			lookDirY = lookDirY / lookDirAbs;
		}
		rollDirX = dirX;
		rollDirY = dirY;
		rollCurrentCooldown -= Time.deltaTime;
	}

	public void Push(float pushForce){
		pushSpeed = pushForce;
	}

	void State(){
		//Movement
		if (state == "movement"){
			Move ();
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (groundReference.transform.position.x - lastShadowX, groundReference.transform.position.y - lastShadowY) / Time.deltaTime;
			lastShadowX = groundReference.transform.position.x;
			lastShadowY = groundReference.transform.position.y;

			if(canEnterDoor /*&& (Input.GetButtonDown("XboxX") || Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))*/)
			{
				canvas.GetComponent<TransitionScript>().ChangeScene();
			}
		}

		//Hit

		//Skill

		//Recover Energy
		if (state == "recoverEnergy") {
			playerStats.energy += 2;
		}

		//Recover Life

		//PrepareAttack
		if (state == "prepareAttack") {

			if (Time.time - beganChargeTime >= chargeAttackTime && !chargedAttack) {
				if (transform.FindChild ("Sounds").transform.FindChild ("ChargedAttackSound").GetComponent<AudioSource> ().isPlaying) {
					transform.FindChild ("Sounds").transform.FindChild ("ChargedAttackSound").GetComponent<AudioSource> ().Stop ();
				}
			}
			if (Time.time - beganChargeTime >= chargeAttackTime) {
				chargedAttack = true;
			} else {
				chargedAttack = false;
			}

			//blink when charging
			Color c = bodyLight.GetComponent<SpriteRenderer> ().color;
			if (blinked) {
				if (Time.time - blinkedTime < 0.08f) {
					c.a = 1f;
				} else if (Time.time - blinkedTime < 0.16f) {
					c.a = 0.5f;
				} else if (Time.time - blinkedTime < 0.24f) {
					c.a = 1f;
				} else
					c.a = 0.5f;
				bodyLight.GetComponent<SpriteRenderer> ().color = c;
			} else if (c.a < 0.5f) {
				c.a += 0.5f * Time.deltaTime / chargeAttackTime;
				bodyLight.GetComponent<SpriteRenderer> ().color = c;
			} else if (c.a > 0.5f) {
				blinked = true;
				blinkedTime = Time.time;
				c.a = 0;
				bodyLight.GetComponent<SpriteRenderer> ().color = c;
			}
		}
		//Attack

		//Roll
		if (state == "roll"){
			groundReference.GetComponent<Rigidbody2D> ().velocity = new Vector2 (speedX, speedY);
			groundReference.GetComponent<Rigidbody2D> ().velocity -= new Vector2 (pushSpeed, 0);
			if (pushSpeed > 0) {
				pushSpeed -= Time.deltaTime * 3.5f;
				if (pushSpeed < 0)
					pushSpeed = 0;
			}
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (groundReference.transform.position.x - lastShadowX, groundReference.transform.position.y - lastShadowY) / Time.deltaTime;
			lastShadowX = groundReference.transform.position.x;
			lastShadowY = groundReference.transform.position.y;
			rollCurrentTime -= Time.deltaTime;
		}
	}

	void BeginState(string newState){
		state = newState;

		//Movement
		if (state == "movement"){
			GetComponent<Collider2D> ().enabled = true;
		}		
		//Hit
		if (state == "hit"){
			invulnerable = true;
			handAnim.SetBool("Flint", true);
			bodyAnim.SetBool("Flint", true);
			bodyLightAnim.SetBool("Flint", true);
			StartCoroutine ("ReceiveDamage");
		}
		//Skill

		//Recover Energy

		//Recover Life

		//PrepareAttack
		if (state == "prepareAttack"){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			groundReference.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		}
		//Attack

		//Roll
		if (state == "roll"){
			Instantiate (dashDust, dashDustPosition.transform.position, new Quaternion (0f, 0f, 0f, 0f), gameObject.transform);
			handAnim.SetBool ("Dash", true);
			bodyAnim.SetBool ("Dash", true);
			bodyLightAnim.SetBool ("Dash", true);
			speedX = rollDirX * rollSpeed;
			speedY = rollDirY * rollSpeed * speedYMult;
		}
	}

	public void FinishState(){
		//Movement

		//Hit
		if (state == "hit"){
			damaged = false;
		}
		//Skill

		//Recover Energy

		//Recover Life

		//PrepareAttack

		//Attack
		if (state == "attack"){
			Color color = bodyLight.GetComponent<SpriteRenderer>().color;
			color.a = 0;
			bodyLight.GetComponent<SpriteRenderer> ().color = color;
			blinked = false;
			canHit = false;
			if (hit.Count > 0) {
				hit.Clear ();
			}
		}

		//Roll
		if (state == "roll"){
			handAnim.SetBool ("Dash", false);
			bodyAnim.SetBool ("Dash", false);
			bodyLightAnim.SetBool ("Dash", false);
			Destroy (GameObject.FindGameObjectWithTag ("DashDust"));
			spawnedDust = false;
			rollCurrentTime = rollTime;
			rollCurrentCooldown = rollCooldown;
		}

		state = "";
	}

	void ChangeState(string newState){
		if (!changedState){
			changedState = true;
			FinishState();
			BeginState(newState);
		}
	}

	void DecideState(){
		changedState = false;

		//Movement
		if (state == ""){
			ChangeState("movement");
		}
		if (state == "recoverEnergy"){
			if (!Input.GetKey(KeyCode.C)){
				ChangeState("movement");
			}
		}
		if (state == "roll"){
			if (rollCurrentTime < 0) {
				ChangeState("movement");
			}
		}

		//Hit
		if (state != "hit") {
			if (damaged) {
				ChangeState ("hit");
			}
		}

		//Skill
		if (state == "movement"){
			if (Input.GetKeyDown (KeyCode.F) || Input.GetMouseButtonDown (2) || Input.GetAxisRaw ("XboxB") > 0 || Input.GetAxisRaw ("XboxR1") > 0) {
				if (GameObject.Find ("Global Controller").GetComponent<GlobalController> ().defeatedTempestade) {
					if (playerStats.energy >= playerStats.maxEnergy / 4) {
						StartCoroutine (SkillRaio ());
						transform.FindChild ("Sounds").transform.FindChild ("SpecialAttackSound").GetComponent<AudioSource> ().Play ();
					}
				} else if (GameObject.Find ("Global Controller").GetComponent<GlobalController> ().defeatedFome) {
					if (playerStats.energy >= playerStats.maxEnergy / 4) {
						StartCoroutine (SkillCorvo ());
						transform.FindChild ("Sounds").transform.FindChild ("SpecialAttackSound").GetComponent<AudioSource> ().Play ();
					}
				}
			}
		}

		//Recover Energy
		if (state == "movement"){
			if (Input.GetKey (KeyCode.C)) {
				ChangeState("recoverEnergy");
			}
		}

		//Recover Life
		if (state == "movement"){
			if (Input.GetKeyDown (KeyCode.Space) || Input.GetAxisRaw ("XboxY") > 0 || Input.GetAxisRaw ("XboxL2") > 0) {
				if (playerStats.energy >= playerStats.maxEnergy / 2) {
					StartCoroutine (SkillLife ());
				}
			}
		}

		//PrepareAttack
		if (state == "movement"){
			if ((Input.GetAxisRaw ("XboxX") > 0 || Input.GetAxisRaw ("XboxR2") > 0 || Input.GetKeyDown (KeyCode.E) || Input.GetMouseButtonDown (0))) {
				ChangeState("prepareAttack");
				beganChargeTime = Time.time;
				if (curAttack == 1) {
					handAnim.SetTrigger ("PrepareAttack");
				} else {
					handAnim.SetTrigger ("PrepareAttack2");
				}
				transform.FindChild ("Sounds").transform.FindChild ("ChargedAttackSound").GetComponent<AudioSource> ().Play ();
			}
		}

		//Attack
		if (state == "prepareAttack"){
			if (!(Input.GetAxisRaw ("XboxX") > 0 || Input.GetAxisRaw ("XboxR2") > 0 || Input.GetKey (KeyCode.E) || Input.GetMouseButton (0))) {
				ChangeState("attack");
				Color color = bodyLight.GetComponent<SpriteRenderer> ().color;
				color.a = 0;
				bodyLight.GetComponent<SpriteRenderer> ().color = color;
				blinked = false;
				if (transform.FindChild ("Sounds").transform.FindChild ("ChargedAttackSound").GetComponent<AudioSource> ().isPlaying) {
					transform.FindChild ("Sounds").transform.FindChild ("ChargedAttackSound").GetComponent<AudioSource> ().Stop ();
				}
				transform.FindChild ("Sounds").FindChild ("ShakeWeaponSound").GetComponent<AudioSource> ().time = 0.25f;
				transform.FindChild ("Sounds").FindChild ("ShakeWeaponSound").GetComponent<AudioSource> ().Play ();
				if (curAttack == 1) {
					curAttack = 2;
					handAnim.SetTrigger ("Attack");
				} else {
					curAttack = 1;
					handAnim.SetTrigger ("Attack2");
				}
			}
		}

		//Roll
		if (state == "movement"){
			if ((Input.GetAxisRaw ("XboxA") > 0 || (Input.GetAxisRaw ("XboxL1") > 0) || Input.GetKey (KeyCode.Q) || Input.GetMouseButtonDown (1)) && rollCurrentCooldown < 0 && dirAbs != 0) {
				ChangeState("roll");
				transform.FindChild ("Sounds").FindChild ("DashSound").GetComponent<AudioSource> ().Play ();
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		DecideState();
		State();
	}

	public void DamagePlayer()
	{
		if (!invulnerable){
			damaged = true;
		}
	}

	public IEnumerator ReceiveDamage()
	{
		GetComponent<PlayerStats> ().DamagePlayer ();
		Color auxColor = new Color(bodyAnim.gameObject.GetComponent<SpriteRenderer>().color.r, bodyAnim.gameObject.GetComponent<SpriteRenderer>().color.g, bodyAnim.gameObject.GetComponent<SpriteRenderer>().color.b, 0);
		StartCoroutine(KnockBack());
		for (int i = 0; i < 9; i++)
		{
			if (handAnim.gameObject.GetComponent<SpriteRenderer>().color != auxColor)
			{
				handAnim.gameObject.GetComponent<SpriteRenderer>().color = auxColor;
				bodyAnim.gameObject.GetComponent<SpriteRenderer>().color = auxColor;
			}
			else
			{
				handAnim.gameObject.GetComponent<SpriteRenderer>().color = new Color(handAnim.gameObject.GetComponent<SpriteRenderer>().color.r, handAnim.gameObject.GetComponent<SpriteRenderer>().color.g, handAnim.gameObject.GetComponent<SpriteRenderer>().color.b, 1);
				bodyAnim.gameObject.GetComponent<SpriteRenderer>().color = new Color(bodyAnim.gameObject.GetComponent<SpriteRenderer>().color.r, bodyAnim.gameObject.GetComponent<SpriteRenderer>().color.g, bodyAnim.gameObject.GetComponent<SpriteRenderer>().color.b, 1);
			}
			yield return new WaitForSeconds(0.125f);
		}
		handAnim.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
		bodyAnim.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
		//			Debug.Log ("Vulnerable");
		invulnerable = false;
	}
	IEnumerator KnockBack()
	{
		for (int i = 0; i < 5; i++)
		{
			if (faceRight)
			{
				//                transform.position -= new Vector3(0.1f, 0f);
				groundReference.GetComponent<Rigidbody2D> ().velocity += new Vector2(-0.1f,0);
				GetComponent<Rigidbody2D> ().velocity = groundReference.GetComponent<Rigidbody2D> ().velocity;
			}
			else
			{
				groundReference.GetComponent<Rigidbody2D> ().velocity += new Vector2(+0.1f,0);
				GetComponent<Rigidbody2D> ().velocity = groundReference.GetComponent<Rigidbody2D> ().velocity;
				//                transform.position += new Vector3(0.1f, 0f);
			}
			yield return new WaitForSeconds(0.05f);
		}

		handAnim.SetBool("Flint", false);
		bodyAnim.SetBool("Flint", false);
		bodyLightAnim.SetBool("Flint", false);
		FinishState ();
		//state = "wait";
		//invulnerable = false;
	}

	IEnumerator Blink(){
		yield return null;
	}
	void Flip()
	{
		faceRight = !faceRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	IEnumerator ChangeScene(){
		yield return new WaitForSeconds (0.75f);
		GameObject.Find ("TransitionCanvas").GetComponent<TransitionScript> ().ChangeScene ();
	}


	IEnumerator SkillLife(){
		if (playerStats.life < playerStats.maxLife && state == "wait" && state == "movement") {
			bodyAnim.SetTrigger ("SkillLife");
			handAnim.SetTrigger ("SkillLife");
			GameObject.FindGameObjectWithTag ("Light").GetComponent<Animator> ().Play(0);
			GameObject.FindGameObjectWithTag ("Light").GetComponent<SpriteRenderer> ().enabled = true;
			playerStats.energy -= playerStats.maxEnergy/2;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			groundReference.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			yield return new WaitForSeconds (0.425f);
			playerStats.life++;
			bodyAnim.SetTrigger ("Idle");
			handAnim.SetTrigger ("Idle");
			GameObject.FindGameObjectWithTag ("Light").GetComponent<SpriteRenderer> ().enabled = false;
		}
		yield return new WaitForSeconds (0);
		FinishState();
	}

	IEnumerator SkillCorvo(){
		Debug.Log ("Skill Corvo");
		if (state == "wait" && state == "movement" && GameObject.Find("Global Controller").GetComponent<GlobalController>().defeatedFome) {
			//			bodyAnim.SetTrigger ("SkillCorvo");
			handAnim.SetTrigger ("SkillCorvo");
			playerStats.energy -= playerStats.maxEnergy/4;
			state = "skill";
			//state = "skill";
			yield return new WaitForSeconds (0.3f);
			state = "wait";
			//			state = "movement";
			handAnim.SetTrigger ("Idle");
		}
		yield return new WaitForSeconds (0.02f);
		FinishState();
	}

	IEnumerator SkillRaio(){
		if (state == "wait" && state == "movement" && GameObject.Find("Global Controller").GetComponent<GlobalController>().defeatedTempestade) {
			handAnim.SetTrigger ("SkillRaio");
			playerStats.energy -= playerStats.maxEnergy/4;
			state = "skill";
			//state = "skill";
			yield return new WaitForSeconds (0.35f);
			state = "wait";
			//			state = "movement";
			handAnim.SetTrigger ("Idle");
		}
		yield return new WaitForSeconds (0.02f);
		FinishState();
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
		FinishState();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.name == "Soul") {
			Destroy (col.gameObject);
			transform.FindChild ("Sounds").transform.FindChild ("GetPowerSound").GetComponent<AudioSource> ().Play();
			handAnim.SetBool("Walking",false);
			bodyAnim.SetBool ("Walking", false);
			bodyLightAnim.SetBool ("Walking", false);
			bodyAnim.SetTrigger ("Idle");
			bodyLightAnim.SetTrigger("Idle");
//			state = "victory";
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0,0);
			groundReference.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0,0);
			if (GameObject.Find ("Soul_Pulse")) {
				Destroy (GameObject.Find ("Soul_Pulse"));
			}
			if (SceneManager.GetActiveScene ().name == "FomeTriangularArena") {
				handAnim.SetTrigger ("GetSkillCorvo");
				GameObject.Find ("Global Controller").GetComponent<GlobalController> ().defeatedFome = true;
			}
			if (SceneManager.GetActiveScene ().name == "Tempestade") {
				handAnim.SetTrigger ("GetSkillRaio");
				GameObject.Find ("Global Controller").GetComponent<GlobalController> ().defeatedTempestade = true;
			}
			StartCoroutine (ChangeScene ());
		}
		if (col.name.Equals("Corredor Intro 5"))
		{
			canEnterDoor = true;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.name.Equals("Corredor Intro 5"))
		{
			canEnterDoor = false;
		}
	}
}
