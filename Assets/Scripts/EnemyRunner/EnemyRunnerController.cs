using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunnerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float direction = -1f;

    [SerializeField]
    private float speed = 10f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Move()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    public void Die()
    {
        Debug.Log("I died!");
        Destroy(gameObject);
    }

    public bool hitByBullet = false;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            hitByBullet = true;
            Destroy(collision.collider.gameObject);
        }
    }
}
