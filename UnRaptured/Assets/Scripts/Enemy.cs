using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
	public int health;
	public float speed;

	protected GameObject player;
	protected NavMeshAgent agent;

	protected bool cooldown = false;
	protected IEnumerator cooldownRoutine;
	protected int cooldownTime = 1;

	// Start is called before the first frame update
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindWithTag("Player");
		cooldownRoutine = WaitCooldown();
	}

	protected IEnumerator WaitCooldown()
	{
		yield return new WaitForSeconds(cooldownTime);
		cooldown = false;
	}

	void OnCollisionEnter(Collision collision)
	{
		if(!cooldown)
		{
			player.GetComponent<Player>().TakeDamage(5);
			cooldown = true;
			StartCoroutine(cooldownRoutine);
		}
	}
	public void Hit(int damage)
	{
		health -= damage;
		if(health <= 0)
		{
			Destroy(gameObject);
		}
	}
}
