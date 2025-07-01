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
    SpriteRenderer upperBodySprite;

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
        upperBodySprite = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        bullet = Resources.Load<GameObject>("Prefabs/Bullet");
    }

    public bool IsGrounded()
    {
        return grounded;
    }

    public bool IsSwimming()
    {
        return swimming;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Water"))
        {
            swimming = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Water"))
        {
            swimming = false;
        }
    }

    [SerializeField]
    private WeaponBehavior currWeapon;
    private float timeSinceLastShot;
    public void SetWeapon(WeaponBehavior newWeapon)
    {
        timeSinceLastShot = 0f;
        currWeapon = newWeapon;
    }

    public void Shoot(Vector2 shootDirection, Vector3 playerPosition, Vector3 offset)
    {
        if (currWeapon != null)
            currWeapon.Shoot(this, shootDirection, playerPosition, offset);
        else
            Debug.Log("No weapon equipped!");
    }

    public bool ShouldShoot()
    {
        if (currWeapon == null)
        {
            return false;
        }

        timeSinceLastShot += Time.deltaTime;

        switch (currWeapon.firingMode)
        {
            case FiringMode.SemiAutomatic:
                return ShootWasPressed();
            case FiringMode.Automatic:
                if (ShootIsPressed() && timeSinceLastShot >= currWeapon.fireRate)
                {
                    timeSinceLastShot = 0f;
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                return false;
        }
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
        upperBodySprite.flipX = direction < 0;
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

    public bool ShootIsPressed()
    {
        return inputManager.FindAction("Shoot").IsPressed();
    }

    public bool LookUpIsPressed()
    {
        return inputManager.FindAction("LookUp").IsPressed();
    }

    public bool RunIsPressed()
    {
        return inputManager.FindAction("Run").IsPressed();
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
