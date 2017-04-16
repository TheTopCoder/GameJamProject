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
    public int maxLife = 500;
	int dashAttackDamage = 10;
	float dashSpeed = 1.5f;
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
    }
    void Update()
    {
        if(player.transform.position.y < transform.FindChild("point").transform.position.y)
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
                Destroy(gameObject);
            }
            cooldownMovement -= Time.deltaTime;
            cooldownAbility -= Time.deltaTime;
            if (cooldownAbility <= 0)
            {
                state = "ChooseAbility";
                cooldownAbility = Random.Range(3, 5);
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
        wave = (GameObject) Instantiate (waveAttack,transform.position,Quaternion.Euler(0f,0f,0f));
		yield return new WaitForSeconds(0.5f);
        state = "movement";
    }
    IEnumerator GroundAttack()
    {
        Debug.Log("Ground Attack");
        GetComponentInChildren<Animator>().SetBool("EarthquakeAttack", true);
        yield return new WaitForSeconds(animClip.length / 2);
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
        for(int i = 0; i < 10; i++)
        {
            Instantiate(rockSpawner, new Vector3(Random.Range(rocksArea[0].position.x, rocksArea[1].position.x), Random.Range(rocksArea[0].position.y, rocksArea[1].position.y)), new Quaternion(0f, 0f, 0f, 0f));
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
