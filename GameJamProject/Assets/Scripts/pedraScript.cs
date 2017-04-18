using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedraScript: MonoBehaviour
{
    GameObject player;
    [SerializeField]
    AnimationClip animClip;
    bool alreadyHit;
    GameObject pedraPlace;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pedraPlace = transform.parent.gameObject;
        transform.parent = null;
        transform.localScale = new Vector3(0.280222f, 0.280222f, 0.280222f);
        alreadyHit = false;
    }
    void Update()
    {
        if (gameObject.transform.position.y <= pedraPlace.transform.position.y + 0.5 && !alreadyHit)
        {
            alreadyHit = true;
            StartCoroutine(FloorHit(pedraPlace));

        }
    }

    IEnumerator FloorHit(GameObject col)
    {
        GetComponent<Animator>().SetTrigger("FloorHit");
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
        if (col.GetComponent<pedraPlaceScript>().playerInCircle)
        {
            StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
        }
        yield return new WaitForSeconds(animClip.length);
        Destroy(gameObject);
        Destroy(col);
    }

}
