using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooty_Enemy : Enemy
{

	public int DesiredDistance = 15;
	public Weapon weapon;

	// Update is called once per frame
	void Update()
	{
		if(Vector3.Distance(player.transform.position, this.transform.position) > 
				DesiredDistance)
		{
			agent.destination = player.transform.position;
		}

		if(!cooldown)
		{
			weapon.Attack();
			cooldown = true;
			StartCoroutine(cooldownRoutine);
		}
	}
}
