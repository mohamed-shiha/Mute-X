using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float fireRate = 2f;
    public float reloadRate = 1.5f;
    public GameObject gun;
    float elipsedTime;
    float reloadElipsedTime;
    Aim aim;
    Color aimeColor;
    GameController gameController;
    PlayerData data;
    Weapons gunData;
    bool shooting;
    bool reloading;
    private new AudioSource audio;

    private void Start()
    {
        data = gameObject.GetComponentInParent<PlayerData>();
        gunData = gun.GetComponentInParent<Weapons>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        aim = GameObject.Find("Aim").GetComponentInChildren<Aim>();
		audio = GetComponent<AudioSource>();

	}

    void Update()
    {

        // if the fire button is pressed and we have ammo shoOt !
        if ( !gameController.paused)
        {
            // shoot
            if (Input.GetButton("Fire1")  && gunData.Magazine>0 && elipsedTime <= Time.time && !reloading)
            {
                InstantiateBullet();
                elipsedTime = Time.time + (gunData.FireRate - fireRate);
                shooting = true;
                gunData.Magazine--;
                if (gunData.Magazine <= 0)
                {
                    gunData.Magazine = 0;
                    Reload();
                }
            }
            else
            {
                shooting = false;
            }
            // reload
            if (Input.GetMouseButton(1) && gunData.Magazine != gunData.FULL_MAGAZINE && !reloading  && data.Ammo!= 0)
            {
                Reload();
            }

            if (reloading && reloadElipsedTime <= Time.time)
            {
                if (data.Ammo >= gunData.FULL_MAGAZINE)
                 {
                    data.Ammo -= (gunData.FULL_MAGAZINE - gunData.Magazine);
                    gunData.Magazine = gunData.FULL_MAGAZINE;
                }
                else if (gunData.Magazine+data.Ammo <= gunData.FULL_MAGAZINE)
                {
                    gunData.Magazine += data.Ammo;
                    data.Ammo = 0;

                }
                else
                {
                    gunData.Magazine += data.Ammo;
                    if (gunData.Magazine > gunData.FULL_MAGAZINE)
                    {
                        data.Ammo = gunData.Magazine - gunData.FULL_MAGAZINE;
                        gunData.Magazine = gunData.FULL_MAGAZINE;
                    }
                }
                reloading = gunData.Magazine <= 0;
            }

        }
        // aim color
        if (reloading || gunData.Magazine <= 0) aimeColor = Color.gray;
        else if (gunData.Magazine < gunData.FULL_MAGAZINE/2) aimeColor = Color.red;
        else aimeColor = new Color(0, 1, 0, 1);
        aim.SetColour(aimeColor);
    }

    private void Reload()
    {
        reloading = true;
        reloadElipsedTime = Time.time + (gunData.ReloadRate - reloadRate);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    string tag = collision.gameObject.tag;

    //    if (tag.Equals("Pickup"))
    //    {
    //        gameController.PickedUp(collision.gameObject);
    //        Destroy(collision.gameObject);
    //    }

    //}

    public bool IsShooting()
    {
        return shooting;
    }

    void InstantiateBullet()
    {
        Vector3 position = new Vector3(
            gun.transform.position.x,
            gun.transform.position.y,
            gun.transform.position.z+1
            );
        // Creates the bullet locally
        GameObject bullet = (GameObject)Instantiate(
                                gunData.bullet,
                                position,
                                gun.transform.rotation
                                );
        // Adds velocity to the bullet
        bullet.GetComponent<Rigidbody2D>().velocity = transform.up * gunData.BulletVelocity;
		audio.Play();
        bullet.GetComponent<Bullet>().SetDamage(gunData.Damage);
        bullet.GetComponent<Bullet>().timer = gunData.BulletTime;
    }

}