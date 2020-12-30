using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Melee_Enemy : Enemy
{
	private RaycastHit hit;
	private bool landed = false;
	private NavMeshAgent agent;

	protected override void Start()
	{
		base.Start();
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
		}
		else if(Mathf.Abs(hit.transform.position.y - this.transform.position.y) < 5)
		{
			Debug.Log("LANDED");
			landed = true;
			gameObject.AddComponent(typeof(NavMeshAgent));
			Destroy(GetComponent<FeatherFall>());
			agent = GetComponent<NavMeshAgent>();
			agent.speed = speed;
		}
		base.Update();
	}
}
