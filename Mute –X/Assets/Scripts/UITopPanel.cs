using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITopPanel : MonoBehaviour {
    public Text score;
    public Text lives;
    GameController gameController;
    LevelController level;

	void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        level = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
    }

    private void Update()
    {
        score.text = "Score: " + level.EnemeiesKilled;
        lives.text = "Lives: " + gameController.GetPlayerLives();
    }
}
