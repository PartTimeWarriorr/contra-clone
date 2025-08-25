using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    private Transform playerTransform;

    const float xOffset = 8f;
    const float zIndex = -10;
    const float yIndex = -2f;

    public GameObject bossFight;
    float bossFightX;
    const float bossFightOffset = 10f;

    float maxX = 0f;

    void Start()
    {
        if (bossFight != null)
        {
            bossFightX = bossFight.transform.position.x;
        }

        if (player != null)
        {
            playerTransform = player.transform;
        }

        maxX = player.transform.position.x + xOffset;
    }

    void Update()
    {
        if (player != null)
        {
            maxX = Mathf.Max(maxX, playerTransform.transform.position.x + xOffset);
            maxX = Mathf.Min(maxX, bossFightX - bossFightOffset);
            transform.position = new Vector3(maxX, yIndex, zIndex);
        }
    }
}
