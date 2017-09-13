using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempestadeController : MonoBehaviour
{

	#region Variables
	Vector3 newPosition;

	public GameObject waveAttack;
	public GameObject groundAttack;
	public GameObject circleCollider;
	public GameObject hurricaneCollider;

	public float life;
	public float maxLife = 300;
	int dashAttackDamage = 10;
	float dashSpeed = 1.5f;
	float slideSpeed = 8f;
	int numAbilities;
	int strikes;
	GameObject fade;
	[SerializeField]
	GameObject FadeOut;
	[SerializeField]
	AnimationClip animClipUp;
	[SerializeField]
	AnimationClip animClipDown;
	[SerializeField]
	AnimationClip animClip;
	[SerializeField]
	GameObject player;
	[SerializeField]
	float range;
	[SerializeField]
	Transform[] spitArea;
	public GameObject arenacolliderup;
	public GameObject arenacolliderdown;
	public GameObject arenacolliderright;
	public GameObject arenacolliderleft;
	float minX;
	float minY;
	float maxX;
	float maxY;
	float cooldownMovement;
	float cooldownAbility;
	List<int> attackProb = new List<int>();
	List<int> attackCurProb = new List<int>();
	List<int> attackProbUp = new List<int>();
	int abilityState=0;
	int prob1;
	int prob1Current;
	int prob2;
	int prob2Current;
	int prob3;
	int prob3Current;
	int attackCount;
	int attackLimit;
	string state;
	bool moving;
	float arenaX = 1.5f;
	float arenaY = 0.7f;
	public bool canHit = false;
	bool hit = false;
	public Transform mainCamera;
	private float shakeDuration;
	private float shakeAmount;
	private float decreaseFactor;
	private Vector3 camPosition;
	private bool canShock;
	public bool faceRight = false;

	[SerializeField]
	GameObject tambor;

	GameObject wave;
	[SerializeField]
	GameObject spawnableCrow;
	GameObject attackHitbox;

	string ability;
	float minWalkTime=2.5f;
	float walkSpeed = 1.6f;
	public bool canAttackGround;
	Vector3 lastDirection = new Vector3(0,0,0);
	float ArenaA = 11f/2,ArenaB = 6.2f / 2;
	Vector2 ArenaCenter;
	bool canStrikeAgain = true;
	bool continuePinball;

	#endregion


	void Start()
	{
		life = maxLife;
		cooldownMovement = 0;
		cooldownAbility = 3f;
		ability = "none";
		numAbilities = 5;
		for (int i = 0; i <= numAbilities; i++) {
			attackProb.Add (1);
			attackProbUp.Add (0);
			attackCurProb.Add(1);
		}
//		1. Walk
//		2. Pinball

		attackCount = 0;
		minX = player.transform.position.x - range;
		minY = player.transform.position.y - range;
		maxX = player.transform.position.x + range;
		maxY = player.transform.position.y + range;
		state = "movement";
		faceRight = false;
		canShock = true;
		mainCamera = Camera.main.transform;
		shakeDuration = 0f;
		shakeAmount = 0.1f;
		decreaseFactor = 1;
		ArenaCenter.x = 0.6f;
		ArenaCenter.y = -3.3f;
	}


	void Update()
	{
		//Chefe morrer
		if (life <= 0)
		{
//			fade = (GameObject) Instantiate (FadeOut, transform.position, new Quaternion(0f,0f,0f,0f));
//			fade.GetComponent<FadeTransition>().nextScene = "BeatDemo";
			GameObject.Find ("Global Controller").GetComponent<GlobalController>().defeatedTempestade = true;
			GameObject.Find("TransitionCanvas").GetComponent<TransitionScript>().ChangeScene();
			Destroy(gameObject);
		}


		//Debug.Log ("State: " + state);
		camPosition = mainCamera.position;

		//Shake
		if (shakeDuration > 0)
		{
			mainCamera.position = camPosition + Random.insideUnitSphere * shakeAmount;
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			mainCamera.position = camPosition;
		}

		//Definir se o boss ou o player vao aparecer na frente
		if (player!=null && player.transform.position.y < transform.FindChild("point").transform.position.y)
		{
			GetComponentInChildren<SpriteRenderer>().sortingOrder = -3;
		}
		else
		{
			GetComponentInChildren<SpriteRenderer>().sortingOrder = -3;
			//            GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
		}

		//Fazer o chefe virar
/*		if (player.transform.position.x < transform.FindChild("point").transform.position.x && faceRight)
        {
            Flip();
            transform.position -= new Vector3(3,0,0);
        }
        else if(player.transform.position.x > transform.FindChild("point").transform.position.x && !faceRight)
        {
            Flip();
            transform.position += new Vector3(3, 0, 0);
        }
*/
		if (state == "movement")
		{
			ability = "none";
			if (attackHitbox != null) {
				DestroyHitbox ();
			}

			float lastSpeed = GetComponent<Rigidbody2D> ().velocity.magnitude;

			if (player != null) {
				MoveInDirection (DecideDirection (), walkSpeed);
			}

			if (GetComponent<Rigidbody2D> ().velocity.magnitude > 0 && lastSpeed == 0) {
				GetComponent<Animator> ().SetTrigger ("Walk");
			} else if (GetComponent<Rigidbody2D> ().velocity.magnitude == 0 && lastSpeed > 0){
				GetComponent<Animator> ().SetTrigger ("Idle");
			}
				

			cooldownMovement -= Time.deltaTime;
			cooldownAbility -= Time.deltaTime;


			//Atacar
			if (cooldownAbility <= 0)
			{
					state = "ChooseAbility";
					cooldownAbility = Random.Range (1.2f, 1.8f);
			}
			/* else if (cooldownMovement <= 0)
            {
                moving = true;
                minX = player.transform.position.x - range;
                minY = player.transform.position.y - range;
                maxX = player.transform.position.x + range;
                maxY = player.transform.position.y + range;
                newPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY));
                cooldownMovement = Random.Range(1, 2);
            }*/
			/*            if (moving)
			{
				if (MoveToPosition(newPosition))
				{
					moving = false;
				}
			}
		}
		*/
	}
	if (state == "ChooseAbility")
	{
		//state = "ability";
		ChooseAbility();
	}

	if (canStrikeAgain&&Mathf.Pow ((transform.position.x + GetComponent<Rigidbody2D>().velocity.x * Time.deltaTime- ArenaCenter.x), 2)/Mathf.Pow(ArenaA,2)+Mathf.Pow ((transform.position.y + GetComponent<Rigidbody2D>().velocity.y * Time.deltaTime - ArenaCenter.y), 2)/Mathf.Pow(ArenaB,2)>=1&&ability=="pinball") {
		ChangeDirection (GameObject.Find("Cenario"));
		strikes--;
		if (strikes == 1) {
			GetComponent<Animator> ().SetTrigger ("Attack_Hurricane_Last");
		}
		canStrikeAgain = false;
	}
	if (Mathf.Pow ((transform.position.x - tambor.transform.position.x), 2) / Mathf.Pow (ArenaA, 2) + Mathf.Pow ((transform.position.y - tambor.transform.position.y), 2) / Mathf.Pow (ArenaB, 2) < 1) {
		canStrikeAgain = true;
	}
}


Vector3 DecideDirection(){
		//Debug.Log ("Deciding Direction");
		Vector3 direction = player.transform.position - transform.position;
		float playerDistance = (player.transform.position - transform.position).magnitude;
		direction.z = 0;
		direction = direction.normalized;
		Vector3 startingPoint = transform.position;
		Vector3 direction1;
		Vector3 direction2;
		Ray ray = new Ray (startingPoint, direction);
		float distance = Vector3.Cross (ray.direction, tambor.transform.position - ray.origin).magnitude;
		if (playerDistance < 0.1f) {
		
			return new Vector3 (0, 0, 0);
		} else if (playerDistance < 0.2f && lastDirection.magnitude == 0) {
			return new Vector3 (0, 0, 0);
		} else if (distance >= tambor.GetComponent<CircleCollider2D> ().radius + circleCollider.GetComponent<CircleCollider2D> ().radius) {
			return direction;
		} else {
			bool tamborInTheMiddle = false;
			//Debug.Log ("PlayerDistanceRay: " + (ray.GetPoint (playerDistance) - tambor.transform.position).magnitude);
			for (float i = 1; i <= 100; i++) {
			if ((ray.GetPoint (i / 100 * playerDistance) - tambor.transform.position).magnitude <= tambor.GetComponent<CircleCollider2D> ().radius + circleCollider.GetComponent<CircleCollider2D> ().radius) {
					tamborInTheMiddle = true;
				}
			}
			if (!tamborInTheMiddle) {
				return direction;
			}
			else{
				int count = 0;
				Vector3 originalDirection = direction;
				do {
					count++;
					direction = Quaternion.Euler (0, 0, 0.1f) * direction;
					ray = new Ray (startingPoint, direction);
					distance = Vector3.Cross (ray.direction, tambor.transform.position - ray.origin).magnitude;
				} while((distance < tambor.GetComponent<CircleCollider2D> ().radius + circleCollider.GetComponent<CircleCollider2D> ().radius || Vector3.Dot (direction, originalDirection) < 0) && count < 1800);
				direction1 = direction;
				direction2 = originalDirection;
				count = 0;
				do {
					count++;
					direction2 = Quaternion.Euler (0, 0, -0.1f) * direction2;
					ray = new Ray (startingPoint, direction2);
					distance = Vector3.Cross (ray.direction, tambor.transform.position - ray.origin).magnitude;
				} while((distance < tambor.GetComponent<CircleCollider2D> ().radius + circleCollider.GetComponent<CircleCollider2D> ().radius || Vector3.Dot (direction2, originalDirection) < 0) && count < 1800);
				if (Vector3.Angle (originalDirection, direction1) < Vector3.Angle (originalDirection, direction2)) {
					//Debug.Log (direction);
					//Debug.Log (count);
					return direction1;
				} else {
					return direction2;
				}
			}
		}
	}
void MoveInDirection(Vector3 direction,float speed){
	lastDirection = direction;
	GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x,direction.y)*speed;
}

