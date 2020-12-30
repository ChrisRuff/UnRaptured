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

		RaycastHit hit;
		if (Physics.Raycast(transform.position, -Vector3.up, out hit)) 
		{
			transform.position = hit.transform.position;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetButtonDown("Interact"))
		{
			if(Vector3.Distance(player.transform.position, this.transform.position) < 5)
			{
				DoSacrifice();
			}
		}
	}

	void DoSacrifice()
	{
		player.GetComponent<Player>().Sacrifice();
		Destroy(this.gameObject);
	}
}
