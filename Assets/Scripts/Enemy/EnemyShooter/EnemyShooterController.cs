using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterController : MonoBehaviour
{
    private Vector2 targetPosition;
    private GameObject player;

    [SerializeField]
    private float shootCooldown = 1.5f;
    private float shootTimer = 0f;

    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            targetPosition = player.transform.position - gameObject.transform.position;
        }

        shootTimer -= Time.deltaTime;
    }

    public Vector2 GetTargetPosition()
    {
        return targetPosition;
    }

    public bool CanShoot()
    {
        return shootTimer <= 0;
    }

    public void Shoot(float shootAngle)
    {
        if (player == null)
        {
            return;
        }

        shootTimer = shootCooldown;

        Vector2 shootVelocity = new Vector2(Mathf.Cos(shootAngle * Mathf.Deg2Rad), Mathf.Sin(shootAngle * Mathf.Deg2Rad));

        GameObject newBullet = Instantiate(bullet);
        newBullet.GetComponent<EnemyBulletLogic>().SetParameters(shootVelocity, transform.position);
        newBullet.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        newBullet.transform.parent = GameObject.Find("BulletHolder").transform;
    }
}
