using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerZombiMode : LevelController {

    public GameObject[] zombies;
    public NavigationPath path;
    public float SpawnRate;
    public int zombiesToKill;
    public int Level =1;
    float elipsedTime;
    
    void Start()
    {
        Objectives = new Objective[1];
        Objectives[0] = new  Objective("Kill As many as you can !!");
        currentObjective = Objectives[0].Text;
        Time.timeScale = 1;
        
    }
    // Update is called once per frame
    void Update () {
		
        if(Time.time > elipsedTime)
        {
            for (int i = 0; i < Level; i++)
            {
               // InstantiateZombie();
                Invoke("InstantiateZombie", i);
            }
            
            elipsedTime = Time.time + SpawnRate;
        }

        if (SomeoneDied)
        {
            zombiesKilled++;
            Objectives[0].SubText = ("Level: "+Level);
            currentObjective = Objectives[0].SubText;
            SomeoneDied = false;
        }

        if(zombiesKilled > zombiesToKill)
        {
            Level++;
           // SpawnRate -= 0.1f;
            zombiesToKill += Random.Range(7,11);
            if(zombiesKilled <= 5)
                zombiesToKill = 5;
            
        }

	}

    void InstantiateZombie()
    {
        float random = Random.Range(0, 100);
        int zombieIndex = 0;
        if (random < 50)
            zombieIndex = 1;
        NavigationPath newPath = Instantiate(path , path.transform.position,Quaternion.identity);
        GameObject zombie = Instantiate(zombies[zombieIndex] , path.transform.position ,Quaternion.identity);
        zombie.GetComponent<FollowPath>().path = newPath;
        zombie.GetComponent<EnemyAttack>().MinDamage = zombiesKilled;
        zombie.GetComponent<EnemyAttack>().MaxDamage = zombiesToKill;
        zombie.GetComponent<EnemyDamage>().DropPickup = true;
        zombie.GetComponent<EnemyDamage>().PickupType = PickupsType.Ammo;
    }
}
