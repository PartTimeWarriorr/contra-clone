using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunBehavior : StateMachineBehaviour
{
    private PlayerController playerController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("I'm now Run.");
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

        if (playerController.JumpWasPressed())
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
    }
}
