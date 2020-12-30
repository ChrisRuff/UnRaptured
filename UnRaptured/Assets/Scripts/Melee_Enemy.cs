using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Enemy : Enemy
{
	// Update is called once per frame
	void Update()
	{
		base.Update();
		agent.destination = player.transform.position;
	}
}
