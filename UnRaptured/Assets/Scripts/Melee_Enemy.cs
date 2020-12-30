using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Melee_Enemy : Enemy
{
	private RaycastHit hit;
	private bool landed;
	private NavMeshAgent agent;

	protected override void Start()
	{
		base.Start();
		landed = false;
	}
	// Update is called once per frame
	protected override void Update()
	{
		if(!landed)
		{
			Physics.Raycast(transform.position, -Vector3.up, out hit);
		}
		if(landed)
		{
			agent.destination = player.transform.position;
			if(STOP)
			{
				agent.velocity = Vector3.zero;
			}
		}
		else if(Mathf.Abs(hit.transform.position.y - this.transform.position.y) < 5)
		{
			landed = true;
			gameObject.AddComponent(typeof(NavMeshAgent));
			Destroy(GetComponent<FeatherFall>());
			agent = GetComponent<NavMeshAgent>();
			agent.speed = speed;
		}
		base.Update();
	}
}
