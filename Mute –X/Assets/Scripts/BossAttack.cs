using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : EnemyAttack {

    public GameObject gun;
    public GameObject Bullet;
    public float bulletSpeed = 10f;
    public float fireRate = 1.5f;
    public float superFireRate = 0.2f;
    public float reloadTime = 3f;
    //public bool reloading = false;
    public bool super = false;
    float elipsedTime;
    float superElipsedTime;
    public int magazine = 10;
    public int FULL_MAGAZINE = 10;
	
	


    virtual protected void FixedUpdate()
    {
        bool foundPlayer = GetComponentInChildren<VisionController>().CanPlayerBeSeen();
        if (foundPlayer)
        {
            Shoot();
        }
        else
        {
            elipsedTime = Time.time + fireRate;
            attacking = false;
        }

    }

    virtual protected void Shoot()
    {
        
        if (!super && elipsedTime <= Time.time && magazine > 0)
        {
			attacking = true;
            //reloading = false;
            bulletSpeed = 15f;
			InstantiateBullet(0.5f);
            magazine--;
            elipsedTime = Time.time + fireRate;
            if (magazine <= 0)
            {
                super = true;
                magazine = FULL_MAGAZINE;
            }
        }

        if (super)
        {
            if (superElipsedTime <= Time.time)
            {
                InstantiateBullet(3);
				
                magazine--;
                bulletSpeed += 0.1f;
                superElipsedTime = Time.time + superFireRate;
                if (magazine <= 0)
                {
                    super = false;
                    //reloading = true;
					attacking = false;
                    elipsedTime = Time.time + reloadTime;
                    magazine = FULL_MAGAZINE;
                }
            }
        }

    }

    

    void InstantiateBullet(float timer)
    {// set the position
        Vector3 position = new Vector3(
            gun.transform.position.x,
            gun.transform.position.y,
            gun.transform.position.z
            );

       
        // Creates the bullet locally
        GameObject bullet = (GameObject)Instantiate(
                                Bullet,
                                position,
								gun.transform.rotation);
		Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(),true);
		Audio.Play();
		bullet.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
        bullet.GetComponent<Bullet>().SetDamage(Damage);
        bullet.GetComponent<Bullet>().timer = timer;
        //bullet.tag = "BulletEnemy";
    }
}
