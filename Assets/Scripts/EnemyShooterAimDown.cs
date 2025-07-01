using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterAimDown : StateMachineBehaviour
{
    private EnemyShooterController enemyShooterController;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyShooterController = animator.GetComponent<EnemyShooterController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 targetPosition = enemyShooterController.GetTargetPosition();
        animator.SetFloat("targetLocation", targetPosition.y);

        if (targetPosition.x < 2f && enemyShooterController.CanShoot())
        {
            enemyShooterController.Shoot();
            Debug.Log("Down!");
            animator.SetTrigger("shoot");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
