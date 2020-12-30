﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
	void Awake()
	{
		cooldown = 0.5f;
		timer = 0;
		accuracy = 10;
		projectileSpeed = 100f;
	}

	void Start()
	{
		gunHolder = transform.parent.gameObject;
		if(!AllyHeld)
		{
			player = GameObject.FindWithTag("Player");
		}
	}

	public override void UpdateTimer()
	{
		timer += Time.deltaTime;
	}

	public void Update()
	{
		UpdateTimer();
	}

	public override void Attack()
	{
		if (cooldown < timer || timer == 0)
		{
			GameObject firedBullet = Instantiate(bullet, gunHolder.transform.position, gunHolder.transform.rotation);
			firedBullet.GetComponent<Projectiles>().damage = damage;
			firedBullet.GetComponent<Projectiles>().distance = gunHolder.GetComponent<Collider>().bounds.size / 2;

			Rigidbody bulletRigidBody = firedBullet.GetComponent<Rigidbody>();
			Vector3 fireDir;
			if(AllyHeld)
			{
				
				Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
				fireDir = ray.origin + ray.direction.normalized * projectileSpeed;
			}
			else
			{
				fireDir = (player.transform.position - gunHolder.transform.position).normalized * projectileSpeed;
			}

			fireDir = fireDir + accuracy * 
				(Vector3.up * Random.value + 
				 Vector3.right * Random.value + 
				 Vector3.forward * Random.value) 
				- gunHolder.transform.position;

			bulletRigidBody.AddForce(fireDir, ForceMode.Impulse);
			timer = 0;
		}
	}

	public override void UpdateAccuracy(int change)
	{
		accuracy += change;
	}

	private Vector3 GetSpread()
	{
		return accuracy * (Vector3.up * Random.value * (Random.value > 0.5 ? 1 : -1) + 
				Vector3.right * Random.value * (Random.value > 0.5 ? 1 : -1) + 
				Vector3.forward * Random.value * (Random.value > 0.5 ? 1 : -1));
	}

	public override void UpdateDamage(int change)
	{
		damage += change;
	}
	public override void UpdateFireSpeed(float change)
	{
		cooldown *= change;
	}
}
