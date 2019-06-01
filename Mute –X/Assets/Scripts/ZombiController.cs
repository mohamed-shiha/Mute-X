using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiController : FollowPath {

    
    public bool Idle = false;
    public bool StayIdle = false;
    protected override void Start()
    {
        base.Start();
        canMove = !Idle;
    }
    protected override void Update () {
        // if found the player start following
        base.Update();

        if (detectedPlayer)
        {
            FoundPlayer();
        }
        else if (following)
        {
            LostPlayer();
        }
    }
    protected override void FixedUpdate()
    {
        if (!Idle)
        {
            base.FixedUpdate();

            if (following)
            {
                
                FollowTarget();
            }
        }
        else
        {
            Stop();
        }
    }

    public void Stop()
    {
        body.velocity = Vector2.zero;
        body.angularVelocity = 0;
    }

    void FoundPlayer()
    {
        gameController.PlayerDetected();
        currentTarget = gameController.PlayerLocation().position;
        following = true;
        Idle = false;
        canMove = !following;
    }

     void LostPlayer()
    {
        currentTarget = path.GetNodePosition(currentNodeIndex);
        gameController.PlayerUnDetected();
        TeleportToNode();
        following = false;
        Idle = StayIdle;
        canMove = !Idle;
    }
    
    void FollowTarget()
    {
        transform.up = currentTarget - body.position;
        body.MovePosition(Vector2.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime));
        body.angularVelocity = 0;
    }


}
