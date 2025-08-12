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

    // TODO: fix this shit code
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            player.transform.SetPositionAndRotation(new Vector3(player.transform.position.x, player.transform.position.y + cellSize.y, player.transform.position.z), Quaternion.identity);
            player.transform.position.Set(player.transform.position.x, player.transform.position.y + cellSize.y, player.transform.position.z);
            animator.SetBool("IsSwimming", false);
            
        }
    } 
}
