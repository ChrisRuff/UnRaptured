using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
	public int health;
	public float speed;
	public int damage;

	protected GameObject player;

	protected bool cooldown = false;
	protected IEnumerator cooldownRoutine;
	protected float cooldownTime = 1f;

	// Start is called before the first frame update
	protected virtual void Start()
	{
		player = GameObject.FindWithTag("Player");
		cooldownRoutine = WaitCooldown();
	}

	protected virtual void Update()
	{
		this.transform.LookAt(player.transform);
	}

	// Update is called once per frame
	protected IEnumerator WaitCooldown()
	{
		yield return new WaitForSeconds(cooldownTime);
		cooldown = false;
	}

	void OnCollisionEnter(Collision collision)
	{
		if(!cooldown && collision.gameObject.tag == "Player")
		{
			player.GetComponent<Player>().TakeDamage(damage);
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
