using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public Vector3 cannonPosition;
    private float vanishThreshhold = 20f;

    void Update()
    {
        if (IsFarAway())
        {
            DeleteCannonBall();
        }
    }

    bool IsFarAway()
    {
        return Mathf.Abs((cannonPosition - transform.position).magnitude) >= vanishThreshhold;
    }

    void DeleteCannonBall()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            DeleteCannonBall();
        }
    }
}
