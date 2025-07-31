using UnityEngine;

[CreateAssetMenu(fileName = "SprayWeapon", menuName = "Weapons/SprayWeapon")]

public class SprayWeapon : WeaponBehavior
{

    public override void Shoot(PlayerController playerController, Vector2 shootDirection, Vector3 playerPosition, Vector3 offset)
    {
        int facingDirection = playerController.FacingDirection();

        Vector3 startingPosition = new Vector3(playerPosition.x + facingDirection * offset.x, playerPosition.y + offset.y, playerPosition.z);
        Vector2 shootVelocity;

        float bullet_amount = 4;
        float bullet_angle = Mathf.PI / 12;

        for (int i = 0; i < bullet_amount; i++)
        {
            // float angle = Mathf.PI / 4 * (i + 1);
            // shootVelocity = new Vector2((shootDirection.x + Mathf.Sin(angle))* facingDirection, shootDirection.y + Mathf.Cos(angle))  * bulletVelocity;

            float angle = bullet_angle * (i - (bullet_amount - 1) / 2);
            Vector2 shootDirectionNormalized = shootDirection.normalized;
            Vector2 shootDirectionFacing = new Vector2(shootDirectionNormalized.x * facingDirection, shootDirectionNormalized.y);
            Vector2 rotatedDirection = RotateVectorRad(shootDirectionFacing, angle);
            shootVelocity = rotatedDirection * bulletVelocity;

            SpawnBullet(bullet, shootVelocity, startingPosition);

        }

    }

    Vector2 RotateVectorRad(Vector2 v, float radians)
    {
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        );
    }
}