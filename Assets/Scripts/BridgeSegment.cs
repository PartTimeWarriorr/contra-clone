using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSegment : MonoBehaviour
{
    public BridgeSegment nextSegment;
    private float collapseDelay = 0.8f;
    private SpriteRenderer sprite;

    private GameObject player;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            TriggerCollapse();
        }
    }

    void Update()
    {
        CheckPlayerPosition();
    }

    void CheckPlayerPosition()
    {
        if (player != null)
        {
            if (player.transform.position.x >= transform.position.x)
            {
                TriggerCollapse();
            }
        }
    }

    public void TriggerCollapse()
    {
        sprite.color = Color.red;

        Invoke(nameof(Collapse), collapseDelay);

        if (nextSegment != null)
        {
            nextSegment.Invoke(nameof(TriggerCollapse), collapseDelay);
        }
    }

    void Collapse()
    {
        Destroy(gameObject);
    }

}
