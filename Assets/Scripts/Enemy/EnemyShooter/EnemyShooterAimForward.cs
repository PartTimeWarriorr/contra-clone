using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterAimForward : StateMachineBehaviour
{
    private EnemyShooterController enemyShooterController;
    private float shootAngle = 0f + 180f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyShooterController = animator.GetComponent<EnemyShooterController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 targetPosition = enemyShooterController.GetTargetPosition();
        animator.SetFloat("targetLocation", targetPosition.y);

        if (enemyShooterController.CanShoot())
        {
            enemyShooterController.Shoot(shootAngle);
            animator.SetTrigger("shoot");
        }
    }

}
