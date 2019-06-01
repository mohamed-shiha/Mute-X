using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenus : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject lostMenu;
    public GameObject wonMenu;
    public Button goToZombiMode;
    GameController gameController;

    
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        lostMenu.SetActive(false);
        pauseMenu.SetActive(false);
        wonMenu.SetActive(false);
        if (goToZombiMode != null) {
            if (SaveData.Load() != null  )
                goToZombiMode.interactable = SaveData.Load().ZombieMode;
            else goToZombiMode.interactable = false;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameController.PlayerAlive())
        {
            if (!gameController.paused && !lostMenu.activeSelf)
            {
                Pause();
            }
            else Resume();
        }

    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        gameController.paused = true;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        gameController.paused = false;
    }

    public void Continue()
    {
        Resume();
        wonMenu.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void GoToSurvival()
    {
        GoToLevel(SceneManager.sceneCountInBuildSettings - 1);
    }

    public void BackToMainMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        Resume();
        GoToLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReTry()
    {
        gameController.paused = false;
        lostMenu.SetActive(false);
        gameController.ReTry();
    }

    public void GoToLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    internal void EnableButton()
    {
        if (!goToZombiMode.interactable)
        {
            goToZombiMode.interactable = true;
            SaveData.Save(0, true);
        }
    }
}
