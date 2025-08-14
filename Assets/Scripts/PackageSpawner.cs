using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
    private GameObject player;
    private float playerDistance = 8f;
    private GameObject package;

    public WeaponBehavior weapon;

    void Awake()
    {
        player = GameObject.Find("Player");
        package = Resources.Load<GameObject>("Prefabs/Package");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = DistanceToPlayer();

        // If player is to the right of the package spawner and at least at playerDistance 
        if (player.transform.position.x - transform.position.x >= playerDistance)
        {
            Spawn();
        }
    }

    float DistanceToPlayer()
    {
        if (player == null)
        {
            return 0;
        }

        return Mathf.Abs(player.transform.position.x - transform.position.x);
    }

    void Spawn()
    {
        GameObject newPackage = Instantiate(package);
        newPackage.transform.position = transform.position;
        newPackage.GetComponent<Package>().SetWeapon(weapon);

        Destroy(gameObject);
    }
}
