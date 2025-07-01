using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    Vector2 bulletVelocity;
    private Vector3 playerPosition;

    private Rigidbody2D rb;

    private float bulletVanishThreshhold = 20f;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (Mathf.Abs((transform.position - playerPosition).magnitude) > bulletVanishThreshhold)
        {
            Destroy(gameObject);
        }

        rb.velocity = bulletVelocity;
    }

    public void SetParameters(Vector2 _bulletVelocity, Vector3 _playerPosition)
    {
        bulletVelocity = _bulletVelocity;
        playerPosition = _playerPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }

        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
