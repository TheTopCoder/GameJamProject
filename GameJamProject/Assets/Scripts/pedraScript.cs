using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedraScript: MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    AnimationClip animClip;
    void Start()
    {
        transform.localScale = new Vector3(0.0571315f, 0.1724727f, 0.3142073f);
    }
    void Update()
    {
        if (gameObject.transform.position.y <= transform.parent.position.y + 0.5)
        {
            StartCoroutine(FloorHit(transform.parent.gameObject));
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
