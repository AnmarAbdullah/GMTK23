using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform playerPosition;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

        transform.position = new Vector3(playerPosition.position.x,
            playerPosition.position.y,
            transform.position.z);
    }
}
