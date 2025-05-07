using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputManager inputManager;
    Rigidbody2D rb;
    SpriteRenderer sprite;

    private float direction = 0f;

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float jumpForce = 20f;

    private bool grounded = true;
    private bool swimming = false;

    private GameObject bullet = null;


    void Awake()
    {
        inputManager = new InputManager();

        inputManager.Player.Run.performed += context => direction = context.ReadValue<float>();
        inputManager.Player.Run.canceled += context => direction = 0;

        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();

        bullet = Resources.Load<GameObject>("Prefabs/Bullet");
    }

    public bool IsGrounded()
    {
        return grounded;
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.collider.CompareTag("Ground"))
    //     {
    //         grounded = true; 
    //     }
    //     else if(collision.collider.CompareTag("Water"))
    //     {
    //         swimming = true;
    //     }
    // }

    // ??
    // private ScriptableObject weapon;
    float bulletVelocity = 6f;

    public void Shoot(Vector2 shootDirection, Vector3 playerPosition, Vector3 offset)
    {
        int facingDirection = FacingDirection();

        Vector2 shootVelocity = new Vector2(shootDirection.x * facingDirection, shootDirection.y) * bulletVelocity;
        Vector3 startingPosition = new Vector3(playerPosition.x + facingDirection * offset.x, playerPosition.y + offset.y, playerPosition.z);

        GameObject newBullet = Instantiate(bullet);
        newBullet.GetComponent<BulletLogic>().SetParameters(shootVelocity, startingPosition);
        newBullet.transform.SetPositionAndRotation(startingPosition + new Vector3(0,0,-2), Quaternion.identity);
        newBullet.transform.parent = GameObject.Find("BulletHolder").transform;
    }

    public int FacingDirection()
    {
        if (sprite.flipX)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    public void Jump()
    {
        grounded = false;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void Move()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    public float GetDirection()
    {
        return direction;
    }

    public void FlipSprite()
    {
        // If player is moving to the left, the sprite is flipped to face left
        sprite.flipX = direction < 0;
    }

    public bool RunWasPressed()
    {
        return inputManager.FindAction("Run").WasPerformedThisFrame();
    }

    public bool LookUpWasPressed()
    {
        return inputManager.FindAction("LookUp").WasPerformedThisFrame();
    }

    public bool LookUpWasReleased()
    {
        return inputManager.FindAction("LookUp").WasReleasedThisFrame();
    }

    public bool LookDownWasPressed()
    {
        return inputManager.FindAction("LookDown").WasPerformedThisFrame();
    }

    public bool LookDownWasReleased()
    {
        return inputManager.FindAction("LookDown").WasReleasedThisFrame();
    }

    public bool JumpWasPressed()
    {
        return inputManager.FindAction("Jump").WasPressedThisFrame();
    }

    public bool ShootWasPressed()
    {
        return inputManager.FindAction("Shoot").WasPressedThisFrame();
    }

    private void OnEnable()
    {
        inputManager.Enable();
    }

    private void OnDisable()
    {
        inputManager.Disable();
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(transform.position + new Vector3(0,-0.9f,0), 0.3f);
    // }
}
