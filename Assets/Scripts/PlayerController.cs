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

    void Awake()
    {
        inputManager = new InputManager();

        inputManager.Player.Run.performed += context => direction = context.ReadValue<float>();
        inputManager.Player.Run.canceled += context => direction = 0;

        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public bool IsGrounded()
    {
        // return rb.velocity.y == 0;
        return grounded;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true; 
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

    private void OnEnable()
    {
        inputManager.Enable();
    }

    private void OnDisable()
    {
        inputManager.Disable();
    }
}
