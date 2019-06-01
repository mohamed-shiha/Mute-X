using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    float vertical;
    float horizontal;
    Rigidbody2D body;
    PlayerData data;
    GameController gameController;


    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        data = GetComponent<PlayerData>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!gameController.paused)
        {
            // get the input 
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            // get the mouse position
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //// rotat the player to the mouse direction
            transform.rotation = Quaternion.LookRotation(Vector3.forward, worldMousePos - transform.position);
        }
    }

    private void FixedUpdate()
    {
        if (!gameController.paused)
        {
            //move the player forward with the speed from the data
            body.velocity = ((
                (Vector2)transform.right * horizontal)+ 
                ((Vector2)transform.up * vertical)
                ) * data.Speed;
            
            body.angularVelocity = 0;
        }
    }

    

}
