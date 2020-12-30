using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public int damage;
	protected int accuracy;

	protected float projectileSpeed;
	protected float cooldown;
	protected float timer;

  public GameObject bullet;
	protected GameObject gunHolder;

	public bool AllyHeld = true;
	protected GameObject player;

	public abstract void Attack();
	public abstract void UpdateAccuracy(int change);
	public abstract void UpdateDamage(int change);
	public abstract void UpdateFireSpeed(float change);
	public abstract void UpdateTimer();

	public void SetCooldown(float duration)
	{
		this.cooldown = duration;
	}
}
