using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    private Transform playerTransform;

    const float zIndex = -10;
    const float yLowerLimit = -4.6f;
    // TODO
    // lower limit should be a world boundary - not camera restriction
    // float xLowerLimit = 0f;

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
            // float x = Mathf.Max(playerTransform.position.x, xLowerLimit);
            float y = Mathf.Max(playerTransform.position.y, yLowerLimit);
            transform.position = new Vector3(playerTransform.position.x, y, zIndex);

            // xLowerLimit = transform.position.x;
        }
    }
}
