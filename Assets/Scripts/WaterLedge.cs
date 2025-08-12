using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterLedge : MonoBehaviour
{
    private Grid grid;
    private Vector2 cellSize;

    void Awake()
    {
        grid = transform.parent.parent.GetComponent<Grid>();
    }

    void Start()
    {
        cellSize = grid.cellSize;
    }

    // TODO: fix this shit code
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Player found");
            other.gameObject.transform.SetPositionAndRotation(new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + cellSize.y, other.gameObject.transform.position.z), Quaternion.identity);
            other.gameObject.GetComponent<Animator>().SetBool("IsSwimming", false);
            
        }
    } 
}
