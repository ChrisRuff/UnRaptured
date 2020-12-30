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
	public float spawnHeightMin = 0.0f;
	public float spawnHeightMax = 0.0f;

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
			Spawn();
			t = 0;
		}
		t += 1 * startRate;
		startRate += rateGrowth;
	}

	void Spawn()
	{
		Vector3 point = Random.insideUnitCircle.normalized;
		Vector3 location = (point * spawnInnerRadius) + Random.value * 
			((point * spawnOuterRadius) - (point * spawnInnerRadius));
		var temp = location.y;
		location.y = location.z + Random.Range(spawnHeightMin, spawnHeightMax);
		location.z = temp;

		// Don't spawn in other geometry
		if(Physics.OverlapSphere(location, 1).Length > 0)
		{
			Debug.Log("CAN'T SPAWN: " + enemies.ToString() + " HERE");
			return;
		}

		Instantiate(enemies, location, Quaternion.identity);
		spawned++;
	}
}
