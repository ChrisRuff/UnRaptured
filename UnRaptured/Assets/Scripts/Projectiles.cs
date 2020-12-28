using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
	public int lifetime = 100;
	public int damage;
    public Vector3 distance;
    private Collider coll;
    private Vector3 origin;
    

    private void Start()
    {
        coll = GetComponent<Collider>();
        origin = this.transform.position;

        coll.enabled = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Enemy")
		{
			other.gameObject.GetComponent<Enemy>().Hit(damage);
			DeleteProjectile();
		}
	}


    private void DeleteProjectile()
	{
		Destroy(gameObject);
	}

	void Update()
	{
		lifetime--;
		if(lifetime <= 0)
		{
			DeleteProjectile();
		}
        if (Vector3.Distance(origin, this.transform.position) > distance.magnitude)
        {
            coll.enabled = true;
        }
	}
}
