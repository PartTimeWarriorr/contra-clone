using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currHealth = 0;

    // Enemies always deal 1 damage
    private int enemyDamage = 1;

    private float iFrames = 3f;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D box;
    private bool invincible = false;

    public static event Action<int> OnHealthChanged;
    public static event Action OnDied;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        currHealth = maxHealth;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            TakeDamage();
        }

        if (other.collider.CompareTag("Pit"))
        {
            TakeFallDamage();
            RecoverFromFall(transform.position);
        }
    }

    private float recoverOffsetX = 5f;
    private float recoverOffsetY = 15f;

    void RecoverFromFall(Vector3 pos)
    {
        Vector3 recoverPosition = new Vector3(pos.x - recoverOffsetX, pos.y + recoverOffsetY, pos.z);
        transform.SetPositionAndRotation(recoverPosition, Quaternion.identity);
    }

    void TakeFallDamage()
    {
        currHealth -= enemyDamage;
        OnHealthChanged?.Invoke(currHealth);

        if (currHealth <= 0)
        {
            Die();
        }

        // Stop already started coroutine to not end iFrames early
        StopCoroutine(nameof(InvincibilityFrames));
        StartCoroutine(InvincibilityFrames());
    }

    void TakeDamage()
    {
        if (invincible)
            return;

        currHealth -= enemyDamage;
        OnHealthChanged?.Invoke(currHealth);

        if (currHealth <= 0)
        {
            Die();
        }

        StartCoroutine(InvincibilityFrames());
    }

    void Die()
    {
        // Visual representation
        // Reset level
        OnDied?.Invoke();
    }

    IEnumerator InvincibilityFrames()
    {
        spriteRenderer.color = Color.red;
        invincible = true;
        // Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer(""), true);
        yield return new WaitForSeconds(iFrames);
        // Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer(""), false);
        invincible = false;
        spriteRenderer.color = Color.white;
    }
}
