using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpBehavior : StateMachineBehaviour
{
    private PlayerController playerController;
    private Rigidbody2D rb;

    private Vector3 shootingOrigin;
    private Vector3 shootOriginOffset = new Vector3(0, 0, 0);
    private Vector2 shootDirection = new Vector2(1,0);  // TODO: PlayerController Aim() function: any shootDirection is valid while jumping.

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("I am now Jump.");
        playerController = animator.GetComponent<PlayerController>();
        rb = animator.GetComponent<Rigidbody2D>(); 
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shootDirection = playerController.Aim();
        if (rb.velocity.y < 0)
        {
            Collider2D hit = Physics2D.OverlapCircle(animator.transform.position + new Vector3(0, -0.9f, 0), 0.4f, LayerMask.GetMask("Platform"));

            if (hit != null && hit.CompareTag("Platform"))
            {
                animator.SetBool("IsJumping", false);
            }
        }

        if (playerController.ShouldShoot())
        {
            shootingOrigin = animator.transform.position;
            playerController.Shoot(shootDirection, shootingOrigin, shootOriginOffset);
        }

        playerController.Move();
        if (playerController.GetDirection() != 0)
        {
            playerController.FlipSprite();
        }

        if (playerController.IsSwimming() && rb.velocity.y <= 0)
        {
            animator.SetBool("IsSwimming", true);
        }
    }
}