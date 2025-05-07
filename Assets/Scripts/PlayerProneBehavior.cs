using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProneBehavior : StateMachineBehaviour
{
    private PlayerController playerController;
    private Vector3 shootingOrigin;
    private Vector3 shootOriginOffset = new Vector3(1f, -0.7f, 0);
    private Vector2 shootDirection = new Vector2(1,0);

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("I am now Prone.");
        playerController = animator.GetComponent<PlayerController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController.ShootWasPressed())
        {
            shootingOrigin = animator.transform.position;
            playerController.Shoot(shootDirection, shootingOrigin, shootOriginOffset);
        }

        if (playerController.LookUpWasPressed())
        {
            animator.SetBool("IsProne", false);
        }
    }
}