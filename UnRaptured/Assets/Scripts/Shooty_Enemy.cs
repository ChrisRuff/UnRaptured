using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooty_Enemy : Enemy
{

	public int desiredDistance = 15;
	public Weapon weapon;

	protected override void Start()
	{
		base.Start();
	}

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
		if(Vector3.Distance(player.transform.position, this.transform.position) > 
				desiredDistance)
		{
			agent.destination = player.transform.position;
		}
		else
		{
			agent.destination = this.transform.position + (this.transform.position - player.transform.position).normalized;
		}

		weapon.Attack();
	}
}
