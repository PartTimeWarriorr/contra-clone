using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterController : MonoBehaviour
{
    private Vector2 targetPosition;
    private GameObject player;

    [SerializeField]
    private float shootCooldown = 0.75f;
    private float shootTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            targetPosition = player.transform.position - gameObject.transform.position;
        }

        shootTimer -= Time.deltaTime;
    }

    public Vector2 GetTargetPosition()
    {
        return targetPosition;
    }

    public bool CanShoot()
    {
        return shootTimer <= 0;
    }

    public void Shoot()
    {
        Debug.Log("I'm shootering now!");
        shootTimer = shootCooldown;
    }

    void Die()
    {
        Debug.Log("I'm dead!");
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Die();
        }
    }
}
