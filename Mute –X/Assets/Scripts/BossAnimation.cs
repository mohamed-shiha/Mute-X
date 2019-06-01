using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : AnimationController {

	public override void CheckState()
	{
		if (GetComponentInParent<BossController>().canMove ||
			GetComponentInParent<BossController>().following)
		{
				SetState(State.Walking);
		}
		else
		{
				SetState(State.Idle);
		}
	}
	
}
