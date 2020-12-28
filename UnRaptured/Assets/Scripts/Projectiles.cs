using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        {
            DeleteProjectile();
        }
    }

    private void DeleteProjectile()
    {
        Destroy(gameObject);
    }


}
