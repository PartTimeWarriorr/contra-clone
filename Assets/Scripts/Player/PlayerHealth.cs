using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currHealth = 0;

    // Enemies always deal 1 damage
    public int enemyDamage = 1;

    private float iFrames = 3f;

    private List<SpriteRenderer> renderers;
    private BoxCollider2D box;
    private bool invincible = false;

    public static event Action<int> OnHealthChanged;
    public static event Action OnDied;

    void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>().ToList();
        box = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        currHealth = maxHealth;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // if (other.collider.CompareTag("Enemy"))
        // {
        //     TakeDamage();
        // }
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyTouch") ||
            collision.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
        {
            TakeDamage();
        }

        if (collision.collider.CompareTag("Pit"))
        {
            TakeFallDamage();
            RecoverFromFall(transform.position);
        }
    }

    private float recoverOffsetX = 0f;
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
        AudioManager.PlayPlayerDamage();

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
        AudioManager.PlayPlayerDamage();

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
        ChangeColors(Color.red);
        invincible = true;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyTouch"), true);
        yield return new WaitForSeconds(iFrames);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyTouch"), false);
        invincible = false;
        ChangeColors(Color.white);
    }

    void ChangeColors(Color color)
    {
        foreach (var r in renderers)
        {
            r.color = color;
        }
    }
}
