using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedraPlaceScript : MonoBehaviour
{
    [SerializeField]
    GameObject rocha;
    [SerializeField]
    GameObject player;
    [HideInInspector]
    public bool playerInCircle;
    float maxDist;
    float distance;
    GameObject rochaGameObject;
    Vector3 maxScale;
    Vector3 minScale;
    void Start()
    {
        rochaGameObject = (GameObject)Instantiate(rocha, new Vector3(this.transform.position.x, 5.21f), new Quaternion(0f, 0f, 0f, 0f), gameObject.transform);
        maxDist = 5.21f - transform.position.y;
        maxScale = new Vector3(2.009045f, 0.6654951f, 0.2722615f);
        minScale = maxScale/ 3;
        transform.localScale = minScale;
        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
    }
    void Update()
    {

        distance = rochaGameObject.transform.position.y - transform.position.y;
        transform.localScale = (((maxDist - distance) * (maxScale - minScale)) / maxDist) + minScale;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag.Equals("Player"))
        {
            playerInCircle = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            playerInCircle = false;
        }
    }

}
