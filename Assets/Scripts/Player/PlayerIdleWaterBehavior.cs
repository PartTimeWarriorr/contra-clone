using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleWaterBehavior : StateMachineBehaviour
{
    private PlayerController playerController;
    private Vector3 shootingOrigin;
    private Vector3 shootOriginOffset = new Vector3(1.2f, 0.2f, 0);
    private Vector2 shootDirection = new Vector2(1,0);

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController = animator.GetComponent<PlayerController>();
        animator.SetBool("IsJumping", false);
        animator.SetBool("LookingDown", false);
        animator.SetBool("LookingUp", false);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController.ShouldShoot())
        {
            shootingOrigin = animator.transform.position;
            playerController.Shoot(shootDirection, shootingOrigin, shootOriginOffset);
            animator.SetTrigger("RunShoot");
        }

        if (playerController.LookUpWasPressed())
        {
            animator.SetBool("LookingUp", true);
        }

        if (playerController.LookUpWasReleased())
        {
            animator.SetBool("LookingUp", false);
        }

        if (playerController.RunIsPressed())
        {
            animator.SetBool("IsWalking", true);
        }

        if (playerController.JumpWasPressed())
        {
            playerController.Jump();
            animator.SetBool("IsSwimming", false);
            animator.SetBool("IsJumping", true);
        }

        // if (!playerController.IsSwimming())
        // {
        //     animator.SetBool("IsSwimming", false);
        // }

        playerController.Move();
        if (playerController.GetDirection() != 0)
        {
            playerController.FlipSprite();
        }
        else
        {
            animator.SetBool("IsWalking", false);
            return;
        }

    }
}
