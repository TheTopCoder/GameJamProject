using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;
    public Transform playerPosition;
    public GameObject[] borders = new GameObject[2];
    
	void Update () {
        UpdateCamPos();
	}

    void UpdateCamPos()
    {
        if (playerPosition.position.x > borders[1].transform.position.x && playerPosition.position.x < borders[0].transform.position.x)
        {
            this.transform.position = new Vector3(playerPosition.position.x + offset.x, offset.y, -14f);
        }
        else if (playerPosition.position.x < borders[1].transform.position.x)
        {
            this.transform.position = new Vector3(borders[1].transform.position.x, offset.y, -14f);
        }
        else if (playerPosition.position.x > borders[0].transform.position.x)
        {
            this.transform.position = new Vector3(borders[0].transform.position.x, offset.y, -14f);
        }
    }
}
