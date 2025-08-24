using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletLogic : MonoBehaviour
{
    Vector2 bulletVelocity;
    private Vector3 shooterPosition;

    private float speed = 4f;
    private float bulletVanishThreshhold = 20f;

    private Rigidbody2D rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (Mathf.Abs((transform.position - shooterPosition).magnitude) > bulletVanishThreshhold)
        {
            Destroy(gameObject);
        }
        rb.velocity = bulletVelocity * speed;
    }

    public void SetParameters(Vector2 _bulletVelocity, Vector3 _shooterPosition)
    {
        bulletVelocity = _bulletVelocity;
        shooterPosition = _shooterPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
