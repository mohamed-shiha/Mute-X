using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public GameObject howToPlay;
    public GameObject mainMenu;
    public GameObject eventSystem;
    public Button goToZombiMode;
    // Use this for initialization
    void Start () {
        howToPlay.SetActive(false);
        mainMenu.SetActive(true);
        if (SaveData.Load() != null)
            goToZombiMode.interactable = SaveData.Load().ZombieMode;
        else
            goToZombiMode.interactable = false;
    }

    public void HowToPlay()
    {
        mainMenu.SetActive(false);
        howToPlay.SetActive(true);
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        howToPlay.SetActive(false);
    }

   

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void GoToSurvival()
    {
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }
}
