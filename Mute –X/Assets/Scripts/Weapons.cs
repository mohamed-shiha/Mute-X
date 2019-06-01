using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : Pickups {

    public int FULL_MAGAZINE = 9;
    public int Magazine;
    public float Damage;
    public float ReloadRate;
    public float FireRate;
    public float BulletTime;
    public float BulletVelocity;
    public GameObject bullet;
    
    public void Copy(GameObject Weapon)
    {
        Weapons newWeapon = Weapon.GetComponent<Weapons>();
        FULL_MAGAZINE = newWeapon.FULL_MAGAZINE;
        Magazine = newWeapon.Magazine;
        Damage =  newWeapon.Damage;
        ReloadRate = newWeapon.ReloadRate;
        FireRate = newWeapon.FireRate;
        BulletTime = newWeapon.BulletTime;
        BulletVelocity = newWeapon.BulletVelocity;
        Sprite = newWeapon.Sprite;
        bullet = newWeapon.bullet;
        Type = newWeapon.Type;
        spriteRenderer.sprite = Sprite;
    }
}

