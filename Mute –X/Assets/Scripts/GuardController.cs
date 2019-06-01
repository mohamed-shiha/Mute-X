using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : FollowPath {

    
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (detectedPlayer)
        {
            gameController.PlayerDetected();
            currentTarget = (Vector2)gameController.PlayerLocation().position;
            LookForward();
            following = true;
        }
        else if (following && (!detectedPlayer || Vector2.Distance(transform.position, gameController.PlayerLocation().position) > 2))
        {
            GetNextNodePosition();
            currentTarget = path.GetNodePosition(currentNodeIndex);
            LookForward();
            gameController.PlayerUnDetected();
            following = false;
            //canMove = true;
        }
    }
}
