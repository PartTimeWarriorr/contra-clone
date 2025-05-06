using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunLookUpBehaviour : StateMachineBehaviour
{
    private PlayerController playerController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("I'm now RunLookUp.");
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

        if (playerController.JumpWasPressed())
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
    }
}
