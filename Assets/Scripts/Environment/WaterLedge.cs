using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterLedge : MonoBehaviour
{
    private Grid grid;
    private Vector2 cellSize;

    private GameObject player;
    private Animator animator;

    void Awake()
    {
        grid = transform.parent.parent.GetComponent<Grid>();
        player = GameObject.Find("Player");
    }

    void Start()
    {
        if (grid != null)
        {
            cellSize = grid.cellSize;
        }

        if (player != null)
        {
            animator = player.GetComponent<Animator>();
        }
    }

    [SerializeField]
    float yOffset = 1.2f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Rigidbody2D rb = other.attachedRigidbody;
            float ledgeTop = transform.position.y + cellSize.y / 2;

            Vector2 newPosition = new Vector2(
                rb.position.x,
                ledgeTop + yOffset 
            );

            rb.position = newPosition;
            rb.velocity = Vector2.zero;

            animator.SetBool("IsSwimming", false);
        }
    }
}
