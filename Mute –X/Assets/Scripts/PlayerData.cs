using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    const int MAX_XP = 100;
    const int MaxHealth = 100;
    const int MAX_SPEED = 6;
    public int Ammo = 20;
    public int Level = 1 ;
    public float Speed = 2;
    public int Score = 0;
    public int HighScore = 0;
    public bool Detected = false;
    public int Health;
    public int Lives;
    public bool ModeUnlocked;

    private void Start()
    {
        ResetPlayerData();
        Ammo = 12;
        if(SaveData.Load() != null)
        {
            HighScore = SaveData.Load().HighScore;
            ModeUnlocked = SaveData.Load().ZombieMode;
        }
    }

    public void ResetPlayerData()
    {
        Health = MaxHealth;
        Detected = false;
    }

    public void DamegPlayer(float amount)
    {
        if (Health > 0)
        {
            Health -= (int)amount;
        }
        if (Health < 0)
        {
            Health = 0;
        }
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    public void AddAmmo(int amount)
    {
        Ammo += amount;
    }


}
