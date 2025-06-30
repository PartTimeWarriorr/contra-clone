using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Package : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 0.01f;
    [SerializeField]
    float magnitude = 0.2f;
    [SerializeField]
    float frequency = 0.2f;

    private GameManager gameManager;
    private List<WeaponBehavior> weapons;
    private GameObject powerUp;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        weapons = gameManager.GetWeaponsList();
        powerUp = Resources.Load<GameObject>("Prefabs/PowerUp");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + movementSpeed, magnitude * (float)Math.Sin(Time.time * frequency));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            GameObject newPowerUp = Instantiate(powerUp);
            newPowerUp.GetComponent<SpriteRenderer>().sprite = weapons[0].sprite;
            newPowerUp.transform.SetPositionAndRotation(gameObject.transform.position, Quaternion.identity);
            newPowerUp.transform.parent = gameManager.transform;
            Destroy(gameObject);
        }
    }
}
