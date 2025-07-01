using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleLookUpBehavior : StateMachineBehaviour
{
    private PlayerController playerController;
    private Vector3 shootingOrigin;
    private Vector3 shootOriginOffset = new Vector3(0.4f, 1.8f, 0);
    private Vector2 shootDirection = new Vector2(0,1);

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("I'm now IdleLookUp.");
        playerController = animator.GetComponent<PlayerController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController.ShouldShoot())
        {
            shootingOrigin = animator.transform.position;
            playerController.Shoot(shootDirection, shootingOrigin, shootOriginOffset);
            animator.SetTrigger("Shoot");
        }

        if (playerController.LookUpWasReleased())
        {
            animator.SetBool("LookingUp", false);
        }

        if (playerController.IsSwimming())
        {
            animator.SetBool("IsSwimming", true);
        }
    }
}