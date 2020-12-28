using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sacrifice : MonoBehaviour
{
	private GameObject player;

	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.FindWithTag("Player");
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetButtonDown("Interact"))
		{
			if(Vector3.Distance(player.transform.position, this.transform.position) < 3)
			{
				sacrifice();
			}
		}
	}

	void sacrifice()
	{
		//player.GetComponent<Player>().add();
		Debug.Log("SACRIFICE");
		Destroy(this.gameObject);
	}
}
