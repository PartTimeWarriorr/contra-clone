using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    private Transform playerTransform;

    const float zIndex = -10;

    void Start()
    {
        if (player != null)
        {
            playerTransform = player.transform; 
        }
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, zIndex);
        }
    }
}
