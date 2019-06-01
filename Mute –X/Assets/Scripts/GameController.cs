using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour {

    public InGameMenus gameMenus;
    public bool paused = false;
    public GameObject[] Pickups;
    Vector3 startingPoint;
    Tilemap walls;
    PlayerData data;
    GameObject player;
    LevelController level;
    int Sceneindex;


    private void Start()
    {
        level = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        player = GameObject.FindGameObjectWithTag("Player");
        walls = GameObject.Find("Walls").GetComponent<Tilemap>();
        data = player.GetComponent<PlayerData>();
        startingPoint = GameObject.FindGameObjectWithTag("StartingPoint").GetComponent<Transform>().position;
        player.transform.position = startingPoint;
    }

    private void Update()
    { 
        Sceneindex = SceneManager.GetActiveScene().buildIndex;
        //if the player is not alive show the lost menu
        if (!PlayerAlive())
        {
            paused = true;
            gameMenus.lostMenu.SetActive(true);
            if (data.Lives <= 0)
            {
                gameMenus.lostMenu.GetComponentInChildren<Text>().text = "Restart";
            }
            if(data.Score > data.HighScore )
            {
                SaveData.Save(data.Score, data.ModeUnlocked);
                data.HighScore = data.Score;
                Debug.Log("Saved... score" + data.Score + " ... highscore" + data.HighScore);
            }
            Time.timeScale = 0;
        }
    }

    internal int GetHighScore()
    {
        return data.HighScore;
    }

    internal int GetPlayerLives()
    {
        if (data != null)
            return data.Lives;
        else return 0;
    }

    internal void PlayerWon(String masage)
    {
        Time.timeScale = 0;
        paused = true;
        gameMenus.wonMenu.SetActive(true);
        gameMenus.wonMenu.GetComponentInChildren<TextMeshProUGUI>().text = masage;
    }
    

    public void ReTry()
    {
        // Take one life and continue 
        // if lives less then 0 Restart 
        data.Lives--;
        data.ResetPlayerData();
        player.transform.position = startingPoint;
        Time.timeScale = 1;
        if (data.Lives < 0)
        {
            GoToLevel(Sceneindex);
        }
    }

    public void GoToLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    // Player
    public void IgnorePlayer(Collider2D Item, bool stet)
    {
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), Item, stet);
    }

    public bool PlayerAlive()
    {
        
        if (data.Health <= 0)
        {
            return false;
        }
        else return true;
    }

    public void PlayerDetected()
    {
        if (data != null)
        {
            data.Detected = true;
        }
    }

    public void PlayerUnDetected()
    {
        if (data != null)
        {
            data.Detected = false;
        }
    }

    public bool IsPlayerDetected()
    {
        if(data!=null)
            return data.Detected;
        return false;
    }

    public Transform PlayerLocation()
    {
        if(player != null)
            return player.transform;
        else return null;
    }

    public void DamagePlayer(int amount)
    {
        data.DamegPlayer(amount);
    }

    // Game - level
    
    public void DestroyWall(Vector3 worldPosition)// used in level 4(moh)
    {
        if (walls != null)
        {
            Vector3Int cellPosition = walls.WorldToCell(worldPosition);
            walls.SetTile(cellPosition, null);
        }
    }

    internal void GameWon()
    {
        gameMenus.EnableButton();
        //data.HighScore = data.Score;
        data.ModeUnlocked = true;
        SaveData.Save(data.HighScore, data.ModeUnlocked);
    }

    public void EnemyKilled()// used in level 4(moh)
    {
        if (level != null)
        {
            level.SomeoneDied = true;
            level.EnemeiesKilled += 1;
            data.Score++;
        }
    }

    internal GameObject DropPickup(Vector3 position, int amount, PickupsType type)
    {
        GameObject pickup;
        pickup = Instantiate(Pickups[(int)type], position, Quaternion.identity);
        pickup.GetComponent<Pickups>().SetAmount(amount);
        return pickup;
    }
    // pickup spawning
    public void PickedUp(GameObject pickup)
    {
        PickupsType type = pickup.GetComponent<Pickups>().Type;
        int amount = pickup.GetComponent<Pickups>().Amount;
        switch (type)
        {
            case PickupsType.Health:
                data.DamegPlayer(amount * -1);
                break;
            case PickupsType.Ammo:
                data.AddAmmo(amount);
                level.AmmoGatherd += amount;
                break;
            case PickupsType.Shotgun:
            case PickupsType.Pistol:
            case PickupsType.Machinegun:
                type = player.GetComponent<PlayerAttack>().gun.GetComponent<Weapons>().Type;
                player.GetComponent<PlayerAttack>().gun.GetComponent<Weapons>().Copy(pickup);
                if (type != PickupsType.Nothing)
                {
                    GameObject temp = DropPickup(
                        pickup.transform.position,
                        amount,
                        type
                        );
                    // ignore the player for 15 sec
                    IgnorePlayer(temp.GetComponent<Collider2D>(), true);
                    temp.GetComponent<Weapons>().Dropped();
                }
                break;
        }
    }
}
