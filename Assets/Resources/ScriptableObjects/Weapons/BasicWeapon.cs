using UnityEngine;

[CreateAssetMenu(fileName = "BasicWeapon", menuName = "Weapons/BasicWeapon")]

public class BasicWeapon : WeaponBehavior
{

    public override void Shoot(PlayerController playerController, Vector2 shootDirection, Vector3 playerPosition, Vector3 offset)
    {
        int facingDirection = playerController.FacingDirection();

        Vector2 shootVelocity = new Vector2(shootDirection.x * facingDirection, shootDirection.y) * bulletVelocity;
        Vector3 startingPosition = new Vector3(playerPosition.x + facingDirection * offset.x, playerPosition.y + offset.y, playerPosition.z);

        GameObject newBullet = Instantiate(bullet);
        newBullet.GetComponent<BulletLogic>().SetParameters(shootVelocity, startingPosition);
        newBullet.transform.SetPositionAndRotation(startingPosition + new Vector3(0,0,-2), Quaternion.identity);
        newBullet.transform.parent = GameObject.Find("BulletHolder").transform;
    }
}