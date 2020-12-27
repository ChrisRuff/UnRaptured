using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int health;
	public float speed;

	public GameObject/*Weapon*/ weapon;

	private GameObject player;
	private UnityEngine.AI.NavMeshAgent agent;

	// Start is called before the first frame update
	void Start()
	{
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		player = GameObject.FindWithTag("Player");
	}

	// Update is called once per frame
	void Update()
	{
		agent.destination = player.transform.position;
	}
}
