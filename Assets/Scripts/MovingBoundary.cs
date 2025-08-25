using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBoundary : MonoBehaviour
{
    public Camera mainCam;
    private float distance;

    void Start()
    {
        // if (mainCam != null)
        // {
        //     distance = mainCam.transform.position.x - transform.position.x; 
        // }
        float worldHeight = 2f * mainCam.orthographicSize;
        float worldWidth = worldHeight * mainCam.aspect;
        distance = worldWidth / 2;
        Debug.Log(distance);
    }

    void Update()
    {
        float newX = Mathf.Max(mainCam.transform.position.x - distance, transform.position.x);
        
        Vector3 newPosition = new Vector3(
            newX,
            transform.position.y,
            transform.position.z
        );

        transform.position = newPosition;
    }
}
