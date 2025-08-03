using UnityEngine;

public enum FiringMode
{
    SemiAutomatic,
    Automatic
}

public abstract class WeaponBehavior : ScriptableObject
{
    public string weaponName;
    public GameObject bullet;
    public float bulletVelocity = 6f;
    public Sprite sprite;
    public FiringMode firingMode;
    public float fireRate = 0.1f;

    public abstract void Shoot(PlayerController playerController, Vector2 shootDirection, Vector3 playerPosition, Vector3 offset);

    protected void SpawnBullet(GameObject bullet, Vector2 shootVelocity, Vector3 startingPosition)
    {
        GameObject newBullet = Instantiate(bullet);
        newBullet.GetComponent<BulletLogic>().SetParameters(shootVelocity, startingPosition);
        newBullet.transform.SetPositionAndRotation(startingPosition + new Vector3(0, 0, -2), Quaternion.identity);
        newBullet.transform.parent = GameObject.Find("BulletHolder").transform;
    }
}