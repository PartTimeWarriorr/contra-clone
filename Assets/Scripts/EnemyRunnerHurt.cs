using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunnerHurt : StateMachineBehaviour
{
    [SerializeField]
    private EnemyRunnerController enemyRunnerController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyRunnerController = animator.GetComponent<EnemyRunnerController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyRunnerController.Die(); 
    }
}
