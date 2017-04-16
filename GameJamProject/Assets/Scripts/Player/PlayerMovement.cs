using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
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
	string state;
	float speed;
	float rollSpeed;
	float rollTime;
	float rollCurrentTime;
	float rollCooldown;
	float rollCurrentCooldown;
    bool faceRight;
    bool damaging = true;
	PlayerAttack playerAttack;
	PlayerStats playerStats;
    [SerializeField]
    Animator bodyAnim;
    [SerializeField]
    Animator handAnim;
    [SerializeField]
    GameObject smearPrefab;
	// Use this for initialization
	void Start (){
        life = maxLife;
		playerAttack = GetComponent<PlayerAttack> ();
		playerStats = GetComponent<PlayerStats> ();
		speed = playerStats.speed;
		rollSpeed = playerStats.rollSpeed;
		rollTime = playerStats.rollTime;
		rollCooldown = playerStats.rollCooldown;
		state = "movement";
		rollCurrentTime = rollTime;
		rollCurrentCooldown = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if (playerAttack.state == "attackStrong") {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		}
		else if (state == "movement") {
			dirX = Input.GetAxis ("Horizontal");
			dirY = Input.GetAxis ("Vertical");

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
            }
            else
            {
                handAnim.SetBool("Walking", true);
                bodyAnim.SetBool("Walking", true);
            }
			float dirAbs = Mathf.Sqrt (dirX * dirX + dirY * dirY);
			if (dirAbs != 0) {
				dirX = dirX / dirAbs;
				dirY = dirY / dirAbs;
			}
			speedX = dirX * speed;
			speedY = dirY * speed;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (speedX, speedY);
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
			if (Input.GetAxisRaw("XboxR1")>0 && rollCurrentCooldown < 0) {
				state = "roll";
			}


		} else if (state == "roll") {
            handAnim.SetBool("Dash", true);
            bodyAnim.SetBool("Dash", true);
			speedX = rollDirX * rollSpeed;
			speedY = rollDirY * rollSpeed;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (speedX, speedY);
			rollCurrentTime -= Time.deltaTime;
			if (rollCurrentTime < 0){
                handAnim.SetBool("Dash", false);
                bodyAnim.SetBool("Dash", false);
                rollCurrentTime = rollTime;
				rollCurrentCooldown = rollCooldown;
				state = "movement";
			}
		}

	}

    public IEnumerator DamagedPlayer()
    {
        if (damaging)
        {
            handAnim.SetBool("Flint", true);
            bodyAnim.SetBool("Flint", true);
            damaging = false;
            life--;
            Color auxColor = new Color(bodyAnim.gameObject.GetComponent<SpriteRenderer>().color.r, bodyAnim.gameObject.GetComponent<SpriteRenderer>().color.g, bodyAnim.gameObject.GetComponent<SpriteRenderer>().color.b, 0);
            StartCoroutine(KnockBack());
            for (int i = 0; i < 7; i++)
            {
                if (handAnim.gameObject.GetComponent<SpriteRenderer>().color != auxColor)
                {
                    handAnim.gameObject.GetComponent<SpriteRenderer>().color = auxColor;
                    bodyAnim.gameObject.GetComponent<SpriteRenderer>().color = auxColor;
                }
                else
                {
                    handAnim.gameObject.GetComponent<SpriteRenderer>().color = new Color(handAnim.gameObject.GetComponent<SpriteRenderer>().color.r, handAnim.gameObject.GetComponent<SpriteRenderer>().color.g, handAnim.gameObject.GetComponent<SpriteRenderer>().color.b, 1f);
                    bodyAnim.gameObject.GetComponent<SpriteRenderer>().color = new Color(bodyAnim.gameObject.GetComponent<SpriteRenderer>().color.r, bodyAnim.gameObject.GetComponent<SpriteRenderer>().color.g, bodyAnim.gameObject.GetComponent<SpriteRenderer>().color.b, 1f);
                }
                yield return new WaitForSeconds(0.1f);
            }
            handAnim.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
            bodyAnim.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
            damaging = true;
        }
    }
    IEnumerator KnockBack()
    {
        for (int i = 0; i < 5; i++)
        {
            if (faceRight)
            {
                transform.position -= new Vector3(0.1f, 0f);
            }
            else
            {
                transform.position += new Vector3(0.1f, 0f);
            }
            yield return new WaitForSeconds(0.05f);
        }

        handAnim.SetBool("Flint", false);
        bodyAnim.SetBool("Flint", false);
    }
    void Flip()
    {
        faceRight = !faceRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
