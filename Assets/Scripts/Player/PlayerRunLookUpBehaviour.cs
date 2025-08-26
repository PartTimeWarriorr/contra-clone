using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunLookUpBehaviour : StateMachineBehaviour
{
    private PlayerController playerController;
    private Vector3 shootingOrigin;
    private Vector3 shootOriginOffset = new Vector3(1.2f, 0.8f, 0);
    private Vector2 shootDirection = new Vector2(0.5f, 0.5f);

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Debug.Log("I'm now RunLookUp.");
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
            animator.SetBool("LookingUp", false);
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

        if (playerController.LookDownWasPressed())
        {
            animator.SetBool("LookingUp", false);
            animator.SetBool("LookingDown", true);
        }
        else if (playerController.LookUpWasReleased())
        {
            animator.SetBool("LookingUp", false);
        }

        if (playerController.IsSwimming())
        {
            animator.SetBool("IsSwimming", true);
        }
    }
}
