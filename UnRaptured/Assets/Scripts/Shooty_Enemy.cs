using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shooty_Enemy : Enemy
{

	public int desiredDistance = 15;
	public Weapon weapon;

	protected override void Start()
	{
		base.Start();
		weapon.SetCooldown(5f);
		weapon.SetAccuracy(0);
		weapon.SetDamage(damage);
	}

	// Update is called once per frame
	protected override void Update()
	{
		
		Vector3 target = player.transform.position;
		target.y = player.transform.position.y + 20;
		if(Vector3.Distance(target, this.transform.position) > desiredDistance)
		{
			this.transform.position = 
				Vector3.MoveTowards(this.transform.position, target, 
						speed * Time.deltaTime);
		}
		else
		{
			this.transform.position = Vector3.MoveTowards(this.transform.position,
					this.transform.position + (this.transform.position - player.transform.position).normalized,
					speed * Time.deltaTime);
		}

		base.Update();
		weapon.Attack();
	}
}
