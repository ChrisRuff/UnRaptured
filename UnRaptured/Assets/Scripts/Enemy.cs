using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	public int health;
	public float speed;

	public GameObject weapon;

	private GameObject player;
	private NavMeshAgent agent;

	private bool cooldown = false;
	private IEnumerator cooldownRoutine;

	// Start is called before the first frame update
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindWithTag("Player");
		cooldownRoutine = WaitCooldown();
	}

	// Update is called once per frame
	void Update()
	{
		if(!cooldown && Vector3.Distance(player.transform.position, this.transform.position) < 3)
		{
			player.GetComponent<Player>().TakeDamage(1);
			cooldown = true;
			StartCoroutine(cooldownRoutine);

		}
		agent.destination = player.transform.position;
	}
	IEnumerator WaitCooldown()
	{
		yield return new WaitForSeconds(1);
		cooldown = false;
	}
}
