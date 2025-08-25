using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTurret : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer spriteRenderer;

    public List<Sprite> sprites;
    public GameObject bullet;

    void Awake()
    {
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
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

        targetAngle += 180f;

        if (targetAngle < 0)
        {
            targetAngle += 360f;
        }

        return targetAngle;
    }

    // Get index of rotation towards player
    int GetRotationIndex(float targetAngle)
    {
        return Mathf.RoundToInt(targetAngle / 30f) % 3;
    }

    bool CanShoot()
    {
        if (player == null)
        {
            return false;
        }

        return player.transform.position.x <= transform.position.x && player.transform.position.y >= transform.position.y;
    }

    void UpdateSprite()
    {
        spriteRenderer.sprite = sprites[currentDirIndex];
    }

    void Rotate(int targetDirIndex)
    {
        if (turnTimer <= 0f)
        {
            turnTimer = turnDelay;

            if (currentDirIndex < targetDirIndex)
            {
                currentDirIndex += 1;
            }
            else if (currentDirIndex > targetDirIndex)
            {
                currentDirIndex -= 1;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CanShoot())
        {
            float targetAngle = GetAngleToPlayer();
            int targetDirIndex = GetRotationIndex(targetAngle);

            turnTimer -= Time.deltaTime;
            Rotate(targetDirIndex);
            UpdateSprite();

            shootTimer -= Time.deltaTime;
            Shoot(targetDirIndex);
        }
    }

    public float shootCooldown = 1f;
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
        float targetAngleDegrees = targetDirIndex * (-30f) + 180f;
        Vector2 bulletVelocity = new Vector2(Mathf.Cos(targetAngleDegrees * Mathf.Deg2Rad), Mathf.Sin(targetAngleDegrees * Mathf.Deg2Rad));

        GameObject newBullet = Instantiate(bullet);
        newBullet.GetComponent<EnemyBulletLogic>().SetParameters(bulletVelocity, player.transform.position);
        newBullet.transform.position = transform.position;
        newBullet.transform.parent = GameObject.Find("BulletHolder").transform;
    }
}
