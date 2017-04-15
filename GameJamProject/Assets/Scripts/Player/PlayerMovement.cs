using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
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
	PlayerAttack playerAttack;
	PlayerStats playerStats;
    [SerializeField]
    Animator bodyAnim;
    [SerializeField]
    Animator handAnim;
	// Use this for initialization
	void Start (){
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
			speedX = rollDirX * rollSpeed;
			speedY = rollDirY * rollSpeed;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (speedX, speedY);
			rollCurrentTime -= Time.deltaTime;
			if (rollCurrentTime < 0){
				rollCurrentTime = rollTime;
				rollCurrentCooldown = rollCooldown;
				state = "movement";
			}
		}

	}

    void Flip()
    {
        faceRight = !faceRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
