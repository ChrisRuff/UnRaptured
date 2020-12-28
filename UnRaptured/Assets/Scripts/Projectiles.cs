using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
	public int lifetime = 100;
	public int damage;

	private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Enemy")
		{
			other.gameObject.GetComponent<Enemy>().Hit(damage);
			DeleteProjectile();
		}
	}

	private void DeleteProjectile()
	{
		Destroy(gameObject);
	}

	void Update()
	{
		lifetime--;
		if(lifetime <= 0)
		{
			DeleteProjectile();
		}
	}
}
