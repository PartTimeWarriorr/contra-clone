using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCannon : MonoBehaviour
{
    public GameObject cannonBall;

    private GameObject bulletHolder;

    private float shootForce = 350;

    public float shootCooldown = 1.5f;
    private float shootTimer = 0f;

    void Awake()
    {
        bulletHolder = GameObject.Find("Bullets");
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            shootTimer = shootCooldown;
            Shoot();
        }
    }

    float GetHorizontalForce()
    {
        return UnityEngine.Random.Range(-4, -1);
    }

    void Shoot()
    {
        GameObject newCannonBall = Instantiate(cannonBall);

        float horizontalForce = GetHorizontalForce();
        newCannonBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontalForce * shootForce, 0));
        newCannonBall.GetComponent<CannonBall>().cannonPosition = gameObject.transform.position;
        newCannonBall.transform.position = transform.position;

        if (bulletHolder)
        {
            newCannonBall.transform.parent = bulletHolder.transform;
        }
    }

    void DestroyCannon()
    {
        Destroy(gameObject);
    }

    void OnEnable()
    {
        BossHeart.OnBossHeartDestroyed += DestroyCannon;
    }
    
    void OnDisable()
    {
        BossHeart.OnBossHeartDestroyed -= DestroyCannon;
    }
}