void ChooseAbility()
{
	attackCount = 0;
	attackLimit = 1;
	//Tornar as habilidades mais provaveis a medida que nao sao usadas
	//Dependendo da posicao/distancia do jogador, a probabilidade de cada ataque é diferente
	if (true/*player != null && Vector3.Distance (transform.position, player.transform.position) > 4.0f*/) {

		attackProb [2] = 1;
		attackProbUp [2] = 1;
		attackProb [3] = 1;
		attackProbUp [3] = 1;
		if (canAttackGround) {
			attackProb [1] = 1;
			attackProbUp [1] = 1;
			attackProb [4] = 3;
			attackProbUp [4] = 0;
		} else {
			attackProb [1] = 3;
			attackProbUp [1] = 1;
			attackProb [4] = 0;
			attackProbUp [4] = 0;
		}
		attackProb [5] = 1;
		attackProbUp [5] = 1;
		if (life <= maxLife * 1 / 2) {
//			attackProb [5] = 2;
//			attackProbUp [5] = 1;
		} else {
//			attackProb [5] = 0;
//			attackProbUp [5] = 0;
		}
		if (life <= maxLife * 2 / 3) {
//			attackProb [6] = 5;
//			attackProbUp [6] = 1;
		} else {
//			attackProb [6] = 0;
//			attackProbUp [6] = 0;
		}
			abilityState = 1;
			for (int i = 1; i <= numAbilities; i++) {
				attackCurProb [i] = attackProb [i];
			}
	}

	int sumProb = 0;
	int sumCurProb = 0;
	int curAbility = 1;
	int thatAttackProb = 0;
	for (int i = 1; i <= numAbilities; i++) {
		sumProb += attackCurProb [i];
	}
	//Debug.Log (sumProb);
	float abilityNumber = Random.Range(0, sumProb);
	//Debug.Log ("Ability Number: " + abilityNumber);
	for (int i = 1; i <= numAbilities; i++) {
		attackCurProb [i]+= attackProbUp[i];
	}

	//Walk
	thatAttackProb = attackCurProb [curAbility] - attackProbUp[curAbility];
	if (abilityNumber >= sumCurProb && abilityNumber < sumCurProb + thatAttackProb)
	{	
		//Debug.Log ("Chose to move");
		state = "movement";
		ability = "none";
		cooldownAbility = 0.5f;
		attackCurProb [curAbility] = attackProb[curAbility];
	}
	sumCurProb += thatAttackProb;
	curAbility++;

	//Pinball
	thatAttackProb = attackCurProb [curAbility] - attackProbUp[curAbility];
	if (abilityNumber >= sumCurProb && abilityNumber < sumCurProb + thatAttackProb)
	{
		state = "ability";
		//Debug.Log ("Pinball");
		StartCoroutine(PinballAttack());
		attackCurProb [curAbility] = attackProb[curAbility];
	}
	sumCurProb += thatAttackProb;
	curAbility++;

	//JumpAttack
	thatAttackProb = attackCurProb [curAbility] - attackProbUp[curAbility];
	if (abilityNumber >= sumCurProb && abilityNumber < sumCurProb + thatAttackProb)		{
		state = "ability";
		StartCoroutine(JumpAttack());
		attackCurProb [curAbility] = attackProb[curAbility];
	}
	sumCurProb += thatAttackProb;
	curAbility++;

	//GroundAttack
	thatAttackProb = attackCurProb [curAbility] - attackProbUp[curAbility];
	if (abilityNumber >= sumCurProb && abilityNumber < sumCurProb + thatAttackProb)		{
		state = "ability";
		StartCoroutine(GroundAttack());
		attackCurProb [curAbility] = attackProb[curAbility];
	}
	sumCurProb += thatAttackProb;
	curAbility++;

	//WaterAttack
	thatAttackProb = attackCurProb [curAbility] - attackProbUp[curAbility];
	if (abilityNumber >= sumCurProb && abilityNumber < sumCurProb + thatAttackProb)		{
		state = "ability";
		StartCoroutine(WaterAttack());
		attackCurProb [curAbility] = attackProb[curAbility];
	}
	sumCurProb += thatAttackProb;
	curAbility++;

/*
	curAbility++;
	thatAttackProb = attackCurProb [curAbility] - attackProbUp[curAbility];
	if (abilityNumber >= sumCurProb && abilityNumber < sumCurProb + thatAttackProb)		{
		StartCoroutine(CrowAttack());
		attackCurProb [curAbility] = attackProb[curAbility];
	}
	sumCurProb += thatAttackProb;
	curAbility++;
	thatAttackProb = attackCurProb [curAbility] - attackProbUp[curAbility];
	if (abilityNumber >= sumCurProb && abilityNumber < sumCurProb + thatAttackProb)		{
		StartCoroutine(GrabAttack());
		attackCurProb [curAbility] = attackProb[curAbility];
	}
	sumCurProb += thatAttackProb;
*/
}

