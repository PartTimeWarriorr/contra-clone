using UnityEngine;

public enum FiringMode
{
    SemiAutomatic,
    Automatic
}

public abstract class WeaponBehavior : ScriptableObject
{
    public GameObject bullet;
    public float bulletVelocity = 6f;
    public Sprite sprite;
    public FiringMode firingMode;

    public abstract void Shoot(PlayerController playerController, Vector2 shootDirection, Vector3 playerPosition, Vector3 offset);
}