using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
	public int damage;
	private GameObject gunHolder;
	private float projectileSpeed;
	public GameObject bullet;
	private int accuracy;

	public bool AllyHeld = true;
	private GameObject player;
	
	// Start is called before the first frame update
	void Start()
	{
		accuracy = 10;
		gunHolder = transform.parent.gameObject;
		projectileSpeed = .2f;
		if(!AllyHeld)
		{
			player = GameObject.FindWithTag("Player");
		}
	}

	// Update is called once per frame
	void Update()
	{
		

	}

	public override void Attack()
	{
		//change so projectile spawns at end of gun
		GameObject firedBullet = Instantiate(bullet, gunHolder.transform.position, gunHolder.transform.rotation); 
		firedBullet.GetComponent<Projectiles>().damage = damage;
		firedBullet.GetComponent<Projectiles>().distance = gunHolder.GetComponent<Collider>().bounds.size/2;

		Rigidbody bulletRigidBody = firedBullet.GetComponent<Rigidbody>();
		Vector3 fireDir;
		if(AllyHeld)
		{
			
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
			fireDir = ray.origin + ray.direction.normalized * projectileSpeed;
		}
		else
		{
			fireDir = (player.transform.position - gunHolder.transform.position).normalized * projectileSpeed;
		}

		fireDir = fireDir + accuracy * (Vector3.up * Random.value + Vector3.right * Random.value + Vector3.forward * Random.value) - gunHolder.transform.position;

		bulletRigidBody.AddForce(fireDir, ForceMode.Impulse);
	}
}
