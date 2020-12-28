using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
	public int damage;
    private GameObject gunHolder;
    private float projectileSpeed;
    public GameObject bullet;
    
    // Start is called before the first frame update
    void Start()
    {
        gunHolder = transform.parent.gameObject;
        projectileSpeed = 200.0f;
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

		Rigidbody bulletRigidBody = firedBullet.GetComponent<Rigidbody>();
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

		Vector3 fireDir;
		fireDir = (ray.origin + ray.direction.normalized * projectileSpeed) - Camera.main.transform.position;

		bulletRigidBody.AddForce(fireDir, ForceMode.Impulse);
	}
}
