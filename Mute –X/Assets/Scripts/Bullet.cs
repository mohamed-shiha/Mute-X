using UnityEngine;
using System.Collections;

public class Bullet : Damageable {
    public float Damage = 1;
    public float timer= 0.5f;
    public float elipsedTime;
    // the bullet will be destroyed if it hit anything 
    // if it hits the Enemy or the player damage the Player
    private void Start()
    {
        elipsedTime = Time.time + timer;
    }

    protected override void Update()
    {
        base.Update();
        
        if (elipsedTime <= Time.time)
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.EndsWith("Enemy"))
        {
            // if the enemy shot the bullet dont damage other enemies
            if (!gameObject.tag.EndsWith(tag))
            {
                DealDamage(collision);
            }
        }
        else if (tag.Equals("Player"))
        {
            collision.GetComponent<PlayerData>().DamegPlayer(Damage);
            hits--;
        }
        else if(tag.Equals("Wall"))
        {
            hits--;
        }
    }

    public void SetDamage(float newDamage)
    {
        Damage = newDamage;
    }
    
    void DealDamage(Collider2D collision) {
        Damageable damage = collision.GetComponent<Damageable>();
        if (damage != null)
        {
            damage.Hit(Damage);
            hits--;
        }
    }
}
