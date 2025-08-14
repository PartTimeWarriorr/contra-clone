using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    private Transform playerTransform;

    const float xOffset = 8f;
    const float zIndex = -10;
    const float yIndex = -2f;
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
            transform.position = new Vector3(playerTransform.position.x + xOffset, yIndex, zIndex);
        }
    }
}
