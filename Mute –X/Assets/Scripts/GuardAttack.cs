using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAttack : EnemyAttack {
    public GameObject gun;
    public GameObject Bullet;
    public float bulletSpeed = 10f;
    public float fireRate = 1.5f;
    protected float elipsedTime;

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
        attacking = true;
        if (elipsedTime <= Time.time)
        {
            InstantiateBullet();
            elipsedTime = Time.time + fireRate;
        }
        
    }

    void InstantiateBullet()
    {

        Vector3 position = new Vector3(
            gun.transform.position.x,
            gun.transform.position.y,
            gun.transform.position.z
            );
        // Creates the bullet locally
        GameObject bullet = (GameObject)Instantiate(
                                Bullet,
                                position,
                                Quaternion.identity);
		// Adds velocity to the bullet
		Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
		bullet.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
		Audio.Play();
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Bullet>().SetDamage(Damage);
        bullet.tag = "BulletEnemy";
    }
}
