using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRunnerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float direction = -1f;

    private float rayLength = 2f;

    [SerializeField]
    private float speed = 10f;
    private float jumpForce = 20f;

    private bool grounded = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Move()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    public bool ShouldJump()
    {
        Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, LayerMask.GetMask("Platform"));

        return hit.collider == null && grounded;
    }

    public void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Platform"))
        {
            grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Platform"))
        {
            grounded = false;
        }
    }

    // public void Die()
    // {
    //     Debug.Log("I died!");
    //     Destroy(gameObject);
    // }

    // public bool hitByBullet = false;

    // public void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.collider.CompareTag("Bullet"))
    //     {
    //         hitByBullet = true;
    //         Destroy(collision.collider.gameObject);
    //     }
    // }
}
