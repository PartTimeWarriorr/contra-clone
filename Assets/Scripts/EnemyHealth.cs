using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 10;
    public float currHealth = 0f;

    // All player bullets deal 1 damage..
    private float bulletDamage = 1f;

    void Start()
    {
        currHealth = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage();
            Debug.Log(currHealth);
        }
    }

    void TakeDamage()
    {
        currHealth -= bulletDamage;

        if (currHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