//Não utilizado
bool MoveToPosition(Vector3 newPosition)
{
	if (Vector3.Distance(transform.position, newPosition) < 0.01f)
	{
		return true;
	}
	else
	{
		transform.position = Vector3.MoveTowards(this.transform.position, newPosition, Time.deltaTime);
		return false;
	}
}

//Ativada durante a animacão
public void FinishAttack(){
	ability = "none";
	attackCount++;
	GetComponent<Animator> ().SetTrigger ("Idle");
	//Debug.Log ("Finished Attack");
	if (attackCount >= attackLimit) {
		state = "movement";
		Destroy (attackHitbox);
		GetComponentInChildren<Animator>().SetTrigger("Fome_Idle");
	}
	canStrikeAgain = true;
}

void CreateHitbox(string name){
	UnityEngine.Object hitboxprefab = Resources.Load ("Tempestade/" + name);
	attackHitbox = (GameObject) Instantiate (hitboxprefab, transform.position, Quaternion.identity);
}
void DestroyHitbox(){
	if (attackHitbox != null) {
		Destroy (attackHitbox);
	}
}

//Ataques

void ChangeDirection(GameObject wall){
	if (wall.name == "Cenario") {
		//Debug.Log ("Hit Cenario");
		Vector2 pos = new Vector2 (transform.position.x, transform.position.y)-ArenaCenter;
		pos = new Vector2 (pos.x, pos.y * ArenaA / ArenaB);
		pos = pos + ArenaCenter;
		Vector2 normal = ArenaCenter - pos;
		Vector2 v = GetComponent<Rigidbody2D> ().velocity;
		v = new Vector2 (v.x, v.y * ArenaA / ArenaB);
		v = Vector2.Reflect (v,normal);
		v = new Vector2 (v.x, v.y * ArenaB / ArenaA);
		v = v.normalized;
		v = v * slideSpeed;
		GetComponent<Rigidbody2D> ().velocity = v;
	}
	if (wall.name == "Tambor"){
		Vector3 wallPos = wall.transform.position;
		Vector3 collisionPos = new Vector3 (wallPos.x+(transform.position.x-wallPos.x)*wall.GetComponent<CircleCollider2D>().radius/(wall.GetComponent<CircleCollider2D>().radius+ circleCollider.GetComponent<CircleCollider2D>().radius),wallPos.y+(transform.position.y-wallPos.y)*wall.GetComponent<CircleCollider2D>().radius/(wall.GetComponent<CircleCollider2D>().radius+circleCollider.GetComponent<CircleCollider2D>().radius),0);
		Vector3 posVector= wallPos - transform.position;
		tambor.GetComponent<TamborScript> ().Raio (posVector.normalized);
		Vector3 newVelocity = new Vector3(GetComponent<Rigidbody2D> ().velocity.x,GetComponent<Rigidbody2D> ().velocity.y,0);
		newVelocity = Vector3.Reflect (newVelocity, -posVector);
//		float angle = Vector3.Angle (GetComponent<Rigidbody2D>().velocity,posVector);
//		angle = Vector3.Angle (new Vector3(1,0,0),-posVector)-angle;
//		Vector2 newVelocity = new Vector2(Mathf.Cos(Mathf.PI/180*angle),Mathf.Sin(Mathf.PI/180*angle));
		newVelocity = newVelocity / newVelocity.magnitude;
		newVelocity = slideSpeed * newVelocity;
		GetComponent<Rigidbody2D> ().velocity = newVelocity;
	}
//	if (transform.position.x > )
}

