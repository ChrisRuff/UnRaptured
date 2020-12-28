using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public abstract void Attack();
    public abstract void UpdateAccuracy(int change);
    public abstract void UpdateDamage(int change);
}
