using System;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer spriteRenderer;

    public List<Sprite> sprites;

    public GameObject bullet;

    private void Awake()
    {
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        spriteRenderer.sprite = sprites[0];
    }

    public float turnDelay = 0.1f; 
    private float turnTimer;

    private int currentDirIndex = 0;

    // Get vector to player and calculate angle through atan
    float GetAngleToPlayer()
    {
        Vector2 dirToTarget = player.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(dirToTarget.y, dirToTarget.x) * Mathf.Rad2Deg;

        if (targetAngle < 0)
        {
            targetAngle += 360f;
        }

        return targetAngle;
    }

    // Get index of rotation towards player
    int GetRotationIndex(float targetAngle)
    {
        return Mathf.RoundToInt(targetAngle / 30f) % 12;
    }

    // Rotate towards player, update currentDirIndex
    void Rotate(int targetDirIndex)
    {
        if (turnTimer <= 0f)
        {
            turnTimer = turnDelay;

            if (currentDirIndex != targetDirIndex)
            {
                // Turn clockwise or counterclockwise, depending on which is faster
                int clockwiseSteps = (targetDirIndex - currentDirIndex + 12) % 12;
                int counterClockwiseSteps = (currentDirIndex - targetDirIndex + 12) % 12;

                if (clockwiseSteps <= counterClockwiseSteps)
                    currentDirIndex = (currentDirIndex + 1) % 12;
                else
                    currentDirIndex = (currentDirIndex - 1 + 12) % 12;
            }
        }
    }

    // Update rotated sprite
    void UpdateSprite()
    {
        spriteRenderer.sprite = sprites[currentDirIndex];
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        float targetAngle = GetAngleToPlayer();
        int targetDirIndex = GetRotationIndex(targetAngle);

        turnTimer -= Time.deltaTime;
        Rotate(targetDirIndex);
        UpdateSprite();

        shootTimer -= Time.deltaTime;
        Shoot(targetDirIndex);
    }

    public float shootCooldown = 2f;
    private float shootTimer = 0f;

    void Shoot(int targetDirIndex)
    {
        if (shootTimer <= 0f)
        {
            shootTimer = shootCooldown;
            if (currentDirIndex == targetDirIndex)
            {
                SpawnBullet(targetDirIndex);
            }
        }
    }

    void SpawnBullet(int targetDirIndex)
    {
        float targetAngleDegrees = targetDirIndex * 30f;
        Vector2 bulletVelocity = new Vector2(Mathf.Cos(targetAngleDegrees * Mathf.Deg2Rad), Mathf.Sin(targetAngleDegrees * Mathf.Deg2Rad));

        GameObject newBullet = Instantiate(bullet);
        newBullet.GetComponent<EnemyBulletLogic>().SetParameters(bulletVelocity, player.transform.position);
        newBullet.transform.position = transform.position;
        newBullet.transform.parent = GameObject.Find("BulletHolder").transform;
    }
}

