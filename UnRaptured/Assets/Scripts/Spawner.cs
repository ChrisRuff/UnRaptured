using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject enemies;
	public float startRate = 0.001f;
	public float rateGrowth = 0.01f;
	public float spawnInnerRadius = 10f;
	public float spawnOuterRadius = 15f;

	private GameObject player;
	private Player p;

	public int spawnLimit = 100;
	private int spawned = 0;

	private float t;
	private float rate;

	void Awake()
	{
		rate = startRate;
	}

	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.FindWithTag("Player");
		if(player == null)
		{
			Debug.Log("SPAWNER REQUIRES PLAYER");
			Application.Quit();
		}
		else if(enemies == null)
		{
			Debug.Log("SPAWNER REQUIRES ENEMY TYPE");
			Application.Quit();
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(spawned >= spawnLimit)
			return;
		if(t * rate >= 1)
		{
			SpawnAngel();
			t = 0;
		}
		t += 1 * startRate;
		startRate += rateGrowth;
	}

	void SpawnAngel()
	{
		Vector3 point = Random.insideUnitCircle.normalized;
		Vector3 location = (point * spawnInnerRadius) + Random.value * 
			((point * spawnOuterRadius) - (point * spawnInnerRadius));
		var temp = location.y;
		location.y = location.z;
		location.z = temp;

		Instantiate(enemies, location, Quaternion.identity);
		spawned++;
	}
}
