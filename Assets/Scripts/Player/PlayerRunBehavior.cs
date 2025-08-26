using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunBehavior : StateMachineBehaviour
{
    private PlayerController playerController;
    private Vector3 shootingOrigin;
    private Vector3 shootOriginOffset = new Vector3(1.2f, 0.2f, 0);
    private Vector2 shootDirection = new Vector2(1,0);

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Debug.Log("I'm now Run.");
        playerController = animator.GetComponent<PlayerController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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

        if (playerController.ShouldShoot())
        {
            shootingOrigin = animator.transform.position;
            playerController.Shoot(shootDirection, shootingOrigin, shootOriginOffset);
            animator.SetTrigger("RunShoot");
        }

        if (playerController.ShouldJump())
        {
            playerController.Jump();
            animator.SetBool("IsJumping", true);
        }

        if (playerController.LookUpWasPressed())
        {
            animator.SetBool("LookingUp", true);
        }
        else if (playerController.LookDownWasPressed())
        {
            animator.SetBool("LookingDown", true);
        }

        if (playerController.IsSwimming())
        {
            animator.SetBool("IsSwimming", true);
        }
    }
}
