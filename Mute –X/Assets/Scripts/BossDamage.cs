using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : Damageable {
    GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
	public void OnDestroy()
	{
		if (hits<=0)
        {
            gameController.DropPickup(transform.position,100,PickupsType.Ammo);
            gameController.DropPickup(transform.position,100,PickupsType.Machinegun);
        }
	}
}
