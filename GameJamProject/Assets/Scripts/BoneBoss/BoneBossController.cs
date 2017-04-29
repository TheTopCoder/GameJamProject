using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneBossController : MonoBehaviour
{

    #region Variables
    Vector3 newPosition;

	public GameObject waveAttack;
	public GameObject groundAttack;
    public int life;
    public int maxLife = 5000;
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
    Transform[] rocksArea;
    float minX;
    float minY;
    float maxX;
    float maxY;
    float cooldownMovement;
    float cooldownAbility;
    int prob1;
    int prob1Current;
    int prob2;
    int prob2Current;
    int prob3;
    int prob3Current;
    string state;
    bool moving;
	float arenaX = 1.5f;
	float arenaY = 0.7f;
	bool canHit = false;
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
    GameObject rockSpawner;
    #endregion
    void Start()
    {
        life = maxLife;
        cooldownMovement = 0;
        cooldownAbility = 0;
        prob1 = 1;
        prob2 = 1;
        prob3 = 1;
        prob1Current = prob1;
        prob2Current = prob2;
        prob3Current = prob3;
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
        if (player.transform.position.y < transform.FindChild("point").transform.position.y)
        {
            GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
        }
        if (player.transform.position.x < transform.FindChild("point").transform.position.x && faceRight)
        {
            Flip();
            transform.position -= new Vector3(3,0,0);
        }
        else if(player.transform.position.x > transform.FindChild("point").transform.position.x && !faceRight)
        {
            Flip();
            transform.position += new Vector3(3, 0, 0);
        }
        if (state == "movement")
        {
            if (life <= 0)
            {
				fade = (GameObject) Instantiate (FadeOut, transform.position, new Quaternion(0f,0f,0f,0f));
				fade.GetComponent<FadeTransition>().nextScene = "BeatDemo";
                Destroy(gameObject);
            }
            cooldownMovement -= Time.deltaTime;
            cooldownAbility -= Time.deltaTime;
            if (cooldownAbility <= 0)
            {
                state = "ChooseAbility";
                cooldownAbility = Random.Range(2.25f, 3.25f);
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
            if (moving)
            {
                if (MoveToPosition(newPosition))
                {
                    moving = false;
                }
            }
        }
        else if (state == "ChooseAbility")
        {
            state = "ability";
            ChooseAbility();
        }

    }
    void ChooseAbility()
    {
		if (Vector3.Distance (transform.position, player.transform.position) > 2f) {
			prob1 = 1;
			prob2 = 1;
			prob3 = 2;
		} else {
			prob1 = 1;
			prob2 = 1;
			prob3 = 1;
		}
        int abilityNumber = Random.Range(1, prob1Current + prob2Current + prob3Current + 1);
        if (abilityNumber >= 1 && abilityNumber <= prob1Current)
        {
            StartCoroutine(DashAttack());
            prob1Current = prob1;
            prob2Current = prob2 + 1;
            prob3Current = prob3 + 1;
        }
        else if (abilityNumber >= prob1Current + 1 && abilityNumber <= prob1Current + prob2Current)
        {
			StartCoroutine(WaveAttack());
            prob2Current = prob2;
            prob1Current = prob1 + 1;
            prob3Current = prob3 + 1;
        }
        else if (abilityNumber >= prob1Current + prob2Current + 1 && abilityNumber <= prob1Current + prob2Current + prob3Current)
        {
			StartCoroutine(GroundAttack());
            prob3Current = prob3;
            prob2Current = prob2 + 1;
            prob1Current = prob1 + 1;
        }
    }
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
    IEnumerator DashAttack()
    {
        GetComponentInChildren<Animator>().SetTrigger("WaveAttack2");
        yield return new WaitForSeconds(animClipDown.length / 2);
        if (canHit)
        {
            StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
        }
        state = "movement";
    }
	IEnumerator WaveAttack(){
        GetComponentInChildren<Animator>().SetTrigger("WaveAttack");
        yield return new WaitForSeconds(animClipUp.length / 2);
        if (canHit)
        {
            StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
        }
		wave = (GameObject) Instantiate (waveAttack,transform.position-new Vector3(-1f,0f,0f),Quaternion.Euler(0f,0f,0f));
		yield return new WaitForSeconds(0.5f);
        state = "movement";
    }
    IEnumerator GroundAttack()
    {
        Debug.Log("Ground Attack");
        GetComponentInChildren<Animator>().SetBool("EarthquakeAttack", true);
        yield return new WaitForSeconds(animClip.length / 2);
        shakeDuration = 5f;
        StartCoroutine(SpawnRocks());
        if(canHit)
        {
            StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
        }
		yield return new WaitForSeconds(0.5f);
        state = "movement";
    }
    IEnumerator SpawnRocks()
    {
//		make rock fall into player
		yield return new WaitForSeconds(0.25f);
		Instantiate(rockSpawner, player.transform.position, new Quaternion(0f, 0f, 0f, 0f));
		yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < 5; i++)
        {
			if (Random.Range(0f,1f) > 0.3f){
			Instantiate(rockSpawner, new Vector3(Random.Range(rocksArea[0].position.x, rocksArea[1].position.x), Random.Range(rocksArea[0].position.y, rocksArea[1].position.y)), new Quaternion(0f, 0f, 0f, 0f));
			}
			else{
				Instantiate(rockSpawner, player.transform.position, new Quaternion(0f, 0f, 0f, 0f));
			}
            yield return new WaitForSeconds(0.5f);
        }
        GetComponentInChildren<Animator>().SetBool("EarthquakeAttack", false);
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
			canHit = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			canHit = false;
		}
	}

}
