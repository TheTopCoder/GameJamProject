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
    void Start()
    {
        Instantiate(rocha, new Vector3(this.transform.position.x, 5.21f), new Quaternion(0f, 0f, 0f, 0f), gameObject.transform);
        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
    }

    
}
