using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunLookDownBehaviour : StateMachineBehaviour
{
    private PlayerController playerController;
    private Vector3 shootingOrigin;
    private Vector3 shootOriginOffset = new Vector3(1.2f, -0.2f, 0);
    private Vector2 shootDirection = new Vector2(0.5f, -0.5f);

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("I'm now RunLookDown.");
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
            animator.SetBool("LookingDown", false);
            return;
        }

        if (playerController.ShootWasPressed())
        {
            shootingOrigin = animator.transform.position;
            playerController.Shoot(shootDirection, shootingOrigin, shootOriginOffset);
            animator.SetTrigger("RunShoot");
        }

        if (playerController.JumpWasPressed())
        {
            playerController.Jump();
            animator.SetBool("IsJumping", true);
        }
       
        if (playerController.LookUpWasPressed())
        {
            animator.SetBool("LookingDown", false);
            animator.SetBool("LookingUp", true);
        }
        else if (playerController.LookDownWasReleased())
        {
            animator.SetBool("LookingDown", false);
        }
    }
}
