using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {

    public SpriteRenderer sprite;
    GameController gameController;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (!gameController.paused)
        {
            Vector2 tempPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = tempPos;
        }
    }

    public void SetColour(Color newColor)
    {
        sprite.color = newColor;
    }

    
    
}