IEnumerator Stunned(){
	GetComponent<Animator> ().SetTrigger ("Attack_Hurricane_Out");
	GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
	yield return new WaitForSeconds (0);
//	FinishAttack ();
}

public void ContinuePinball(){
	continuePinball = true;
}

IEnumerator PinballAttack()
{
	//slideSpeed = 8f;
	continuePinball=false;
	ability = "pinball";
//	Debug.Log ("Pinball");
	GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
	GetComponent<Animator> ().SetTrigger ("Prepare_Attack_Hurricane");
	while (!continuePinball) {
		yield return new WaitForSeconds (Time.deltaTime);
	}
	continuePinball = false;
//	GetComponent<Animator> ().SetTrigger ("Attack_Hurricane");
	Vector3 playerPos = player.transform.position;
	Vector3 newVelocity = new Vector3(playerPos.x-transform.position.x,playerPos.y-transform.position.y,0);
	newVelocity = slideSpeed*newVelocity/newVelocity.magnitude;

	strikes = 5;

	GetComponent<Rigidbody2D>().velocity = newVelocity;
//	GetComponentInChildren<Animator>().SetTrigger("Fome_ClawAttack");
	bool hit = false;
	while(state == "ability"){
		//strikes--;
		yield return new WaitForSeconds (Time.deltaTime);
		if (/*!hit&&*/player!=null&&hurricaneCollider.GetComponent<HurricaneHitbox>().canHit)
		{
			hit = true;
			StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
		}
		if (strikes <= 0) {
			break;
		}
	}
	StartCoroutine ("Stunned");
	//Stunned ();
}
	
