using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ZombiAttack : EnemyAttack
{
    public float attackRate = 2f;
    float elipsedTime;

    private void FixedUpdate()
    {
        if (attacking)
        {
            if (Vector2.Distance(transform.position, gameController.PlayerLocation().position) >= 1)
            {
                attacking = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Player"))
        {

            if (elipsedTime <= Time.time)
            {
                gameController.DamagePlayer(Damage);
				Audio.Play();
                attacking = true;
                elipsedTime = Time.time + attackRate;
            }
            MoveBackOneStep();

        }
        else if (tag.Equals("Wall"))
        {
            // move the zombie back after attacking 
            foreach (ContactPoint2D hit in collision.contacts)
            {
                gameController.DestroyWall(hit.point + (Vector2)transform.up);
            }
            MoveBackOneStep();
        }
    }

    void MoveBackOneStep()
    {
        transform.position -= new Vector3(
                transform.up.x / 2,
                transform.up.y / 2,
                0
                );
    }
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag.Equals("Wall"))
    //    {
    //        foreach (ContactPoint2D hit in collision.contacts)
    //        {
    //            gameController.DestroyWall(hit.point);
    //        }
    //        Debug.Log("Wall hit by:  " + gameObject.name);
    //    }
    //}
}
