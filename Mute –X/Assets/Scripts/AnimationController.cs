using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Walking,
    Atacking,
}

public class AnimationController : MonoBehaviour {

    State state;
    State previousState;
    Animator animator;
    public bool Player, AI;
	void Start () {
        animator = GetComponent<Animator>();
    }

	void FixedUpdate()
    {
        // if the state changed update the animator
        CheckState();
        if (state != previousState)
        {
            animator.SetInteger("State", (int)state);
        }
    }

    //check if the player is moving 
    bool MovementKeyDown()
    {
        return Input.GetButton("Horizontal") || Input.GetButton("Vertical");
    }

	public virtual void CheckState()
    {
        // player animation
        if (Player)
        {
            if (MovementKeyDown())
            {
                SetState(State.Walking);
            }
            else if (GetComponent<PlayerAttack>().IsShooting())
            {
                SetState(State.Atacking);
            }
            else
            {
                SetState(State.Idle);
            }
        }
        // AI ( zombies and guards)
        else if (AI)
        {
            
            if (GetComponent<EnemyAttack>().attacking)
            {
				SetState(State.Atacking);
            }
            else if (GetComponent<FollowPath>().canMove || GetComponent<FollowPath>().following)
            {
                SetState(State.Walking);
            }
            else
            {
                SetState(State.Idle);
            }
        }

    }


    public void SetState(State newState)
    {
        previousState = state;
        state = newState;
    }


}