IEnumerator JumpAttack(){
	//Debug.Log ("Jump");
	ability = "jump";
	GetComponentInChildren<Animator>().SetTrigger("Prepare_Attack_Jump");
	GetComponent<Rigidbody2D> ().velocity = new Vector2 (0,0);
	yield return new WaitForSeconds (0.5f);
	GetComponentInChildren<Animator>().SetTrigger("Attack_Jump");
	Vector3 originalPos = transform.position;
	GetComponent<Rigidbody2D> ().velocity = new Vector2 (0,9f);
	yield return new WaitForSeconds (1.25f);
	Vector2 newPos = transform.position;
	Vector2 newDir;
	Vector2 target;
	if (player!=null&&Random.Range (0, 2) == 1) {
		target = player.transform.position - originalPos;
		//target = target * ((target.magnitude)/target.magnitude);
		newDir = target.normalized;
	} else {
		target = tambor.transform.position - originalPos;
		target = target * ((target.magnitude+0.5f)/target.magnitude);
		newDir = target.normalized;
	}
/*		if (newDir.magnitude >= 3.0f) {
		newDir = newDir.normalized;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (newDir.x,newDir.y)*4.8f;
	}
	else if (newDir.magnitude < 1.0f) {
		newDir = newDir.normalized;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (newDir.x,newDir.y)*2.4f;
	}
	else {
		newDir = newDir.normalized;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (newDir.x,newDir.y)*(2.4f+2.4f*(newDir.magnitude-1f)/(3f-1f));
	}
*/
	GetComponent<Rigidbody2D> ().velocity = new Vector2 (newDir.x,newDir.y)*4.8f;
	while ((new Vector2(transform.position.x,transform.position.y) - newPos).magnitude <= target.magnitude) {
		yield return new WaitForSeconds (Time.deltaTime);
	}
	GetComponentInChildren<Animator>().SetTrigger("Attack_Jump_Fall");
	GetComponent<Rigidbody2D> ().velocity = new Vector2 (0,-9f);
	yield return new WaitForSeconds (1.25f);
	GetComponent<Rigidbody2D> ().velocity = new Vector2 (0,0);
	GetComponentInChildren<Animator>().SetTrigger("Attack_Jump_Hit");
}

