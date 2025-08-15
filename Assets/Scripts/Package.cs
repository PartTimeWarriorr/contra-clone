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

    // private GameManager gameManager;
    private WeaponBehavior weapon;
    private GameObject powerUp;
    private GameObject pickupHolder;

    // Start is called before the first frame update
    void Awake()
    {
        // gameManager = GameManager.Instance();
        powerUp = Resources.Load<GameObject>("Prefabs/PowerUp");
        pickupHolder = GameObject.Find("Pickups");
    }

    void Start()
    {
        // weapon = gameManager.GetWeaponsList().Find(w => w.weaponName == "Spray Weapon");
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
            newPowerUp.GetComponent<PowerUp>().weapon = weapon;
            newPowerUp.GetComponent<SpriteRenderer>().sprite = weapon.sprite;
            newPowerUp.transform.SetPositionAndRotation(gameObject.transform.position, Quaternion.identity);
            newPowerUp.transform.parent = pickupHolder.transform;
            Destroy(gameObject);
        }
    }

    public void SetWeapon(WeaponBehavior _weapon)
    {
        weapon = _weapon;
    }
}
