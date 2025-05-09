using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleLookUpShootBehavior : StateMachineBehaviour
{
    private PlayerController playerController;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController = animator.GetComponent<PlayerController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController.LookUpWasReleased())
        {
            animator.SetBool("LookingUp", false);
        }
    }
}