IEnumerator GrabAttack()
{
	//Debug.Log ("Grab Attack");
	GetComponentInChildren<Animator>().SetTrigger("Fome_GrabAttack");
	while(state == "ability"){
		yield return new WaitForSeconds(0.005f);
		if (player!=null&&canHit)
		{
			StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
		}
		//        state = "movement";
	}
}

IEnumerator GroundAttack()
{
	ability = "groundAttack";
	GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
	GetComponentInChildren<Animator>().SetTrigger("Attack_Ground");
	//	GetComponentInChildren<Animator>().SetTrigger("Fome_GroundAttack");
	while(state == "ability"){
		yield return new WaitForSeconds(0.005f);
	}
}

IEnumerator WaterAttack()
{
	ability = "waterAttack";
	GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
	yield return new WaitForSeconds(0.25f);
	GetComponentInChildren<Animator>().SetTrigger("Attack_Water");
	//	GetComponentInChildren<Animator>().SetTrigger("Fome_GroundAttack");
}

IEnumerator SpitAttack(){
	if (player!=null&&canHit)
	{
		//            StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
	}
	GetComponent<Animator>().SetTrigger("Fome_SpitAttack");
	yield return new WaitForSeconds(0.3f);
	//		GetComponentInChildren<Animator>().SetBool("EarthquakeAttack", false);
	UnityEngine.Object vomitSpawnerObject = Resources.Load ("Fome/" + "VomitSpawner");
	UnityEngine.Object vomitObject= Resources.Load ("Fome/" + "Vomit");
	UnityEngine.Object vomitSpitObject = Resources.Load ("Fome/" + "VomitSpit");
	GameObject vomitSpit = (GameObject) Instantiate (vomitSpitObject,transform.position+new Vector3(-0.45f,1.4f,0f),Quaternion.identity);
	for(int i = 0; i < 3; i++)
	{
		GameObject vomitSpawner = (GameObject)Instantiate(vomitSpawnerObject, new Vector3(Random.Range(spitArea[0].position.x, spitArea[1].position.x), Random.Range(spitArea[0].position.y, spitArea[1].position.y)), Quaternion.identity);
		GameObject vomit = (GameObject) Instantiate (vomitObject,transform.position+new Vector3(3f,6f,0f),Quaternion.identity);
		//			GameObject vomitSpit = (GameObject) Instantiate (vomitSpitObject,transform.position+new Vector3(-0.45f,1.4f,0f),Quaternion.identity);
		vomit.transform.parent = vomitSpawner.transform;
		yield return new WaitForSeconds(0.4f);
	}
	yield return new WaitForSeconds(0.5f);
	state = "movement";
}

