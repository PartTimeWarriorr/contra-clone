using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleBehavior : StateMachineBehaviour
{
    private PlayerController playerController;
    private Vector3 shootingOrigin;
    private Vector3 shootOriginOffset = new Vector3(1.2f, 0.2f, 0);
    private Vector2 shootDirection = new Vector2(1,0);

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("I'm now Idle.");
        playerController = animator.GetComponent<PlayerController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController.ShootWasPressed())
        {
            shootingOrigin = animator.transform.position;
            playerController.Shoot(shootDirection, shootingOrigin, shootOriginOffset);
            animator.SetTrigger("RunShoot");
        }

        if (playerController.LookDownWasPressed())
        {
            animator.SetBool("IsProne", true);
        }
        else if (playerController.LookUpWasPressed())
        {
            animator.SetBool("LookingUp", true);
        }

        if (playerController.JumpWasPressed())
        {
            playerController.Jump();
            animator.SetBool("IsJumping", true);
        }

        if (playerController.RunWasPressed())
        {
            animator.SetBool("IsWalking", true);
        }
    }
}
