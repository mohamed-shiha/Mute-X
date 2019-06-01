using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {

    public NavigationPath path;
    public float moveSpeed = 2f;
    public bool PickRandomStartNode = false;
    public bool detectedPlayer = false;
    public bool canMove = true;
    public bool following = false;
    protected GameController gameController;
    protected Rigidbody2D body;
    protected float distanceToNodeTolerance = 0.2f;
    protected Vector2 currentTarget;
    protected int currentNodeIndex = 0;


    virtual protected void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        body = GetComponent<Rigidbody2D>();
        if (PickRandomStartNode)
        {
            currentNodeIndex = Random.Range(1, path.NodeCount - 1);
        }
        GetNextNodePosition();
        TeleportToNode();
    }


    virtual protected void Update()
    {
        if (Vector2.Distance(transform.position , currentTarget) <= distanceToNodeTolerance )
        {
            GetNextNodePosition();
        }

    }

    protected virtual void FixedUpdate()
    {
        if (canMove)
        {
            body.MovePosition(Vector2.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime));
            body.angularVelocity = 0;
        }
    }

    public void TeleportToNode()
    {
        transform.position = currentTarget;
    }

    public void GetNextNodePosition()
    {
        if (currentNodeIndex >= path.NodeCount) currentNodeIndex = 0;
        currentTarget = path.GetNodePosition(currentNodeIndex);
        LookForward();
        currentNodeIndex++;
    }


    public void LookForward()
    {
        transform.up = currentTarget - body.position;
    }

}