IEnumerator BoneAttack(){
	//GetComponent<Animator> ().SetTrigger ("Fome_BoneAttack");
	yield return new WaitForSeconds (0.5f);
	transform.FindChild("Corvos").gameObject.GetComponentInChildren<Animator>().SetTrigger("Crow_BoneAttack");
	yield return new WaitForSeconds (0.3f);
	UnityEngine.Object boneObject1 = Resources.Load ("Fome/" + "Bone1");
	UnityEngine.Object boneObject2 = Resources.Load ("Fome/" + "Bone2");
	UnityEngine.Object boneObject3 = Resources.Load ("Fome/" + "Bone3");
	UnityEngine.Object boneTarget = Resources.Load ("Fome/" + "BoneTarget");
	//		GameObject boneObject = (GameObject)Instantiate (boneObject1, transform.position + new Vector3 (-0.45f, 1.4f, 0f), Quaternion.identity);
	for (int i = 0; i < 8; i++) {
		GameObject boneObject;
		if (i % 3 == 0) {
			boneObject = (GameObject)Instantiate (boneObject1, transform.position + new Vector3 (-0.45f, 1.4f, 0f), Quaternion.identity);
		}
		else if (i % 3 == 1) {
			boneObject = (GameObject)Instantiate (boneObject2, transform.position + new Vector3 (-0.45f, 1.4f, 0f), Quaternion.identity);
		}
		else {
			boneObject = (GameObject)Instantiate (boneObject3, transform.position + new Vector3 (-0.45f, 1.4f, 0f), Quaternion.identity);
		}

		GameObject boneTargetObject = (GameObject)Instantiate (boneTarget, new Vector3(Random.Range(spitArea[0].position.x, spitArea[1].position.x), Random.Range(spitArea[0].position.y, spitArea[1].position.y)), Quaternion.identity);
		boneObject.transform.parent = boneTargetObject.transform;
		//boneObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-3f,2f);
		yield return new WaitForSeconds (0.05f);
	}
	//yield return new WaitForSeconds (0.5f);
	//state = "movement";
}


IEnumerator CrowAttack()
{
	//Debug.Log("Crow Attack");
	GetComponent<Animator>().SetTrigger("Fome_CrowAttack");
	transform.FindChild("Corvos").gameObject.GetComponentInChildren<Animator>().SetTrigger("CrowAttack");
	yield return new WaitForSeconds(0.5f);
	//        shakeDuration = 5f;
	StartCoroutine(SpawnCrow());
	while(state == "ability"){
		yield return new WaitForSeconds(0.005f);
		if (player!=null&&canHit)
		{
			StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
		}
		//        state = "movement";
	}
	yield return new WaitForSeconds(0.25f);
	state = "movement";
}
IEnumerator SpawnCrow()
{
	//		make rock fall into player
	yield return new WaitForSeconds(0.2f);
	UnityEngine.Object crowprefab = Resources.Load ("Fome/" + "SpawnableCrow");
	if (player != null) {
		spawnableCrow = (GameObject)Instantiate (crowprefab, player.transform.position, Quaternion.identity);
	}
	/*		yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < 5; i++)
        {
			if (Random.Range(0f,1f) > 0.3f){
			Instantiate(rockSpawner, new Vector3(Random.Range(spitArea[0].position.x, spitArea[1].position.x), Random.Range(spitArea[0].position.y, spitArea[1].position.y)), new Quaternion(0f, 0f, 0f, 0f));
			}
			else{
				Instantiate(rockSpawner, player.transform.position, new Quaternion(0f, 0f, 0f, 0f));
			}
            yield return new WaitForSeconds(0.5f);
        }
        GetComponentInChildren<Animator>().SetBool("EarthquakeAttack", false);
 */   }

