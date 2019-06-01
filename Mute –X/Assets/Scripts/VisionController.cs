using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour {
    // is the player within the enemy's sight range collider 
    //(this only checks if the enemy can theoretically see the player if nothing is in the way)
    public bool playerInRange;
    public SpriteRenderer sprit;
    public Transform lineOfSightEnd;
    public float sightDis = 10;
    public float visionAngle = 360;
    Transform player;
    
    void Start()
    {
        playerInRange = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void FixedUpdate()
    {
        if (CanPlayerBeSeen())
        { 
            gameObject.GetComponentInParent<FollowPath>().detectedPlayer = true;
        }
        else if (!CanPlayerBeSeen() && Vector2.Distance(player.transform.position,transform.position) > (sightDis *3.5))
        {
            gameObject.GetComponentInParent<FollowPath>().detectedPlayer = false;
        }
    }


    public bool CanPlayerBeSeen()
    {
        // we only need to check visibility if the player is within the enemy's visual range
        if (playerInRange)
        {
            if (PlayerInFieldOfView())
                return (!PlayerHiddenByObstacles());
            else
                return false;

        }
        else
        {
            // always false if the player is not within the enemy's range
            return false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // if 'other' is player, the player is seen 
        // note, we don't really need to check the transform tag since the collision matrix is set to only 'see' collisions with the player layer
        if (other.transform.tag == "Player")
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // if 'other' is player, the player is seen
        // note, we don't really need to check the transform tag since the collision matrix is set to only 'see' collisions with the player layer
        if (other.transform.tag == "Player")
            playerInRange = false;
    }

    bool PlayerInFieldOfView()
    {
        // check if the player is within the enemy's field of view
        // this is only checked if the player is within the enemy's sight range
        // find the angle between the enemy's 'forward' direction and the player's 
        //location and return true if it's within 65 degrees (for 130 degree field of view)

        Vector2 directionToPlayer = player.position - transform.position; // represents the direction from the enemy to the player    
        Debug.DrawLine(transform.position, player.position, Color.magenta); // a line drawn in the Scene window equivalent to directionToPlayer

        Vector2 lineOfSight = lineOfSightEnd.position - transform.position; // the centre of the enemy's field of view, the direction of looking directly ahead
        Debug.DrawLine(transform.position, lineOfSightEnd.position, Color.yellow); // a line drawn in the Scene window equivalent to the enemy's field of view centre

        // calculate the angle formed between the player's position and the centre of the enemy's line of sight
        float angle = Vector2.Angle(directionToPlayer, lineOfSight);

        // if the player is within visionAngle degrees (either direction) of the enemy's centre of vision
        //(i.e. within a 130 degree cone whose centre is directly ahead of the enemy) return true
        if (angle < visionAngle)
            return true;
        else
            return false;
    }

    bool PlayerHiddenByObstacles()
    {

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, player.position - transform.position, distanceToPlayer);
        // draw line in the Scene window to show where the raycast is looking
       // Debug.DrawRay(transform.position, player.position - transform.position, Color.blue); 
        

        foreach (RaycastHit2D hit in hits)
        {
            // ignore the enemy's own colliders (and other enemies)
            if (hit.transform.tag.EndsWith("Enemy"))
                continue;

            // if anything other than the player is hit then it must be between the player and the enemy's eyes
            //(since the player can only see as far as the player)
            if (!hit.transform.tag.Equals("Player"))
            {
                return true;
            }
        }

        // if no objects were closer to the enemy than the player return false (player is not hidden by an object)
        return false;

    }

}
