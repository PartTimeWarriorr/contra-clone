using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunBehavior : StateMachineBehaviour
{
    private float speed = 10f;
    private float direction = 0f;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("I'm now Run.");
        rb = animator.gameObject.GetComponent<Rigidbody2D>();
        sprite = animator.gameObject.GetComponent<SpriteRenderer>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        direction = Input.GetAxis("Horizontal");
        if (direction != 0)
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);

            // If player is moving to the left, the sprite is flipped to face left
            sprite.flipX = direction < 0;
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
