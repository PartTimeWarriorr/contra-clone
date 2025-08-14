using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject player;
    private float minimalPlayerDistance = 10f;

    private GameObject entityParent;

    public GameObject entity;

    private void Awake()
    {
        player = GameObject.Find("Player");
        entityParent = GameObject.Find("EntityParent");
    }

    void Update()
    {
        float distance = DistanceToPlayer();

        if (distance <= minimalPlayerDistance)
        {
            Spawn();
        }
    }

    float DistanceToPlayer()
    {
        if (player == null)
        {
            return 0;
        }

        return Mathf.Abs(player.transform.position.x - transform.position.x);
    }

    void Spawn()
    {
        GameObject newEntity = Instantiate(entity);
        newEntity.transform.position = transform.position;

        if (entityParent != null)
        {
            newEntity.transform.parent = entityParent.transform;
        }

        Destroy(gameObject);
    }
}
