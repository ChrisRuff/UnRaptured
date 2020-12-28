using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public GameObject player;
    private float projectileSpeed;
    public GameObject bullet;
    
    // Start is called before the first frame update
    void Start()
    {
        projectileSpeed = 200.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack()
    {
        //change so projectile spawns at end of gun
        GameObject firedBullet = Instantiate(bullet, Camera.main.transform.position, player.transform.rotation);

        Rigidbody bulletRigidBody = firedBullet.GetComponent<Rigidbody>();
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        Vector3 fireDir;
        fireDir = (ray.origin + ray.direction.normalized * projectileSpeed) - Camera.main.transform.position;

        bulletRigidBody.AddForce(fireDir, ForceMode.Impulse);
    }
}
