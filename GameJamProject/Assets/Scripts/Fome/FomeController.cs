using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FomeController : MonoBehaviour
{

    #region Variables
    Vector3 newPosition;

	public GameObject waveAttack;
	public GameObject groundAttack;
    public int life;
    public int maxLife = 300;
	int dashAttackDamage = 10;
	float dashSpeed = 1.5f;
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
    GameObject wave;
    [SerializeField]
	GameObject spawnableCrow;
	GameObject attackHitbox;
    #endregion
    void Start()
    {
        life = maxLife;
        cooldownMovement = 0;
        cooldownAbility = 2f;
		for (int i = 0; i <= 6; i++) {
			attackProb.Add (1);
			attackProbUp.Add (1);
			attackCurProb.Add(1);
		}
/*        prob1 = 5;
        prob2 = 15;
        prob3 = 5;
        prob1Current = prob1;
        prob2Current = prob2;
        prob3Current = prob3;
*/		attackCount = 0;
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
    }
    void Update()
    {
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
            GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
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
			//Chefe morrer
            if (life <= 0)
            {
				fade = (GameObject) Instantiate (FadeOut, transform.position, new Quaternion(0f,0f,0f,0f));
				fade.GetComponent<FadeTransition>().nextScene = "BeatDemo";
                Destroy(gameObject);
            }

            cooldownMovement -= Time.deltaTime;
            cooldownAbility -= Time.deltaTime;
			//Atacar
            if (cooldownAbility <= 0)
            {
				//Debug.Log ("cooldownAbility < 0");
                state = "ChooseAbility";
                cooldownAbility = Random.Range(1.75f, 3.0f);
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
		if (state == "ChooseAbility")
        {
            state = "ability";
            ChooseAbility();
        }
	}
    }
	

    void ChooseAbility()
    {
		Debug.Log ("Choose Ability");
		attackCount = 0;
		attackLimit = 1;
		//Tornar as habilidades mais provaveis a medida que nao sao usadas
		//Dependendo da posicao/distancia do jogador, a probabilidade de cada ataque é diferente
		if (player!=null&&Vector3.Distance (transform.position, player.transform.position) > 2.5f) {
			attackProb [1] = 0;
			attackProbUp [1] = 0;
			attackProb [2] = 1;
			attackProbUp [2] = 1;
			attackProb [3] = 4;
			attackProbUp [3] = 1;
			attackProb [4] = 0;
			attackProbUp [4] = 0;
			if (life <= maxLife * 1 / 2) {
				attackProb [5] = 2;
				attackProbUp [5] = 1;
			} else {
				attackProb [5] = 0;
				attackProbUp [5] = 0;
			}
			if (life <= maxLife * 2 / 3) {
				attackProb [6] = 5;
				attackProbUp [6] = 1;
			} else {
				attackProb [6] = 0;
				attackProbUp [6] = 0;
			}

			if (abilityState != 1) {
				abilityState = 1;
				for (int i = 1; i <= 6; i++) {
					attackCurProb [i] = attackProb[i];
				}
			}
		} else {
			attackProb [1] = 3;
			attackProbUp [1] = 1;
			attackProb [2] = 1;
			attackProbUp [2] = 0;
			attackProb [3] = 2;
			attackProbUp [3] = 1;
			attackProb [4] = 4;
			attackProbUp [4] = 1;
			if (life <= maxLife * 1 / 2) {
				attackProb [5] = 2;
				attackProbUp [5] = 1;
			} else {
				attackProb [5] = 0;
				attackProbUp [5] = 0;
			}
			if (life <= maxLife * 2 / 3) {
				attackProb [6] = 3;
				attackProbUp [6] = 1;
			} else {
				attackProb [6] = 0;
				attackProbUp [6] = 0;
			}
			if (abilityState != 2) {
				abilityState = 2;
				for (int i = 1; i <= 6; i++) {
					attackCurProb [i] = attackProb [i];
				}
			}
		}
		int sumProb = 0;
		int sumCurProb = 0;
		int curAbility = 1;
		int thatAttackProb = 0;
		for (int i = 1; i <= 6; i++) {
			sumProb += attackCurProb [i];
		}
		float abilityNumber = Random.Range(0, sumProb);
		for (int i = 1; i <= 6; i++) {
			attackCurProb [i]+= attackProbUp[i];
		}
		thatAttackProb = attackCurProb [curAbility] - attackProbUp[curAbility];
		if (abilityNumber >= sumCurProb && abilityNumber < sumCurProb + thatAttackProb)
        {
            StartCoroutine(ClawAttack());
			attackCurProb [curAbility] = attackProb[curAbility];
        }
		sumCurProb += thatAttackProb;
		curAbility++;
		thatAttackProb = attackCurProb [curAbility] - attackProbUp[curAbility];
		if (abilityNumber >= sumCurProb && abilityNumber < sumCurProb + thatAttackProb)		{
			StartCoroutine(SpitAttack());
			attackCurProb [curAbility] = attackProb[curAbility];
		}
		sumCurProb += thatAttackProb;
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
		curAbility++;
		thatAttackProb = attackCurProb [curAbility] - attackProbUp[curAbility];
		if (abilityNumber >= sumCurProb && abilityNumber < sumCurProb + thatAttackProb)		{
			StartCoroutine(GroundAttack());
			attackCurProb [curAbility] = attackProb[curAbility];
		}
		sumCurProb += thatAttackProb;
		curAbility++;
		thatAttackProb = attackCurProb [curAbility] - attackProbUp[curAbility];
		if (abilityNumber >= sumCurProb && abilityNumber < sumCurProb + thatAttackProb)
		{
			StartCoroutine(CrowMinion());
			attackCurProb [curAbility] = attackProb[curAbility];
		}
		sumCurProb += thatAttackProb;
		curAbility++;
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
		attackCount++;
		//Debug.Log ("Finished Attack");
		if (attackCount >= attackLimit) {
			state = "movement";
			Destroy (attackHitbox);
			GetComponentInChildren<Animator>().SetTrigger("Fome_Idle");
		}
	}

void CreateHitbox(string name){
	UnityEngine.Object hitboxprefab = Resources.Load ("Fome/" + name);
	attackHitbox = (GameObject) Instantiate (hitboxprefab, transform.position, Quaternion.identity);
}
void DestroyHitbox(){
	if (attackHitbox != null) {
		Destroy (attackHitbox);
	}
}

//Ataques
    IEnumerator ClawAttack()
    {
		//Debug.Log ("Claw Attack");
        GetComponentInChildren<Animator>().SetTrigger("Fome_ClawAttack");
		while(state == "ability"){
			yield return new WaitForSeconds(0.005f);
	        if (player!=null&&canHit)
	        {
    	        StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
        	}
//        state = "movement";
		}
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
		attackLimit = 5;
		//Debug.Log ("Ground Attack");
		GetComponentInChildren<Animator>().SetTrigger("Fome_PrepareGroundAttack");
		yield return new WaitForSeconds(0.75f);
		GetComponentInChildren<Animator>().SetTrigger("Fome_GroundAttack");
		UnityEngine.Object shockwave = Resources.Load ("Fome/Fome_Shockwave");
		Instantiate (shockwave, new Vector3(transform.position.x-1.0f,transform.position.y-1f,0f), Quaternion.identity);
		while(state == "ability"){
			yield return new WaitForSeconds(0.005f);
			if (player!=null&&canHit)
			{
				StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
			}
		}
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
		yield return new WaitForSeconds(0.25f);
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

	public void ReceiveDamage(int damage){
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
		if (other.tag == "Player") {
			//canHit = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			//canHit = false;
		}
	}
	IEnumerator DamageTime(int attackDamage)
	{
		yield return new WaitForSeconds(0.05f);
		if (name.Equals ("BoneBoss")) {
			GetComponentInChildren<BoneBossController> ().life -= attackDamage;
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
		} else if (name.Equals ("FireBoss")) {
			//            GetComponent<FireBossController>().life -= attackDamage;
		}
	}
}