IEnumerator CrowMinion()
{
	//Debug.Log("Crow Attack");
	GetComponent<Animator>().SetTrigger("Fome_SpawnMinion");
	transform.FindChild("Corvos").gameObject.GetComponentInChildren<Animator>().SetTrigger("CrowAttack");
	yield return new WaitForSeconds(0.5f);
	//        shakeDuration = 5f;
	StartCoroutine(SpawnMinion());
	while(state == "ability"){
		yield return new WaitForSeconds(0.005f);
		//			if (player!=null&&canHit)
		//			{
		//				StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
		//			}
		//        state = "movement";
	}
	yield return new WaitForSeconds(0.25f);
	state = "movement";
}

IEnumerator SpawnMinion()
{
	//		make rock fall into player
	yield return new WaitForSeconds (0.25f);
	UnityEngine.Object minionprefab = Resources.Load ("Fome/" + "CrowMinion");
	if (player != null) {
		spawnableCrow = (GameObject)Instantiate (minionprefab, player.transform.position, Quaternion.identity);
	}
}

public void ReceiveDamage(float damage){
//	Debug.Log ("Ouch");
	StartCoroutine ("DamageTime", damage);
}

void Flip()
{
	faceRight = !faceRight;

	Vector3 scale = transform.localScale;
	scale.x *= -1;
	transform.localScale = scale;
}

void OnTriggerEnter2D(Collider2D other){
	//Debug.Log (other.transform.tag);
	if ((/*other.transform.tag == "Cenario"||*/other.name == "Tambor")&&ability=="pinball"){
		Debug.Log ("PinballHit");
		canStrikeAgain = false;
		if (other.name != "Tambor") {
			strikes--;
		}
//		Debug.Log ("Strikes: " + strikes);
		ChangeDirection(other.gameObject);
	}
	if (other.transform.tag == "Player") {
		//canHit = true;
	}
}
void OnTriggerExit2D(Collider2D other){
	if ((other.transform.tag == "Cenario" || other.name == "Tambor") && ability == "pinball") {
		canStrikeAgain = true;
	}
	if (other.transform.tag == "Player") {
		//canHit = false;
	}
}
IEnumerator DamageTime(float attackDamage)
	{
		yield return new WaitForSeconds (0.05f);
		if (name.Equals ("BoneBoss")) {
			//GetComponentInChildren<BoneBossController> ().life -= attackDamage;
			GetComponentInChildren<SpriteRenderer> ().color = Color.red;
			yield return new WaitForSeconds (0.05f);
			GetComponentInChildren<SpriteRenderer> ().color = Color.white;
		} else if (name.Equals ("Fome")) {
			GetComponent<FomeController> ().life -= attackDamage;
			GetComponent<SpriteRenderer> ().color = Color.red;
			GameObject.FindGameObjectWithTag ("Lifebar").GetComponent<Image> ().color = Color.red;
			yield return new WaitForSeconds (0.05f);
			GameObject.FindGameObjectWithTag ("Lifebar").GetComponent<Image> ().color = Color.white;
			GetComponent<SpriteRenderer> ().color = Color.white;
		} else if (name.Equals ("Tempestade")) {
			GetComponent<TempestadeController> ().life -= attackDamage;
			GetComponent<SpriteRenderer> ().color = Color.red;
			GameObject.FindGameObjectWithTag ("Lifebar").GetComponent<Image> ().color = Color.red;
			yield return new WaitForSeconds (0.05f);
			GameObject.FindGameObjectWithTag ("Lifebar").GetComponent<Image> ().color = Color.white;
			GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}
}

